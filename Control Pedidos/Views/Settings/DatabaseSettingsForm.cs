using System;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;

namespace Control_Pedidos.Views.Settings
{
    public partial class DatabaseSettingsForm : Form
    {
        public DatabaseConfig Configuration { get; private set; }

        public DatabaseSettingsForm(DatabaseConfig existingConfig = null)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            if (existingConfig != null)
            {
                hostTextBox.Text = existingConfig.Host;
                userTextBox.Text = existingConfig.Username;
                passwordTextBox.Text = existingConfig.Password;
                databaseTextBox.Text = existingConfig.Database;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hostTextBox.Text) || string.IsNullOrWhiteSpace(userTextBox.Text) || string.IsNullOrWhiteSpace(databaseTextBox.Text))
            {
                MessageBox.Show("Host, usuario y base de datos son obligatorios", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Configuration = new DatabaseConfig
            {
                Host = hostTextBox.Text,
                Username = userTextBox.Text,
                Password = passwordTextBox.Text,
                Database = databaseTextBox.Text
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
