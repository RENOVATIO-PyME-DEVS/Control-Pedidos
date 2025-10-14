using System;
using System.Windows.Forms;
using Control_Pedidos.Controllers;

namespace Control_Pedidos.Views
{
    public partial class LoginForm : Form
    {
        private readonly AuthController _authController;

        public string AuthenticatedRole { get; private set; }
        public string AuthenticatedUser { get; private set; }

        public LoginForm(AuthController authController, string lastUsername)
        {
            _authController = authController;
            InitializeComponent();
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
                if (_authController.TryLogin(usernameTextBox.Text, passwordTextBox.Text, out var role))
                {
                    AuthenticatedRole = role;
                    AuthenticatedUser = usernameTextBox.Text;
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
    }
}
