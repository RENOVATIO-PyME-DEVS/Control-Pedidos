using System;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Data
{
    public class PedidoDetalleDao
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly PedidoDao _pedidoDao;

        public PedidoDetalleDao(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _pedidoDao = new PedidoDao(connectionFactory);
        }

        public bool Agregar(PedidoDetalle detalle, out string message)
        {
            if (detalle == null)
            {
                throw new ArgumentNullException(nameof(detalle));
            }

            message = string.Empty;

            if (detalle.PedidoId <= 0 && detalle.Pedido == null)
            {
                message = "No se encontró el pedido asociado.";
                return false;
            }

            try
            {
                if (detalle.PedidoId <= 0 && detalle.Pedido != null)
                {
                    if (!_pedidoDao.Agregar(detalle.Pedido, out message))
                    {
                        return false;
                    }

                    detalle.PedidoId = detalle.Pedido.Id;
                }

                if (detalle.ArticuloId <= 0 && detalle.Articulo != null)
                {
                    detalle.ArticuloId = detalle.Articulo.Id;
                }

                if (detalle.ArticuloId <= 0)
                {
                    message = "Seleccione un artículo válido.";
                    return false;
                }

                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(@"INSERT INTO banquetes.pedidos_detalles
(pedido_id, articulo_id, cantidad, precio_unitario, total)
VALUES
(@pedidoId, @articuloId, @cantidad, @precioUnitario, @total);", connection))
                {
                    command.Parameters.AddWithValue("@pedidoId", detalle.PedidoId);
                    command.Parameters.AddWithValue("@articuloId", detalle.ArticuloId);
                    command.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    command.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);
                    command.Parameters.AddWithValue("@total", detalle.Total);

                    connection.Open();
                    command.ExecuteNonQuery();
                    detalle.Id = Convert.ToInt32(command.LastInsertedId);
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo agregar el artículo al pedido: {ex.Message}";
                return false;
            }
        }

        public bool Eliminar(int pedidoDetalleId, out string message)
        {
            if (pedidoDetalleId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pedidoDetalleId));
            }

            message = string.Empty;

            try
            {
                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand("DELETE FROM banquetes.pedidos_detalles WHERE pedido_detalle_id = @detalleId;", connection))
                {
                    command.Parameters.AddWithValue("@detalleId", pedidoDetalleId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = $"No se pudo eliminar el artículo del pedido: {ex.Message}";
                return false;
            }
        }
    }
}
