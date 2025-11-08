using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using Control_Pedidos.Properties;

namespace Control_Pedidos.Printing
{
    public sealed class CobroPrintDocument : PrintDocument
    {
        private readonly CobroTicketInfo _ticket;
        private readonly Font _tituloFont = new Font("Segoe UI", 11, FontStyle.Bold);
        private readonly Font _subtituloFont = new Font("Segoe UI", 9, FontStyle.Bold);
        private readonly Font _textoRegularFont = new Font("Segoe UI", 9);
        private readonly Font _textoPequenoFont = new Font("Segoe UI", 8);
        private Image _logoEmpresa;

        public CobroPrintDocument(CobroTicketInfo ticket)
        {
            _ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));

            DefaultPageSettings = new PageSettings
            {
                Margins = new Margins(10, 10, 10, 10),
                PaperSize = new PaperSize("Ticket80mm", 315, 1000)
            };

            DocumentName = _ticket.Cobro?.Id > 0 ? $"Cobro_{_ticket.Cobro.Id}" : "Cobro";
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PageUnit = GraphicsUnit.Display;

            var bounds = e.MarginBounds;
            float y = bounds.Top;

            y = DibujarEncabezado(graphics, bounds, y);
            y = DibujarDatosCobro(graphics, bounds, y);
            y = DibujarDistribucion(graphics, bounds, y);
            DibujarPie(graphics, bounds, y);

