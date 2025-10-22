using System;
using System.Collections.Generic;
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
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            var estados = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("N", "Normal"),
                new KeyValuePair<string, string>("P", "Pendiente"),
                new KeyValuePair<string, string>("B", "Baja")
            };

            statusComboBox.DataSource = estados;
            statusComboBox.DisplayMember = "Value";
            statusComboBox.ValueMember = "Key";
            statusComboBox.SelectedValue = "N";
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
                DataPropertyName = nameof(Cliente.Nombre),
                HeaderText = "Nombre",
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
                DataPropertyName = nameof(Cliente.Correo),
                HeaderText = "Correo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            clientsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cliente.EstatusDescripcion),
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
                Nombre = nombreTextBox.Text.Trim(),
                Rfc = rfcTextBox.Text.Trim(),
                Telefono = telefonoTextBox.Text.Trim(),
                Correo = correoTextBox.Text.Trim(),
                Estatus = statusComboBox.SelectedValue?.ToString() ?? "N"
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

            _selectedClient.Nombre = nombreTextBox.Text.Trim();
            _selectedClient.Rfc = rfcTextBox.Text.Trim();
            _selectedClient.Telefono = telefonoTextBox.Text.Trim();
            _selectedClient.Correo = correoTextBox.Text.Trim();
            _selectedClient.Estatus = statusComboBox.SelectedValue?.ToString() ?? "N";

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
                nombreTextBox.Text = cliente.Nombre;
                rfcTextBox.Text = cliente.Rfc;
                telefonoTextBox.Text = cliente.Telefono;
                correoTextBox.Text = cliente.Correo;
                var estatus = string.IsNullOrWhiteSpace(cliente.Estatus) ? "N" : cliente.Estatus;
                if (statusComboBox.SelectedValue?.ToString() != estatus)
                {
                    statusComboBox.SelectedValue = estatus;
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            nombreTextBox.Clear();
            rfcTextBox.Clear();
            telefonoTextBox.Clear();
            correoTextBox.Clear();
            statusComboBox.SelectedValue = "N";
            clientsGrid.ClearSelection();
            _selectedClient = null;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(nombreTextBox.Text))
            {
                MessageBox.Show("Ingrese el nombre", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
