using System;
using System.Data;
using System.Windows.Forms;
using Control_Pedidos.Controllers;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Views.Articles;
using Control_Pedidos.Views.Clients;
using Control_Pedidos.Views.Users;

namespace Control_Pedidos.Views
{
    public partial class DashboardForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly OrderController _orderController;
        private readonly string _role;

        public DashboardForm(string username, string role, DatabaseConnectionFactory connectionFactory)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _connectionFactory = connectionFactory;
            _orderController = new OrderController(connectionFactory);
            _role = role;

            welcomeLabel.Text = $"Bienvenido, {username}";
            roleLabel.Text = $"Rol: {role}";

            var isAdmin = string.Equals(role, "administrador", StringComparison.OrdinalIgnoreCase);
            usersButton.Enabled = isAdmin;
            clientsButton.Enabled = isAdmin;
            articlesButton.Enabled = isAdmin;

            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                var table = _orderController.GetOrderTable();
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
            using (var ordersForm = new Orders.OrderManagementForm(_orderController))
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
            using (var form = new ClientManagementForm(_connectionFactory))
            {
                form.ShowDialog();
            }
        }

        private void articlesButton_Click(object sender, EventArgs e)
        {
            using (var form = new ArticleManagementForm(_connectionFactory))
            {
                form.ShowDialog();
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}