            e.HasMorePages = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tituloFont.Dispose();
                _subtituloFont.Dispose();
                _textoRegularFont.Dispose();
                _textoPequenoFont.Dispose();
            }

            base.Dispose(disposing);
        }

        private float DibujarEncabezado(Graphics graphics, Rectangle bounds, float y)
        {
            var centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            var ancho = bounds.Width;
            var logo = ObtenerLogoEmpresa();

            if (logo != null)
            {
                const float maxHeight = 80f;
                var aspect = (float)logo.Width / Math.Max(1, logo.Height);
                var height = Math.Min(maxHeight, logo.Height);
                var width = height * aspect;
                var x = bounds.Left + (ancho - width) / 2f;
                graphics.DrawImage(logo, new RectangleF(x, y, width, height));
                y += height + 6;
            }

            if (!string.IsNullOrWhiteSpace(_ticket.Empresa?.Nombre))
            {
                var rect = new RectangleF(bounds.Left, y, ancho, _tituloFont.GetHeight(graphics) + 2);
                graphics.DrawString(_ticket.Empresa.Nombre, _tituloFont, Brushes.Black, rect, centerFormat);
                y += _tituloFont.GetHeight(graphics) + 4;
            }

            var infoEmpresa = string.Empty;
            if (!string.IsNullOrWhiteSpace(_ticket.Empresa?.Rfc))
            {
                infoEmpresa = $"RFC: {_ticket.Empresa.Rfc}";
            }

            if (!string.IsNullOrWhiteSpace(_ticket.Empresa?.Telefono))
            {
                infoEmpresa = string.IsNullOrWhiteSpace(infoEmpresa)
                    ? $"TEL: {_ticket.Empresa.Telefono}"
                    : string.Concat(infoEmpresa, "\nTEL: ", _ticket.Empresa.Telefono);
            }

            if (!string.IsNullOrWhiteSpace(infoEmpresa))
            {
                var rect = new RectangleF(bounds.Left, y, ancho, graphics.MeasureString(infoEmpresa, _textoRegularFont, ancho).Height + 2);
                graphics.DrawString(infoEmpresa, _textoRegularFont, Brushes.Black, rect, centerFormat);
                y += rect.Height + 2;
            }

            y += 4;
            DibujarSeparador(graphics, bounds, ref y);
            return y;
        }

        private float DibujarDatosCobro(Graphics graphics, Rectangle bounds, float y)
        {
            var ancho = bounds.Width;
            var lineHeight = _textoRegularFont.GetHeight(graphics) + 2;
            var leftFormat = new StringFormat { Alignment = StringAlignment.Near };

            var clienteNombre = _ticket.Cliente?.NombreComercial;
            if (string.IsNullOrWhiteSpace(clienteNombre))
            {
                clienteNombre = _ticket.Cliente?.Nombre;
            }

            graphics.DrawString($"CLIENTE: {clienteNombre}", _textoRegularFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), leftFormat);
            y += lineHeight;

            var fecha = _ticket.Cobro?.Fecha ?? DateTime.Now;
            graphics.DrawString($"FECHA: {fecha:dd/MM/yyyy HH:mm}", _textoRegularFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), leftFormat);
            y += lineHeight;

            var cobroId = _ticket.Cobro?.Id ?? 0;
            graphics.DrawString($"COBRO #: {cobroId}", _textoRegularFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), leftFormat);
            y += lineHeight;

            graphics.DrawString($"FORMA DE PAGO: {_ticket.FormaCobroNombre}", _textoRegularFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), leftFormat);
            y += lineHeight + 4;

            DibujarSeparador(graphics, bounds, ref y);

            return y;
        }

        private float DibujarDistribucion(Graphics graphics, Rectangle bounds, float y)
        {
            var ancho = bounds.Width;
            var lineHeight = _textoRegularFont.GetHeight(graphics) + 2;
            var leftFormat = new StringFormat { Alignment = StringAlignment.Near };
            var rightFormat = new StringFormat { Alignment = StringAlignment.Far };

            graphics.DrawString("DISTRIBUCIÓN DEL ABONO:", _subtituloFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), leftFormat);
            y += lineHeight;

            if (_ticket.Detalles != null)
            {
                foreach (var detalle in _ticket.Detalles)
                {
                    graphics.DrawString($"Folio {detalle.Folio}", _textoRegularFont, Brushes.Black,
                        new RectangleF(bounds.Left, y, ancho * 0.6f, lineHeight), leftFormat);
                    graphics.DrawString(detalle.Monto.ToString("C2"), _textoRegularFont, Brushes.Black,
                        new RectangleF(bounds.Left, y, ancho, lineHeight), rightFormat);
                    y += lineHeight;
                }
            }

            y += 2;
            DibujarSeparador(graphics, bounds, ref y);

            graphics.DrawString("TOTAL ABONADO:", _subtituloFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho * 0.6f, lineHeight), leftFormat);
            graphics.DrawString((_ticket.Cobro?.Monto ?? 0m).ToString("C2"), _subtituloFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), rightFormat);
            y += lineHeight;

            graphics.DrawString("SALDO RESTANTE:", _textoRegularFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho * 0.6f, lineHeight), leftFormat);
            graphics.DrawString(_ticket.SaldoRestante.ToString("C2"), _textoRegularFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), rightFormat);
            y += lineHeight + 4;

            DibujarSeparador(graphics, bounds, ref y);

            graphics.DrawString("Gracias por su pago.", _textoRegularFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), leftFormat);
            y += lineHeight;
            graphics.DrawString("Este comprobante no es fiscal.", _textoPequenoFont, Brushes.Black,
                new RectangleF(bounds.Left, y, ancho, lineHeight), leftFormat);
            y += lineHeight;

            return y;
        }

        private void DibujarPie(Graphics graphics, Rectangle bounds, float y)
        {
            var lineHeight = _textoPequenoFont.GetHeight(graphics) + 2;
            var rect = new RectangleF(bounds.Left, y, bounds.Width, lineHeight * 2);
            graphics.DrawString("Saldo anterior:" + " " + (_ticket.SaldoAnterior.ToString("C2")), _textoPequenoFont, Brushes.Black, rect);
            y += lineHeight;
            rect = new RectangleF(bounds.Left, y, bounds.Width, lineHeight);
            graphics.DrawString("Saldo después del abono:" + " " + (_ticket.SaldoRestante.ToString("C2")), _textoPequenoFont, Brushes.Black, rect);
        }

        private void DibujarSeparador(Graphics graphics, Rectangle bounds, ref float y)
        {
            var lineHeight = _textoPequenoFont.GetHeight(graphics) + 2;
            graphics.DrawString(new string('-', 40), _textoPequenoFont, Brushes.Black,
                new RectangleF(bounds.Left, y, bounds.Width, lineHeight), new StringFormat { Alignment = StringAlignment.Center });
            y += lineHeight + 2;
        }

        private Image ObtenerLogoEmpresa()
        {
            if (_logoEmpresa != null)
            {
                return _logoEmpresa;
            }

            try
            {
                var manager = Resources.ResourceManager;
                if (manager == null)
                {
                    return null;
                }

                var posiblesNombres = new[] { "CompanyLogo", "LogoEmpresa", "Logo", "Logotipo" };
                foreach (var nombre in posiblesNombres)
                {
                    var recurso = manager.GetObject(nombre);
                    if (recurso is Image imagen)
                    {
                        _logoEmpresa = imagen;
                        return _logoEmpresa;
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }
    }
}
