using System;
using System.Collections.Generic;

namespace Control_Pedidos.Models
{
    /// <summary>
    /// DTO especializado para la vista previa de pedidos.
    /// </summary>
    public class PedidoDetalleDto
    {
        public int PedidoId { get; set; }
        public string Folio { get; set; } = string.Empty;
        public string FolioFormateado { get; set; } = string.Empty;
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteTelefono { get; set; } = string.Empty;
        public string ClienteDomicilio { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public TimeSpan? HoraEntrega { get; set; }
        public string EventoNombre { get; set; } = string.Empty;
        public string UsuarioNombre { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public decimal TotalCobrado { get; set; }
        public decimal Descuento { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string EstatusCodigo { get; set; } = string.Empty;
        public string EstatusNombre { get; set; } = string.Empty;

        public IList<PedidoDetalleArticuloDto> Articulos { get; } = new List<PedidoDetalleArticuloDto>();
    }

    public class PedidoDetalleArticuloDto
    {
        public int ArticuloId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string NombreCorto { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal { get; set; }
        public bool EsKit { get; set; }

        public IList<PedidoDetalleComponenteDto> Componentes { get; } = new List<PedidoDetalleComponenteDto>();

        public string TipoIcono => EsKit ? "📦" : string.Empty;
    }

    public class PedidoDetalleComponenteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; } = string.Empty;
    }
}
