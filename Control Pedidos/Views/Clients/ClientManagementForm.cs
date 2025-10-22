using System;
using System.ComponentModel;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.Clients
{
    public partial class ClientManagementForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly BindingList<Cliente> _clientes = new BindingList<Cliente>();
        private readonly BindingSource _bindingSource = new BindingSource();
        private Cliente _selectedClient;

        public ClientManagementForm(DatabaseConnectionFactory connectionFactory)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            statusComboBox.DataSource = new[] { "Activo", "Inactivo" };
            ConfigureGrid();
            LoadClientes();
        }

        private void ConfigureGrid()
        {
            _bindingSource.DataSource = _clientes;
            clientsGrid.AutoGenerateColumns = false;
            clientsGrid.Columns.Clear();

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Id),
                HeaderText = "ID",
                Width = 60
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.RazonSocial),
                HeaderText = "Razón social",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.NombreComercial),
                HeaderText = "Nombre comercial",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Rfc),
                HeaderText = "RFC",
                Width = 120
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Telefono),
                HeaderText = "Teléfono",
                Width = 120
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.Estatus),
                HeaderText = "Estatus",
                Width = 90
            });

            clientsGrid.DataSource = _bindingSource;
        }

        private void LoadClientes(string filtro = "")
        {
            try
            {
                _clientes.Clear();
                foreach (var cliente in Cliente.Listar(_connectionFactory, filtro))
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
                RazonSocial = razonSocialTextBox.Text.Trim(),
                NombreComercial = nombreComercialTextBox.Text.Trim(),
                Rfc = rfcTextBox.Text.Trim(),
                Telefono = telefonoTextBox.Text.Trim(),
                Correo = correoTextBox.Text.Trim(),
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

            _selectedClient.RazonSocial = razonSocialTextBox.Text.Trim();
            _selectedClient.NombreComercial = nombreComercialTextBox.Text.Trim();
            _selectedClient.Rfc = rfcTextBox.Text.Trim();
            _selectedClient.Telefono = telefonoTextBox.Text.Trim();
            _selectedClient.Correo = correoTextBox.Text.Trim();
            // _selectedClient.Direccion = direccionTextBox.Text.Trim();
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
                razonSocialTextBox.Text = cliente.RazonSocial;
                nombreComercialTextBox.Text = cliente.NombreComercial;
                rfcTextBox.Text = cliente.Rfc;
                telefonoTextBox.Text = cliente.Telefono;
                correoTextBox.Text = cliente.Correo;
               // direccionTextBox.Text = cliente.Direccion;
                statusComboBox.SelectedItem = cliente.Estatus;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            razonSocialTextBox.Clear();
            nombreComercialTextBox.Clear();
            rfcTextBox.Clear();
            telefonoTextBox.Clear();
            correoTextBox.Clear();
           // direccionTextBox.Clear();
            statusComboBox.SelectedIndex = 0;
            clientsGrid.ClearSelection();
            _selectedClient = null;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(razonSocialTextBox.Text))
            {
                MessageBox.Show("Ingrese la razón social", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!ValidationHelper.IsRfc(rfcTextBox.Text.Trim()))
            {
                MessageBox.Show("Ingrese un RFC válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
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
    }
}
