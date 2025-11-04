using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Control_Pedidos.Views.Users
{
    public partial class UserManagementForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly BindingList<Usuario> _usuarios = new BindingList<Usuario>();
        private readonly BindingSource _bindingSource = new BindingSource();
        private IList<Rol> _roles = new List<Rol>();
        private readonly List<Rol> _selectedRoles = new List<Rol>();
        private Usuario _selectedUser;

        public UserManagementForm(DatabaseConnectionFactory connectionFactory)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            statusComboBox.DataSource = new[] { "Activo", "Inactivo" };
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
                DataPropertyName = nameof(Usuario.Estatus),
                HeaderText = "Estatus",
                Width = 90
            });

            usersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Usuario.RolesResumen),
                HeaderText = "Roles",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

        private void addRoleButton_Click(object sender, EventArgs e)
        {
            if (roleComboBox.SelectedItem is Rol rol)
            {
                if (_selectedRoles.Any(r => r.Id == rol.Id))
                {
                    return;
                }

                _selectedRoles.Clear();
                _selectedRoles.Add(rol);
                RefreshSelectedRoles();
            }
        }

        private void RefreshSelectedRoles()
        {
            rolesListBox.DataSource = null;
            rolesListBox.DataSource = _selectedRoles.Select(r => r.Nombre).ToList();
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
                Estatus = statusComboBox.SelectedItem.ToString() == "Activo" ? "N" : "N",
                Roles = _selectedRoles.ToList(),
                RolUsuarioId = _selectedRoles.FirstOrDefault()?.Id
            };

            if (Usuario.Agregar(_connectionFactory, usuario, usuario.Roles.Select(r => r.Id), out var message))
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
            _selectedUser.Estatus = statusComboBox.SelectedItem?.ToString() ?? "Activo";
            _selectedUser.Roles = _selectedRoles.ToList();
            _selectedUser.RolUsuarioId = _selectedRoles.FirstOrDefault()?.Id;

            if (!string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                _selectedUser.PasswordHash = PasswordHelper.HashPassword(passwordTextBox.Text.Trim());
            }

            if (Usuario.Actualizar(_connectionFactory, _selectedUser, _selectedRoles.Select(r => r.Id), out var message))
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
                statusComboBox.SelectedItem = (usuario.Estatus == "N") ? "Activo" : "Inactivo";
                passwordTextBox.Text = string.Empty;

                _selectedRoles.Clear();
                if (usuario.Roles != null && usuario.Roles.Any())
                {
                    _selectedRoles.Add(usuario.Roles.First());
                }

                RefreshSelectedRoles();
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
            statusComboBox.SelectedIndex = 0;
            _selectedRoles.Clear();
            RefreshSelectedRoles();
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

            if (_selectedRoles.Count == 0)
            {
                MessageBox.Show("Seleccione un rol", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_selectedRoles.Count > 1)
            {
                MessageBox.Show("Solo se permite seleccionar un rol", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            LoadUsuarios(searchTextBox.Text.Trim());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (passwordTextBox.PasswordChar == '*')
                    passwordTextBox.PasswordChar = '\0';
            }
            else
            {
                passwordTextBox.PasswordChar = '*';
            }
        }
    }
}
