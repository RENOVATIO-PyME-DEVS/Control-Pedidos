using System;
using System.Data;
using System.Windows.Forms;
using Control_Pedidos.Controllers;
using Control_Pedidos.Data;

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

            _connectionFactory = connectionFactory;
            _orderController = new OrderController(connectionFactory);
            _role = role;

            welcomeLabel.Text = $"Bienvenido, {username}";
            roleLabel.Text = $"Rol: {role}";

            if (!string.Equals(role, "administrador", StringComparison.OrdinalIgnoreCase))
            {
                usersButton.Enabled = false;
            }

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
    }
}
