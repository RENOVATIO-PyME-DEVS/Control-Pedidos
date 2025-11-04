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
            // Configuramos el look and feel estándar de WinForms para que todo se vea como debe.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Levantamos la configuración de la base de datos guardada en disco, si es que existe.
            var config = DatabaseConfig.Load();
            if (config == null || !config.IsValid())
            {
                // Si no tenemos datos o están incompletos, le pedimos al usuario que los ingrese antes de seguir.
                config = PromptForConfiguration(config);
                if (config == null)
                {
                    // Si el usuario cancela la configuración simplemente salimos sin abrir la app.
                    return;
                }
            }

            // Preparamos una fábrica de conexiones con la configuración que tenemos hasta ahora.
            var connectionFactory = new DatabaseConnectionFactory(config);
            while (!connectionFactory.TestConnection(out var message))
            {
                // Si no podemos conectar, avisamos y volvemos a pedir los datos hasta que todo esté en orden o cancelen.
                MessageBox.Show($"No se pudo conectar a MySQL: {message}", "Configuración requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                config = PromptForConfiguration(config);
                if (config == null)
                {
                    return;
                }

                connectionFactory = new DatabaseConnectionFactory(config);
            }

            // Creamos el controlador de autenticación y cargamos las preferencias del último usuario logueado.
            var authController = new AuthController(connectionFactory);
            var userPreferences = UserPreferences.Load();
            using (var loginForm = new LoginForm(authController, userPreferences.LastUsername))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Guardamos quién se logueó por última vez para completar el campo automáticamente la próxima vez.
                    userPreferences.LastUsername = loginForm.AuthenticatedUserCorreo;
                    userPreferences.Save();
                    config.Save();
                    Application.Run(new DashboardForm(
                        loginForm.AuthenticatedUserId,
                        loginForm.AuthenticatedUser,
                        loginForm.AuthenticatedUserCorreo,
                        loginForm.AuthenticatedRole,
                        connectionFactory,
                        loginForm.SelectedEmpresaId,
                        loginForm.SelectedEmpresaNombre));
                }
            }
        }

        private static DatabaseConfig PromptForConfiguration(DatabaseConfig existing)
        {
            // Abrimos el formulario de configuración pasando lo que ya tengamos para que el usuario lo corrija.
            using (var settingsForm = new DatabaseSettingsForm(existing))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Si confirman, devolvemos la nueva configuración y la guardamos en disco.
                    var newConfig = settingsForm.Configuration;
                    newConfig.Save();
                    return newConfig;
                }
            }

            // Si se cerró sin confirmar devolvemos null para que el flujo principal decida qué hacer.
            return null;
        }
    }
}
