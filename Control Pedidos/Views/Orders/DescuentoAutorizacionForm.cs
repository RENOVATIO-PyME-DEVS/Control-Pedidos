using System;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.Orders
{
    public partial class DescuentoAutorizacionForm : Form
    {
        private readonly UsuarioDao _usuarioDao;

        public string AdministradorCorreo { get; private set; } = string.Empty;

        public DescuentoAutorizacionForm(DatabaseConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _usuarioDao = new UsuarioDao(connectionFactory);

            InitializeComponent();
            UIStyles.ApplyTheme(this);
        }

        private void autorizarButton_Click(object sender, EventArgs e)
        {
            var correo = correoTextBox.Text.Trim();
            var password = passwordTextBox.Text;

            if (!_usuarioDao.TryAutorizarDescuento(correo, password, out var usuario, out var message))
            {
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message, "Autorización", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                passwordTextBox.SelectAll();
                passwordTextBox.Focus();
                return;
            }

            AdministradorCorreo = usuario.Correo;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
