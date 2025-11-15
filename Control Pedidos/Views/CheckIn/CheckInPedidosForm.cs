using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.CheckIn
{
    /// <summary>
    /// Formulario principal para registrar el CheckIN de los pedidos utilizando una interfaz creada con diseñador.
    /// </summary>
    public partial class CheckInPedidosForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly PedidoCheckDao _pedidoCheckDao;
        private readonly int _empresaId;

        private readonly BindingList<PedidoCheckInfo> _pedidos = new BindingList<PedidoCheckInfo>();
        private readonly ToolTip _toolTip = new ToolTip();

        private bool _cargandoEventos;

        public CheckInPedidosForm(DatabaseConnectionFactory connectionFactory, int empresaId)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _pedidoCheckDao = new PedidoCheckDao(connectionFactory);
            _empresaId = empresaId;

            InitializeComponent();
            PrepararInterfaz();
        }

        /// <summary>
        /// Configura elementos comunes de la interfaz que no es posible definir en el diseñador visual.
        /// </summary>
        private void PrepararInterfaz()
        {
            dgvPedidos.AutoGenerateColumns = false;
            dgvPedidos.DataSource = _pedidos;
            dgvPedidos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPedidos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ConfigurarColumnas();

            _toolTip.SetToolTip(cmbEventos, "Seleccione el evento que desea revisar. Si existe un evento del día se seleccionará de forma automática.");
            _toolTip.SetToolTip(txtBuscar, "Escriba el nombre del cliente o el folio del pedido para filtrar los resultados en tiempo real.");
            _toolTip.SetToolTip(txtCodigoBarras, "Coloque el cursor aquí y escanee el código del pedido. El lector debe enviar Enter al finalizar.");
            _toolTip.SetToolTip(btnRefrescar, "Recarga la información desde la base de datos.");
            _toolTip.SetToolTip(btnPedidosEscaneados, "Muestra los pedidos que ya cuentan con CheckIN (estatus CI).");
        }

        /// <summary>
        /// Define las columnas que se mostrarán en el listado principal.
        /// </summary>
        private void ConfigurarColumnas()
        {
            dgvPedidos.Columns.Clear();

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FolioFormateado),
                HeaderText = "Folio",
                Width = 120,
                MinimumWidth = 100
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ClienteNombre),
                HeaderText = "Cliente",
                Width = 220,
                MinimumWidth = 180
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FechaEntregaDescripcion),
                HeaderText = "Entrega",
                Width = 160,
                MinimumWidth = 140
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.EventoNombre),
                HeaderText = "Evento",
                Width = 200,
                MinimumWidth = 160
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.Total),
                HeaderText = "Total",
                Width = 120,
                MinimumWidth = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.Abonado),
                HeaderText = "Abonado",
                Width = 120,
                MinimumWidth = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvPedidos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.SaldoPendiente),
                HeaderText = "Saldo",
                Width = 120,
                MinimumWidth = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
        }

        /// <summary>
        /// Evento Load del formulario. Solicita los eventos y ubica el enfoque en el campo de escaneo.
        /// </summary>
        private void CheckInPedidosForm_Load(object sender, EventArgs e)
        {
            CargarEventos();
            EnfocarEscaneo();
        }

        /// <summary>
        /// Obtiene la lista de eventos disponibles, agrega la opción "Sin evento" y selecciona el evento del día cuando existe.
        /// </summary>
        private void CargarEventos()
        {
            try
            {
                _cargandoEventos = true;
                cmbEventos.Items.Clear();

                var eventos = _pedidoCheckDao.ObtenerEventosPorEmpresa(_empresaId)
                    .OrderByDescending(e => e.FechaEvento)
                    .ToList();

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
                MessageBox.Show($"No se pudieron cargar los eventos: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargandoEventos = false;
                CargarPedidos();
            }
        }

        /// <summary>
        /// Recupera los pedidos pendientes de CheckIN aplicando los filtros seleccionados.
        /// </summary>
        private void CargarPedidos()
        {
            if (_cargandoEventos)
            {
                return;
            }

            try
            {
                var filtro = txtBuscar.Text?.Trim() ?? string.Empty;
                var eventoId = ObtenerEventoSeleccionado(out var sinEvento);

                var pedidos = _pedidoCheckDao.ObtenerPedidosPorEstatus(
                    _empresaId,
                    "N",
                    eventoId,
                    sinEvento,
                    filtro,
                    incluirProductos: false,
                    ordenarPorFechaCheckIn: false);

                _pedidos.Clear();
                foreach (var pedido in pedidos)
                {
                    _pedidos.Add(pedido);
                }

                if (_pedidos.Count == 0)
                {
                    dgvPedidos.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los pedidos: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Devuelve el evento seleccionado en pantalla, indicando si se eligió la opción "Sin evento".
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

        /// <summary>
        /// Evento que responde al cambio de evento seleccionado refrescando la lista de pedidos.
        /// </summary>
        private void cmbEventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoEventos)
            {
                return;
            }

            CargarPedidos();
            EnfocarEscaneo();
        }

        /// <summary>
        /// Filtra la información cada vez que se modifica el texto de búsqueda.
        /// </summary>
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarPedidos();
        }

        /// <summary>
        /// Maneja la lectura del código de barras y dispara el flujo de validación del CheckIN.
        /// </summary>
        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ProcesarEscaneo();
            }
        }

        /// <summary>
        /// Ubica la fila seleccionada con el botón derecho para mostrar el menú contextual.
        /// </summary>
        private void dgvPedidos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvPedidos.ClearSelection();
                dgvPedidos.Rows[e.RowIndex].Selected = true;
            }
        }

        /// <summary>
        /// Ejecuta el flujo de CheckIN cuando se selecciona la opción del menú contextual.
        /// </summary>
        private void registrarCheckInMenuItem_Click(object sender, EventArgs e)
        {
            RegistrarCheckInManual();
        }

        /// <summary>
        /// Botón para refrescar los pedidos desde la base de datos.
        /// </summary>
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarPedidos();
        }

        /// <summary>
        /// Muestra el listado de pedidos que ya cuentan con CheckIN.
        /// </summary>
        private void btnPedidosEscaneados_Click(object sender, EventArgs e)
        {
            var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
            var descripcion = cmbEventos.SelectedItem?.ToString() ?? "Todos los eventos";

            using (var form = new CheckInPedidosEscaneadosForm(_connectionFactory, _empresaId, eventoId, sinEvento, descripcion))
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// Procesa el código recibido del lector para validar y registrar el CheckIN.
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
                var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
                var pedido = _pedidoCheckDao.ObtenerPedidoPorCodigo(_empresaId, codigo, "N");
                if (pedido == null)
                {
                    MessageBox.Show("No se encontró un pedido pendiente con ese folio.", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!_pedidoCheckDao.ValidarPedidoParaCheckIn(_empresaId, pedido.PedidoId, eventoId, sinEvento, out var mensaje, out var informacion))
                {
                    MessageBox.Show(mensaje, "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var confirmForm = new CheckInConfirmDialog(informacion))
                {
                    if (confirmForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (_pedidoCheckDao.RegistrarCheckIn(informacion.PedidoId, out var error))
                        {
                            MessageBox.Show("CheckIN registrado correctamente.", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarPedidos();
                        }
                        else
                        {
                            MessageBox.Show(error, "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el CheckIN: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtCodigoBarras.SelectAll();
            }
        }

        /// <summary>
        /// Ejecuta manualmente el proceso de CheckIN a partir del pedido seleccionado en la tabla.
        /// </summary>
        private void RegistrarCheckInManual()
        {
            if (!(dgvPedidos.CurrentRow?.DataBoundItem is PedidoCheckInfo pedidoSeleccionado))
            {
                return;
            }

            try
            {
                var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
                if (!_pedidoCheckDao.ValidarPedidoParaCheckIn(_empresaId, pedidoSeleccionado.PedidoId, eventoId, sinEvento, out var mensaje, out var informacion))
                {
                    MessageBox.Show(mensaje, "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var confirmForm = new CheckInConfirmDialog(informacion))
                {
                    if (confirmForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (_pedidoCheckDao.RegistrarCheckIn(informacion.PedidoId, out var error))
                        {
                            MessageBox.Show("CheckIN registrado correctamente.", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarPedidos();
                        }
                        else
                        {
                            MessageBox.Show(error, "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el CheckIN: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Coloca el foco en el cuadro de texto de escaneo para agilizar la captura.
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
        /// Representa un elemento del ComboBox de eventos conservando información adicional.
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
