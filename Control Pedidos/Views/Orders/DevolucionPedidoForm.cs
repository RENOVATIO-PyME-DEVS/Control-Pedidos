using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Control_Pedidos.Views.Orders
{
    /*
     * Clase: DevolucionPedidoForm
     * Descripción: Permite seleccionar un pedido activo del cliente y ejecutar su devolución automática,
     *              mostrando los detalles del pedido y los cobros registrados.
     */
    public partial class DevolucionPedidoForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly DevolucionDao _devolucionDao;
        private readonly PedidoDao _pedidoDao;
        private readonly DevolucionPrintingService _printingService = new DevolucionPrintingService();
        private readonly Cliente _cliente;
        private readonly Usuario _usuario;
        private readonly Empresa _empresa;

        private readonly BindingList<Pedido> _pedidos = new BindingList<Pedido>();
        private readonly BindingSource _pedidosBinding = new BindingSource();
        private readonly BindingList<PedidoDetalle> _detalles = new BindingList<PedidoDetalle>();
        private readonly BindingSource _detallesBinding = new BindingSource();
        private List<FormaCobro> _formasDevolucion = new List<FormaCobro>();
        private Pedido _pedidoSeleccionado;

        /*
         * Constructor: DevolucionPedidoForm
         * Descripción: Inicializa los componentes gráficos y prepara los DAOs necesarios para las consultas.
         */
        public DevolucionPedidoForm(DatabaseConnectionFactory connectionFactory, Cliente cliente, Usuario usuario, Empresa empresa)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));

            _devolucionDao = new DevolucionDao(_connectionFactory);
            _pedidoDao = new PedidoDao(_connectionFactory);

            clienteTextBox.Text = string.IsNullOrWhiteSpace(_cliente.NombreComercial)
                ? _cliente.Nombre
                : _cliente.NombreComercial;

            ConfigurePedidosGrid();
            ConfigureDetallesGrid();
        }

        /*
         * Evento: DevolucionPedidoForm_Load
         * Descripción: Carga el catálogo de formas de devolución y los pedidos activos del cliente.
         */
        private void DevolucionPedidoForm_Load(object sender, EventArgs e)
        {
            LoadFormasDevolucion();
            LoadPedidos();
        }

        /*
         * Método: ConfigurePedidosGrid
         * Descripción: Define las columnas visibles del grid de pedidos y enlaza el origen de datos.
         */
        private void ConfigurePedidosGrid()
        {
            _pedidosBinding.DataSource = _pedidos;

            pedidosDataGridView.AutoGenerateColumns = false;
            pedidosDataGridView.DataSource = _pedidosBinding;
            pedidosDataGridView.MultiSelect = false;
            pedidosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pedidosDataGridView.AllowUserToAddRows = false;
            pedidosDataGridView.AllowUserToDeleteRows = false;
            pedidosDataGridView.ReadOnly = true;
            pedidosDataGridView.Columns.Clear();

            // Columna de folio del pedido.
            pedidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Pedido.Folio),
                HeaderText = "Folio",
                Width = 90
            });

            // Columna de fecha de creación.
            pedidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Pedido.Fecha),
                HeaderText = "Fecha",
                DefaultCellStyle = { Format = "dd/MM/yyyy" },
                Width = 110
            });

            // Columna del total del pedido.
            pedidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Pedido.Total),
                HeaderText = "Total",
                DefaultCellStyle = { Format = "C2" },
                Width = 110
            });

            // Columna de cobros registrados.
            pedidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Pedido.MontoAbonado),
                HeaderText = "Abonado",
                DefaultCellStyle = { Format = "C2" },
                Width = 110
            });

            // Columna de saldo pendiente.
            pedidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Pedido.SaldoPendiente),
                HeaderText = "Saldo",
                DefaultCellStyle = { Format = "C2" },
                Width = 110
            });

            // Columna del estatus actual.
            pedidosDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Pedido.Estatus),
                HeaderText = "Estatus",
                Width = 80
            });

            pedidosDataGridView.SelectionChanged += pedidosDataGridView_SelectionChanged;
        }

        /*
         * Método: ConfigureDetallesGrid
         * Descripción: Configura el grid que muestra los artículos y cantidades del pedido seleccionado.
         */
        private void ConfigureDetallesGrid()
        {
            _detallesBinding.DataSource = _detalles;

            detallesDataGridView.AutoGenerateColumns = false;
            detallesDataGridView.DataSource = _detallesBinding;
            detallesDataGridView.MultiSelect = false;
            detallesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            detallesDataGridView.AllowUserToAddRows = false;
            detallesDataGridView.AllowUserToDeleteRows = false;
            detallesDataGridView.ReadOnly = true;
            detallesDataGridView.Columns.Clear();

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoDetalle.ArticuloNombre),
                HeaderText = "Artículo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoDetalle.Cantidad),
                HeaderText = "Cantidad",
                DefaultCellStyle = { Format = "N2" },
                Width = 90
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoDetalle.PrecioUnitario),
                HeaderText = "Precio",
                DefaultCellStyle = { Format = "C2" },
                Width = 90
            });

            detallesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoDetalle.Total),
                HeaderText = "Total",
                DefaultCellStyle = { Format = "C2" },
                Width = 90
            });
        }

        /*
         * Método: LoadFormasDevolucion
         * Descripción: Obtiene del catálogo las formas de cobro tipo devolución y las asigna al combo.
         */
        private void LoadFormasDevolucion()
        {
            try
            {
                _formasDevolucion = _devolucionDao.ObtenerFormasCobroDevolucion();
                formaCobroComboBox.DisplayMember = nameof(FormaCobro.Nombre);
                formaCobroComboBox.ValueMember = nameof(FormaCobro.Id);
                formaCobroComboBox.DataSource = _formasDevolucion;
                formaCobroComboBox.SelectedIndex = -1;

                if (!_formasDevolucion.Any())
                {
                    MessageBox.Show(this, "No hay formas de cobro configuradas para devoluciones. Registre al menos una forma de devolución en el catálogo para poder reembolsar montos abonados.", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudieron cargar las formas de devolución: {ex.Message}", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * Método: LoadPedidos
         * Descripción: Consulta los pedidos activos del cliente y actualiza el grid.
         */
        private void LoadPedidos()
        {
            try
            {
                pedidosDataGridView.SelectionChanged -= pedidosDataGridView_SelectionChanged;

                _pedidos.Clear();
                foreach (var pedido in _devolucionDao.ObtenerPedidosActivos(_cliente.Id))
                {
                    _pedidos.Add(pedido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudieron obtener los pedidos activos: {ex.Message}", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                devolverButton.Enabled = false;
                return;
            }
            finally
            {
                pedidosDataGridView.SelectionChanged += pedidosDataGridView_SelectionChanged;
            }

            if (_pedidos.Any())
            {
                pedidosDataGridView.Rows[0].Selected = true;
                MostrarPedidoSeleccionado(_pedidos.First());
                devolverButton.Enabled = true;
            }
            else
            {
                _detalles.Clear();
                devolverButton.Enabled = false;
                totalPedidoTextBox.Text = "-";
                totalCobrosTextBox.Text = "-";
                saldoTextBox.Text = "-";
                formaCobroActualTextBox.Text = string.Empty;
            }
        }

        /*
         * Evento: pedidosDataGridView_SelectionChanged
         * Descripción: Muestra la información del pedido cada vez que el usuario cambia la selección.
         */
        private void pedidosDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (pedidosDataGridView.CurrentRow?.DataBoundItem is Pedido pedido)
            {
                MostrarPedidoSeleccionado(pedido);
            }
        }

        /*
         * Método: MostrarPedidoSeleccionado
         * Descripción: Guarda el pedido seleccionado y consulta su detalle completo para mostrarlo.
         */
        private void MostrarPedidoSeleccionado(Pedido pedido)
        {
            _pedidoSeleccionado = pedido;

            if (_pedidoSeleccionado == null)
            {
                _detalles.Clear();
                devolverButton.Enabled = false;
                return;
            }

            try
            {
                // Recupera el pedido desde la base de datos para incluir los artículos registrados.
                var pedidoCompleto = _pedidoDao.ObtenerPedidoCompleto(_pedidoSeleccionado.Id);
                _detalles.Clear();

                if (pedidoCompleto?.Detalles != null)
                {
                    foreach (var detalle in pedidoCompleto.Detalles)
                    {
                        _detalles.Add(detalle);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudo obtener el detalle del pedido: {ex.Message}", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ActualizarResumen();
            devolverButton.Enabled = true;
        }

        /*
         * Método: ActualizarResumen
         * Descripción: Calcula los totales, determina si hay cobros y actualiza los controles de resumen.
         */
        private void ActualizarResumen()
        {
            if (_pedidoSeleccionado == null)
            {
                return;
            }

            try
            {
                // Se obtienen los montos desde la base para asegurar información fresca.
                var totalCobros = _devolucionDao.ObtenerTotalCobros(_pedidoSeleccionado.Id);
                _pedidoSeleccionado.MontoAbonado = totalCobros;
                _pedidoSeleccionado.SaldoPendiente = _pedidoSeleccionado.Total - _pedidoSeleccionado.MontoAbonado;

                totalPedidoTextBox.Text = _pedidoSeleccionado.Total.ToString("C2");
                totalCobrosTextBox.Text = totalCobros.ToString("C2");
                saldoTextBox.Text = _pedidoSeleccionado.SaldoPendiente.ToString("C2");

                // Se muestra la forma de cobro del último abono realizado (si existe).
                formaCobroActualTextBox.Text = _devolucionDao.ObtenerFormaCobroUltimoAbono(_pedidoSeleccionado.Id);

                formaCobroComboBox.Enabled = totalCobros > 0 && _formasDevolucion.Any();
                if (!formaCobroComboBox.Enabled)
                {
                    formaCobroComboBox.SelectedIndex = -1;
                }
                else if (formaCobroComboBox.SelectedIndex < 0 && _formasDevolucion.Count > 0)
                {
                    formaCobroComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudieron calcular los montos del pedido: {ex.Message}", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * Evento: devolverButton_Click
         * Descripción: Solicita confirmación y ejecuta el flujo de devolución del pedido seleccionado.
         */
        private void devolverButton_Click(object sender, EventArgs e)
        {
            if (_pedidoSeleccionado == null)
            {
                MessageBox.Show(this, "Seleccione un pedido para continuar.", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalCobros;
            try
            {
                totalCobros = _devolucionDao.ObtenerTotalCobros(_pedidoSeleccionado.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudo validar el total de cobros: {ex.Message}", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int formaCobroId = 0;
            FormaCobro formaSeleccionada = null;
            if (totalCobros > 0)
            {
                if (!_formasDevolucion.Any())
                {
                    MessageBox.Show(this, "Registre una forma de devolución en el catálogo para poder reembolsar los cobros del pedido.", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formaSeleccionada = ObtenerFormaDevolucionSeleccionada();
                if (formaSeleccionada == null)
                {
                    MessageBox.Show(this, "Seleccione la forma de cobro que se utilizará para la devolución.", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formaCobroId = formaSeleccionada.Id;
            }

            var folio = string.IsNullOrWhiteSpace(_pedidoSeleccionado.Folio) ? _pedidoSeleccionado.Id.ToString() : _pedidoSeleccionado.Folio;
            var confirmResult = MessageBox.Show(this, $"¿Desea realizar la devolución del pedido folio {folio}?", "Confirmar devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            if (!_devolucionDao.RegistrarDevolucion(_pedidoSeleccionado.Id, _usuario.Id, formaCobroId, out var message))
            {
                MessageBox.Show(this, message, "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si se registró un cobro de devolución, se solicita la impresión del comprobante.
            if (totalCobros > 0)
            {
                if (formaSeleccionada != null && _devolucionDao.UltimoCobroGenerado != null)
                {
                    _devolucionDao.UltimoCobroGenerado.FormaCobroNombre = formaSeleccionada.Nombre;
                }

                var ticketData = new DevolucionTicketData
                {
                    ClienteNombre = clienteTextBox.Text,
                    FolioPedido = folio,
                    TotalPedido = _pedidoSeleccionado.Total,
                    MontoDevuelto = totalCobros,
                    FormaDevolucion = formaSeleccionada?.Nombre ?? string.Empty,
                    FechaDevolucion = DateTime.Now,
                    Empresa = _empresa
                };

                _printingService.Print(ticketData, this);
                MessageBox.Show(this, message, "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "Pedido cancelado sin devolución monetaria.", "Devolución de pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /*
         * Método: ObtenerFormaDevolucionSeleccionada
         * Descripción: Devuelve la forma de cobro seleccionada en el combo o null si no hay selección.
         */
        private FormaCobro ObtenerFormaDevolucionSeleccionada()
        {
            return formaCobroComboBox.SelectedItem as FormaCobro;
        }

        /*
         * Evento: cancelarButton_Click
         * Descripción: Cierra el formulario sin realizar cambios.
         */
        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
