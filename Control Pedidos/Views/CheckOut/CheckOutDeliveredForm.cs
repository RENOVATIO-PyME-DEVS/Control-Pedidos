using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;
using Control_Pedidos.Views.Reports;

namespace Control_Pedidos.Views.CheckOut
{
    /// <summary>
    /// Lista los pedidos que ya fueron entregados (estatus CO) y permite exportar la información.
    /// </summary>
    public class CheckOutDeliveredForm : Form
    {
        private readonly PedidoCheckDao _pedidoCheckDao;
        private readonly int _empresaId;
        private readonly int? _eventoId;
        private readonly bool _sinEvento;
        private readonly string _descripcionEvento;

        private readonly BindingList<PedidoCheckInfo> _pedidos = new BindingList<PedidoCheckInfo>();

        private TextBox _searchTextBox;
        private DataGridView _grid;

        public CheckOutDeliveredForm(DatabaseConnectionFactory connectionFactory, int empresaId, int? eventoId, bool sinEvento, string descripcionEvento)
        {
            _pedidoCheckDao = new PedidoCheckDao(connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory)));
            _empresaId = empresaId;
            _eventoId = eventoId;
            _sinEvento = sinEvento;
            _descripcionEvento = descripcionEvento;

            InitializeComponent();
            ConfigurarGrid();
            CargarPedidos();
        }

        private void InitializeComponent()
        {
            Text = "Pedidos entregados";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(950, 620);

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(12)
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var filtrosPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true
            };

            var eventoLabel = new Label
            {
                Text = $"Evento: {_descripcionEvento}",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(0, 6, 16, 0)
            };

            var searchLabel = new Label
            {
                Text = "Buscar:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(0, 6, 8, 0)
            };

            _searchTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Width = 240,
                Margin = new Padding(0, 4, 8, 0)
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

            var exportButton = new Button
            {
                Text = "Exportar a Excel",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 8, 0)
            };
            exportButton.Click += ExportButton_Click;

            filtrosPanel.Controls.Add(eventoLabel);
            filtrosPanel.Controls.Add(searchLabel);
            filtrosPanel.Controls.Add(_searchTextBox);
            filtrosPanel.Controls.Add(searchButton);
            filtrosPanel.Controls.Add(exportButton);

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = _pedidos,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            mainPanel.Controls.Add(filtrosPanel, 0, 0);
            mainPanel.Controls.Add(_grid, 0, 1);
            Controls.Add(mainPanel);
        }

        private void ConfigurarGrid()
        {
            _grid.AutoGenerateColumns = false;
            _grid.Columns.Clear();

            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FolioFormateado),
                HeaderText = "Folio",
                Width = 120
            });

            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ClienteNombre),
                HeaderText = "Cliente",
                Width = 240
            });

            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ProductosDescripcion),
                HeaderText = "Productos",
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True }
            });

            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FechaCheckIn),
                HeaderText = "Fecha CheckIN",
                Width = 160,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });
        }

        private void CargarPedidos()
        {
            try
            {
                var filtro = _searchTextBox.Text?.Trim() ?? string.Empty;
                var pedidos = _pedidoCheckDao.ObtenerPedidosPorEstatus(
                    _empresaId,
                    "CO",
                    _eventoId,
                    _sinEvento,
                    filtro,
                    incluirProductos: true,
                    ordenarPorFechaCheckIn: true);

                _pedidos.Clear();
                foreach (var pedido in pedidos)
                {
                    _pedidos.Add(pedido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los pedidos entregados: {ex.Message}", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (_pedidos.Count == 0)
            {
                MessageBox.Show("No hay información para exportar.", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                dialog.FileName = $"PedidosEntregados_{DateTime.Today:yyyyMMdd}.xlsx";
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Exportar(dialog.FileName);
                }
            }
        }

        private void Exportar(string ruta)
        {
            try
            {
                var tabla = CrearTabla();
                if (ReportsForm.TablaAExcel(tabla, ruta, "PedidosEntregados"))
                {
                    MessageBox.Show("Exportación completada correctamente.", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No fue posible exportar la información: {ex.Message}", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable CrearTabla()
        {
            var tabla = new DataTable("PedidosEntregados");
            tabla.Columns.Add("Folio");
            tabla.Columns.Add("Cliente");
            tabla.Columns.Add("Productos");
            tabla.Columns.Add("Fecha CheckIN");

            foreach (var pedido in _pedidos)
            {
                tabla.Rows.Add(
                    pedido.FolioFormateado,
                    pedido.ClienteNombre,
                    pedido.ProductosDescripcion,
                    pedido.FechaCheckIn?.ToString("dd/MM/yyyy HH:mm") ?? string.Empty);
            }

            return tabla;
        }
    }
}
