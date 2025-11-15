using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Control_Pedidos.Controllers;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Printing;

namespace Control_Pedidos.Views.Orders
{
    public partial class OrderDeliveryDashboardForm : Form
    {
        private static readonly string[] StatusOptions =
        {
            "En espera del cliente",
            "Cliente llegó",
            "En ensamble",
            "Entregado"
        };

        private readonly OrderController _orderController;
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly int? _empresaId;
        private readonly Timer _refreshTimer;
        private readonly ContextMenuStrip _statusMenu;
        private readonly ContextMenuStrip _ordersContextMenu;
        private readonly ToolStripMenuItem _reprintMenuItem;
        private readonly PedidoDao _pedidoDao;
        private readonly PedidoPrintingService _printingService;

        private DataTable _allOrders;
        private bool _suppressFilterEvents;
        private bool _isRefreshing;
        private int? _selectedOrderId;

        public OrderDeliveryDashboardForm(OrderController orderController, DatabaseConnectionFactory connectionFactory, int? empresaId = null, string empresaNombre = null)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _orderController = orderController ?? throw new ArgumentNullException(nameof(orderController));
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _empresaId = empresaId;

            if (!string.IsNullOrWhiteSpace(empresaNombre))
            {
                Text = $"Pedidos del día - {empresaNombre}";
            }

            _statusMenu = new ContextMenuStrip();
            foreach (var status in StatusOptions)
            {
                var item = new ToolStripMenuItem(status);
                item.Click += StatusMenuItem_Click;
                _statusMenu.Items.Add(item);
            }

            _ordersContextMenu = new ContextMenuStrip();
            _ordersContextMenu.Opening += OrdersContextMenu_Opening;
            _reprintMenuItem = new ToolStripMenuItem("Reimprimir pedido");
            _reprintMenuItem.Click += ReprintMenuItem_Click;
            _ordersContextMenu.Items.Add(_reprintMenuItem);

            _pedidoDao = new PedidoDao(_connectionFactory);
            _printingService = new PedidoPrintingService();

            _refreshTimer = new Timer
            {
                Interval = 30_000
            };
            _refreshTimer.Tick += RefreshTimer_Tick;
        }

        private void OrderDeliveryDashboardForm_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            InitializeFilters();
            RefreshOrders();
            _refreshTimer.Start();

