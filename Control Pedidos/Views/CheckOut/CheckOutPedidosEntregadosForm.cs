using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;
using Control_Pedidos.Views.Reports;

namespace Control_Pedidos.Views.CheckOut
{
    /// <summary>
    /// Presenta los pedidos que ya fueron entregados (estatus CO) permitiendo búsquedas y exportación a Excel.
    /// </summary>
    public partial class CheckOutPedidosEntregadosForm : Form
    {
        private readonly PedidoCheckDao _pedidoCheckDao;
        private readonly int _empresaId;
        private readonly int? _eventoId;
        private readonly bool _sinEvento;
        private readonly string _descripcionEvento;

        private readonly BindingList<PedidoCheckInfo> _pedidos = new BindingList<PedidoCheckInfo>();

        public CheckOutPedidosEntregadosForm(DatabaseConnectionFactory connectionFactory, int empresaId, int? eventoId, bool sinEvento, string descripcionEvento)
        {
            _pedidoCheckDao = new PedidoCheckDao(connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory)));
            _empresaId = empresaId;
            _eventoId = eventoId;
            _sinEvento = sinEvento;
            _descripcionEvento = descripcionEvento;

            InitializeComponent();
            PrepararInterfaz();
            CargarPedidos();
        }

        /// <summary>
        /// Configura la interfaz del formulario.
        /// </summary>
        private void PrepararInterfaz()
        {
            lblEvento.Text = $"Evento: {_descripcionEvento}";
            dgvPedidos.AutoGenerateColumns = false;
            dgvPedidos.DataSource = _pedidos;
            ConfigurarColumnas();
        }

        /// <summary>
        /// Genera las columnas que se visualizarán en la tabla.
        /// </summary>
        private void ConfigurarColumnas()
        {
            dgvPedidos.Columns.Clear();

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FolioFormateado),
                HeaderText = "Folio",
                Width = 120
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ClienteNombre),
                HeaderText = "Cliente",
                Width = 220
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ProductosDescripcion),
                HeaderText = "Productos",
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True }
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FechaCheckIn),
                HeaderText = "Fecha CheckIN",
                Width = 160,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
        }

        /// <summary>
        /// Recupera los pedidos entregados desde la base de datos.
        /// </summary>
        private void CargarPedidos()
        {
            try
            {
                var filtro = txtBuscar.Text?.Trim() ?? string.Empty;
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

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                CargarPedidos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarPedidos();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_pedidos.Count == 0)
            {
                MessageBox.Show("No hay información para exportar.", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                dialogo.FileName = $"PedidosEntregados_{DateTime.Today:yyyyMMdd}.xlsx";
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Exportar(dialogo.FileName);
                }
            }
        }

        /// <summary>
        /// Apoya la exportación a Excel utilizando la utilidad existente en ReportsForm.
        /// </summary>
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

        /// <summary>
        /// Construye un DataTable con la información mostrada en pantalla.
        /// </summary>
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
