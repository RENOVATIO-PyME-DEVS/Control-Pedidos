using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Control_Pedidos.Controllers;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views
{
    public partial class LoginForm : Form
    {
        private readonly AuthController _authController;

        public string AuthenticatedRole { get; private set; }
        public string AuthenticatedUserId { get; private set; } //id usuario
        public string AuthenticatedUser { get; private set; } //nombre usuario
        public string AuthenticatedUserCorreo { get; private set; } //nombre usuario
        public int SelectedEmpresaId { get; private set; }
        public string SelectedEmpresaNombre { get; private set; } = string.Empty;

        public LoginForm(AuthController authController, string lastUsername)
        {
            _authController = authController;
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            if (!string.IsNullOrEmpty(lastUsername))
            {
                usernameTextBox.Text = lastUsername;
                usernameTextBox.SelectionStart = usernameTextBox.Text.Length;
            }
        }

        public LoginForm(AuthController authController)
            : this(authController, string.Empty)
        {
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_authController.TryLogin(usernameTextBox.Text, passwordTextBox.Text, out var role, out var user, out var userId))
                {
                    var empresas = _authController.GetEmpresasPorUsuario(userId);

                    if (empresas.Count == 0)
                    {
                        messageLabel.Text = "El usuario no tiene empresas asignadas.";
                        return;
                    }

                    var empresaSeleccionada = SeleccionarEmpresa(empresas);
                    if (empresaSeleccionada == null)
                    {
                        messageLabel.Text = "Debe seleccionar una empresa para continuar.";
                        return;
                    }

                    SelectedEmpresaId = empresaSeleccionada.Id;
                    SelectedEmpresaNombre = empresaSeleccionada.Nombre;
                    AuthenticatedRole = role;
                    AuthenticatedUser = user;
                    AuthenticatedUserId = userId;
                    AuthenticatedUserCorreo = usernameTextBox.Text;
                    messageLabel.Text = string.Empty;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    messageLabel.Text = "Usuario o contraseña incorrectos";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Empresa SeleccionarEmpresa(IList<Empresa> empresas)
        {
            if (empresas.Count == 1)
            {
                return empresas[0];
            }

            using (var seleccionForm = new SeleccionEmpresaForm(empresas))
            {
                return seleccionForm.ShowDialog() == DialogResult.OK
                    ? seleccionForm.EmpresaSeleccionada
                    : null;
            }
        }
    }
}