            searchTextBox.TextChanged += searchTextBox_TextChanged;

        }

        private void OrderDeliveryDashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _refreshTimer.Stop();
            _refreshTimer.Dispose();
            _statusMenu?.Dispose();
            _ordersContextMenu?.Dispose();
        }

        private void TodaysOrdersGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            var hit = todaysOrdersGrid.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0 && hit.RowIndex < todaysOrdersGrid.Rows.Count)
            {
                todaysOrdersGrid.ClearSelection();
                todaysOrdersGrid.Rows[hit.RowIndex].Selected = true;

                if (todaysOrdersGrid.Rows[hit.RowIndex].DataBoundItem is DataRowView rowView)
                {
                    _selectedOrderId = rowView.Row.Field<int>("PedidoId");
                }
            }
            else
            {
                todaysOrdersGrid.ClearSelection();
                _selectedOrderId = null;
            }
        }

        private void OrdersContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (todaysOrdersGrid.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            var row = todaysOrdersGrid.SelectedRows[0];
            if (!(row?.DataBoundItem is DataRowView rowView))
            {
                e.Cancel = true;
                return;
            }

            _selectedOrderId = rowView.Row.Field<int>("PedidoId");
            _reprintMenuItem.Enabled = _selectedOrderId.HasValue;
        }

        private void ReprintMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedOrderId == null)
            {
                MessageBox.Show("Seleccione un pedido para reimprimir.", "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Pedido pedido;
            try
            {
                pedido = _pedidoDao.ObtenerPedidoCompleto(_selectedOrderId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo obtener la información del pedido: {ex.Message}", "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pedido == null)
            {
                MessageBox.Show("No se encontró el pedido seleccionado.", "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PedidoPrintingResult resultado;
            try
            {
                resultado = _printingService.Print(pedido, this, pedido.EstaImpreso);
            }
            catch (Exception ex)
            {
                ActualizarImpresionPedido(pedido.Id, false);
                MessageBox.Show($"No se pudo generar el archivo PDF del pedido: {ex.Message}", "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultado.Printed)
            {
                ActualizarImpresionPedido(pedido.Id, true);
                var mensajeExito = pedido.EstaImpreso ? "Pedido reimpreso correctamente." : "Pedido impreso correctamente.";
                MessageBox.Show(mensajeExito, "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resultado.SavedPdf)
            {
                ActualizarImpresionPedido(pedido.Id, false);

                var mensaje = new StringBuilder();
                if (resultado.CancelledByUser)
                {
                    mensaje.AppendLine("La impresión fue cancelada por el usuario.");
                }
                else if (resultado.Error != null)
                {
                    mensaje.AppendLine("Ocurrió un problema al imprimir el pedido.");
                    mensaje.AppendLine(resultado.Error.Message);
                }

                if (!string.IsNullOrWhiteSpace(resultado.PdfPath))
                {
                    if (mensaje.Length > 0)
                    {
                        mensaje.AppendLine();
                    }

                    mensaje.AppendLine("El pedido se guardó automáticamente en formato PDF en:");
                    mensaje.AppendLine(resultado.PdfPath);
                }

                MessageBox.Show(mensaje.ToString(), "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            _selectedOrderId = null;
        }

        private void ActualizarImpresionPedido(int pedidoId, bool impreso)
        {
            try
            {
                _pedidoDao.ActualizarImpresion(pedidoId, impreso);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo actualizar el estado de impresión del pedido: {ex.Message}", "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigureGrid()
        {
            todaysOrdersGrid.AutoGenerateColumns = false;
            todaysOrdersGrid.EnableHeadersVisualStyles = false;
            todaysOrdersGrid.ColumnHeadersDefaultCellStyle.BackColor = UIStyles.SurfaceColor;
            todaysOrdersGrid.ColumnHeadersDefaultCellStyle.ForeColor = UIStyles.TextPrimaryColor;
            todaysOrdersGrid.ColumnHeadersDefaultCellStyle.Font = UIStyles.SemiBoldFont;
            todaysOrdersGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            todaysOrdersGrid.DefaultCellStyle.Font = UIStyles.DefaultFont;
            todaysOrdersGrid.DefaultCellStyle.ForeColor = UIStyles.TextPrimaryColor;
            todaysOrdersGrid.DefaultCellStyle.SelectionBackColor = UIStyles.AccentColor;
            todaysOrdersGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            todaysOrdersGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            if (todaysOrdersGrid.Columns["horaEntregaColumn"] is DataGridViewColumn horaColumn)
            {
                horaColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (todaysOrdersGrid.Columns["totalColumn"] is DataGridViewColumn totalColumn)
            {
                totalColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                totalColumn.DefaultCellStyle.Format = "C2";
            }
            if (todaysOrdersGrid.Columns["actualizarEstatusColumn"] is DataGridViewButtonColumn buttonColumn)
            {
                buttonColumn.FlatStyle = FlatStyle.Flat;

                buttonColumn.Visible = false;
            }

            todaysOrdersGrid.ContextMenuStrip = _ordersContextMenu;
            todaysOrdersGrid.MouseDown += TodaysOrdersGrid_MouseDown;
        }

        private void InitializeFilters()
        {
            _suppressFilterEvents = true;

            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.Add("Todos");
            statusFilterComboBox.Items.AddRange(StatusOptions);
            statusFilterComboBox.SelectedIndex = 0;

            clientFilterComboBox.Items.Clear();
            clientFilterComboBox.Items.Add(new ClientFilterItem(null, "Todos"));
            clientFilterComboBox.SelectedIndex = 0;

            _suppressFilterEvents = false;
        }

        private void RefreshOrders()
        {
            if (_isRefreshing)
            {
                return;
            }

            try
            {
                _isRefreshing = true;
                var data = _orderController.GetTodayDeliveries(_empresaId);
                _allOrders = data;

                UpdateClientFilterOptions(data);
                UpdateCounters(data);
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los pedidos del día: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        private void ApplyFiltersORIGINAL()
        {
            if (_allOrders == null)
            {
                todaysOrdersGrid.DataSource = null;
                return;
            }

            var view = new DataView(_allOrders);
            var filters = new List<string>();

            var selectedClient = (clientFilterComboBox.SelectedItem as ClientFilterItem)?.Id;
            if (selectedClient.HasValue)
            {
                filters.Add($"ClienteId = {selectedClient.Value}");
            }

            var selectedStatus = GetSelectedStatus();
            if (!string.IsNullOrEmpty(selectedStatus))
            {
                var sanitizedStatus = selectedStatus.Replace("'", "''");
                filters.Add($"Estatus = '{sanitizedStatus}'");
            }

            view.RowFilter = string.Join(" AND ", filters);

            todaysOrdersGrid.DataSource = view;
            todaysOrdersGrid.ClearSelection();
            ApplyRowStyles();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents) return;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (_allOrders == null)
            {
                todaysOrdersGrid.DataSource = null;
                return;
            }

            var view = new DataView(_allOrders);
            var filters = new List<string>();

            // -------------------------
            // FILTRO POR CLIENTE
            // -------------------------
            var selectedClient = (clientFilterComboBox.SelectedItem as ClientFilterItem)?.Id;
            if (selectedClient.HasValue)
            {
                filters.Add($"ClienteId = {selectedClient.Value}");
            }

            // -------------------------
            // FILTRO POR ESTATUS
            // -------------------------
            var selectedStatus = GetSelectedStatus();
            if (!string.IsNullOrEmpty(selectedStatus))
            {
                var sanitizedStatus = selectedStatus.Replace("'", "''");
                filters.Add($"Estatus = '{sanitizedStatus}'");
            }

            // -------------------------
            // FILTRO POR TEXTO (NOMBRE / FOLIO)
            // -------------------------
            string search = searchTextBox.Text.Trim().Replace("'", "''");

            if (!string.IsNullOrEmpty(search))
            {
                filters.Add(
                    $"(Cliente LIKE '%{search}%' OR Folio LIKE '%{search}%')"
                );
            }

            view.RowFilter = string.Join(" AND ", filters);

            todaysOrdersGrid.DataSource = view;
            todaysOrdersGrid.ClearSelection();
            ApplyRowStyles();
        }

        private string GetSelectedStatus()
        {
            if (statusFilterComboBox.SelectedIndex <= 0)
            {
                return null;
            }

            return statusFilterComboBox.SelectedItem as string;
        }

        private void UpdateClientFilterOptions(DataTable data)
        {
            var previousSelection = (clientFilterComboBox.SelectedItem as ClientFilterItem)?.Id;

            var clients = data?.AsEnumerable()
                .Where(row => !row.IsNull("ClienteId"))
                .GroupBy(row => Convert.ToInt32(row["ClienteId"]))
                .Select(group =>
                {
                    var name = group.Select(r => Convert.ToString(r["Cliente"]))
                        .FirstOrDefault() ?? string.Empty;
                    return new ClientFilterItem(group.Key, name);
                })
                .OrderBy(item => item.Name, StringComparer.CurrentCultureIgnoreCase)
                .ToList() ?? new List<ClientFilterItem>();

            _suppressFilterEvents = true;
            clientFilterComboBox.BeginUpdate();
            try
            {
                clientFilterComboBox.Items.Clear();
                clientFilterComboBox.Items.Add(new ClientFilterItem(null, "Todos"));

                var selectedIndex = 0;
                for (var i = 0; i < clients.Count; i++)
                {
                    var item = clients[i];
                    clientFilterComboBox.Items.Add(item);

                    if (previousSelection.HasValue && item.Id == previousSelection.Value)
                    {
                        selectedIndex = i + 1;
                    }
                }

                clientFilterComboBox.SelectedIndex = selectedIndex;
            }
            finally
            {
                clientFilterComboBox.EndUpdate();
                _suppressFilterEvents = false;
            }
        }

        private void UpdateCounters(DataTable data)
        {
            var total = data?.Rows.Count ?? 0;
            var waiting = 0;
            var arrived = 0;
            var assembling = 0;
            var delivered = 0;
            var pend = 0;

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    var status = Convert.ToString(row["Estatus"]);
                    switch (status)
                    {
                        case "N":
                            waiting++;
                            break;
                        case "CI":
                            arrived++;
                            break;
                        case "D":
                            assembling++;
                            break;
                        case "P":
                            pend++;
                            break;
                        case "CO":
                            delivered++;
                            break;
                    }
                }
            }

            totalOrdersLabel.Text = $"Pedidos totales: {total}";
            waitingOrdersLabel.Text = $"Pedidos en espera de entrega: {waiting}";
            assemblingOrdersLabel.Text = $"Pedidos Devueltos: {assembling}";
            deliveredOrdersLabel.Text = $"Pedidos entregados: {delivered}";
            pedidosPendientesLabel.Text = $"Pedidos pendientes:{pend}";
            pedidosEnProduccionLabel.Text = $"Pedidos en produccion:{arrived}";
        }

        private void ApplyRowStyles()
        {
            foreach (DataGridViewRow row in todaysOrdersGrid.Rows)
            {
                var status = Convert.ToString(row.Cells["estatusCol"].Value);
                var color = GetStatusColor(status);

                row.DefaultCellStyle.BackColor = color;
                row.DefaultCellStyle.ForeColor = UIStyles.TextPrimaryColor;
                row.DefaultCellStyle.SelectionBackColor = UIStyles.AccentColor;
                row.DefaultCellStyle.SelectionForeColor = Color.White;
            }
        }


        private Color GetStatusColor(string status)
        {
            switch (status)
            {
                case "En espera del cliente":
                    return Color.FromArgb(254, 243, 199);
                case "Cliente llegó":
                    return Color.FromArgb(220, 252, 231);
                case "En ensamble":
                    return Color.FromArgb(219, 234, 254);
                case "Entregado":
                    return Color.FromArgb(229, 231, 235);
                default:
                    return Color.White;
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshOrders();
        }

        private void clientFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents)
            {
                return;
            }

            ApplyFilters();
        }

        private void statusFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressFilterEvents)
            {
                return;
            }

            ApplyFilters();
        }

        private void todaysOrdersGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (todaysOrdersGrid.Columns[e.ColumnIndex].Name != "actualizarEstatusColumn")
            {
                return;
            }

            ShowStatusMenu(e.RowIndex, e.ColumnIndex);
        }

        private void ShowStatusMenu(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= todaysOrdersGrid.Rows.Count)
            {
                return;
            }

            var rowView = todaysOrdersGrid.Rows[rowIndex].DataBoundItem as DataRowView;
            if (rowView == null)
            {
                return;
            }

            _selectedOrderId = rowView.Row.Field<int>("PedidoId");
            var currentStatus = rowView.Row.Field<string>("Estatus");

            foreach (ToolStripMenuItem item in _statusMenu.Items)
            {
                item.Checked = string.Equals(item.Text, currentStatus, StringComparison.OrdinalIgnoreCase);
            }

            todaysOrdersGrid.Rows[rowIndex].Selected = true;
            var cellRectangle = todaysOrdersGrid.GetCellDisplayRectangle(columnIndex, rowIndex, true);
            var menuLocation = todaysOrdersGrid.PointToScreen(new Point(cellRectangle.Left, cellRectangle.Bottom));
            _statusMenu.Show(menuLocation);
        }

        private void StatusMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedOrderId == null)
            {
                return;
            }

            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
            {
                return;
            }

            var newStatus = menuItem.Text;

            try
            {
                _refreshTimer.Stop();
                Cursor.Current = Cursors.WaitCursor;
                _orderController.UpdateOrderStatus(_selectedOrderId.Value, newStatus);
                RefreshOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo actualizar el estatus del pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                _selectedOrderId = null;
                if (!IsDisposed)
                {
                    _refreshTimer.Start();
                }
            }
        }

        private void todaysOrdersGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyRowStyles();
        }

        private sealed class ClientFilterItem
        {
            public ClientFilterItem(int? id, string name)
            {
                Id = id;
                Name = name;
            }

            public int? Id { get; }

            public string Name { get; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
