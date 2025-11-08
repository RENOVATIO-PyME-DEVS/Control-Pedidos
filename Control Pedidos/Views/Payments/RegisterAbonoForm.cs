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

        public Cobro CobroRegistrado { get; private set; }

        private void ConfigureGrid()
        {
            _bindingSource.DataSource = _pedidosConSaldo;
            pedidosGrid.AutoGenerateColumns = false;
            pedidosGrid.DataSource = _bindingSource;
        }

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
                    MessageBox.Show("No hay formas de cobro disponibles.", "Registrar abono", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron obtener las formas de cobro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

            montoNumericUpDown.Maximum = Math.Max(0, _saldoCliente);
            montoNumericUpDown.Value = 0;

            if (_saldoCliente <= 0)
            {
                MessageBox.Show("El cliente no tiene saldo pendiente.", "Registrar abono", MessageBoxButtons.OK, MessageBoxIcon.Information);
                guardarButton.Enabled = false;
                return;
            }

            if (_pedidoPrioritarioId.HasValue)
            {
                var pedidoPrioritario = _pedidosConSaldo.FirstOrDefault(p => p.PedidoId == _pedidoPrioritarioId.Value);
                if (pedidoPrioritario != null)
                {
                    montoNumericUpDown.Value = Math.Min(pedidoPrioritario.Saldo, _saldoCliente);
                    SeleccionarPedidoEnGrid(pedidoPrioritario.PedidoId);
                }
            }

            if (montoNumericUpDown.Value <= 0 && _pedidosConSaldo.Any())
            {
                montoNumericUpDown.Value = Math.Min(_pedidosConSaldo.First().Saldo, _saldoCliente);
            }

            DistribuirMonto();
        }

        private void SeleccionarPedidoEnGrid(int pedidoId)
        {
            foreach (DataGridViewRow row in pedidosGrid.Rows)
            {
                if (row.DataBoundItem is PedidoSaldo pedido && pedido.PedidoId == pedidoId)
                {
                    row.Selected = true;
                    pedidosGrid.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        private void montoNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DistribuirMonto();
        }

        private void DistribuirMonto()
        {
            foreach (var pedido in _pedidosConSaldo)
            {
                pedido.MontoAsignado = 0;
            }

            var montoRestante = Math.Min(montoNumericUpDown.Value, _saldoCliente);
            foreach (var pedido in _pedidosConSaldo.OrderBy(p => p.FechaEntrega))
            {
                if (montoRestante <= 0)
                {
                    break;
                }

                var saldo = pedido.Saldo;
                if (saldo <= 0)
                {
                    pedido.MontoAsignado = 0;
                    continue;
                }

                var asignado = Math.Min(saldo, montoRestante);
                pedido.MontoAsignado = asignado;
                montoRestante -= asignado;
            }

            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            var totalAsignado = _pedidosConSaldo.Sum(p => p.MontoAsignado);
            var saldoRestante = Math.Max(0, _saldoCliente - totalAsignado);
            saldoRestanteLabel.Text = saldoRestante.ToString("C2");

            var builder = new StringBuilder();
            if (totalAsignado > 0)
            {
                builder.AppendLine("Se abonarán:");
                foreach (var pedido in _pedidosConSaldo.Where(p => p.MontoAsignado > 0).OrderBy(p => p.FechaEntrega))
                {
                    builder.AppendLine($"Folio {pedido.Folio} → {pedido.MontoAsignado:C2}");
                }
                builder.AppendLine();
                builder.AppendLine($"Total abono: {totalAsignado:C2}");
                builder.AppendLine($"Saldo restante: {saldoRestante:C2}");
            }
            else
            {
                builder.AppendLine("Ingrese el monto del abono para ver la distribución.");
            }

            resumenTextBox.Text = builder.ToString();
            guardarButton.Enabled = totalAsignado > 0 && formaCobroComboBox.SelectedItem != null;
        }

        private void formaCobroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void guardarButton_Click(object sender, EventArgs e)
        {
            if (montoNumericUpDown.Value <= 0)
            {
                MessageBox.Show("Ingrese un monto mayor a $0.00", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (formaCobroComboBox.SelectedItem is not FormaCobro formaCobro)
            {
                MessageBox.Show("Seleccione la forma de cobro.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var totalAsignado = _pedidosConSaldo.Sum(p => p.MontoAsignado);
            if (totalAsignado <= 0)
            {
                MessageBox.Show("El monto no se asignó a ningún pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Math.Abs(totalAsignado - montoNumericUpDown.Value) > 0.01m)
            {
                MessageBox.Show("El monto asignado debe coincidir con el monto ingresado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (totalAsignado > _saldoCliente)
            {
                MessageBox.Show("No se puede abonar más del saldo del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var detalles = _pedidosConSaldo
                .Where(p => p.MontoAsignado > 0)
                .Select(p => new CobroDetalle
                {
                    PedidoId = p.PedidoId,
                    Folio = p.Folio,
                    FechaEntrega = p.FechaEntrega,
                    Monto = p.MontoAsignado
                })
                .ToList();

            if (detalles.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un pedido para registrar el abono.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cobro = new Cobro
            {
                ClienteId = _cliente.Id,
                Cliente = _cliente,
                EmpresaId = _empresa.Id,
                Empresa = _empresa,
                UsuarioId = _usuario.Id,
                FormaCobroId = formaCobro.Id,
                FormaCobroNombre = formaCobro.Nombre,
                Monto = totalAsignado,
                Fecha = DateTime.Now,
                FechaCreacion = DateTime.Now,
                Estatus = "N",
                SaldoAnterior = _saldoCliente,
                SaldoDespues = Math.Max(0, _saldoCliente - totalAsignado),
                Impreso = false,
                Detalles = detalles
            };

            if (!_cobroDao.RegistrarCobro(cobro, detalles, out var message))
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CobroRegistrado = cobro;
            MessageBox.Show("Abono registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            PreguntarImpresion(cobro);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void PreguntarImpresion(Cobro cobro)
        {
            var respuesta = MessageBox.Show("¿Desea imprimir el ticket del abono?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
            {
                _cobroDao.ActualizarEstadoImpresion(cobro.Id, false);
                return;
            }

            CobroPrintingResult resultado;
            try
            {
                resultado = _printingService.Print(cobro, this);
            }
            catch (Exception ex)
            {
                _cobroDao.ActualizarEstadoImpresion(cobro.Id, false);
                MessageBox.Show($"No se pudo imprimir el ticket: {ex.Message}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultado.Printed)
            {
                _cobroDao.ActualizarEstadoImpresion(cobro.Id, true);
                MessageBox.Show("Ticket impreso correctamente.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _cobroDao.ActualizarEstadoImpresion(cobro.Id, false);
            var mensaje = resultado.CancelledByUser
                ? "Impresión cancelada."
                : "Error al imprimir, se guardó una copia en PDF.";

            if (!string.IsNullOrWhiteSpace(resultado.PdfPath))
            {
                mensaje += $"\n\nRuta del archivo: {resultado.PdfPath}";
            }

            if (resultado.Error != null && !resultado.CancelledByUser)
            {
                mensaje += $"\nDetalle: {resultado.Error.Message}";
            }

            MessageBox.Show(mensaje, "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
