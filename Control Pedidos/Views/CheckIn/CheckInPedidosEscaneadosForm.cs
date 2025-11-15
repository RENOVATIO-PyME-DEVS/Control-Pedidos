using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;
using Control_Pedidos.Views.Reports;

namespace Control_Pedidos.Views.CheckIn
{
    /// <summary>
    /// Muestra los pedidos con estatus CI y permite buscar o exportar la información.
    /// </summary>
    public partial class CheckInPedidosEscaneadosForm : Form
    {
        private readonly PedidoCheckDao _pedidoCheckDao;
        private readonly int _empresaId;
        private readonly int? _eventoId;
        private readonly bool _sinEvento;
        private readonly string _descripcionEvento;

        private readonly BindingList<PedidoCheckInfo> _pedidos = new BindingList<PedidoCheckInfo>();

        public CheckInPedidosEscaneadosForm(DatabaseConnectionFactory connectionFactory, int empresaId, int? eventoId, bool sinEvento, string descripcionEvento)
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
        /// Configura controles y columnas del grid.
        /// </summary>
        private void PrepararInterfaz()
        {
            lblEvento.Text = $"Evento: {_descripcionEvento}";

            dgvPedidos.AutoGenerateColumns = false;
            dgvPedidos.DataSource = _pedidos;
            ConfigurarColumnas();
        }

        /// <summary>
        /// Define las columnas visibles en la tabla.
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
                DataPropertyName = nameof(PedidoCheckInfo.FechaEntregaDescripcion),
                HeaderText = "Entrega",
                Width = 160
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.EventoNombre),
                HeaderText = "Evento",
                Width = 200
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FechaCheckIn),
                HeaderText = "Fecha CheckIN",
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
        }

        /// <summary>
        /// Recupera los pedidos con estatus CI aplicando el filtro de búsqueda.
        /// </summary>
        private void CargarPedidos()
        {
            try
            {
                var filtro = txtBuscar.Text?.Trim() ?? string.Empty;
                var pedidos = _pedidoCheckDao.ObtenerPedidosPorEstatus(
                    _empresaId,
                    "CI",
                    _eventoId,
                    _sinEvento,
                    filtro,
                    incluirProductos: false,
                    ordenarPorFechaCheckIn: true);

                _pedidos.Clear();
                foreach (var pedido in pedidos)
                {
                    _pedidos.Add(pedido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los pedidos escaneados: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("No hay información para exportar.", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                dialogo.FileName = $"PedidosEscaneados_{DateTime.Today:yyyyMMdd}.xlsx";
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    Exportar(dialogo.FileName);
                }
            }
        }

        /// <summary>
        /// Realiza la exportación a Excel usando la utilidad existente de reportes.
        /// </summary>
        private void Exportar(string ruta)
        {
            try
            {
                var tabla = CrearTabla();
                if (ReportsForm.TablaAExcel(tabla, ruta, "PedidosEscaneados"))
                {
                    MessageBox.Show("Exportación completada correctamente.", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al exportar: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Construye un DataTable con la información mostrada en pantalla.
        /// </summary>
        private DataTable CrearTabla()
        {
            var tabla = new DataTable("PedidosEscaneados");
            tabla.Columns.Add("Folio");
            tabla.Columns.Add("Cliente");
            tabla.Columns.Add("Entrega");
            tabla.Columns.Add("Evento");
            tabla.Columns.Add("Fecha CheckIN");

            foreach (var pedido in _pedidos)
            {
                tabla.Rows.Add(
                    pedido.FolioFormateado,
                    pedido.ClienteNombre,
                    pedido.FechaEntregaDescripcion,
                    string.IsNullOrWhiteSpace(pedido.EventoNombre) ? "Sin evento" : pedido.EventoNombre,
                    pedido.FechaCheckIn?.ToString("dd/MM/yyyy HH:mm") ?? string.Empty);
            }

            return tabla;
        }
    }
}
