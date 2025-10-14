using System;
using System.Data;
using MySql.Data.MySqlClient;
using Control_Pedidos.Data;
using Control_Pedidos.Models;
using System.Collections.Generic;

namespace Control_Pedidos.Controllers
{
    public class OrderController
    {
        private readonly DatabaseConnectionFactory _connectionFactory;

        public OrderController(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public DataTable GetOrderTable()
        {
            const string query = @"SELECT o.id AS Id, c.name AS Cliente, e.name AS Evento, o.status AS Estado, o.total_amount AS Total, o.balance AS SaldoPendiente, o.delivery_date AS FechaEntrega 
                                    FROM orders o INNER JOIN customers c ON o.customer_id = c.id INNER JOIN events e ON o.event_id = e.id ORDER BY o.created_at DESC";

            using (var connection = _connectionFactory.Create())
            using (var adapter = new MySqlDataAdapter(query, connection))
            {
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            const string query = @"SELECT o.id, o.order_number, o.total_amount, o.balance, o.status, o.delivery_date, c.id AS customer_id, c.name AS customer_name, e.id AS event_id, e.name AS event_name FROM orders o INNER JOIN customers c ON o.customer_id = c.id INNER JOIN events e ON o.event_id = e.id ORDER BY o.created_at DESC";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new Order
                        {
                            Id = reader.GetInt32("id"),
                            OrderNumber = reader.GetString("order_number"),
                            TotalAmount = reader.GetDecimal("total_amount"),
                            Balance = reader.GetDecimal("balance"),
                            Status = reader.GetString("status"),
                            DeliveryDate = reader.IsDBNull(reader.GetOrdinal("delivery_date")) ? (DateTime?)null : reader.GetDateTime("delivery_date"),
                            Customer = new Customer
                            {
                                Id = reader.GetInt32("customer_id"),
                                Name = reader.GetString("customer_name")
                            },
                            Event = new Event
                            {
                                Id = reader.GetInt32("event_id"),
                                Name = reader.GetString("event_name")
                            }
                        };

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        public void CreateOrder(Order order)
        {
            const string query = @"INSERT INTO orders (order_number, customer_id, event_id, total_amount, balance, status, delivery_date) VALUES (@orderNumber, @customerId, @eventId, @totalAmount, @balance, @status, @deliveryDate)";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@orderNumber", order.OrderNumber);
                command.Parameters.AddWithValue("@customerId", order.Customer.Id);
                command.Parameters.AddWithValue("@eventId", order.Event.Id);
                command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
                command.Parameters.AddWithValue("@balance", order.Balance);
                command.Parameters.AddWithValue("@status", order.Status);
                command.Parameters.AddWithValue("@deliveryDate", order.DeliveryDate);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateOrder(Order order)
        {
            const string query = @"UPDATE orders SET order_number = @orderNumber, customer_id = @customerId, event_id = @eventId, total_amount = @totalAmount, balance = @balance, status = @status, delivery_date = @deliveryDate WHERE id = @id";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@orderNumber", order.OrderNumber);
                command.Parameters.AddWithValue("@customerId", order.Customer.Id);
                command.Parameters.AddWithValue("@eventId", order.Event.Id);
                command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
                command.Parameters.AddWithValue("@balance", order.Balance);
                command.Parameters.AddWithValue("@status", order.Status);
                command.Parameters.AddWithValue("@deliveryDate", order.DeliveryDate);
                command.Parameters.AddWithValue("@id", order.Id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(int id)
        {
            const string query = "DELETE FROM orders WHERE id = @id";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
