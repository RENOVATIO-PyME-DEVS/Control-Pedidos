using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Printing;
using Control_Pedidos.Views.Payments;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Control_Pedidos.Views.Orders
{
    /*
     * Clase: OrderManagementForm
     * Descripción: Ventana principal para la captura y administración de pedidos.
     *               Coordina la impresión, el cierre de pedidos y ahora el flujo de cobros.
     */
    public partial class OrderManagementForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly Cliente _cliente;
        private readonly Usuario _usuario;
        private readonly IList<Empresa> _empresas;
        private readonly PedidoDao _pedidoDao;
        private readonly PedidoDetalleDao _pedidoDetalleDao;
        private readonly PedidoPrintingService _printingService;
        private readonly BindingList<PedidoDetalle> _detalles = new BindingList<PedidoDetalle>();
        private readonly List<Articulo> _articulos = new List<Articulo>();
        private List<Articulo> _articulosOriginales = new List<Articulo>();

        private readonly int _detallesGridTopBase;
        private readonly int _detallesGridHeightBase;
        private readonly int _kitComponentsBaseHeight;
        private Pedido _pedido;
        private bool _readOnlyMode;
        private bool _isUpdatingDiscountValue;



        public OrderManagementForm(DatabaseConnectionFactory connectionFactory, Cliente cliente, Usuario usuario, Empresa empresa, IList<Empresa> empresasDisponibles = null, Pedido pedido = null)
        {
            InitializeComponent();

            noStyle();
            UIStyles.ApplyTheme(this);

            //   Aquí coloca el comportamiento correcto del ComboBox
            articuloComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            articuloComboBox.AutoCompleteMode = AutoCompleteMode.None;
            articuloComboBox.AutoCompleteSource = AutoCompleteSource.None;
            articuloComboBox.TextUpdate += articuloComboBox_TextUpdate;


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
            _printingService = new PedidoPrintingService();

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

            if (pedido != null)
            {
                _pedido.Impreso = pedido.Impreso;
            }

            ConfigureGrid();
            _detallesGridTopBase = detallesGrid.Top;
            _detallesGridHeightBase = detallesGrid.Height;
            _kitComponentsBaseHeight = kitComponentsRichTextBox.Height;
            BindClientData();
            BindUserData();
            LoadEmpresas();
            LoadEventos();
            LoadArticulos();
            BindPedidoData();
            UpdateDetalleTotal();
            UpdateTotals();
            UpdateControlsState();
            ConfigComboDescuentos();
            FormClosing += OrderManagementForm_FormClosing;
        }

        private void ConfigComboDescuentos()
        {
            descuentoComboBox.Items.AddRange(new object[] { "5", "10", "100" });
            descuentoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            descuentoComboBox.SelectedIndexChanged += descuentoComboBox_SelectedIndexChanged;

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
            userAndRolLabel.Text = $"{_usuario.Nombre} - {_usuario.RolesResumen ?? string.Empty}";
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
            if (_pedido.Evento != null)
            {
                for (var i = 0; i < eventComboBox.Items.Count; i++)
                {
                    if (eventComboBox.Items[i] is Evento item && item.Id == _pedido.Evento.Id)
                    {
                        eventComboBox.SelectedIndex = i;
                        return;
                    }
                }
            }

            // Si hay más de un evento, seleccionar el siguiente a "Sin evento"
            //if (eventos.Count > 1)
            //    eventComboBox.SelectedIndex = 1;
            //else
            //    eventComboBox.SelectedIndex = 0;

        }

        private void LoadArticulos()
        {
            _articulos.Clear();
            try
            {
                _articulos.AddRange(Articulo.ListarSinProd(_connectionFactory, string.Empty));
                _articulosOriginales = _articulos.ToList();   // 🔥 guardamos copia
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los artículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            articuloComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            articuloComboBox.DataSource = _articulos.ToList();

            articuloComboBox.DisplayMember = nameof(Articulo.Nombre);
            articuloComboBox.ValueMember = nameof(Articulo.Id);
            //articuloComboBox.DataSource = _articulos.ToList();

                      articuloComboBox.SelectedIndex = _articulos.Count > 0 ? 0 : -1;

            if (_articulos.Count > 0)
            {
                precioNumericUpDown.Value = Math.Max(0, Convert.ToDecimal(_articulos[0].Precio));
                RefreshKitComponents(_articulos[0]);
            }
            else
            {
                RefreshKitComponents(null);
            }
        }

        private void articuloComboBox_TextUpdate(object sender, EventArgs e)
        {
            
            string filtro = articuloComboBox.Text.Trim().ToLower();

                // Filtrar artículos
                var filtrados = _articulosOriginales
                    .Where(a => a.Nombre.ToLower().Contains(filtro) ||
                                a.NombreCorto.ToLower().Contains(filtro))
                    .ToList();

                // 🔥 QUITAR datasource ANTES DE ASIGNAR LISTA
                articuloComboBox.DataSource = null;

                // Asignar lista filtrada
                articuloComboBox.Items.Clear();
                foreach (var a in filtrados)
                    articuloComboBox.Items.Add(a);

                articuloComboBox.DisplayMember = nameof(Articulo.Nombre);

                articuloComboBox.DroppedDown = true;

                // 🔥 Mantener texto del usuario sin sobrescribir
                articuloComboBox.Text = filtro;

                articuloComboBox.SelectionStart = filtro.Length;
                articuloComboBox.SelectionLength = 0;

            try
            {

            // Si no hay coincidencias
            if (articuloComboBox.Items.Count == 0)
            {
                articuloComboBox.DroppedDown = false;

                // Limpia correctamente el ComboBox
                articuloComboBox.SelectedIndex = -1;
                articuloComboBox.Text = string.Empty;

                return; // 🛑 DETIENE EL MÉTODO Y EVITA EL ERROR
            }
            }
            catch (Exception ex)
            {

            }


            // Garantizar selección válida
            //articuloComboBox.SelectedIndex = 0;

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
                //horaEntregaDateTimePicker.Checked = false;
                horaEntregaDateTimePicker.Checked = true;
                //horaEntregaDateTimePicker.Value =  DateTime.Today;
                horaEntregaDateTimePicker.Value = DateTime.Today.AddHours(10);

            }
            UpdateFolioDisplay();
            statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);
            notesTextBox.Text = _pedido.Notas ?? string.Empty;

            if (_pedido.Detalles != null && _pedido.Detalles.Count > 0)
            {
                _detalles.Clear();
                foreach (var detalle in _pedido.Detalles)
                {
                    _detalles.Add(detalle);
                }
            }

            SetDiscountControlValue(_pedido.Descuento);
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
            _pedido.Total = total;
            totalGeneralValueLabel.Text = total.ToString("C2");

            var descuento = descuentoNumericUpDown.Value;
            if (descuento > total)
            {
                SetDiscountControlValue(total);
                descuento = descuentoNumericUpDown.Value;
            }

            var totalConDescuento = Math.Max(0, total - descuento);
            totalWithDiscountValueLabel.Text = totalConDescuento.ToString("C2");
        }

        private void SetDiscountControlValue(decimal value)
        {
            _isUpdatingDiscountValue = true;
            try
            {
                var boundedValue = Math.Max(descuentoNumericUpDown.Minimum, Math.Min(descuentoNumericUpDown.Maximum, value));
                descuentoNumericUpDown.Value = boundedValue;
            }
            finally
            {
                _isUpdatingDiscountValue = false;
            }
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
            notesTextBox.ReadOnly = _readOnlyMode;

            articuloComboBox.Enabled = !_readOnlyMode;
            cantidadNumericUpDown.Enabled = !_readOnlyMode;
           // precioNumericUpDown.Enabled = !_readOnlyMode;
            agregarArticuloButton.Enabled = !_readOnlyMode;
            eliminarArticuloButton.Enabled = !_readOnlyMode && _detalles.Count > 0;
            cerrarPedidoButton.Enabled = !_readOnlyMode && _pedido.Id > 0;
            cancelarPedidoButton.Enabled = !_readOnlyMode && _pedido.Id > 0;
            descuentoNumericUpDown.Enabled = !_readOnlyMode && _pedido.Id > 0 && _detalles.Count > 0;
            applyDiscountButton.Enabled = !_readOnlyMode && _pedido.Id > 0 && _detalles.Count > 0;
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
            var notas = notesTextBox.Text?.Trim();
            _pedido.Notas = string.IsNullOrWhiteSpace(notas) ? null : notas;

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

        //private void articuloComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (articuloComboBox.SelectedItem is Articulo articulo)
        //    {
        //        precioNumericUpDown.Value = Math.Max(0, Convert.ToDecimal(articulo.Precio));
        //        RefreshKitComponents(articulo);
        //    }
        //    else
        //    {
        //        RefreshKitComponents(null);
        //    }

        //    UpdateDetalleTotal();
        //}
        private void articuloComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (articuloComboBox.SelectedItem is Articulo articulo)
            {
                precioNumericUpDown.Value = Math.Max(0, Convert.ToDecimal(articulo.Precio));
                RefreshKitComponents(articulo);
            }
            else
            {
                RefreshKitComponents(null);
            }

            UpdateDetalleTotal();
            
        }


        private void DetalleValueChanged(object sender, EventArgs e)
        {
            UpdateDetalleTotal();
        }

        private void descuentoNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_isUpdatingDiscountValue)
            {
                return;
            }

            var total = _detalles.Sum(d => d.Total);
            if (descuentoNumericUpDown.Value > total)
            {
                SetDiscountControlValue(total);
            }

            UpdateTotals();
        }

        private bool ConfirmarFechaYHoraEntrega()
        {
            var fecha = fechaEntregaDateTimePicker.Value.ToString("dd/MM/yyyy");
            var hora = horaEntregaDateTimePicker.Value.ToString("HH:mm");

            using (var form = new Form())
            {
                form.Text = "Confirmar Fecha y Hora de Entrega";
                form.StartPosition = FormStartPosition.CenterParent;
                form.Size = new Size(520, 300);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;
                form.BackColor = Color.White;

                var label = new Label
                {
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    Text = $"¿Son correctas la fecha y hora de entrega?\n\n" +
                           $"📅 Fecha: {fecha}\n🕓 Hora: {hora}"
                };
                form.Controls.Add(label);

                var panel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    Height = 70,
                    FlowDirection = FlowDirection.RightToLeft,
                    Padding = new Padding(10)
                };
                var btnSi = new Button
                {
                    Text = "✔ Sí, continuar",
                    DialogResult = DialogResult.Yes,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    BackColor = Color.FromArgb(0, 120, 215),
                    ForeColor = Color.White,
                    Width = 180,
                    Height = 45
                };
                var btnNo = new Button
                {
                    Text = "✖ No, corregir",
                    DialogResult = DialogResult.No,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    BackColor = Color.IndianRed,
                    ForeColor = Color.White,
                    Width = 180,
                    Height = 45
                };

                panel.Controls.Add(btnSi);
                panel.Controls.Add(btnNo);
                form.Controls.Add(panel);

                form.AcceptButton = btnSi;
                form.CancelButton = btnNo;

                return form.ShowDialog() == DialogResult.Yes;
            }
        }

        private void agregarArticuloButton_Click(object sender, EventArgs e)
        {
            

            
            if (_readOnlyMode)
            {
                return;
            }

            var articuloSeleccionado = articuloComboBox.SelectedItem as Articulo;
            if (articuloSeleccionado == null)
            {
                MessageBox.Show("Seleccione un artículo", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cantidadNumericUpDown.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_pedido.Id == 0)
            {
                if (!ConfirmarFechaYHoraEntrega())
                {
                    //MessageBox.Show("Corrija la fecha u hora antes de continuar.",
                    //    "Corrección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }            //// 🕓 Validar fecha y hora antes de crear encabezado
            //if (_pedido.Id == 0) // solo cuando aún no se ha creado el pedido
            //{
            //    var fecha = fechaEntregaDateTimePicker.Value.ToString("dd/MM/yyyy");
            //    var hora = horaEntregaDateTimePicker.Value.ToString("HH:mm");

            //    var confirm = MessageBox.Show(
            //        $"🕓 **VERIFIQUE LA FECHA Y HORA DE ENTREGA** 🕓\n\n" +
            //        $"Fecha de entrega: {fecha}\nHora de entrega: {hora}\n\n" +
            //        $"¿Desea continuar con estos valores?",
            //        "Confirmar Fecha y Hora",
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question,
            //        MessageBoxDefaultButton.Button2);

            //    if (confirm == DialogResult.No)
            //    {
            //        MessageBox.Show("Corrija la fecha u hora antes de continuar.", "Corrección requerida",
            //            MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}

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
            }

            UpdateFolioDisplay();
            statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);

            _detalles.Add(detalle);
            _pedido.Detalles.Add(detalle);
            UpdateTotals();
            UpdateControlsState();
            RefreshKitComponents(articuloSeleccionado);
        }

        private void eliminarArticuloButton_Click(object sender, EventArgs e)
        {
            if (_readOnlyMode)
            {
                return;
            }

            if (detallesGrid.CurrentRow == null)
                return;

            var detalle = detallesGrid.CurrentRow.DataBoundItem as PedidoDetalle;
            if (detalle == null)
                return;


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

        private void applyDiscountButton_Click(object sender, EventArgs e)
        {
            
            
            if (_readOnlyMode)
            {
                return;
            }

            if (_pedido.Id == 0)
            {
                MessageBox.Show("El pedido aún no ha sido creado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_detalles.Count == 0)
            {
                MessageBox.Show("Agregue al menos un artículo antes de aplicar un descuento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var descuento = descuentoNumericUpDown.Value;
            if (descuento <= 0)
            {
                MessageBox.Show("Ingrese un descuento mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var total = _detalles.Sum(d => d.Total);
            if (descuento > total)
            {
                MessageBox.Show("El descuento no puede ser mayor que el total del pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var autorizacionForm = new DescuentoAutorizacionForm(_connectionFactory))
            {
                if (autorizacionForm.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                var administradorCorreo = autorizacionForm.AdministradorCorreo;
                if (string.IsNullOrWhiteSpace(administradorCorreo))
                {
                    MessageBox.Show("No se obtuvo la autorización del administrador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_pedidoDao.AplicarDescuento(_pedido.Id, descuento, administradorCorreo, out var message, out var folioGenerado, out var folioFormateado))
                {
                    _pedido.Descuento = descuento;
                    _pedido.UsuarioDescuento = administradorCorreo;
                    _pedido.Estatus = "N";

                    if (!string.IsNullOrEmpty(folioGenerado))
                    {
                        _pedido.Folio = folioGenerado;
                    }

                    if (!string.IsNullOrEmpty(folioFormateado))
                    {
                        _pedido.FolioFormateado = folioFormateado;
                    }

                    statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);
                    UpdateFolioDisplay();
                    SetDiscountControlValue(descuento);
                    UpdateTotals();
                    UpdateControlsState();

                    MessageBox.Show("Descuento aplicado y pedido cerrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GestionarCobroEImpresionTrasCierre();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        message = "No se pudo aplicar el descuento.";
                    }

                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Pregunta al cajero si desea imprimir el pedido considerando si se registró un cobro previamente.
        /// </summary>
        /// <param name="cobroAsociado">Cobro registrado durante el cierre del pedido, si existe.</param>
        private void PreguntarImpresionPedidoCerrado(Cobro cobroAsociado)
        {
            var mensaje = cobroAsociado != null
                ? "Se registró un cobro para este pedido. ¿Desea imprimir el pedido con el abono registrado?"
                : "Pedido cerrado correctamente. ¿Desea imprimirlo ahora?";

            var respuesta = MessageBox.Show(
                mensaje,
                "Pedido cerrado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                ProcesarImpresionPedido(false);
            }
            else
            {
                ActualizarEstadoImpresion(false);
            }
        }

        /// <summary>
        /// Invoca el formulario de cobros y posteriormente pregunta por la impresión del pedido.
        /// </summary>
        private void GestionarCobroEImpresionTrasCierre()
        {
            Cobro cobroRegistrado = null;

            if (_pedido?.Empresa != null)
            {
                var respuestaCobro = MessageBox.Show(
                    "¿Desea registrar un cobro para este pedido?",
                    "Registrar cobro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuestaCobro == DialogResult.Yes)
                {
                    using (var form = new RegisterAbonoForm(_connectionFactory, _cliente, _usuario, _pedido.Empresa, _pedido.Id))
                    {
                        if (form.ShowDialog(this) == DialogResult.OK && form.CobroRegistrado != null)
                        {
                            cobroRegistrado = form.CobroRegistrado;
                        }
                    }
                }
            }

            PreguntarImpresionPedidoCerrado(cobroRegistrado);
        }

        /// <summary>
        /// Ejecuta la impresión (o generación de PDF) del pedido actual.
        /// </summary>
        /// <param name="esReimpresion">Indica si se trata de una reimpresión para mostrar la leyenda correspondiente.</param>
        private void ProcesarImpresionPedido(bool esReimpresion)
        {
            if (_pedido?.Id <= 0)
            {
                MessageBox.Show("El pedido debe guardarse antes de poder imprimirse.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Pedido pedidoCompleto;
            try
            {
                pedidoCompleto = _pedidoDao.ObtenerPedidoCompleto(_pedido.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo obtener la información completa del pedido: {ex.Message}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pedidoCompleto == null)
            {
                MessageBox.Show("No se encontró el pedido seleccionado.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _pedido.Folio = pedidoCompleto.Folio;
            _pedido.FolioFormateado = pedidoCompleto.FolioFormateado;

            PedidoPrintingResult resultado;
            try
            {
                var mostrarLeyenda = esReimpresion || pedidoCompleto.EstaImpreso;
                resultado = _printingService.Print(pedidoCompleto, this, mostrarLeyenda);
            }
            catch (Exception ex)
            {
                ActualizarEstadoImpresion(false);
                MessageBox.Show($"No se pudo generar el archivo PDF del pedido: {ex.Message}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultado.Printed)
            {
                ActualizarEstadoImpresion(true);
                MessageBox.Show(esReimpresion ? "Pedido reimpreso correctamente." : "Pedido impreso correctamente.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (resultado.SavedPdf)
            {
                ActualizarEstadoImpresion(false);

                var mensaje = new StringBuilder();
                if (resultado.CancelledByUser)
                {
                    mensaje.AppendLine("La impresión fue cancelada por el usuario.");
                }
                else if (resultado.Error != null)
                {
                    mensaje.AppendLine("Ocurrió un problema al imprimir el pedido.");
                    mensaje.AppendLine(resultado.Error.Message);
                }

                if (!string.IsNullOrWhiteSpace(resultado.PdfPath))
                {
                    if (mensaje.Length > 0)
                    {
                        mensaje.AppendLine();
                    }

                    mensaje.AppendLine("El pedido se guardó automáticamente en formato PDF en:");
                    mensaje.AppendLine(resultado.PdfPath);
                }

                MessageBox.Show(mensaje.ToString(), "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ActualizarEstadoImpresion(bool impreso)
        {
            if (_pedido == null || _pedido.Id <= 0)
            {
                return;
            }

            try
            {
                _pedidoDao.ActualizarImpresion(_pedido.Id, impreso);
                _pedido.Impreso = impreso ? "S" : "N";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo actualizar el estado de impresión del pedido: {ex.Message}", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cerrarPedidoButton_Click(object sender, EventArgs e)
        {
            if (_pedido.Id == 0)
            {
                MessageBox.Show("Agregue al menos un artículo antes de cerrar el pedido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 Verifica si hay un importe mayor a 0 y aún no se aplicó descuento
            if (descuentoNumericUpDown.Value > 0)
            {
                var respuesta = MessageBox.Show(
                    "El pedido tiene ingresado un descuento mayor a $0.00.\n\n" +
                    "Si desea aplicar un descuento, hágalo con el botón 'Aplicar descuento'.\n\n" +
                    "¿Desea cerrar el pedido sin aplicar descuento?",
                    "Confirmar cierre sin descuento",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    // Cancela el cierre
                    return;
                }
            }


            if (_pedidoDao.ActualizarEstatus(_pedido.Id, "N", out var message, out var folioGenerado, out var folioFormateado))
            {
                _pedido.Estatus = "N";
                _pedido.Folio = folioGenerado;
                _pedido.FolioFormateado = folioFormateado;
                statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);
                UpdateFolioDisplay();
//                MessageBox.Show("Pedido cerrado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateControlsState();

                GestionarCobroEImpresionTrasCierre();

                //this.Close();
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

            if (_pedidoDao.ActualizarEstatus(_pedido.Id, "C", out var message, out _, out _))
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

        private void eventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventComboBox.SelectedItem is Evento evento && evento.Id > 0)
            {
                fechaEntregaDateTimePicker.Value = evento.FechaEvento;
            }
        }

        private void RefreshKitComponents(Articulo articulo)
        {
            kitComponentsRichTextBox.Clear();

            if (articulo == null || !articulo.EsKit)
            {
                ToggleKitComponentsVisibility(false);
                return;
            }

            try
            {
                var componentes = ObtenerComponentesKit(articulo.Id);

                if (componentes.Count == 0)
                {
                    ToggleKitComponentsVisibility(false);
                    return;
                }

                var builder = new StringBuilder();
                builder.AppendLine("Componentes del kit:");

                foreach (var componente in componentes)
                {
                    builder.Append("• ");
                    builder.AppendLine($"{componente.NombreArticulo} {componente.Cantidad.ToString("N2")} {componente.Articulo.UnidadMedida}" );
                }

                kitComponentsRichTextBox.Text = builder.ToString().TrimEnd();
                var preferredHeight = kitComponentsRichTextBox.GetPositionFromCharIndex(kitComponentsRichTextBox.TextLength).Y + kitComponentsRichTextBox.Font.Height;
                var minimumHeight = kitComponentsRichTextBox.Font.Height * 2;
                var maximumHeight = Math.Max(_kitComponentsBaseHeight, kitComponentsRichTextBox.Font.Height * 4);
                kitComponentsRichTextBox.Height = Math.Max(minimumHeight, Math.Min(preferredHeight, maximumHeight));
                ToggleKitComponentsVisibility(true);
            }
            catch (Exception ex)
            {
                ToggleKitComponentsVisibility(false);
                MessageBox.Show($"No se pudieron cargar los componentes del kit: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IList<KitDetalle> ObtenerComponentesKit(int kitId)
        {
            var componentes = new List<KitDetalle>();

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(@"SELECT ak.articulo_compuesto_id, ak.cantidad, a.nombre, a.unidad_medida
FROM banquetes.articulos_kit ak
INNER JOIN banquetes.articulos a ON a.articulo_id = ak.articulo_compuesto_id
WHERE ak.articulo_id = @kitId;", connection))
            {
                command.Parameters.AddWithValue("@kitId", kitId);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        componentes.Add(new KitDetalle
                        {
                            KitId = kitId,
                            ArticuloId = reader.GetInt32("articulo_compuesto_id"),
                            Cantidad = reader.GetDecimal("cantidad"),
                            Articulo = new Articulo
                            {
                                Id = reader.GetInt32("articulo_compuesto_id"),
                                Nombre = reader.GetString("nombre"),
                                UnidadMedida = reader.GetString("unidad_medida")
                            }
                        });
                    }
                }
            }

            return componentes;
        }

        private void ToggleKitComponentsVisibility(bool visible)
        {
            //kitComponentsLabel.Visible = visible;
            kitComponentsRichTextBox.Visible = visible;

            if (!visible)
            {
                kitComponentsRichTextBox.Clear();
                kitComponentsRichTextBox.Height = _kitComponentsBaseHeight;
            }

            if (visible)
            {
                var detallesGridTopWithKit = kitComponentsRichTextBox.Bottom + 12;
                detallesGrid.Top = detallesGridTopWithKit;
                var newHeight = _detallesGridHeightBase - (detallesGridTopWithKit - _detallesGridTopBase);
                detallesGrid.Height = Math.Max(100, newHeight);
            }
            else
            {
                detallesGrid.Top = _detallesGridTopBase;
                detallesGrid.Height = _detallesGridHeightBase;
            }
        }

        private void UpdateFolioDisplay()
        {
            folioTextBox.Text = ObtenerFolioParaMostrar(_pedido);
        }

        private static string ObtenerFolioParaMostrar(Pedido pedido)
        {
            if (pedido == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(pedido.FolioFormateado))
            {
                return pedido.FolioFormateado;
            }

            if (int.TryParse(pedido.Folio, out var folioNumerico))
            {
                var serie = pedido.Evento != null && pedido.Evento.TieneSerie ? pedido.Evento.Serie : string.Empty;
                return FormatearFolioParaMostrar(serie, folioNumerico);
            }

            return pedido.Folio ?? string.Empty;
        }

        private static string FormatearFolioParaMostrar(string serie, int folio)
        {
            var consecutivo = folio.ToString("D5");
            return string.IsNullOrEmpty(serie) ? consecutivo : string.Concat(serie, consecutivo);
        }

        private void OrderManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.TaskManagerClosing)
            {
                return;
            }

            if (_pedido == null || _pedido.Id <= 0 || !string.Equals(_pedido.Estatus, "P", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var result = MessageBox.Show("El pedido está pendiente, ¿desea cancelarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            if (!_pedidoDao.ActualizarEstatus(_pedido.Id, "C", out var message, out _, out _))
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            _pedido.Estatus = "C";
            statusTextBox.Text = ObtenerDescripcionEstatus(_pedido.Estatus);
        }

        private void OrderManagementForm_Load(object sender, EventArgs e)
        {
            

        }

        private void noStyle()
        {
            groupBox2.Tag = "no_style";
            //discountNoteLabel.Tag = "no_style";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_readOnlyMode)
            {
                return;
            }

            if (detallesGrid.CurrentRow == null)
                return;

            var detalle = detallesGrid.CurrentRow.DataBoundItem as PedidoDetalle;
            if (detalle == null)
                return;


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

        private void descuentoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (descuentoComboBox.SelectedItem == null)
                return;

            // Obtiene el valor seleccionado (por ejemplo "10") y lo convierte a decimal
            if (decimal.TryParse(descuentoComboBox.SelectedItem.ToString(), out var porcentaje))
            {
                var total = _detalles.Sum(d => d.Total);
                var descuento = total * (porcentaje / 100);

                // Evita que se dispare el evento ValueChanged mientras actualizamos manualmente
                _isUpdatingDiscountValue = true;
                descuentoNumericUpDown.Value = Math.Min(descuento, descuentoNumericUpDown.Maximum);
                _isUpdatingDiscountValue = false;

                UpdateTotals();
            }
        }
    }
}
