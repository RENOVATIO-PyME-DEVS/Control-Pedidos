using System;
using System.Data;
using System.Windows.Forms;
using Control_Pedidos.Controllers;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Views.Articles;
using Control_Pedidos.Views.Clients;
using Control_Pedidos.Views.Events;
using Control_Pedidos.Views.Users;

namespace Control_Pedidos.Views
{
    public partial class DashboardForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly OrderController _orderController;
        private readonly string _role;
        private readonly string _user;
        private readonly string _userId;
        private readonly string _userEmail;
        private readonly int _empresaId;
        private readonly string _empresaNombre;

        public DashboardForm(string usernameid, string usernamename, string usernamecorreo, string role, DatabaseConnectionFactory connectionFactory, int empresaId, string empresaNombre)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _connectionFactory = connectionFactory;
            _orderController = new OrderController(connectionFactory);
            _role = role;
            _user = usernamename;
            _userId = usernameid;
            _userEmail = usernamecorreo;
            _empresaId = empresaId;
            _empresaNombre = empresaNombre;

            // Personalizamos la cabecera con la info del usuario autenticado.
            welcomeLabel.Text = $"Bienvenido, {usernamename}";
            roleLabel.Text = $"Rol: {role}";
            companyLabel.Text = $"Empresa: {empresaNombre}";

            // Solo los administradores pueden ver la sección de catálogos.
            var isAdmin = string.Equals(role, "Administrador", StringComparison.OrdinalIgnoreCase);            
            if (isAdmin) {
                usersButton.Enabled = isAdmin;
                clientsButton.Enabled = isAdmin;
                articlesButton.Enabled = isAdmin;
            }

            // Solo los administradores pueden ver la sección de catálogos.
            var isCajero = string.Equals(role, "Cajero", StringComparison.OrdinalIgnoreCase);
            if (isCajero)
            {
                usersButton.Enabled = !isCajero;
                clientsButton.Enabled = isCajero;
                articlesButton.Enabled = isCajero;
            }

            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                // Falta integrar métricas reales, por ahora dejamos una tabla vacía para evitar romper la UI.
                var table = new DataTable(); // _orderController.GetOrderTable(_empresaId);
                activeOrdersGrid.DataSource = table;
                activeOrdersCountLabel.Text = table.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar el dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void ordersButton_Click(object sender, EventArgs e)
        {
            using (var ordersForm = new Orders.OrderDeliveryDashboardForm(_orderController, _connectionFactory, _empresaId, _empresaNombre))
            {
                ordersForm.ShowDialog();
            }
        }

        private void usersButton_Click(object sender, EventArgs e)
        {
            using (var form = new UserManagementForm(_connectionFactory))
            {
                form.ShowDialog();
            }
        }

        private void clientsButton_Click(object sender, EventArgs e)
        {
            var usuarioActual = new Usuario
            {
                Id = int.TryParse(_userId, out var parsedId) ? parsedId : 0,
                Nombre = _user,
                Correo = _userEmail
            };

            if (!string.IsNullOrWhiteSpace(_role))
            {
                usuarioActual.Roles.Add(new Rol { Nombre = _role });
            }

            var empresaActual = new Empresa
            {
                Id = _empresaId,
                Nombre = _empresaNombre
            };

            using (var form = new ClientManagementForm(_connectionFactory, usuarioActual, empresaActual))
            {
                form.ShowDialog();
            }
        }

        private void articlesButton_Click(object sender, EventArgs e)
        {
            using (var form = new ArticleManagementForm(_connectionFactory, _userId))
            {
                form.ShowDialog();
            }
        }

        private void eventsButton_Click(object sender, EventArgs e)
        {
            using (var form = new EventManagementForm(_connectionFactory, _empresaId))
            {
                form.ShowDialog();
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}
