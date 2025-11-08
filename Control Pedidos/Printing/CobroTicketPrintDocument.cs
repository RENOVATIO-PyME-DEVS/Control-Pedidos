using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using Control_Pedidos.Models;
using Control_Pedidos.Properties;

namespace Control_Pedidos.Printing
{
    public class CobroTicketPrintDocument : PrintDocument
    {
        private readonly Cobro _cobro;
        private readonly Font _tituloFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font _subtituloFont = new Font("Segoe UI", 8, FontStyle.Bold);
        private readonly Font _textoFont = new Font("Segoe UI", 8);
        private readonly Font _textoNegritasFont = new Font("Segoe UI", 8, FontStyle.Bold);
        private Image _logo;

        public CobroTicketPrintDocument(Cobro cobro)
        {
            _cobro = cobro ?? throw new ArgumentNullException(nameof(cobro));

            DefaultPageSettings = new PageSettings
            {
                PaperSize = new PaperSize("Ticket", 300, 1000),
                Margins = new Margins(10, 10, 10, 10)
            };

            PrinterSettings.DefaultPageSettings.PaperSize = DefaultPageSettings.PaperSize;
            PrinterSettings.DefaultPageSettings.Margins = DefaultPageSettings.Margins;

            DocumentName = _cobro.Id > 0 ? $"Cobro_{_cobro.Id}" : "Cobro";
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            var g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            var bounds = e.MarginBounds;
            float y = bounds.Top;

            y = DibujarEncabezado(g, bounds, y);
            y = DibujarDatosCobro(g, bounds, y);
            y = DibujarDistribucion(g, bounds, y);
            y = DibujarTotales(g, bounds, y);

            e.HasMorePages = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tituloFont.Dispose();
                _subtituloFont.Dispose();
                _textoFont.Dispose();
                _textoNegritasFont.Dispose();
                _logo?.Dispose();
            }

            base.Dispose(disposing);
        }

        private float DibujarEncabezado(Graphics g, Rectangle bounds, float y)
        {
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

            y = DibujarTextoCentrado(g, bounds, y, _tituloFont, _cobro.Empresa?.Nombre);
            y = DibujarTextoCentrado(g, bounds, y, _textoFont, _cobro.Empresa?.Rfc);
            y = DibujarTextoCentrado(g, bounds, y, _textoFont, _cobro.Empresa?.Telefono);

            y += 4f;
            g.DrawString(new string('-', 40), _textoFont, Brushes.Black, bounds, new StringFormat { Alignment = StringAlignment.Center });
            y += _textoFont.GetHeight(g) + 2f;

            return y;
        }

        private float DibujarDatosCobro(Graphics g, Rectangle bounds, float y)
        {
            var nombreCliente = _cobro.Cliente == null
                ? string.Empty
                : (!string.IsNullOrWhiteSpace(_cobro.Cliente.NombreComercial)
                    ? _cobro.Cliente.NombreComercial
                    : _cobro.Cliente.Nombre);

            y = DibujarTexto(g, bounds, y, $"CLIENTE:  {nombreCliente}", _textoNegritasFont);
            var fechaCobro = _cobro.Fecha == default ? DateTime.Now : _cobro.Fecha;
            y = DibujarTexto(g, bounds, y, $"FECHA:    {fechaCobro:dd/MM/yyyy HH:mm}", _textoFont);
            y = DibujarTexto(g, bounds, y, $"COBRO #:  {_cobro.Id}", _textoFont);
            var formaCobro = string.IsNullOrWhiteSpace(_cobro.FormaCobroNombre) ? string.Empty : _cobro.FormaCobroNombre;
            y = DibujarTexto(g, bounds, y, $"FORMA DE PAGO: {formaCobro}", _textoFont);

            y += 4f;
            g.DrawString(new string('-', 40), _textoFont, Brushes.Black, bounds, new StringFormat { Alignment = StringAlignment.Center });
            y += _textoFont.GetHeight(g) + 2f;

            g.DrawString("DISTRIBUCIÓN DEL ABONO:", _subtituloFont, Brushes.Black, new RectangleF(bounds.Left, y, bounds.Width, _subtituloFont.GetHeight(g) + 2f));
            y += _subtituloFont.GetHeight(g) + 4f;

            return y;
        }

        private float DibujarDistribucion(Graphics g, Rectangle bounds, float y)
        {
            var detalles = _cobro.Detalles ?? Array.Empty<CobroDetalle>();
            var formatoMonto = new StringFormat { Alignment = StringAlignment.Far };
            var formatoFolio = new StringFormat { Alignment = StringAlignment.Near };

            foreach (var detalle in detalles)
            {
                var linea = $"Folio {detalle.Folio}";
                var monto = detalle.Monto.ToString("C2");
                g.DrawString(linea, _textoFont, Brushes.Black, new RectangleF(bounds.Left, y, bounds.Width / 2f, _textoFont.GetHeight(g) + 2f), formatoFolio);
                g.DrawString(monto, _textoFont, Brushes.Black, new RectangleF(bounds.Left + bounds.Width / 2f, y, bounds.Width / 2f, _textoFont.GetHeight(g) + 2f), formatoMonto);
                y += _textoFont.GetHeight(g) + 2f;
            }

            if (!detalles.Any())
            {
                y = DibujarTexto(g, bounds, y, "No hay pedidos seleccionados.", _textoFont);
            }

            y += 4f;
            g.DrawString(new string('-', 40), _textoFont, Brushes.Black, bounds, new StringFormat { Alignment = StringAlignment.Center });
            y += _textoFont.GetHeight(g) + 2f;

            return y;
        }

        private float DibujarTotales(Graphics g, Rectangle bounds, float y)
        {
            y = DibujarTextoResaltado(g, bounds, y, $"TOTAL ABONADO: ........ {_cobro.Monto:C2}");
            y = DibujarTexto(g, bounds, y, $"SALDO RESTANTE: ....... {_cobro.SaldoDespues:C2}", _textoFont);

            if (_cobro.SaldoAnterior > 0)
            {
                y = DibujarTexto(g, bounds, y, $"SALDO ANTERIOR: ....... {_cobro.SaldoAnterior:C2}", _textoFont);
            }

            y += 4f;
            g.DrawString(new string('-', 40), _textoFont, Brushes.Black, bounds, new StringFormat { Alignment = StringAlignment.Center });
            y += _textoFont.GetHeight(g) + 2f;

            y = DibujarTextoCentrado(g, bounds, y, _textoFont, "Gracias por su pago.");
            y = DibujarTextoCentrado(g, bounds, y, _textoFont, "Este comprobante no es fiscal.");
            y += 4f;
            g.DrawString(new string('-', 40), _textoFont, Brushes.Black, bounds, new StringFormat { Alignment = StringAlignment.Center });
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
            return y + font.GetHeight(g) + 2f;
        }

        private float DibujarTextoResaltado(Graphics g, Rectangle bounds, float y, string texto)
        {
            return DibujarTexto(g, bounds, y, texto, _textoNegritasFont);
        }

        private float DibujarTextoCentrado(Graphics g, Rectangle bounds, float y, Font font, string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return y;
            }

            var rect = new RectangleF(bounds.Left, y, bounds.Width, font.GetHeight(g) + 2f);
            g.DrawString(texto, font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center });
            return y + font.GetHeight(g) + 2f;
        }

        private Image ObtenerLogo()
        {
            if (_logo != null)
            {
                return _logo;
            }

            try
            {
                _logo = (Image)Resources.LogoEmpresa.Clone();
            }
            catch
            {
                _logo = null;
            }

            return _logo;
        }
    }
}
