using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Views.Orders;
using Control_Pedidos.Views.Payments;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_Pedidos.Views.Clients
{
    public partial class ClientManagementForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly Usuario _usuarioActual;
        private readonly Empresa _empresaSeleccionada;
        private readonly ContextMenuStrip _clientsContextMenu;
        private readonly BindingList<Cliente> _clientes = new BindingList<Cliente>();
        private readonly BindingSource _bindingSource = new BindingSource();
        private Cliente _selectedClient;
        private bool _handlingFacturaCheck;
        private bool _suppressRegimenReload;

        private class RegimenFiscalOption
        {
            public int Id { get; set; }
            public string Descripcion { get; set; } = string.Empty;
        }

        private enum RegimenFiscalTipo
        {
            PersonaFisica,
            PersonaMoral
        }

        public ClientManagementForm(DatabaseConnectionFactory connectionFactory, Usuario usuarioActual, Empresa empresaSeleccionada)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _usuarioActual = usuarioActual ?? throw new ArgumentNullException(nameof(usuarioActual));
            _empresaSeleccionada = empresaSeleccionada ?? throw new ArgumentNullException(nameof(empresaSeleccionada));

            //_clientsContextMenu = new ContextMenuStrip(components);
            _clientsContextMenu = new ContextMenuStrip();
            _clientsContextMenu.Items.Add("Agregar pedido", null, agregarPedidoMenuItem_Click);
            _clientsContextMenu.Items.Add("Registrar cobro", null, registrarCobroMenuItem_Click);
            _clientsContextMenu.Items.Add("Ver cobros", null, verCobrosMenuItem_Click);
            _clientsContextMenu.Items.Add("Devolver pedido", null, devolverPedidoMenuItem_Click);
            clientsGrid.ContextMenuStrip = _clientsContextMenu;

            statusComboBox.DataSource = new[] { "Activo", "Inactivo" };
            ConfigureGrid();
            LoadClientes();
            UpdateFacturaFieldsState();
        }

        private void ConfigureGrid()
        {
            _bindingSource.DataSource = _clientes;
            clientsGrid.AutoGenerateColumns = false;
            clientsGrid.Columns.Clear();

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Id),
                HeaderText = "Folio",
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Width = 80
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.NombreComercial),
                HeaderText = "Nombre",
               // AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Width = 250
            });


            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Rfc),
                HeaderText = "RFC",
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Width = 140
            });
            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Correo),
                HeaderText = "Correo",
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Width = 185
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Telefono),
                HeaderText = "Teléfono",
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Width = 130
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Estatus),
                HeaderText = "Estatus",
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Width = 120
            });
            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.RequiereFacturaStr),
                HeaderText = "Requiere Factura",
                //AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Width = 180
            });

            clientsGrid.DataSource = _bindingSource;
            clientsGrid.CellMouseDown += clientsGrid_CellMouseDown;
        }

        //private void LoadClientes(string filtro = "")
        //{
        //    try
        //    {
        //        _clientes.Clear();
        //        foreach (var cliente in Cliente.Listar(_connectionFactory, filtro))
        //        {
        //            _clientes.Add(cliente);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"No se pudo obtener la lista de clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private async void LoadClientes(string filtro = "")
        {
            try
            {
                var clientes = await Task.Run(() => Cliente.Listar(_connectionFactory, filtro));

                _clientes.Clear();
                foreach (var cliente in clientes)
                {
                    _clientes.Add(cliente);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo obtener la lista de clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            var cliente = new Cliente
            {
               // RazonSocial = razonSocialTextBox.Text.Trim(),
                NombreComercial = nombreComercialTextBox.Text.Trim(),
                Rfc = chkRequiereFactura.Checked ? rfcTextBox.Text.Trim() : string.Empty,
                Telefono = telefonoTextBox.Text.Trim(),
                Correo = correoTextBox.Text.Trim(),
                Direccion = direccionTextBox.Text.Trim(),
                CodigoPostal = chkRequiereFactura.Checked ? codigoPostalTextBox.Text.Trim() : string.Empty,
                RequiereFactura = chkRequiereFactura.Checked,
                RegimenFiscalId = chkRequiereFactura.Checked ? GetSelectedRegimenFiscalId() : null,
               // Direccion = direccionTextBox.Text.Trim(),
                Estatus = statusComboBox.SelectedItem?.ToString() ?? "Activo"
            };

            if (Cliente.Agregar(_connectionFactory, cliente, out var message))
            {
                MessageBox.Show("Cliente agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadClientes();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Seleccione un cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm())
            {
                return;
            }

            //_selectedClient.RazonSocial = razonSocialTextBox.Text.Trim();
            _selectedClient.NombreComercial = nombreComercialTextBox.Text.Trim();
            _selectedClient.Rfc = chkRequiereFactura.Checked ? rfcTextBox.Text.Trim() : string.Empty;
            _selectedClient.Telefono = telefonoTextBox.Text.Trim();
            _selectedClient.Correo = correoTextBox.Text.Trim();
            _selectedClient.CodigoPostal = chkRequiereFactura.Checked ? codigoPostalTextBox.Text.Trim() : string.Empty;
            _selectedClient.RequiereFactura = chkRequiereFactura.Checked;
            _selectedClient.RegimenFiscalId = chkRequiereFactura.Checked ? GetSelectedRegimenFiscalId() : null;
            _selectedClient.Direccion = direccionTextBox.Text.Trim();
            _selectedClient.Estatus = statusComboBox.SelectedItem?.ToString() ?? "Activo";

            if (Cliente.Actualizar(_connectionFactory, _selectedClient, out var message))
            {
                MessageBox.Show("Cliente actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadClientes(searchTextBox.Text.Trim());
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Seleccione un cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea inactivar este cliente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Cliente.Eliminar(_connectionFactory, _selectedClient.Id, out var message))
                {
                    MessageBox.Show("Cliente inactivado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadClientes(searchTextBox.Text.Trim());
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void clientsGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (clientsGrid.CurrentRow?.DataBoundItem is Cliente cliente)
            {
                _selectedClient = cliente;
               // razonSocialTextBox.Text = cliente.RazonSocial;
                nombreComercialTextBox.Text = cliente.NombreComercial;
                _handlingFacturaCheck = true;
                chkRequiereFactura.Checked = cliente.RequiereFactura;
                UpdateFacturaFieldsState();
                _handlingFacturaCheck = false;

                codigoPostalTextBox.Text = cliente.CodigoPostal ?? string.Empty;

                _suppressRegimenReload = true;
                rfcTextBox.Text = cliente.Rfc ?? string.Empty;
                _suppressRegimenReload = false;

                telefonoTextBox.Text = cliente.Telefono;
                correoTextBox.Text = cliente.Correo;
                direccionTextBox.Text = cliente.Direccion;
                statusComboBox.SelectedItem = cliente.Estatus;
                 
                if (cliente.RequiereFactura)
                {
                    LoadRegimenFiscalOptionsByRfc(cliente.Rfc, cliente.RegimenFiscalId);
                }
                else
                {
                    ClearRegimenFiscalDataSource();
                }
            }
        }

        private void chkRequiereFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (_handlingFacturaCheck)
            {
                return;
            }

            UpdateFacturaFieldsState();

            if (chkRequiereFactura.Checked)
            {
                LoadRegimenFiscalOptionsByRfc(rfcTextBox.Text.Trim(), _selectedClient?.RegimenFiscalId);
            }
        }

        private void rfcTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressRegimenReload || !chkRequiereFactura.Checked)
            {
                return;
            }

            LoadRegimenFiscalOptionsByRfc(rfcTextBox.Text.Trim());
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            //razonSocialTextBox.Clear();
            nombreComercialTextBox.Clear();
            _suppressRegimenReload = true;
            rfcTextBox.Clear();
            _suppressRegimenReload = false;
            telefonoTextBox.Clear();
            correoTextBox.Clear();
            codigoPostalTextBox.Clear();
             direccionTextBox.Clear();
            statusComboBox.SelectedIndex = 0;
            _handlingFacturaCheck = true;
            chkRequiereFactura.Checked = false;
            UpdateFacturaFieldsState();
            _handlingFacturaCheck = false;
            ClearRegimenFiscalDataSource();
            clientsGrid.ClearSelection();
            _selectedClient = null;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(nombreComercialTextBox.Text))
            {
                MessageBox.Show("Ingrese El nombre del cliente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (chkRequiereFactura.Checked)
            {
                if (!ValidationHelper.IsRfc(rfcTextBox.Text.Trim()))
                {
                    MessageBox.Show("Ingrese un RFC válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(codigoPostalTextBox.Text.Trim()))
                {
                    MessageBox.Show("Ingrese el código postal", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (cmbRegimenFiscal.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un régimen fiscal", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(correoTextBox.Text) && !ValidationHelper.IsEmail(correoTextBox.Text.Trim()))
            {
                MessageBox.Show("Ingrese un correo válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            LoadClientes(searchTextBox.Text.Trim());
        }

        private void clientsGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                clientsGrid.ClearSelection();
                clientsGrid.Rows[e.RowIndex].Selected = true;
                clientsGrid.CurrentCell = clientsGrid.Rows[e.RowIndex].Cells[0];
                if (clientsGrid.Rows[e.RowIndex].DataBoundItem is Cliente cliente)
                {
                    _selectedClient = cliente;
                }
            }
        }

        private void devolverPedidoMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Seleccione un cliente", "Devolver pedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new DevolucionPedidoForm(_connectionFactory, _selectedClient, _usuarioActual, _empresaSeleccionada))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadClientes(searchTextBox.Text.Trim());
                }
            }
        }

        private void agregarPedidoMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Seleccione un cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new OrderManagementForm(_connectionFactory, _selectedClient, _usuarioActual, _empresaSeleccionada))
            {
                form.ShowDialog(this);
            }
        }

        private void registrarCobroMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Seleccione un cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new RegisterAbonoForm(_connectionFactory, _selectedClient, _usuarioActual, _empresaSeleccionada))
            {
                form.ShowDialog(this);
            }
        }

        private void verCobrosMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedClient == null)
            {
                MessageBox.Show("Seleccione un cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new CobrosClienteForm(_connectionFactory, _selectedClient))
            {
                form.ShowDialog(this);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rfcTextBox.Text = "XAXA010101000";
        }

        private void LoadRegimenFiscalOptionsByRfc(string rfc, int? regimenFiscalId = null)
        {
            if (string.IsNullOrWhiteSpace(rfc))
            {
                ClearRegimenFiscalDataSource();
                return;
            }

            RegimenFiscalTipo? tipo = null;

            if (rfc.Length == 13)
            {
                tipo = RegimenFiscalTipo.PersonaFisica;
            }
            else if (rfc.Length == 12)
            {
                tipo = RegimenFiscalTipo.PersonaMoral;
            }
            else
            {
                ClearRegimenFiscalDataSource();
                return;
            }

            try
            {
                var options = FetchRegimenFiscalOptions(tipo.Value);
                cmbRegimenFiscal.DisplayMember = nameof(RegimenFiscalOption.Descripcion);
                cmbRegimenFiscal.ValueMember = nameof(RegimenFiscalOption.Id);
                cmbRegimenFiscal.DataSource = options;

                var hasOptions = options.Count > 0;
                var selected = false;

                if (regimenFiscalId.HasValue)
                {
                    foreach (var option in options)
                    {
                        if (option.Id == regimenFiscalId.Value)
                        {
                            cmbRegimenFiscal.SelectedValue = regimenFiscalId.Value;
                            selected = true;
                            break;
                        }
                    }
                }

                if (!selected)
                {
                    cmbRegimenFiscal.SelectedIndex = hasOptions ? 0 : -1;
                }
            }
            catch (Exception ex)
            {
                ClearRegimenFiscalDataSource();
                MessageBox.Show($"No se pudo cargar el catálogo de régimen fiscal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private BindingList<RegimenFiscalOption> FetchRegimenFiscalOptions(RegimenFiscalTipo tipo)
        {
            var options = new BindingList<RegimenFiscalOption>();
            var query = tipo == RegimenFiscalTipo.PersonaFisica
                ? @"SELECT rg.c_regimenfiscal_id, CONCAT(rg.clave_fiscal, ' - ', rg.descripcion) AS descripcion
FROM banquetes.c_regimenfiscal rg
WHERE rg.fisica = 'S';"
                : @"SELECT rg.c_regimenfiscal_id, CONCAT(rg.clave_fiscal, ' - ', rg.descripcion) AS descripcion
FROM banquetes.c_regimenfiscal rg
WHERE rg.moral = 'S';";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        options.Add(new RegimenFiscalOption
                        {
                            Id = reader.GetInt32("c_regimenfiscal_id"),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString("descripcion")
                        });
                    }
                }
            }

            return options;
        }

        private void ClearRegimenFiscalDataSource()
        {
            cmbRegimenFiscal.DataSource = null;
            cmbRegimenFiscal.Items.Clear();
            cmbRegimenFiscal.SelectedIndex = -1;
        }

        private void UpdateFacturaFieldsState()
        {
            var enabled = chkRequiereFactura.Checked;

            codigoPostalTextBox.Enabled = enabled;
            rfcTextBox.Enabled = enabled;
            cmbRegimenFiscal.Enabled = enabled;

            if (!enabled)
            {
                ClearRegimenFiscalDataSource();
            }
        }

        private int? GetSelectedRegimenFiscalId()
        {
            if (cmbRegimenFiscal.SelectedValue is int id)
            {
                return id;
            }

            if (cmbRegimenFiscal.SelectedValue is long longId)
            {
                return (int)longId;
            }

            return null;
        }

        private void ClientManagementForm_Load(object sender, EventArgs e)
        {
            ClearForm();
            ClearForm();
        }
    }
}
