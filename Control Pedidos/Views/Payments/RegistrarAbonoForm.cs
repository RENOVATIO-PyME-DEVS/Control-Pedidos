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
    public partial class RegistrarAbonoForm : Form
    {
        private readonly CobroDao _cobroDao;
        private readonly Cliente _cliente;
        private readonly Usuario _usuario;
        private readonly Empresa _empresa;
        private readonly BindingList<PedidoSaldo> _pedidos = new BindingList<PedidoSaldo>();
        private readonly int? _pedidoPreferidoId;
        private readonly decimal? _montoInicial;
        private readonly CobroPrintingService _printingService = new CobroPrintingService();
        private decimal _saldoCliente;

        public Cobro CobroRegistrado { get; private set; }

        public RegistrarAbonoForm(DatabaseConnectionFactory connectionFactory, Cliente cliente, Usuario usuario, Empresa empresa, int? pedidoPreferidoId = null, decimal? montoInicial = null)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _cobroDao = new CobroDao(connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory)));
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            _empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
            _pedidoPreferidoId = pedidoPreferidoId;
            _montoInicial = montoInicial;

            ConfigureGrid();
            BindClient();
            LoadFormasCobro();
            LoadPedidos();
            ActualizarSaldoCliente();
            InicializarMonto();
            ActualizarDistribucion();
        }

        private void ConfigureGrid()
        {
            pedidosGrid.AutoGenerateColumns = false;
            pedidosGrid.AllowUserToAddRows = false;
            pedidosGrid.AllowUserToDeleteRows = false;
            pedidosGrid.ReadOnly = true;
            pedidosGrid.MultiSelect = false;
            pedidosGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pedidosGrid.RowHeadersVisible = false;
            pedidosGrid.Columns.Clear();

            pedidosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoSaldo.Folio),
                HeaderText = "Folio",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 25
            });

            pedidosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoSaldo.FechaEntrega),
                HeaderText = "Fecha Entrega",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 20,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            pedidosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoSaldo.Total),
                HeaderText = "Total",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 18,
                DefaultCellStyle = { Format = "C2" }
            });

            pedidosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoSaldo.Abonado),
                HeaderText = "Abonado",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 18,
                DefaultCellStyle = { Format = "C2" }
            });

            pedidosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoSaldo.Saldo),
                HeaderText = "Saldo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 18,
                DefaultCellStyle = { Format = "C2" }
            });

            pedidosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoSaldo.MontoAsignado),
                HeaderText = "Monto Asignado",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 21,
                DefaultCellStyle = { Format = "C2" }
            });

            pedidosGrid.DataSource = _pedidos;
        }

        private void BindClient()
        {
            clienteTextBox.Text = string.IsNullOrWhiteSpace(_cliente.NombreComercial)
                ? _cliente.Nombre
                : _cliente.NombreComercial;
        }

        private void LoadFormasCobro()
        {
            try
            {
                var formas = _cobroDao.ObtenerFormasCobro()
                    .Where(f => f.Activo)
                    .OrderBy(f => f.Nombre)
                    .ToList();

                formaCobroComboBox.DisplayMember = nameof(FormaCobro.Nombre);
                formaCobroComboBox.ValueMember = nameof(FormaCobro.Id);
                formaCobroComboBox.DataSource = formas;

                if (formas.Count > 0)
                {
                    formaCobroComboBox.SelectedIndex = 0;
                    formaCobroComboBox.Enabled = true;
                }
                else
                {
                    formaCobroComboBox.Enabled = false;
                    registrarButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar las formas de cobro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                formaCobroComboBox.DataSource = null;
                formaCobroComboBox.Enabled = false;
                registrarButton.Enabled = false;
            }
        }

        private void LoadPedidos()
        {
            try
            {
                _pedidos.Clear();
                foreach (var pedido in _cobroDao.ObtenerPedidosConSaldo(_cliente.Id))
                {
                    if (pedido.Saldo <= 0)
                    {
                        continue;
                    }

                    _pedidos.Add(pedido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los pedidos del cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarSaldoCliente()
        {
            try
            {
                _saldoCliente = _cobroDao.ObtenerSaldoCliente(_cliente.Id);
                saldoActualValueLabel.Text = _saldoCliente.ToString("C2");

                var minimo = montoNumericUpDown.Minimum;
                if (_saldoCliente < minimo)
                {
                    montoNumericUpDown.Value = minimo;
                    montoNumericUpDown.Maximum = minimo;
                    montoNumericUpDown.Enabled = false;
                    registrarButton.Enabled = false;
                }
                else
                {
                    montoNumericUpDown.Maximum = _saldoCliente;
                    if (montoNumericUpDown.Value > _saldoCliente)
                    {
                        montoNumericUpDown.Value = _saldoCliente;
                    }

                    montoNumericUpDown.Enabled = true;
                    registrarButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                _saldoCliente = 0m;
                saldoActualValueLabel.Text = 0m.ToString("C2");
                montoNumericUpDown.Enabled = false;
                registrarButton.Enabled = false;
                MessageBox.Show($"No se pudo calcular el saldo del cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InicializarMonto()
        {
            if (!_saldoCliente.Equals(0m) && montoNumericUpDown.Enabled)
            {
                decimal monto = 0m;

                if (_montoInicial.HasValue)
                {
                    monto = Math.Min(_montoInicial.Value, _saldoCliente);
                }
                else if (_pedidoPreferidoId.HasValue)
                {
                    var pedido = _pedidos.FirstOrDefault(p => p.PedidoId == _pedidoPreferidoId.Value);
                    if (pedido != null)
                    {
                        monto = Math.Min(pedido.Saldo, _saldoCliente);
                    }
                }

                if (monto > 0)
                {
                    montoNumericUpDown.Value = Math.Min(Math.Max(montoNumericUpDown.Minimum, monto), montoNumericUpDown.Maximum);
                }
            }
        }

        private void montoNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (montoNumericUpDown.Value > _saldoCliente)
            {
                montoNumericUpDown.Value = _saldoCliente;
            }

            ActualizarDistribucion();
        }

        private void ActualizarDistribucion()
        {
            foreach (var pedido in _pedidos)
            {
                pedido.MontoAsignado = 0;
            }

            var montoIngresado = montoNumericUpDown.Value;
            var montoRestante = montoIngresado;

            if (montoIngresado > 0)
            {
                foreach (var pedido in _pedidos.OrderBy(p => p.FechaEntrega))
                {
                    if (montoRestante <= 0)
                    {
                        break;
                    }

                    var saldo = pedido.Saldo;
                    if (saldo <= 0)
                    {
                        continue;
                    }

                    var asignado = Math.Min(saldo, montoRestante);
                    pedido.MontoAsignado = asignado;
                    montoRestante -= asignado;
                }
            }

            _pedidos.ResetBindings();
            pedidosGrid.Refresh();
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            var montoIngresado = montoNumericUpDown.Value;
            var builder = new StringBuilder();
            var asignaciones = _pedidos.Where(p => p.MontoAsignado > 0).ToList();

            if (montoIngresado <= 0)
            {
                builder.AppendLine("Ingrese el monto del abono para ver la distribución.");
            }
            else if (!asignaciones.Any())
            {
                builder.AppendLine("No hay pedidos con saldo pendiente.");
            }
            else
            {
                builder.AppendLine("Se abonarán:");
                foreach (var pedido in asignaciones)
                {
                    builder.AppendLine($"Folio {pedido.Folio} → {pedido.MontoAsignado:C2}");
                }

                builder.AppendLine();
                builder.AppendLine($"Total abono: {montoIngresado:C2}");
                var saldoRestante = Math.Max(0m, _saldoCliente - montoIngresado);
                builder.AppendLine($"Saldo restante: {saldoRestante:C2}");
            }

            resumenTextBox.Text = builder.ToString().TrimEnd();
            saldoDespuesValueLabel.Text = Math.Max(0m, _saldoCliente - montoIngresado).ToString("C2");
            var puedeRegistrar = montoNumericUpDown.Enabled && montoIngresado > 0 && montoIngresado <= _saldoCliente && asignaciones.Any() && formaCobroComboBox.SelectedItem != null;
            registrarButton.Enabled = puedeRegistrar;
        }

        private void registrarButton_Click(object sender, EventArgs e)
        {
            if (formaCobroComboBox.SelectedItem == null)
            {
                MessageBox.Show("Seleccione la forma de cobro.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var monto = montoNumericUpDown.Value;
            if (monto <= 0)
            {
                MessageBox.Show("Ingrese el monto del abono.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (monto > _saldoCliente)
            {
                MessageBox.Show("El monto del abono no puede ser mayor al saldo del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var asignaciones = _pedidos.Where(p => p.MontoAsignado > 0).ToList();
            if (!asignaciones.Any())
            {
                MessageBox.Show("Distribuya el monto en al menos un pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var formaSeleccionada = (FormaCobro)formaCobroComboBox.SelectedItem;

            var cobro = new Cobro
            {
                ClienteId = _cliente.Id,
                EmpresaId = _empresa.Id,
                UsuarioId = _usuario.Id,
                FormaCobroId = formaSeleccionada.Id,
                Monto = monto,
                Estatus = "N"
            };

            var detalles = asignaciones
                .Select(p => new CobroDetalle
                {
                    PedidoId = p.PedidoId,
                    Monto = p.MontoAsignado
                })
                .ToList();

            if (!_cobroDao.RegistrarCobro(cobro, detalles, out var message))
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CobroRegistrado = cobro;
            MessageBox.Show("Abono registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var imprimir = MessageBox.Show("¿Desea imprimir el ticket del abono?", "Impresión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (imprimir == DialogResult.Yes)
            {
                ImprimirTicket(cobro, formaSeleccionada, asignaciones);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ImprimirTicket(Cobro cobro, FormaCobro forma, IReadOnlyCollection<PedidoSaldo> asignaciones)
        {
            var ticket = new CobroTicketInfo
            {
                Cobro = cobro,
                Cliente = _cliente,
                Empresa = _empresa,
                FormaCobroNombre = forma.Nombre,
                SaldoAnterior = _saldoCliente,
                SaldoRestante = Math.Max(0m, _saldoCliente - cobro.Monto),
                Detalles = asignaciones
                    .Select(p => new CobroTicketDetalle
                    {
                        Folio = p.Folio,
                        Monto = p.MontoAsignado
                    })
                    .ToList()
            };

            var resultado = _printingService.Print(ticket, this);

            if (resultado.Printed)
            {
                return;
            }

            if (resultado.SavedPdf)
            {
                if (resultado.Error != null)
                {
                    MessageBox.Show($"Error al imprimir, se guardó una copia en PDF.\n\nUbicación: {resultado.PdfPath}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (resultado.CancelledByUser)
                {
                    MessageBox.Show($"La impresión fue cancelada. Se guardó una copia en PDF en:\n{resultado.PdfPath}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"No se pudo imprimir el ticket, pero se guardó una copia en PDF en:\n{resultado.PdfPath}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (resultado.Error != null)
            {
                MessageBox.Show($"No se pudo imprimir el ticket del abono: {resultado.Error.Message}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void formaCobroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarResumen();
        }
    }
}
