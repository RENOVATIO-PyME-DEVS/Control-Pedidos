using System;
using System.Windows.Forms;
using Control_Pedidos.Controllers;
using Control_Pedidos.Data;
using Control_Pedidos.Views;
using Control_Pedidos.Views.Settings;

namespace Control_Pedidos
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var config = DatabaseConfig.Load();
            if (config == null || !config.IsValid())
            {
                config = PromptForConfiguration(config);
                if (config == null)
                {
                    return;
                }
            }

            var connectionFactory = new DatabaseConnectionFactory(config);
            while (!connectionFactory.TestConnection(out var message))
            {
                MessageBox.Show($"No se pudo conectar a MySQL: {message}", "Configuración requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                config = PromptForConfiguration(config);
                if (config == null)
                {
                    return;
                }

                connectionFactory = new DatabaseConnectionFactory(config);
            }

            var authController = new AuthController(connectionFactory);
            var userPreferences = UserPreferences.Load();
            using (var loginForm = new LoginForm(authController, userPreferences.LastUsername))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    userPreferences.LastUsername = loginForm.AuthenticatedUser;
                    userPreferences.Save();
                    config.Save();
                    Application.Run(new DashboardForm(loginForm.AuthenticatedUser, loginForm.AuthenticatedRole, connectionFactory));
                }
            }
        }

        private static DatabaseConfig PromptForConfiguration(DatabaseConfig existing)
        {
            using (var settingsForm = new DatabaseSettingsForm(existing))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    var newConfig = settingsForm.Configuration;
                    newConfig.Save();
                    return newConfig;
                }
            }

            return null;
        }
    }
}
