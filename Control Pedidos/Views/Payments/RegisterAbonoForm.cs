using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Printing;

namespace Control_Pedidos.Views.Payments
{
    /*
     * Clase: RegisterAbonoForm
     * Descripción: Permite registrar un cobro dirigido a un único pedido del cliente seleccionado.
     *               Se muestran los pedidos con saldo y se valida que el monto ingresado no exceda el saldo pendiente.
     */
    public partial class RegisterAbonoForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly CobroDao _cobroDao;
        private readonly CobroPrintingService _printingService = new CobroPrintingService();
        private readonly Cliente _cliente;
        private readonly Usuario _usuario;
        private readonly Empresa _empresa;
        private readonly int? _pedidoPrioritarioId;
        private readonly BindingList<PedidoSaldo> _pedidosConSaldo = new BindingList<PedidoSaldo>();
        private readonly BindingSource _bindingSource = new BindingSource();
        private List<FormaCobro> _formasCobro = new List<FormaCobro>();
        private decimal _saldoCliente;
        private PedidoSaldo _pedidoSeleccionado;

        /// <summary>
        /// Constructor del formulario. Recibe la información necesaria para registrar el cobro.
        /// </summary>
        /// <param name="connectionFactory">Fábrica de conexiones a la base de datos.</param>
        /// <param name="cliente">Cliente del cual se consultan los pedidos.</param>
        /// <param name="usuario">Usuario que realiza el registro.</param>
        /// <param name="empresa">Empresa propietaria del pedido.</param>
        /// <param name="pedidoPrioritarioId">Pedido que se desea seleccionar automáticamente (al cerrar desde pedidos).</param>
        public RegisterAbonoForm(DatabaseConnectionFactory connectionFactory, Cliente cliente, Usuario usuario, Empresa empresa, int? pedidoPrioritarioId = null)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
            _pedidoPrioritarioId = pedidoPrioritarioId;

            _cobroDao = new CobroDao(_connectionFactory);

            clienteTextBox.Text = string.IsNullOrWhiteSpace(_cliente.NombreComercial) ? _cliente.Nombre : _cliente.NombreComercial;
            saldoClienteTextBox.Text = "-";
            saldoRestanteLabel.Text = "-";

