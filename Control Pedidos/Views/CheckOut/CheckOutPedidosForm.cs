using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.CheckOut
{
    /// <summary>
    /// Formulario con diseñador para administrar el CheckOUT (entrega) de los pedidos que ya cuentan con CheckIN.
    /// </summary>
    public partial class CheckOutPedidosForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly PedidoCheckDao _pedidoCheckDao;
        private readonly int _empresaId;

        private readonly BindingList<PedidoCheckInfo> _pedidos = new BindingList<PedidoCheckInfo>();
        private readonly ToolTip _toolTip = new ToolTip();
        private readonly Timer _timerActualizacion = new Timer();

        private bool _cargandoEventos;
        private bool _actualizando;

        public CheckOutPedidosForm(DatabaseConnectionFactory connectionFactory, int empresaId)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _pedidoCheckDao = new PedidoCheckDao(connectionFactory);
            _empresaId = empresaId;

            InitializeComponent();
            PrepararInterfaz();
            ConfigurarTimer();
        }

        /// <summary>
        /// Configura los controles que requieren inicialización manual.
        /// </summary>
        private void PrepararInterfaz()
        {
            dgvPedidos.AutoGenerateColumns = false;
            dgvPedidos.DataSource = _pedidos;
            dgvPedidos.DefaultCellStyle.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            dgvPedidos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dgvPedidos.RowTemplate.Height = 70;
            dgvPedidos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            ConfigurarColumnas();

            _toolTip.SetToolTip(cmbEventos, "Seleccione el evento para ver los pedidos con CheckIN en espera de entrega.");
            _toolTip.SetToolTip(txtBuscar, "Filtra por nombre del cliente o por folio.");
            _toolTip.SetToolTip(txtCodigoBarras, "Escanee nuevamente el folio del pedido para liberarlo.");
            _toolTip.SetToolTip(btnPedidosEntregados, "Abre el historial de pedidos entregados (estatus CO).");
        }

        /// <summary>
        /// Define las columnas visibles en la tabla con tipografía grande.
        /// </summary>
        private void ConfigurarColumnas()
        {
            dgvPedidos.Columns.Clear();

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FolioFormateado),
                HeaderText = "Folio",
                Width = 160
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ClienteNombre),
                HeaderText = "Cliente",
                Width = 260
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ProductosDescripcion),
                HeaderText = "Productos",
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    WrapMode = DataGridViewTriState.True
                }
            });
        }

        /// <summary>
        /// Configura el temporizador que mantiene actualizada la lista cada pocos segundos.
        /// </summary>
        private void ConfigurarTimer()
        {
            _timerActualizacion.Interval = 5000;
            _timerActualizacion.Tick += (s, e) =>
            {
                if (!_actualizando)
                {
                    CargarPedidos();
                }
            };
        }

        /// <summary>
        /// Evento Load: carga los eventos y posiciona el foco en el lector.
        /// </summary>
        private void CheckOutPedidosForm_Load(object sender, EventArgs e)
        {
            CargarEventos();
            EnfocarEscaneo();
            _timerActualizacion.Start();
        }

        /// <summary>
        /// Detiene el temporizador al cerrar la ventana.
        /// </summary>
        private void CheckOutPedidosForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timerActualizacion.Stop();
        }

        /// <summary>
        /// Obtiene el catálogo de eventos y establece el evento del día en caso de existir.
        /// </summary>
        private void CargarEventos()
        {
            try
            {
                _cargandoEventos = true;
                cmbEventos.Items.Clear();

                var eventos = _pedidoCheckDao.ObtenerEventosPorEmpresa(_empresaId);
                cmbEventos.Items.Add(new EventoComboItem(null, "Sin evento", true));

                EventoComboItem eventoHoy = null;
                foreach (var evento in eventos)
                {
                    var item = new EventoComboItem(evento.Id, $"{evento.Nombre} ({evento.FechaEvento:dd/MM/yyyy})", false);
                    cmbEventos.Items.Add(item);

                    if (eventoHoy == null && evento.FechaEvento.Date == DateTime.Today)
                    {
                        eventoHoy = item;
                    }
                }

                if (eventoHoy != null)
                {
                    cmbEventos.SelectedItem = eventoHoy;
                }
                else
                {
                    cmbEventos.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los eventos: {ex.Message}", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargandoEventos = false;
                CargarPedidos();
            }
        }

        /// <summary>
        /// Recupera los pedidos con estatus CI aplicando los filtros vigentes.
        /// </summary>
        private void CargarPedidos()
        {
            if (_cargandoEventos)
            {
                return;
            }

            try
            {
                _actualizando = true;
                var filtro = txtBuscar.Text?.Trim() ?? string.Empty;
                var eventoId = ObtenerEventoSeleccionado(out var sinEvento);

                var pedidos = _pedidoCheckDao.ObtenerPedidosPorEstatus(
                    _empresaId,
                    "CI",
                    eventoId,
                    sinEvento,
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
                MessageBox.Show($"No se pudieron cargar los pedidos: {ex.Message}", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _actualizando = false;
            }
        }

        /// <summary>
        /// Devuelve el identificador del evento seleccionado y si se eligió "Sin evento".
        /// </summary>
        private int? ObtenerEventoSeleccionado(out bool sinEvento)
        {
            sinEvento = false;
            if (cmbEventos.SelectedItem is EventoComboItem item)
            {
                sinEvento = item.EsSinEvento;
                return item.EventoId;
            }

            return null;
        }

        private void cmbEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoEventos)
            {
                return;
            }

            CargarPedidos();
            EnfocarEscaneo();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarPedidos();
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ProcesarEscaneo();
            }
        }

        private void dgvPedidos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvPedidos.ClearSelection();
                dgvPedidos.Rows[e.RowIndex].Selected = true;
            }
        }

        private void liberarPedidoMenuItem_Click(object sender, EventArgs e)
        {
            RegistrarCheckOutManual();
        }

        private void btnPedidosEntregados_Click(object sender, EventArgs e)
        {
            var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
            var descripcion = cmbEventos.SelectedItem?.ToString() ?? "Todos los eventos";
            using (var form = new CheckOutPedidosEntregadosForm(_connectionFactory, _empresaId, eventoId, sinEvento, descripcion))
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// Procesa el código escaneado y registra el CheckOUT si pasa las validaciones.
        /// </summary>
        private void ProcesarEscaneo()
        {
            var codigo = txtCodigoBarras.Text?.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return;
            }

            try
            {
                var pedido = _pedidoCheckDao.ObtenerPedidoPorCodigo(_empresaId, codigo, "CI");
                if (pedido == null)
                {
                    MessageBox.Show("No se encontró un pedido con CheckIN usando ese folio.", "CheckOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtCodigoBarras.SelectAll();
            }
        }

        /// <summary>
        /// Ejecuta el CheckOUT para el pedido seleccionado manualmente.
        /// </summary>
        private void RegistrarCheckOutManual()
        {
            if (!(dgvPedidos.CurrentRow?.DataBoundItem is PedidoCheckInfo pedido))
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

        /// <summary>
        /// Coloca el foco en el cuadro de escaneo.
        /// </summary>
        private void EnfocarEscaneo()
        {
            BeginInvoke(new Action(() =>
            {
                txtCodigoBarras.Focus();
                txtCodigoBarras.SelectAll();
            }));
        }

        /// <summary>
        /// Representa la opción de evento utilizada por el ComboBox.
        /// </summary>
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
