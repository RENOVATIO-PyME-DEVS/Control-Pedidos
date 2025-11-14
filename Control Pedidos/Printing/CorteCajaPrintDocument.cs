using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using Control_Pedidos.Models;
using Control_Pedidos.Properties;

namespace Control_Pedidos.Printing
{
    /// <summary>
    /// Define el layout en papel carta del reporte de corte de caja. Dibuja encabezado, tabla de movimientos
    /// y secciones de firmas de acuerdo con los requerimientos funcionales del módulo.
    /// </summary>
    public class CorteCajaPrintDocument : PrintDocument
    {
        private readonly CorteCajaReporte _reporte;
        private readonly Font _tituloFont = new Font("Segoe UI", 16, FontStyle.Bold);
        private readonly Font _subtituloFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font _textoFont = new Font("Segoe UI", 9);
        private readonly Font _tablaHeaderFont = new Font("Segoe UI", 9, FontStyle.Bold);
        private readonly Font _tablaFont = new Font("Segoe UI", 9);
        private readonly Font _firmaFont = new Font("Segoe UI", 9, FontStyle.Bold);

        private Image _logo;
        private int _rowIndex;
        private float[] _columnWidths;

        private const float TablaPadding = 6f;
        private const float EspacioFirmas = 140f;

        public CorteCajaPrintDocument(CorteCajaReporte reporte)
        {
            _reporte = reporte ?? throw new ArgumentNullException(nameof(reporte));

            DefaultPageSettings = new PageSettings
            {
                PaperSize = new PaperSize("Carta", 850, 1100),
                Margins = new Margins(60, 60, 70, 80)
            };

            DocumentName = $"CorteCaja_{_reporte.UsuarioNombre}_{_reporte.FechaCorte:yyyyMMdd}";
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            _rowIndex = 0;
            _columnWidths = null;
            _logo = CargarLogo();
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PageUnit = GraphicsUnit.Display;

            var bounds = e.MarginBounds;
            float y = bounds.Top;

            y = DibujarEncabezado(g, bounds, y);
            y += 12f;

            if (_reporte.Movimientos == null || _reporte.Movimientos.Columns.Count == 0)
            {
                g.DrawString("No se registran movimientos para la fecha seleccionada.", _textoFont, Brushes.Black,
                    new RectangleF(bounds.Left, y, bounds.Width, _textoFont.GetHeight(g) + 4));
                y += _textoFont.GetHeight(g) + 24f;
            }
            else
            {
                if (_columnWidths == null)
                {
                    _columnWidths = CalcularAnchosColumnas(g, bounds.Width);
                }

                y = DibujarTabla(g, bounds, y, e);
            }

            if (e.HasMorePages)
            {
                return;
            }

            if (y > bounds.Bottom - EspacioFirmas)
            {
                e.HasMorePages = true;
                return;
            }

            DibujarFirmas(g, bounds, Math.Max(y + 20f, bounds.Bottom - EspacioFirmas));
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            base.OnEndPrint(e);
            _logo?.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tituloFont.Dispose();
                _subtituloFont.Dispose();
                _textoFont.Dispose();
                _tablaHeaderFont.Dispose();
                _tablaFont.Dispose();
                _firmaFont.Dispose();
                _logo?.Dispose();
            }

            base.Dispose(disposing);
        }