            ConfigureGrid();
            LoadFormasCobro();
            LoadDatosCliente();
        }

        /// <summary>
        /// Cobro recién registrado. Se expone para que otras pantallas puedan conocer el resultado.
        /// </summary>
        public Cobro CobroRegistrado { get; private set; }

        /// <summary>
        /// Configura el grid para que muestre los pedidos con saldo y permita seleccionar una sola fila.
        /// </summary>
        private void ConfigureGrid()
        {
            _bindingSource.DataSource = _pedidosConSaldo;
            pedidosGrid.AutoGenerateColumns = false;
            pedidosGrid.DataSource = _bindingSource;
            pedidosGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pedidosGrid.MultiSelect = false;
        }

        /// <summary>
        /// Carga el catálogo de formas de cobro para que el usuario elija cómo recibirá el pago.
        /// </summary>
        private void LoadFormasCobro()
        {
            try
            {
                _formasCobro = _cobroDao.ObtenerFormasCobro();
                formaCobroComboBox.DisplayMember = nameof(FormaCobro.Nombre);
                formaCobroComboBox.ValueMember = nameof(FormaCobro.Id);
                formaCobroComboBox.DataSource = _formasCobro;
                formaCobroComboBox.Enabled = _formasCobro.Count > 0;

                if (_formasCobro.Count == 0)
                {
                    MessageBox.Show("No hay formas de cobro disponibles.", "Registrar cobro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron obtener las formas de cobro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene el saldo pendiente del cliente y la lista de pedidos abiertos.
        /// </summary>
        private void LoadDatosCliente()
        {
            try
            {
                _saldoCliente = _cobroDao.ObtenerSaldoCliente(_cliente.Id);
                saldoClienteTextBox.Text = _saldoCliente.ToString("C2");
                saldoRestanteLabel.Text = _saldoCliente.ToString("C2");

                _pedidosConSaldo.Clear();
                foreach (var pedido in _cobroDao.ObtenerPedidosConSaldo(_cliente.Id))
                {
                    _pedidosConSaldo.Add(pedido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo obtener la información del cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            montoNumericUpDown.Maximum = _pedidosConSaldo.Any() ? _pedidosConSaldo.Max(p => p.Saldo) : 0;
            montoNumericUpDown.Value = 0;

            if (_saldoCliente <= 0)
            {
                MessageBox.Show("El cliente no tiene saldo pendiente.", "Registrar cobro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                guardarButton.Enabled = false;
                return;
            }

            if (_pedidoPrioritarioId.HasValue)
            {
                var pedidoPrioritario = _pedidosConSaldo.FirstOrDefault(p => p.PedidoId == _pedidoPrioritarioId.Value);
                if (pedidoPrioritario != null)
                {
                    SeleccionarPedidoEnGrid(pedidoPrioritario.PedidoId);
                }
            }

            ActualizarResumen();
        }

        /// <summary>
        /// Selecciona en el grid el pedido indicado y actualiza los datos mostrados.
        /// </summary>
        private void SeleccionarPedidoEnGrid(int pedidoId)
        {
            foreach (DataGridViewRow row in pedidosGrid.Rows)
            {
                if (row.DataBoundItem is PedidoSaldo pedido && pedido.PedidoId == pedidoId)
                {
                    row.Selected = true;
                    pedidosGrid.CurrentCell = row.Cells[0];
                    ActualizarPedidoSeleccionado(pedido);
                    break;
                }
            }
        }

        /// <summary>
        /// Evento del control numérico. Cada vez que cambia se recalcula el monto aplicado al pedido seleccionado.
        /// </summary>
        private void montoNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ActualizarMontoAsignado();
        }

        /// <summary>
        /// Ajusta la propiedad MontoAsignado de los pedidos y refresca el resumen para mostrar el saldo restante.
        /// </summary>
        private void ActualizarMontoAsignado()
        {
            if (_pedidoSeleccionado == null)
            {
                foreach (var pedido in _pedidosConSaldo)
                {
                    pedido.MontoAsignado = 0;
                }

                ActualizarResumen();
                return;
            }

            foreach (var pedido in _pedidosConSaldo)
            {
                pedido.MontoAsignado = pedido.PedidoId == _pedidoSeleccionado.PedidoId
                    ? Math.Min(montoNumericUpDown.Value, pedido.Saldo)
                    : 0m;
            }

            ActualizarResumen();
        }

        /// <summary>
        /// Genera el texto del resumen y valida si se puede guardar el cobro.
        /// </summary>
        private void ActualizarResumen()
        {
            var totalAsignado = _pedidosConSaldo.Sum(p => p.MontoAsignado);
            var saldoRestanteCliente = Math.Max(0, _saldoCliente - totalAsignado);
            saldoRestanteLabel.Text = saldoRestanteCliente.ToString("C2");

            var builder = new StringBuilder();
            if (_pedidoSeleccionado == null)
            {
                builder.AppendLine("Seleccione un pedido para registrar el cobro.");
            }
            else if (totalAsignado <= 0)
            {
                builder.AppendLine("Ingrese el monto del cobro.");
            }
            else
            {
                var saldoPedido = Math.Max(0, _pedidoSeleccionado.Saldo - _pedidoSeleccionado.MontoAsignado);
                builder.AppendLine($"Pedido seleccionado: {_pedidoSeleccionado.Folio}");
                builder.AppendLine($"Saldo antes del cobro: {_pedidoSeleccionado.Saldo:C2}");
                builder.AppendLine($"Monto a abonar: {_pedidoSeleccionado.MontoAsignado:C2}");
                builder.AppendLine($"Saldo después del cobro: {saldoPedido:C2}");
            }

            builder.AppendLine();
            builder.AppendLine($"Saldo general del cliente después del cobro: {saldoRestanteCliente:C2}");

            resumenTextBox.Text = builder.ToString();
            guardarButton.Enabled = totalAsignado > 0 && formaCobroComboBox.SelectedItem != null;
        }

        /// <summary>
        /// Al cambiar la forma de cobro se actualiza el resumen para habilitar o deshabilitar el botón Guardar.
        /// </summary>
        private void formaCobroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarResumen();
        }

        /// <summary>
        /// Botón cancelar. Cierra el formulario sin realizar cambios.
        /// </summary>
        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Botón guardar. Valida la información y registra el cobro en la base de datos.
        /// </summary>
        private void guardarButton_Click(object sender, EventArgs e)
        {
            if (_pedidoSeleccionado == null)
            {
                MessageBox.Show("Seleccione el pedido al que se aplicará el cobro.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (montoNumericUpDown.Value <= 0)
            {
                MessageBox.Show("Ingrese un monto mayor a $0.00", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var formaCobro = formaCobroComboBox.SelectedItem as FormaCobro;
            if (formaCobro == null)
            {
                MessageBox.Show("Seleccione la forma de cobro.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var montoAbono = Math.Min(_pedidoSeleccionado.MontoAsignado, montoNumericUpDown.Value);
            if (montoAbono <= 0)
            {
                MessageBox.Show("El monto no se asignó al pedido seleccionado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var saldoPedido = _cobroDao.ObtenerSaldoPedido(_pedidoSeleccionado.PedidoId);
            if (montoAbono > saldoPedido)
            {
                MessageBox.Show("El monto ingresado supera el saldo pendiente del pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (montoAbono > _saldoCliente)
            {
                MessageBox.Show("No se puede abonar más del saldo total del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var detalles = new List<CobroDetalle>
            {
                new CobroDetalle
                {
                    PedidoId = _pedidoSeleccionado.PedidoId,
                    Folio = _pedidoSeleccionado.Folio,
                    FechaEntrega = _pedidoSeleccionado.FechaEntrega,
                    Monto = montoAbono
                }
            };

            var cobro = new Cobro
            {
                ClienteId = _cliente.Id,
                Cliente = _cliente,
                EmpresaId = _empresa.Id,
                Empresa = _empresa,
                UsuarioId = _usuario.Id,
                FormaCobroId = formaCobro.Id,
                FormaCobroNombre = formaCobro.Nombre,
                Monto = montoAbono,
                Fecha = DateTime.Now,
                FechaCreacion = DateTime.Now,
                Estatus = "N",
                SaldoAnterior = _saldoCliente,
                SaldoDespues = Math.Max(0, _saldoCliente - montoAbono),
                Impreso = "N",
                Detalles = detalles
            };

            if (!_cobroDao.RegistrarCobro(cobro, detalles, out var message))
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CobroRegistrado = cobro;
            MessageBox.Show("Cobro registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            PreguntarImpresion(cobro);

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Solicita al usuario imprimir el ticket del cobro recién registrado.
        /// </summary>
        private void PreguntarImpresion(Cobro cobro)
        {
            var respuesta = MessageBox.Show("¿Desea imprimir el ticket del cobro?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                _cobroDao.MarcarCobroImpreso(cobro.Id, false);
                cobro.Impreso = "N";
                return;
            }

            CobroPrintingResult resultado;
            try
            {
                resultado = _printingService.Print(cobro, this);
            }
            catch (Exception ex)
            {
                _cobroDao.MarcarCobroImpreso(cobro.Id, false);
                cobro.Impreso = "N";
                MessageBox.Show($"No se pudo imprimir el ticket: {ex.Message}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultado.Printed)
            {
                _cobroDao.MarcarCobroImpreso(cobro.Id, true);
                cobro.Impreso = "S";
                MessageBox.Show("Ticket impreso correctamente.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _cobroDao.MarcarCobroImpreso(cobro.Id, false);
            cobro.Impreso = "N";
            var mensaje = resultado.CancelledByUser ? "Impresión cancelada." : "Error al imprimir.";

            if (!string.IsNullOrWhiteSpace(resultado.PdfPath))
            {
                mensaje += "\n\nSe generó un archivo PDF con el ticket.";
                mensaje += $"\nRuta del archivo: {resultado.PdfPath}";
            }
            else if (!resultado.CancelledByUser)
            {
                mensaje += "\nNo se pudo generar el PDF automáticamente.";
            }

            if (resultado.Error != null && !resultado.CancelledByUser)
            {
                mensaje += $"\nDetalle: {resultado.Error.Message}";
            }

            if (resultado.PdfError != null)
            {
                mensaje += $"\nPDF: {resultado.PdfError.Message}";
            }

            MessageBox.Show(mensaje, "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Evento del grid. Permite actualizar los datos auxiliares cuando el usuario cambia de pedido.
        /// </summary>
        private void pedidosGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (pedidosGrid.CurrentRow?.DataBoundItem is PedidoSaldo pedido)
            {
                ActualizarPedidoSeleccionado(pedido);
            }
        }

        private void saldoRestanteLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El abono registrado no puede ser mayor al saldo del pedido.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RegisterAbonoForm_Load(object sender, EventArgs e)
        {
            // El cargado principal se realiza en el constructor; se mantiene este evento para compatibilidad con el diseñador.
        }

        /// <summary>
        /// Actualiza los controles auxiliares con la información del pedido seleccionado.
        /// </summary>
        /// <param name="pedido">Pedido seleccionado en el grid.</param>
        private void ActualizarPedidoSeleccionado(PedidoSaldo pedido)
        {
            _pedidoSeleccionado = pedido;

            if (pedido == null)
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                montoNumericUpDown.Value = 0;
                montoNumericUpDown.Maximum = 0;
                ActualizarMontoAsignado();
                return;
            }

            textBox1.Text = pedido.Folio;
            textBox2.Text = pedido.Saldo.ToString("C2");

            montoNumericUpDown.Maximum = pedido.Saldo;
            montoNumericUpDown.Value = pedido.Saldo;

            ActualizarMontoAsignado();
        }
    }
}
