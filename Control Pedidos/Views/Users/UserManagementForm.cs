using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.Users
{
    public partial class UserManagementForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly BindingList<Usuario> _usuarios = new BindingList<Usuario>();
        private readonly BindingSource _bindingSource = new BindingSource();
        private IList<Rol> _roles = new List<Rol>();
        private Usuario _selectedUser;

        public UserManagementForm(DatabaseConnectionFactory connectionFactory)
        {
            InitializeComponent();
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            statusComboBox.DataSource = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("N", "Normal"),
                new KeyValuePair<string, string>("B", "Baja")
            };
            statusComboBox.DisplayMember = "Value";
            statusComboBox.ValueMember = "Key";
            statusComboBox.SelectedValue = "N";
            ConfigureGrid();
            LoadRoles();
            LoadUsuarios();
        }

        private void ConfigureGrid()
        {
            _bindingSource.DataSource = _usuarios;
            usersGrid.AutoGenerateColumns = false;
            usersGrid.Columns.Clear();

            usersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Usuario.Id),
                HeaderText = "ID",
                Width = 60
            });

            usersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Usuario.Nombre),
                HeaderText = "Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            usersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Usuario.Correo),
                HeaderText = "Correo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            usersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Usuario.EstatusDescripcion),
                HeaderText = "Estatus",
                Width = 120
            });

            usersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Usuario.RolNombre),
                HeaderText = "Rol",
                Width = 160
            });

            usersGrid.DataSource = _bindingSource;
        }

        private void LoadRoles()
        {
            try
            {
                _roles = Rol.Listar(_connectionFactory);
                roleComboBox.DataSource = _roles;
                roleComboBox.DisplayMember = nameof(Rol.Nombre);
                roleComboBox.ValueMember = nameof(Rol.Id);
                roleComboBox.SelectedIndex = _roles.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar la lista de roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsuarios(string filtro = "")
        {
            try
            {
                _usuarios.Clear();
                foreach (var usuario in Usuario.Listar(_connectionFactory, filtro))
                {
                    _usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar la lista de usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(requirePassword: true))
            {
                return;
            }

            var usuario = new Usuario
            {
                Nombre = nameTextBox.Text.Trim(),
                Correo = emailTextBox.Text.Trim(),
                PasswordHash = PasswordHelper.HashPassword(passwordTextBox.Text.Trim()),
                Estatus = statusComboBox.SelectedValue?.ToString() ?? "N",
                RolUsuarioId = roleComboBox.SelectedValue is int rolId ? rolId : (int?)null
            };

            if (Usuario.Agregar(_connectionFactory, usuario, out var message))
            {
                MessageBox.Show("Usuario agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadUsuarios();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Seleccione un usuario para actualizar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm(requirePassword: false))
            {
                return;
            }

            _selectedUser.Nombre = nameTextBox.Text.Trim();
            _selectedUser.Correo = emailTextBox.Text.Trim();
            _selectedUser.Estatus = statusComboBox.SelectedValue?.ToString() ?? "N";
            _selectedUser.RolUsuarioId = roleComboBox.SelectedValue is int rolId ? rolId : (int?)null;

            if (!string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                _selectedUser.PasswordHash = PasswordHelper.HashPassword(passwordTextBox.Text.Trim());
            }

            if (Usuario.Actualizar(_connectionFactory, _selectedUser, out var message))
            {
                MessageBox.Show("Usuario actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadUsuarios(searchTextBox.Text.Trim());
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea inactivar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Usuario.Eliminar(_connectionFactory, _selectedUser.Id, out var message))
                {
                    MessageBox.Show("Usuario inactivado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadUsuarios(searchTextBox.Text.Trim());
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void usersGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (usersGrid.CurrentRow?.DataBoundItem is Usuario usuario)
            {
                _selectedUser = usuario;
                nameTextBox.Text = usuario.Nombre;
                emailTextBox.Text = usuario.Correo;
                statusComboBox.SelectedValue = usuario.Estatus;
                if (usuario.RolUsuarioId.HasValue)
                {
                    roleComboBox.SelectedValue = usuario.RolUsuarioId.Value;
                }
                else
                {
                    roleComboBox.SelectedIndex = -1;
                }
                passwordTextBox.Text = string.Empty;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            nameTextBox.Clear();
            emailTextBox.Clear();
            passwordTextBox.Clear();
            statusComboBox.SelectedValue = "N";
            roleComboBox.SelectedIndex = roleComboBox.Items.Count > 0 ? 0 : -1;
            usersGrid.ClearSelection();
            _selectedUser = null;
        }

        private bool ValidateForm(bool requirePassword)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Ingrese el nombre del usuario", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!ValidationHelper.IsEmail(emailTextBox.Text.Trim()))
            {
                MessageBox.Show("Ingrese un correo electrónico válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (requirePassword && string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Ingrese una contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (roleComboBox.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un rol", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            LoadUsuarios(searchTextBox.Text.Trim());
        }
    }
}