        private Image CargarLogo()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_reporte.LogoPath) && File.Exists(_reporte.LogoPath))
                {
                    return Image.FromFile(_reporte.LogoPath);
                }

                return Resources.LogoEmpresa != null ? (Image)Resources.LogoEmpresa.Clone() : null;
            }
            catch
            {
                return null;
            }
        }

        private float DibujarEncabezado(Graphics g, Rectangle bounds, float y)
        {
            float logoWidth = 0f;
            float logoHeight = 0f;

            if (_logo != null)
            {
                logoHeight = 80f;
                var aspect = _logo.Width / Math.Max(1f, (float)_logo.Height);
                logoWidth = Math.Min(100f, logoHeight * aspect);

                var logoRect = new RectangleF(bounds.Left, y, logoWidth, logoHeight);
                g.DrawImage(_logo, logoRect);
            }

            var textoLeft = bounds.Left + (logoWidth > 0 ? logoWidth + 12f : 0f);
            var textoWidth = bounds.Width - (textoLeft - bounds.Left);
            var yTexto = y;

            g.DrawString(string.IsNullOrWhiteSpace(_reporte.EmpresaNombre) ? "Corte de Caja" : _reporte.EmpresaNombre,
                _tituloFont, Brushes.Black, new RectangleF(textoLeft, yTexto, textoWidth, _tituloFont.GetHeight(g) + 2));

            yTexto += _tituloFont.GetHeight(g) + 4f;

            if (!string.IsNullOrWhiteSpace(_reporte.EmpresaDireccion))
            {
                g.DrawString(_reporte.EmpresaDireccion, _textoFont, Brushes.Black,
                    new RectangleF(textoLeft, yTexto, textoWidth, _textoFont.GetHeight(g) + 2));
                yTexto += _textoFont.GetHeight(g) + 2f;
            }

            if (!string.IsNullOrWhiteSpace(_reporte.EmpresaRfc))
            {
                g.DrawString($"RFC: {_reporte.EmpresaRfc}", _textoFont, Brushes.Black,
                    new RectangleF(textoLeft, yTexto, textoWidth, _textoFont.GetHeight(g) + 2));
                yTexto += _textoFont.GetHeight(g) + 2f;
            }

            yTexto += 6f;

            g.DrawString($"Fecha del corte: {_reporte.FechaCorte:dd/MM/yyyy}", _subtituloFont, Brushes.Black,
                new RectangleF(textoLeft, yTexto, textoWidth, _subtituloFont.GetHeight(g) + 2));
            yTexto += _subtituloFont.GetHeight(g) + 2f;

            g.DrawString($"Usuario responsable: {_reporte.UsuarioNombre}", _textoFont, Brushes.Black,
                new RectangleF(textoLeft, yTexto, textoWidth, _textoFont.GetHeight(g) + 2));
            yTexto += _textoFont.GetHeight(g) + 2f;

            var bottom = Math.Max(y + logoHeight, yTexto);

            using (var pen = new Pen(Color.Black, 1f))
            {
                g.DrawLine(pen, bounds.Left, bottom + 8f, bounds.Right, bottom + 8f);
            }

            return bottom + 16f;
        }

        private float DibujarTabla(Graphics g, Rectangle bounds, float y, PrintPageEventArgs e)
        {
            var tabla = _reporte.Movimientos;
            if (tabla == null)
            {
                return y;
            }

            var headerHeight = Math.Max(_tablaHeaderFont.GetHeight(g) + TablaPadding, 24f);
            var rowHeight = Math.Max(_tablaFont.GetHeight(g) + TablaPadding, 22f);

            using (var headerBrush = new SolidBrush(Color.FromArgb(220, 220, 220)))
            using (var pen = new Pen(Color.DarkGray, 1f))
            {
                var x = bounds.Left;
                for (int i = 0; i < tabla.Columns.Count; i++)
                {
                    var width = _columnWidths[i];
                    var cellRect = new RectangleF(x, y, width, headerHeight);
                    g.FillRectangle(headerBrush, cellRect);
                    g.DrawRectangle(pen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    var headerText = FormatearEncabezado(tabla.Columns[i].ColumnName);
                    var formato = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    };

                    var textoRect = new RectangleF(cellRect.Left + TablaPadding / 2f, cellRect.Top, cellRect.Width - TablaPadding, cellRect.Height);
                    g.DrawString(headerText, _tablaHeaderFont, Brushes.Black, textoRect, formato);
                    x += width;
                }
            }

            y += headerHeight;

            using (var pen = new Pen(Color.Gainsboro, 1f))
            {
                for (; _rowIndex < tabla.Rows.Count; _rowIndex++)
                {
                    if (y + rowHeight > bounds.Bottom - EspacioFirmas)
                    {
                        e.HasMorePages = true;
                        return y;
                    }

                    var row = tabla.Rows[_rowIndex];
                    var x = bounds.Left;
                    for (int col = 0; col < tabla.Columns.Count; col++)
                    {
                        var width = _columnWidths[col];
                        var cellRect = new RectangleF(x, y, width, rowHeight);
                        g.DrawRectangle(pen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                        var texto = Convert.ToString(row[col]);
                        if (texto == null)
                        {
                            texto = string.Empty;
                        }

                        var formato = new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Trimming = StringTrimming.EllipsisCharacter
                        };

                        if (EsTipoNumerico(tabla.Columns[col].DataType))
                        {
                            formato.Alignment = StringAlignment.Far;
                            texto = FormatearNumero(row[col]);
                        }
                        else
                        {
                            formato.Alignment = StringAlignment.Near;
                        }

                        var textoRect = new RectangleF(cellRect.Left + TablaPadding / 2f, cellRect.Top, cellRect.Width - TablaPadding, cellRect.Height);
                        g.DrawString(texto, _tablaFont, Brushes.Black, textoRect, formato);

                        x += width;
                    }

                    y += rowHeight;
                }
            }

            e.HasMorePages = false;
            return y;
        }

        private void DibujarFirmas(Graphics g, Rectangle bounds, float y)
        {
            var anchoFirma = (bounds.Width / 2f) - 30f;
            var alturaFirma = 60f;

            var rectUsuario = new RectangleF(bounds.Left, y, anchoFirma, alturaFirma);
            var rectEncargado = new RectangleF(bounds.Left + bounds.Width / 2f + 30f, y, anchoFirma, alturaFirma);

            DibujarLineaFirma(g, rectUsuario, $"Firma del Usuario: {_reporte.UsuarioNombre}");
            DibujarLineaFirma(g, rectEncargado, "Firma del Encargado");
        }

        private void DibujarLineaFirma(Graphics g, RectangleF rect, string leyenda)
        {
            var lineaY = rect.Bottom - 25f;
            using (var pen = new Pen(Color.Black, 1f))
            {
                g.DrawLine(pen, rect.Left, lineaY, rect.Right, lineaY);
            }

            var textoRect = new RectangleF(rect.Left, lineaY + 4f, rect.Width, _firmaFont.GetHeight(g) + 2f);
            var formato = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString(leyenda, _firmaFont, Brushes.Black, textoRect, formato);
        }

        private float[] CalcularAnchosColumnas(Graphics g, float availableWidth)
        {
            var tabla = _reporte.Movimientos;
            if (tabla == null)
            {
                return Array.Empty<float>();
            }

            var widths = new float[tabla.Columns.Count];
            var previewRows = Math.Min(50, tabla.Rows.Count);

            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                var headerText = FormatearEncabezado(tabla.Columns[i].ColumnName);
                var maxWidth = g.MeasureString(headerText, _tablaHeaderFont).Width + TablaPadding;

                for (int rowIndex = 0; rowIndex < previewRows; rowIndex++)
                {
                    var texto = Convert.ToString(tabla.Rows[rowIndex][i]) ?? string.Empty;
                    var width = g.MeasureString(texto, _tablaFont).Width + TablaPadding;
                    if (width > maxWidth)
                    {
                        maxWidth = width;
                    }
                }

                widths[i] = Math.Max(80f, maxWidth);
            }

            var total = widths.Sum();
            if (total <= availableWidth)
            {
                return widths;
            }

            var factor = availableWidth / total;
            for (int i = 0; i < widths.Length; i++)
            {
                widths[i] = Math.Max(60f, widths[i] * factor);
            }

            var nuevoTotal = widths.Sum();
            if (nuevoTotal > availableWidth)
            {
                var ajuste = availableWidth / nuevoTotal;
                for (int i = 0; i < widths.Length; i++)
                {
                    widths[i] *= ajuste;
                }
            }

            return widths;
        }

        private static string FormatearEncabezado(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return string.Empty;
            }

            var limpio = texto.Replace('_', ' ');
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(limpio.ToLowerInvariant());
        }

        private static bool EsTipoNumerico(Type type)
        {
            return type == typeof(decimal) || type == typeof(double) || type == typeof(float) ||
                   type == typeof(int) || type == typeof(long) || type == typeof(short);
        }

        private static string FormatearNumero(object valor)
        {
            if (valor == null || valor == DBNull.Value)
            {
                return string.Empty;
            }

            if (valor is IFormattable formattable)
            {
                return formattable.ToString("N2", CultureInfo.CurrentCulture);
            }

            return Convert.ToString(valor, CultureInfo.CurrentCulture) ?? string.Empty;
        }
    }
}
