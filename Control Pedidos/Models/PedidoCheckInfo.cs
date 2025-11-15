using System;

namespace Control_Pedidos.Models
{
    /// <summary>
    /// Representa la información necesaria para gestionar el CheckIN y CheckOUT de un pedido.
    /// </summary>
    public class PedidoCheckInfo
    {
        /// <summary>
        /// Identificador interno del pedido en la base de datos.
        /// </summary>
        public int PedidoId { get; set; }

        /// <summary>
        /// Folio numérico del pedido tal como se almacena en la tabla (puede ser nulo).
        /// </summary>
        public int? FolioNumerico { get; set; }

        /// <summary>
        /// Folio mostrado al usuario ya formateado con serie y ceros a la izquierda.
        /// </summary>
        public string FolioFormateado { get; set; } = string.Empty;

        /// <summary>
        /// Nombre completo del cliente propietario del pedido.
        /// </summary>
        public string ClienteNombre { get; set; } = string.Empty;

        /// <summary>
        /// Notas del pedido.
        /// </summary>
        public string Notas { get; set; } = string.Empty;

        /// <summary>
        /// Fecha programada para la entrega del pedido.
        /// </summary>
        public DateTime FechaEntrega { get; set; }

        /// <summary>
        /// Hora pactada de entrega. Puede ser nula cuando no se estableció.
        /// </summary>
        public TimeSpan? HoraEntrega { get; set; }

        /// <summary>
        /// Estatus actual del pedido (N, CI, CO, etc.).
        /// </summary>
        public string Estatus { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del evento asociado. Es nulo cuando el pedido no tiene evento.
        /// </summary>
        public int? EventoId { get; set; }

        /// <summary>
        /// Nombre del evento asociado al pedido. Si no tiene evento se deja en blanco.
        /// </summary>
        public string EventoNombre { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora en la que se registró el CheckIN. Es nula cuando aún no se ha escaneado.
        /// </summary>
        public DateTime? FechaCheckIn { get; set; }

        /// <summary>
        /// Descripción agregada de los productos del pedido (utilizada para el CheckOUT).
        /// </summary>
        public string ProductosDescripcion { get; set; } = string.Empty;

        /// <summary>
        /// Monto total del pedido (considerando descuentos) calculado con la función f_totalPedido.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Monto abonado por el cliente mediante cobros registrados (función f_cobroPedido).
        /// </summary>
        public decimal Abonado { get; set; }

        /// <summary>
        /// Saldo pendiente (Total - Abonado). Si es mayor a cero no se considera pagado.
        /// </summary>
        public decimal SaldoPendiente { get; set; }

        /// <summary>
        /// Indica si el pedido está completamente pagado.
        /// </summary>
        public bool EstaPagado => Math.Round(SaldoPendiente, 2) <= 0m;

        /// <summary>
        /// Cadena amigable con la fecha y hora de entrega para mostrarse en los formularios.
        /// </summary>
        public string FechaEntregaDescripcion
        {
            get
            {
                var fecha = FechaEntrega.ToString("dd/MM/yyyy");
                if (HoraEntrega.HasValue)
                {
                    var hora = new DateTime(HoraEntrega.Value.Ticks).ToString("HH:mm");
                    return $"{fecha} {hora} hrs";
                }

                return fecha;
            }
        }
    }
}
