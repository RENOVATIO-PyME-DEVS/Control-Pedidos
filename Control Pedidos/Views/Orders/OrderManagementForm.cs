using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.Orders
{
    public partial class OrderManagementForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly Cliente _cliente;
        private readonly Usuario _usuario;
        private readonly IList<Empresa> _empresas;
        private readonly PedidoDao _pedidoDao;
        private readonly PedidoDetalleDao _pedidoDetalleDao;
        private readonly BindingList<PedidoDetalle> _detalles = new BindingList<PedidoDetalle>();
        private readonly List<Articulo> _articulos = new List<Articulo>();
        private Pedido _pedido;
        private bool _readOnlyMode;

        public OrderManagementForm(DatabaseConnectionFactory connectionFactory, Cliente cliente, Usuario usuario, Empresa empresa, IList<Empresa> empresasDisponibles = null, Pedido pedido = null)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));

            _empresas = empresasDisponibles?.Count > 0
                ? new List<Empresa>(empresasDisponibles)
                : new List<Empresa> { empresa ?? throw new ArgumentNullException(nameof(empresa)) };

            if (empresa != null && _empresas.All(e => e.Id != empresa.Id))
            {
                _empresas.Insert(0, empresa);
            }

            _pedidoDao = new PedidoDao(_connectionFactory);
            _pedidoDetalleDao = new PedidoDetalleDao(_connectionFactory);

            _pedido = pedido ?? new Pedido
            {
                Cliente = _cliente,
                Usuario = _usuario,
                Empresa = empresa ?? _empresas.First(),
                Fecha = DateTime.Now,
                FechaEntrega = DateTime.Now,
                HoraEntrega = null,
                Estatus = "P"
            };

            ConfigureGrid();
            BindClientData();
            BindUserData();
            LoadEmpresas();
            LoadEventos();
            LoadArticulos();
            BindPedidoData();
            UpdateDetalleTotal();
            UpdateTotals();
            UpdateControlsState();
        }

        private void ConfigureGrid()
        {
            detallesGrid.AutoGenerateColumns = false;
            detallesGrid.Columns.Clear();

            detallesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoDetalle.ArticuloNombre),
                HeaderText = "Artículo",
                FillWeight = 40
            });

            detallesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoDetalle.Cantidad),
                HeaderText = "Cantidad",
                FillWeight = 15,
                DefaultCellStyle = { Format = "N2" }
            });

            detallesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoDetalle.PrecioUnitario),
                HeaderText = "Precio unitario",
                FillWeight = 20,
                DefaultCellStyle = { Format = "C2" }
            });

            detallesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoDetalle.Total),
                HeaderText = "Total",
                FillWeight = 25,
                DefaultCellStyle = { Format = "C2" }
            });

            detallesGrid.DataSource = _detalles;
        }

        private void BindClientData()
        {
            clientNameTextBox.Text = string.IsNullOrWhiteSpace(_cliente.NombreComercial) ? _cliente.Nombre : _cliente.NombreComercial;
            clientPhoneTextBox.Text = _cliente.Telefono;
            clientAddressTextBox.Text = _cliente.Direccion;
            clientRfcTextBox.Text = _cliente.Rfc;
            clientEmailTextBox.Text = _cliente.Correo;
        }

        private void BindUserData()
        {
            userNameTextBox.Text = _usuario.Nombre;
            userRoleTextBox.Text = _usuario.RolesResumen ?? string.Empty;
        }

        private void LoadEmpresas()
        {
            companyComboBox.DisplayMember = nameof(Empresa.Nombre);
            companyComboBox.ValueMember = nameof(Empresa.Id);
            companyComboBox.DataSource = _empresas.ToList();

            if (_pedido.Empresa != null)
            {
                for (var i = 0; i < companyComboBox.Items.Count; i++)
                {
                    if (companyComboBox.Items[i] is Empresa empresa && empresa.Id == _pedido.Empresa.Id)
                    {
                        companyComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void LoadEventos()
        {
            var selectedEmpresa = companyComboBox.SelectedItem as Empresa;
            var eventos = new List<Evento>
            {
                new Evento { Id = 0, Nombre = "Sin evento", TieneSerie = false, Serie = string.Empty, SiguienteFolio = 0, EmpresaId = selectedEmpresa?.Id ?? 0 }
            };

            try
            {
                foreach (var evento in Evento.Listar(_connectionFactory, string.Empty).Where(e => selectedEmpresa == null || e.EmpresaId == selectedEmpresa.Id))
                {
                    eventos.Add(evento);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los eventos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            eventComboBox.DisplayMember = nameof(Evento.Nombre);
            eventComboBox.ValueMember = nameof(Evento.Id);
            eventComboBox.DataSource = eventos;
            eventComboBox.SelectedIndex = 0;
        }

        private void LoadArticulos()
        {
            _articulos.Clear();
            try
            {
                _articulos.AddRange(Articulo.Listar(_connectionFactory, string.Empty));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los artículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            articuloComboBox.DisplayMember = nameof(Articulo.Nombre);
            articuloComboBox.ValueMember = nameof(Articulo.Id);
            articuloComboBox.DataSource = _articulos.ToList();
            articuloComboBox.SelectedIndex = _articulos.Count > 0 ? 0 : -1;

            if (_articulos.Count > 0)
            {
                precioNumericUpDown.Value = Math.Max(0, Convert.ToDecimal(_articulos[0].Precio));
            }
        }

        private void BindPedidoData()
        {
            fechaDateTimePicker.Value = _pedido.Fecha == default ? DateTime.Now : _pedido.Fecha;
            fechaEntregaDateTimePicker.Value = _pedido.FechaEntrega == default ? DateTime.Now : _pedido.FechaEntrega;
            if (_pedido.HoraEntrega.HasValue)
            {
                horaEntregaDateTimePicker.Checked = true;
                horaEntregaDateTimePicker.Value = DateTime.Today.Add(_pedido.HoraEntrega.Value);
            }
            else
            {
                horaEntregaDateTimePicker.Checked = false;
                horaEntregaDateTimePicker.Value = DateTime.Today;
            }
            folioTextBox.Text = _pedido.Folio;
            statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);

            if (_pedido.Detalles != null && _pedido.Detalles.Count > 0)
            {
                _detalles.Clear();
                foreach (var detalle in _pedido.Detalles)
                {
                    _detalles.Add(detalle);
                }
            }
        }

        private static string ObtenerDescripcionEstatus(string estatus)
        {
            switch (estatus)
            {
                case "N":
                    return "Cerrado";
                case "C":
                    return "Cancelado";
                case "P":
                default:
                    return "Pendiente";
            }
        }

        private void UpdateDetalleTotal()
        {
            var total = cantidadNumericUpDown.Value * precioNumericUpDown.Value;
            totalArticuloTextBox.Text = total.ToString("C2");
        }

        private void UpdateTotals()
        {
            var total = _detalles.Sum(d => d.Total);
            totalGeneralValueLabel.Text = total.ToString("C2");
        }

        private void UpdateControlsState()
        {
            _readOnlyMode = _pedido != null && (_pedido.Estatus == "N" || _pedido.Estatus == "C");

            var canEditHeader = !_readOnlyMode && (_pedido == null || _pedido.Id == 0);

            companyComboBox.Enabled = canEditHeader && _empresas.Count > 1;
            eventComboBox.Enabled = !_readOnlyMode && (_pedido == null || _pedido.Id == 0);
            fechaDateTimePicker.Enabled = !_readOnlyMode;
            fechaEntregaDateTimePicker.Enabled = !_readOnlyMode;
            horaEntregaDateTimePicker.Enabled = !_readOnlyMode;

            articuloComboBox.Enabled = !_readOnlyMode;
            cantidadNumericUpDown.Enabled = !_readOnlyMode;
            precioNumericUpDown.Enabled = !_readOnlyMode;
            agregarArticuloButton.Enabled = !_readOnlyMode;
            eliminarArticuloButton.Enabled = !_readOnlyMode && _detalles.Count > 0;
            cerrarPedidoButton.Enabled = !_readOnlyMode && _pedido.Id > 0;
            cancelarPedidoButton.Enabled = !_readOnlyMode && _pedido.Id > 0;
        }

        private bool TryPreparePedido(out string message)
        {
            message = string.Empty;

            if (!(companyComboBox.SelectedItem is Empresa empresaSeleccionada))
            {
                message = "Seleccione una empresa.";
                return false;
            }

            _pedido.Empresa = empresaSeleccionada;
            _pedido.Fecha = fechaDateTimePicker.Value.Date;
            _pedido.FechaEntrega = fechaEntregaDateTimePicker.Value.Date;
            _pedido.HoraEntrega = horaEntregaDateTimePicker.Checked ? (TimeSpan?)horaEntregaDateTimePicker.Value.TimeOfDay : null;

            if (eventComboBox.SelectedItem is Evento evento && evento.Id > 0)
            {
                _pedido.Evento = evento;
            }
            else
            {
                _pedido.Evento = null;
            }

            return true;
        }

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEventos();
        }

        private void articuloComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (articuloComboBox.SelectedItem is Articulo articulo)
            {
                precioNumericUpDown.Value = Math.Max(0, Convert.ToDecimal(articulo.Precio));
            }

            UpdateDetalleTotal();
        }

        private void DetalleValueChanged(object sender, EventArgs e)
        {
            UpdateDetalleTotal();
        }

        private void agregarArticuloButton_Click(object sender, EventArgs e)
        {
            if (_readOnlyMode)
            {
                return;
            }

            if (articuloComboBox.SelectedItem is not Articulo articuloSeleccionado)
            {
                MessageBox.Show("Seleccione un artículo", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cantidadNumericUpDown.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryPreparePedido(out var headerMessage))
            {
                MessageBox.Show(headerMessage, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var detalle = new PedidoDetalle
            {
                Pedido = _pedido,
                PedidoId = _pedido.Id,
                Articulo = articuloSeleccionado,
                ArticuloId = articuloSeleccionado.Id,
                Cantidad = cantidadNumericUpDown.Value,
                PrecioUnitario = precioNumericUpDown.Value,
                Total = cantidadNumericUpDown.Value * precioNumericUpDown.Value
            };

            if (!_pedidoDetalleDao.Agregar(detalle, out var message))
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_pedido.Id == 0)
            {
                _pedido.Id = detalle.PedidoId;
                if (string.IsNullOrWhiteSpace(_pedido.Folio) && detalle.Pedido != null)
                {
                    _pedido.Folio = detalle.Pedido.Folio;
                }
                folioTextBox.Text = _pedido.Folio;
                statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);
            }

            _detalles.Add(detalle);
            _pedido.Detalles.Add(detalle);
            UpdateTotals();
            UpdateControlsState();
        }

        private void eliminarArticuloButton_Click(object sender, EventArgs e)
        {
            if (_readOnlyMode)
            {
                return;
            }

            if (detallesGrid.CurrentRow?.DataBoundItem is not PedidoDetalle detalle)
            {
                return;
            }

            if (MessageBox.Show("¿Desea eliminar el artículo seleccionado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (!_pedidoDetalleDao.Eliminar(detalle.Id, out var message))
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _detalles.Remove(detalle);
            _pedido.Detalles.Remove(detalle);
            UpdateTotals();
            UpdateControlsState();
        }

        private void cerrarPedidoButton_Click(object sender, EventArgs e)
        {
            if (_pedido.Id == 0)
            {
                MessageBox.Show("Agregue al menos un artículo antes de cerrar el pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_pedidoDao.ActualizarEstatus(_pedido.Id, "N", out var message))
            {
                _pedido.Estatus = "N";
                statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);
                MessageBox.Show("Pedido cerrado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateControlsState();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelarPedidoButton_Click(object sender, EventArgs e)
        {
            if (_pedido.Id == 0)
            {
                MessageBox.Show("El pedido aún no ha sido creado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("¿Desea cancelar el pedido?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (_pedidoDao.ActualizarEstatus(_pedido.Id, "C", out var message))
            {
                _pedido.Estatus = "C";
                statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);
                MessageBox.Show("Pedido cancelado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateControlsState();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cerrarVentanaButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
