using System;
using System.Data;
using System.Windows.Forms;
using Control_Pedidos.Controllers;
using Control_Pedidos.Helpers;

namespace Control_Pedidos.Views.Orders
{
    public partial class OrderManagementForm : Form
    {
        private readonly OrderController _orderController;
        private readonly int _empresaId;

        public OrderManagementForm(OrderController orderController, int empresaId)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            _orderController = orderController;
            _empresaId = empresaId;
        }

        private void OrderManagementForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                DataTable table = _orderController.GetOrderTable(_empresaId);
                ordersGrid.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pedidos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aquí se abriría el formulario para crear un pedido", "Acción no implementada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (ordersGrid.CurrentRow == null)
            {
                return;
            }

            var id = Convert.ToInt32(ordersGrid.CurrentRow.Cells["Id"].Value);
            MessageBox.Show($"Editar pedido con Id {id}", "Acción no implementada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ordersGrid.CurrentRow == null)
            {
                return;
            }

            var id = Convert.ToInt32(ordersGrid.CurrentRow.Cells["Id"].Value);
            var confirm = MessageBox.Show($"¿Eliminar pedido {id}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _orderController.DeleteOrder(id);
                    LoadOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo eliminar el pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
