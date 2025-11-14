using System;
using System.Data;

namespace Control_Pedidos.Models
{
    /// <summary>
    /// Contiene toda la información necesaria para generar el reporte de corte de caja.
    /// La clase se mantiene simple (solo propiedades) para que pueda serializarse o
    /// reutilizarse tanto en la impresión directa como en la generación de PDF.
    /// </summary>
    public class CorteCajaReporte
    {
        /// <summary>
        /// Identificador de la empresa a la que pertenece el corte. Se utiliza para
        /// localizar el logo correspondiente cuando existen múltiples compañías.
        /// </summary>
        public int EmpresaId { get; set; }

        /// <summary>
        /// Nombre comercial de la empresa que aparece en el encabezado del reporte.
        /// </summary>
        public string EmpresaNombre { get; set; } = string.Empty;

        /// <summary>
        /// Domicilio fiscal o dirección donde opera la empresa.
        /// </summary>
        public string EmpresaDireccion { get; set; } = string.Empty;

        /// <summary>
        /// RFC o identificación fiscal que debe mostrarse en el encabezado.
        /// </summary>
        public string EmpresaRfc { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del usuario que realiza el corte. Se imprime tanto en el encabezado
        /// como en la línea de firma correspondiente.
        /// </summary>
        public string UsuarioNombre { get; set; } = string.Empty;

        /// <summary>
        /// Fecha del corte. Solo se usa la parte de fecha pero se conserva el tipo DateTime
        /// para futuras extensiones donde se requiera una hora específica.
        /// </summary>
        public DateTime FechaCorte { get; set; }

        /// <summary>
        /// Resultado completo del SP_CorteCaja. Se imprime sin modificaciones para reflejar
        /// los movimientos, métodos de pago y totales calculados por la base de datos.
        /// </summary>
        public DataTable Movimientos { get; set; }

        /// <summary>
        /// Ruta opcional al logo de la empresa. Si está vacía se utilizará el recurso por defecto.
        /// </summary>
        public string LogoPath { get; set; } = string.Empty;
    }
}
