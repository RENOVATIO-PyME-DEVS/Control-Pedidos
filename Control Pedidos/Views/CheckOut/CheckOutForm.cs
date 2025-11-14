using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.CheckOut
{
    /// <summary>
    /// Permite liberar pedidos que ya fueron escaneados (CheckIN) realizando el CheckOUT.
    /// </summary>
    public class CheckOutForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly PedidoCheckDao _pedidoCheckDao;
        private readonly int _empresaId;

        private readonly BindingList<PedidoCheckInfo> _pedidos = new BindingList<PedidoCheckInfo>();
        private readonly ToolTip _toolTip = new ToolTip();
        private readonly Timer _timer = new Timer();

        private ComboBox _eventComboBox;
        private TextBox _searchTextBox;
        private TextBox _scanTextBox;
        private DataGridView _grid;
        private bool _loadingEventos;
        private bool _actualizando;

        public CheckOutForm(DatabaseConnectionFactory connectionFactory, int empresaId)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _pedidoCheckDao = new PedidoCheckDao(connectionFactory);
            _empresaId = empresaId;

            InitializeComponent();
            ConfigurarGrid();
            ConfigurarTimer();
            CargarEventos();
            FocusScanInput();
        }

        private void InitializeComponent()
        {
            Text = "CheckOUT de pedidos";
            StartPosition = FormStartPosition.CenterParent;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.White;

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(16)
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var filtrosPanel = new TableLayoutPanel
            {
                ColumnCount = 6,
                RowCount = 2,
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var eventoLabel = new Label
            {
                Text = "Evento:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(0, 8, 8, 0)
            };

            _eventComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 16, 0),
                Width = 240
            };
            _eventComboBox.SelectedIndexChanged += (s, e) =>
            {
                if (!_loadingEventos)
                {
                    CargarPedidos();
                }
            };

            var searchLabel = new Label
            {
                Text = "Buscar:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(0, 8, 8, 0)
            };

            _searchTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 8, 0),
                Width = 240
            };
            _searchTextBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    CargarPedidos();
                }
            };

            var searchButton = new Button
            {
                Text = "Buscar",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 8, 0)
            };
            searchButton.Click += (s, e) => CargarPedidos();

            var deliveredButton = new Button
            {
                Text = "Pedidos Entregados",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 0, 0)
            };
            deliveredButton.Click += DeliveredButton_Click;

            var scanLabel = new Label
            {
                Text = "Escanear código de barras:",
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(0, 12, 0, 4)
            };

            _scanTextBox = new TextBox
            {
                Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 0, 0, 12),
                Width = 320
            };
            _scanTextBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    ProcesarEscaneo();
                }
            };

            filtrosPanel.Controls.Add(eventoLabel, 0, 0);
            filtrosPanel.Controls.Add(_eventComboBox, 1, 0);
            filtrosPanel.Controls.Add(searchLabel, 2, 0);
            filtrosPanel.Controls.Add(_searchTextBox, 3, 0);
            filtrosPanel.Controls.Add(searchButton, 4, 0);
            filtrosPanel.Controls.Add(deliveredButton, 5, 0);
            filtrosPanel.SetColumnSpan(scanLabel, 6);
            filtrosPanel.Controls.Add(scanLabel, 0, 1);
            filtrosPanel.Controls.Add(_scanTextBox, 1, 1);

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                DataSource = _pedidos,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };
            _grid.CellMouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    _grid.ClearSelection();
                    _grid.Rows[e.RowIndex].Selected = true;
                }
            };

            var menu = new ContextMenuStrip();
            var liberarItem = new ToolStripMenuItem("Liberar pedido");
            liberarItem.Click += (s, e) => RegistrarCheckOutManual();
            menu.Items.Add(liberarItem);
            _grid.ContextMenuStrip = menu;

            mainPanel.Controls.Add(filtrosPanel, 0, 0);
            mainPanel.Controls.Add(_grid, 0, 1);
            Controls.Add(mainPanel);

            _toolTip.SetToolTip(_eventComboBox, "Seleccione el evento correspondiente. La lista se actualiza automáticamente cada 5 segundos.");
            _toolTip.SetToolTip(_scanTextBox, "Escanee el código del pedido para liberarlo inmediatamente.");
            _toolTip.SetToolTip(deliveredButton, "Muestra el historial de pedidos entregados (estatus CO).");
        }

        private void ConfigurarGrid()
        {
            _grid.AutoGenerateColumns = false;
            _grid.Columns.Clear();

            var folioColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FolioFormateado),
                HeaderText = "Folio",
                Width = 140
            };

            var clienteColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ClienteNombre),
                HeaderText = "Cliente",
                Width = 220
            };

            var productosColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ProductosDescripcion),
                HeaderText = "Productos",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    WrapMode = DataGridViewTriState.True
                }
            };

            _grid.Columns.Add(folioColumn);
            _grid.Columns.Add(clienteColumn);
            _grid.Columns.Add(productosColumn);

            _grid.DefaultCellStyle.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            _grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            _grid.RowTemplate.Height = 60;
            _grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        private void ConfigurarTimer()
        {
            _timer.Interval = 5000;
            _timer.Tick += (s, e) =>
            {
                if (!_actualizando)
                {
                    CargarPedidos();
                }
            };
            _timer.Start();

            FormClosed += (s, e) => _timer.Stop();
        }

        private void CargarEventos()
        {
            try
            {
                _loadingEventos = true;
                _eventComboBox.Items.Clear();
                var eventos = _pedidoCheckDao.ObtenerEventosPorEmpresa(_empresaId);

                _eventComboBox.Items.Add(new EventoComboItem(null, "Sin evento", true));
                EventoComboItem eventoHoy = null;
                foreach (var evento in eventos)
                {
                    var descripcion = $"{evento.Nombre} ({evento.FechaEvento:dd/MM/yyyy})";
                    var item = new EventoComboItem(evento.Id, descripcion, false);
                    _eventComboBox.Items.Add(item);
                    if (evento.FechaEvento.Date == DateTime.Today && eventoHoy == null)
                    {
                        eventoHoy = item;
                    }
                }

                if (eventoHoy != null)
                {
                    _eventComboBox.SelectedItem = eventoHoy;
                }
                else
                {
                    _eventComboBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los eventos: {ex.Message}", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingEventos = false;
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            if (_loadingEventos)
            {
                return;
            }

            try
            {
                _actualizando = true;
                var filtro = _searchTextBox.Text?.Trim() ?? string.Empty;
                var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
                var pedidos = _pedidoCheckDao.ObtenerPedidosPorEstatus(
                    _empresaId,
                    "CI",
                    eventoId,
                    sinEvento,
                    filtro,
                    includeProductos: true,
                    ordenarPorFechaCheckIn: true);

                _pedidos.Clear();
                foreach (var pedido in pedidos)
                {
                    _pedidos.Add(pedido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los pedidos: {ex.Message}", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _actualizando = false;
            }
        }

        private void ProcesarEscaneo()
        {
            var codigo = _scanTextBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return;
            }

            try
            {
                var pedido = _pedidoCheckDao.ObtenerPedidoPorCodigo(_empresaId, codigo, "CI");
                if (pedido == null)
                {
                    MessageBox.Show("No se encontró un pedido con CheckIN utilizando ese folio.", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!_pedidoCheckDao.ValidarPedidoParaCheckOut(_empresaId, pedido.PedidoId, out var mensaje, out var informacion))
                {
                    MessageBox.Show(mensaje, "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_pedidoCheckDao.RegistrarCheckOut(informacion.PedidoId, out var error))
                {
                    MessageBox.Show("Pedido entregado correctamente.", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarPedidos();
                }
                else
                {
                    MessageBox.Show(error, "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al liberar el pedido: {ex.Message}", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _scanTextBox.SelectAll();
            }
        }

        private void RegistrarCheckOutManual()
        {
            if (_grid.CurrentRow?.DataBoundItem is not PedidoCheckInfo pedido)
            {
                return;
            }

            try
            {
                if (!_pedidoCheckDao.ValidarPedidoParaCheckOut(_empresaId, pedido.PedidoId, out var mensaje, out var informacion))
                {
                    MessageBox.Show(mensaje, "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_pedidoCheckDao.RegistrarCheckOut(informacion.PedidoId, out var error))
                {
                    MessageBox.Show("Pedido entregado correctamente.", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarPedidos();
                }
                else
                {
                    MessageBox.Show(error, "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al liberar el pedido: {ex.Message}", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeliveredButton_Click(object sender, EventArgs e)
        {
            var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
            var descripcion = _eventComboBox.SelectedItem?.ToString() ?? "Todos los eventos";
            using (var form = new CheckOutDeliveredForm(_connectionFactory, _empresaId, eventoId, sinEvento, descripcion))
            {
                form.ShowDialog(this);
            }
        }

        private int? ObtenerEventoSeleccionado(out bool sinEvento)
        {
            sinEvento = false;
            if (_eventComboBox.SelectedItem is EventoComboItem item)
            {
                sinEvento = item.EsSinEvento;
                return item.EventoId;
            }

            return null;
        }

        private void FocusScanInput()
        {
            BeginInvoke(new Action(() =>
            {
                _scanTextBox.Focus();
                _scanTextBox.SelectAll();
            }));
        }

        private sealed class EventoComboItem
        {
            public EventoComboItem(int? eventoId, string descripcion, bool esSinEvento)
            {
                EventoId = eventoId;
                Descripcion = descripcion;
                EsSinEvento = esSinEvento;
            }

            public int? EventoId { get; }
            public string Descripcion { get; }
            public bool EsSinEvento { get; }

            public override string ToString() => Descripcion;
        }
    }
}
