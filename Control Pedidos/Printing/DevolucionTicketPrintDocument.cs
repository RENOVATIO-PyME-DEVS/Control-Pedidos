using Control_Pedidos.Models;
using Control_Pedidos.Properties;
using System;
using System.Drawing;
using System.Drawing.Printing;

namespace Control_Pedidos.Printing
{
    /*
     * Clase: DevolucionTicketPrintDocument
     * Descripción: Define el diseño del ticket de devolución dibujando encabezado, datos del pedido,
     *              montos y el espacio de firma requerido.
     */
    public class DevolucionTicketPrintDocument : PrintDocument
    {
        private readonly DevolucionTicketData _data;
        private readonly bool _esCopia;
        private readonly Font _tituloFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font _subtituloFont = new Font("Segoe UI", 8, FontStyle.Bold);
        private readonly Font _textoFont = new Font("Segoe UI", 8);
        private readonly Font _textoItalicFont = new Font("Segoe UI", 7, FontStyle.Italic);
        private Image _logo;

        /*
         * Constructor: DevolucionTicketPrintDocument
         * Descripción: Recibe la información a imprimir y un indicador para saber si el ticket es copia.
         *              También establece el tamaño de página habitual de las impresoras térmicas.
         */
        public DevolucionTicketPrintDocument(DevolucionTicketData data, bool esCopia)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _esCopia = esCopia;

            DefaultPageSettings = new PageSettings
            {
                PaperSize = new PaperSize("Ticket", 300, 1000),
                Margins = new Margins(10, 10, 10, 10)
            };

            PrinterSettings.DefaultPageSettings.PaperSize = DefaultPageSettings.PaperSize;
            PrinterSettings.DefaultPageSettings.Margins = DefaultPageSettings.Margins;
            DocumentName = esCopia ? "Devolucion_Copia" : "Devolucion_Original";
        }

        /*
         * Método: OnPrintPage
         * Descripción: Dibuja cada bloque del ticket (encabezado, datos y pie) respetando el orden solicitado.
         */
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            var g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            var bounds = e.MarginBounds;
            float y = bounds.Top;

            y = DibujarEncabezado(g, bounds, y);
            y = DibujarDatosPrincipales(g, bounds, y);
            y = DibujarPie(g, bounds, y);

            e.HasMorePages = false;
        }

        /*
         * Método: Dispose
         * Descripción: Libera correctamente los recursos asociados a fuentes e imágenes.
         */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tituloFont.Dispose();
                _subtituloFont.Dispose();
                _textoFont.Dispose();
                _textoItalicFont.Dispose();
                _logo?.Dispose();
            }

            base.Dispose(disposing);
        }

        /*
         * Método: DibujarEncabezado
         * Descripción: Imprime el logo, la leyenda de copia/original y los datos de la empresa.
         */
        private float DibujarEncabezado(Graphics g, Rectangle bounds, float y)
        {
            if (_esCopia)
            {
                y = DibujarTextoCentrado(g, bounds, y, _subtituloFont, "*** COPIA CLIENTE ***");
                y += 2f;
            }
            else
            {
                y = DibujarTextoCentrado(g, bounds, y, _subtituloFont, "*** ORIGINAL ***");
                y += 2f;
            }

            var logo = ObtenerLogo();
            if (logo != null)
            {
                var maxWidth = bounds.Width;
                var aspect = (float)logo.Width / Math.Max(1, logo.Height);
                var width = Math.Min(maxWidth, 120f);
                var height = width / aspect;
                var x = bounds.Left + (bounds.Width - width) / 2f;
                g.DrawImage(logo, new RectangleF(x, y, width, height));
                y += height + 4f;
            }

            var nombreEmpresa = _data.Empresa?.Nombre;
            if (!string.IsNullOrWhiteSpace(nombreEmpresa))
            {
                y = DibujarTextoCentrado(g, bounds, y, _tituloFont, nombreEmpresa);
            }

            y = DibujarTextoCentrado(g, bounds, y, _subtituloFont, "DEVOLUCIÓN DE PEDIDO");
            y += 4f;
            g.DrawString(new string('-', 40), _textoFont, Brushes.Black, bounds, new StringFormat { Alignment = StringAlignment.Center });
            y += _textoFont.GetHeight(g) + 2f;

            return y;
        }

        /*
         * Método: DibujarDatosPrincipales
         * Descripción: Presenta la información del cliente, folio, totales y forma de devolución.
         */
        private float DibujarDatosPrincipales(Graphics g, Rectangle bounds, float y)
        {
            y = DibujarTexto(g, bounds, y, $"Cliente: {_data.ClienteNombre}", _textoFont);
            y = DibujarTexto(g, bounds, y, $"Folio pedido: {_data.FolioPedido}", _textoFont);
            y = DibujarTexto(g, bounds, y, $"Total del pedido: {_data.TotalPedido:C2}", _textoFont);

            if (_data.MontoDevuelto > 0)
            {
                y = DibujarTexto(g, bounds, y, $"Monto devuelto: {_data.MontoDevuelto:C2}", _textoFont);
                y = DibujarTexto(g, bounds, y, $"Forma de devolución: {_data.FormaDevolucion}", _textoFont);
            }
            else
            {
                y = DibujarTexto(g, bounds, y, "Pedido cancelado sin devolución monetaria.", _textoFont);
            }

            y = DibujarTexto(g, bounds, y, $"Fecha: {_data.FechaDevolucion:dd/MM/yyyy HH:mm}", _textoFont);
            y += 4f;
            g.DrawString(new string('-', 40), _textoFont, Brushes.Black, bounds, new StringFormat { Alignment = StringAlignment.Center });
            y += _textoFont.GetHeight(g) + 2f;

            return y;
        }

        /*
         * Método: DibujarPie
         * Descripción: Añade el espacio para la firma del cliente y una leyenda aclaratoria.
         */
        private float DibujarPie(Graphics g, Rectangle bounds, float y)
        {
            y = DibujarTextoCentrado(g, bounds, y, _textoFont, "______________________________");
            y = DibujarTextoCentrado(g, bounds, y, _textoFont, "Firma del cliente");
            y += 4f;
            y = DibujarTextoCentrado(g, bounds, y, _textoItalicFont, "Este comprobante no es fiscal.");
            y += _textoFont.GetHeight(g);
            return y;
        }

        private float DibujarTexto(Graphics g, Rectangle bounds, float y, string texto, Font font)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return y;
            }

            var rect = new RectangleF(bounds.Left, y, bounds.Width, font.GetHeight(g) + 2f);
            g.DrawString(texto, font, Brushes.Black, rect);
            y += font.GetHeight(g) + 2f;
            return y;
        }

        private float DibujarTextoCentrado(Graphics g, Rectangle bounds, float y, Font font, string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return y;
            }

            var rect = new RectangleF(bounds.Left, y, bounds.Width, font.GetHeight(g) + 2f);
            g.DrawString(texto, font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center });
            y += font.GetHeight(g) + 2f;
            return y;
        }

        private Image ObtenerLogo()
        {
            if (_logo != null)
            {
                return _logo;
            }

            if (Resources.LogoEmpresa != null)
            {
                _logo = (Image)Resources.LogoEmpresa.Clone();
            }

            return _logo;
        }
    }
}
