using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using Control_Pedidos.Models;
using Control_Pedidos.Properties;

namespace Control_Pedidos.Printing
{
    /*
     * Clase: PedidoPrintDocument
     * Descripción: Genera la representación impresa del pedido en formato carta.
     */
    public sealed class PedidoPrintDocument : PrintDocument
    {
        private readonly Pedido _pedido;
        private readonly bool _mostrarLeyendaReimpreso;
        private readonly Font _tituloEmpresaFont = new Font("Segoe UI", 14, FontStyle.Bold);
        private readonly Font _textoNegritasFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font _textoRegularFont = new Font("Segoe UI", 10);
        private readonly Font _textoPequenoFont = new Font("Segoe UI", 8);
        private readonly Font _encabezadoTablaFont = new Font("Segoe UI", 9, FontStyle.Bold);
        private readonly Font _detalleTablaFont = new Font("Segoe UI", 9);
        private readonly Font _totalFont = new Font("Segoe UI", 11, FontStyle.Bold);

        private bool _encabezadoDibujado;


        private int _indiceDetalleActual;
        private Image _logoEmpresa;

        public PedidoPrintDocument(Pedido pedido, bool mostrarLeyendaReimpreso)
        {
            _pedido = pedido ?? throw new ArgumentNullException(nameof(pedido));
            _mostrarLeyendaReimpreso = mostrarLeyendaReimpreso;

            DefaultPageSettings = new PageSettings
            {
                Margins = new Margins(60, 60, 60, 80)
            };

            DocumentName = string.IsNullOrWhiteSpace(pedido.FolioFormateado)
                ? $"Pedido_{pedido.Id}"
                : pedido.FolioFormateado;


        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            _indiceDetalleActual = 0;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PageUnit = GraphicsUnit.Display;

            var bounds = e.MarginBounds;
            float y = bounds.Top;

            // 🔹 Solo en la primera página dibuja encabezado y datos del cliente
            if (!_encabezadoDibujado)
            {
                y = DibujarEncabezado(graphics, bounds, y);
                y = DibujarDatosCliente(graphics, bounds, y);
                y = DibujarDatosPedido(graphics, bounds, y);

                _encabezadoDibujado = true; // Marcar como ya dibujado
            }
            y += 10;
            y = DibujarEncabezadoTabla(graphics, bounds, y, out var columnas);

            var espacioReservado = CalcularEspacioTotalesYFooter(graphics);
            var hayMasPaginas = DibujarDetalles(graphics, bounds, columnas, ref y, espacioReservado);

            if (hayMasPaginas)
            {
                e.HasMorePages = true;
                return;
            }

            y = DibujarTotales(graphics, bounds, y);
            DibujarPie(graphics, bounds, y);

            e.HasMorePages = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tituloEmpresaFont.Dispose();
                _textoNegritasFont.Dispose();
                _textoRegularFont.Dispose();
                _textoPequenoFont.Dispose();
                _encabezadoTablaFont.Dispose();
                _detalleTablaFont.Dispose();
                _totalFont.Dispose();
            }

            base.Dispose(disposing);
        }

        private float DibujarEncabezado(Graphics graphics, Rectangle bounds, float y)
        {
            var logo = ObtenerLogoEmpresa();
            var left = bounds.Left;
            var top = y;

            float logoWidth = 0;
            float logoHeight = 0;
            if (logo != null)
            {
                const float maxLogoHeight = 80f;
                var aspect = (float)logo.Width / Math.Max(1, logo.Height);
                logoHeight = maxLogoHeight;
                logoWidth = maxLogoHeight * aspect;
                var logoRect = new RectangleF(left, top, logoWidth, logoHeight);
                graphics.DrawImage(logo, logoRect);
            }

            float textLeft = logo != null ? left + logoWidth + 12 : left;
            float textWidth = bounds.Right - textLeft;
            float currentY = top;

            var empresaNombre = "CENAS NAVIDEÑAS Y AÑO NUEVO \"MORA\"";// _pedido.Empresa?.Nombre ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(empresaNombre))
            {
                var rect = new RectangleF(textLeft, currentY, textWidth, _tituloEmpresaFont.GetHeight(graphics) + 2);
                graphics.DrawString(empresaNombre, _tituloEmpresaFont, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
                currentY += _tituloEmpresaFont.GetHeight(graphics) + 4;
            }

            var lines = new List<string>();
            if (!string.IsNullOrWhiteSpace(_pedido.Empresa?.Rfc))
            {
                lines.Add($"RFC: {_pedido.Empresa.Rfc}");
            }

            if (!string.IsNullOrWhiteSpace(_pedido.Empresa?.Direccion))
            {
                //lines.Add(_pedido.Empresa.Direccion);
                lines.Add("AV. DE LA CONVENCION ESQUINA BEETHOVEN, COL. DEL TRABAJO, AGUASCALIENTES.");
            }

            if (!string.IsNullOrWhiteSpace(_pedido.Empresa?.Telefono))
            {
                //lines.Add($"Tel.: {_pedido.Empresa.Telefono}");
                lines.Add($"Tels.: 449-975-0551 / 449-145-1959");
            }

            if (!string.IsNullOrWhiteSpace(_pedido.Empresa?.Correo))
            {
                //lines.Add(_pedido.Empresa.Correo);
            }

            foreach (var line in lines)
            {
                var rect = new RectangleF(textLeft, currentY, textWidth, _textoRegularFont.GetHeight(graphics) + 2);
                graphics.DrawString(line, _textoRegularFont, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
                currentY += _textoRegularFont.GetHeight(graphics) + 2;
            }

            var bloqueAltura = Math.Max(logoHeight, currentY - top);
            y = top + bloqueAltura + 12;

            var folioTexto = !string.IsNullOrWhiteSpace(_pedido.FolioFormateado)
                ? _pedido.FolioFormateado
                : (!string.IsNullOrWhiteSpace(_pedido.Folio) ? _pedido.Folio : _pedido.Id.ToString());
            var folioRect = new RectangleF(bounds.Left, y, bounds.Width / 2f, _textoNegritasFont.GetHeight(graphics) + 4);
            graphics.DrawString($"Folio Pedido: {folioTexto}", _textoNegritasFont, Brushes.Black, folioRect, new StringFormat { Alignment = StringAlignment.Near });


            //  Dibujar cpdigo de barras con el folio 3 of 9 Barcode
            try
            {
                // Generar código de barras tipo Code128
                using (var barcodeFont = new Font("3 of 9 Barcode", 25)) // Asegúrate de tener instalada la fuente “Free 3 of 9”
                {
                    var barcodeText = $"*{folioTexto}*"; // Formato requerido por la fuente Code 39
                    var barcodeRect = new RectangleF(bounds.Right - 250, top + 10, 240, 50);
                    graphics.DrawString(barcodeText, barcodeFont, Brushes.Black, barcodeRect, new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Near
                    });

                }
            }
            catch
            {
                // Si no está la fuente, no interrumpe la impresión
            }

            
            if (_mostrarLeyendaReimpreso)
            {
                var reimpresoRect = new RectangleF(bounds.Left, y, bounds.Width, _textoNegritasFont.GetHeight(graphics) + 4);
                graphics.DrawString("Pedido Reimpreso", _textoNegritasFont, Brushes.Maroon, reimpresoRect, new StringFormat { Alignment = StringAlignment.Far });
            }

            return y + _textoNegritasFont.GetHeight(graphics) + 6;
        }

        private float DibujarDatosPedido(Graphics graphics, Rectangle bounds, float y)
        {
            var left = bounds.Left;
            var width = bounds.Width;
            var columnWidth = width / 2f;
            var lineaAltura = _textoRegularFont.GetHeight(graphics) + 2;

            var formatLeft = new StringFormat { Alignment = StringAlignment.Near };
            var formatRight = new StringFormat { Alignment = StringAlignment.Near };

            var rectLeft = new RectangleF(left, y+7, columnWidth, lineaAltura);
            graphics.DrawString($"Fecha creación: {_pedido.FechaCreacion:dd/MM/yyyy}", _textoRegularFont, Brushes.Black, rectLeft, formatLeft);

            var horaEntrega = _pedido.HoraEntrega.HasValue ? _pedido.HoraEntrega.Value.ToString(@"hh\:mm") : "--";
            var rectRight = new RectangleF(left + columnWidth, y+7, columnWidth, lineaAltura);
            graphics.DrawString($"FECHA Y HORA ENTREGA: {_pedido.FechaEntrega:dd/MM/yyyy} {horaEntrega}", _textoNegritasFont, Brushes.Black, rectRight, formatRight);
            y += lineaAltura;

            rectLeft = new RectangleF(left, y + 7, columnWidth, lineaAltura);
            graphics.DrawString($"Lo atendió: {_pedido.Usuario.Nombre}", _textoRegularFont, Brushes.Black, rectLeft, formatLeft);

            if (_pedido.Evento != null && !string.IsNullOrWhiteSpace(_pedido.Evento.Nombre))
            {
                rectRight = new RectangleF(left + columnWidth, y + 7, columnWidth, lineaAltura);
                graphics.DrawString($"Evento: {_pedido.Evento.Nombre}", _textoRegularFont, Brushes.Black, rectRight, formatRight);
            }
            y += lineaAltura;

            //if (!string.IsNullOrWhiteSpace(_pedido.Notas))
            //{
            //    y += 4;
            //    var notasTexto = $"Notas: {_pedido.Notas}";
            //    var notasSize = graphics.MeasureString(notasTexto, _textoRegularFont, (int)width);
            //    var notasRect = new RectangleF(left, y + 7, width, notasSize.Height);
            //    var notasFormat = new StringFormat { Alignment = StringAlignment.Near };
            //    graphics.DrawString(notasTexto, _textoRegularFont, Brushes.Black, notasRect, notasFormat);
            //    y += notasSize.Height + 4;
            //}
            if (!string.IsNullOrWhiteSpace(_pedido.Notas))
            {
                y += 6;
                var notasTexto = $"Notas: {_pedido.Notas}";
                var notasSize = graphics.MeasureString(notasTexto, _textoRegularFont, (int)width - 10);

                // 📦 Dibujar borde del recuadro
                var padding = 6f;
                var rectBox = new RectangleF(left, y + 4, width, notasSize.Height + (padding * 2));
                graphics.DrawRectangle(Pens.Black, rectBox.Left, rectBox.Top, rectBox.Width, rectBox.Height);

                // 📝 Dibujar texto dentro del recuadro con margen interno
                var notasRect = new RectangleF(rectBox.Left + padding, rectBox.Top + padding, rectBox.Width - (padding * 2), rectBox.Height - (padding * 2));
                var notasFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
                graphics.DrawString(notasTexto, _textoRegularFont, Brushes.Black, notasRect, notasFormat);

                y += rectBox.Height + 8;
            }



            // --- Recomendaciones ---
            // --- Recomendaciones ---
            y += 20;

            // 🔹 Fuentes en negritas
            using (var fuenteTitulo = new Font(_textoNegritasFont.FontFamily, 11, FontStyle.Bold))
            using (var fuenteTexto = new Font(_textoNegritasFont.FontFamily, 9, FontStyle.Bold))
            {
                // --- Título centrado ---
                var titulo = "RECOMENDACIONES PARA EVITAR AGLOMERACIONES";
                var rectTitulo = new RectangleF(bounds.Left, y, bounds.Width, fuenteTitulo.GetHeight(graphics) + 6);
                var formatoCentro = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };
                graphics.DrawString(titulo, fuenteTitulo, Brushes.Black, rectTitulo, formatoCentro);
                y += rectTitulo.Height + 4;

                // --- Texto de recomendaciones ---
                var recomendaciones = new StringBuilder();
                recomendaciones.AppendLine("1. RECOGER SU PRODUCTO DENTRO DEL HORARIO Y DÍA MARCADO EN SU PEDIDO.");
                recomendaciones.AppendLine("2. PARA RECOGER PRODUCTO ES POR LA CALLE BEETHOVEN EN PLANTA ALTA.");
                recomendaciones.AppendLine("3. PRODUCTO NO RECOGIDO EN LA FECHA MARCADA Y EN EL HORARIO NO NOS HACEMOS RESPONSABLES Y");
                recomendaciones.AppendLine("   NO SE DEVOLVERÁ SU ANTICIPO.");
                recomendaciones.AppendLine("4. ¡¡¡TODOS LOS PRODUCTOS SE ENTREGAN A TEMPERATURA AMBIENTE!!!");

                var rectTexto = new RectangleF(bounds.Left, y, bounds.Width, graphics.MeasureString(recomendaciones.ToString(), fuenteTexto, (int)bounds.Width).Height + 6);
                var formatoIzquierda = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                };
                graphics.DrawString(recomendaciones.ToString(), fuenteTexto, Brushes.Black, rectTexto, formatoIzquierda);
                y += rectTexto.Height + 10;
            }



            return y + 6;
        }

        private float DibujarDatosCliente(Graphics graphics, Rectangle bounds, float y)
        {
            var left = bounds.Left;
            var width = bounds.Width;
            var rect = new RectangleF(left, y, width, _textoNegritasFont.GetHeight(graphics));
            graphics.DrawString("Datos del cliente", _textoNegritasFont, Brushes.Black, rect);
            y += _textoNegritasFont.GetHeight(graphics) + 2;

            var datos = new List<string>();
            if (!string.IsNullOrWhiteSpace(_pedido.Cliente?.Nombre))
            {
                datos.Add($"Cliente: {_pedido.Cliente.Nombre}");
            }

            if (!string.IsNullOrWhiteSpace(_pedido.Cliente?.Telefono))
            {
                datos.Add($"Teléfono: {_pedido.Cliente.Telefono}");
            }

            if (!string.IsNullOrWhiteSpace(_pedido.Cliente?.Correo))
            {
                datos.Add($"Correo: {_pedido.Cliente.Correo}");
            }

            if (!string.IsNullOrWhiteSpace(_pedido.Cliente?.Direccion))
            {
                datos.Add($"Domicilio: {_pedido.Cliente.Direccion}");
            }

            foreach (var dato in datos)
            {
                var datoRect = new RectangleF(left, y, width, _textoRegularFont.GetHeight(graphics) + 2);
                graphics.DrawString(dato, _textoRegularFont, Brushes.Black, datoRect);
                y += _textoRegularFont.GetHeight(graphics) + 2;
            }

            return y + 4;
        }

        private float DibujarEncabezadoTabla(Graphics graphics, Rectangle bounds, float y, out TablaColumnas columnas)
        {
            columnas = new TablaColumnas(bounds, new[] { 0.12f, 0.48f, 0.2f, 0.2f });

            using (var brush = new SolidBrush(Color.FromArgb(230, 230, 230)))
            {
                graphics.FillRectangle(brush, new RectangleF(bounds.Left, y, bounds.Width, _encabezadoTablaFont.GetHeight(graphics) + 10));
            }

            graphics.DrawRectangle(Pens.Black, bounds.Left, y, bounds.Width, _encabezadoTablaFont.GetHeight(graphics) + 10);

            var headers = new[] { "Cantidad", "Descripción", "Precio unitario", "Total" };
            for (var i = 0; i < headers.Length; i++)
            {
                var headerRect = columnas.GetColumnRectangle(i, y, _encabezadoTablaFont.GetHeight(graphics) + 10);
                var formato = new StringFormat { Alignment = i == 1 ? StringAlignment.Near : StringAlignment.Center, LineAlignment = StringAlignment.Center };
                graphics.DrawString(headers[i], _encabezadoTablaFont, Brushes.Black, headerRect, formato);
            }

            return y + _encabezadoTablaFont.GetHeight(graphics) + 12;
        }

        private bool DibujarDetalles(Graphics graphics, Rectangle bounds, TablaColumnas columnas, ref float y, float espacioReservado)
        {
            var subtotal = _pedido.Detalles?.Sum(d => d.Total) ?? 0m;
            if (subtotal <= 0m)
            {
                return false;
            }

            while (_indiceDetalleActual < _pedido.Detalles.Count)
            {
                var detalle = _pedido.Detalles[_indiceDetalleActual];

                // --- Construimos texto ---
                var descripcionNormal = ConstruirDescripcionDetalle(detalle);
                var descripcionRect = columnas.GetColumnRectangle(1, y, 0); // ancho base

                // --- Calculamos altura ---
                var descripcionSize = graphics.MeasureString(descripcionNormal, _detalleTablaFont, (int)columnas.GetColumnWidth(1));
                var filaAltura = Math.Max(descripcionSize.Height, _detalleTablaFont.GetHeight(graphics)) + 8;

                if (y + filaAltura > bounds.Bottom - espacioReservado)
                    return true;

                // --- Columna Cantidad ---
                var cantidadRect = columnas.GetColumnRectangle(0, y, filaAltura);
                // graphics.DrawString($"{detalle.Cantidad.ToString("N2")} {detalle.Articulo.UnidadMedida.ToString()}(s)", _detalleTablaFont, Brushes.Black, cantidadRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                var formatoCantidad = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near // 🔹 Alinea arriba, al nivel del título del artículo
                };
                graphics.DrawString($"{detalle.Cantidad:N2} {detalle.Articulo.UnidadMedida}(s)", _detalleTablaFont, Brushes.Black, cantidadRect, formatoCantidad);

                // --- Columna Descripción (separar título y componentes) ---
                var descripcionY = y;
                var fuenteNegrita = new Font(_detalleTablaFont, FontStyle.Bold);
                var fuenteNormal = _detalleTablaFont;
                var brush = Brushes.Black;

                // 1️⃣ Nombre del artículo (en negritas)
                graphics.DrawString(detalle.ArticuloNombre.ToUpper(), fuenteNegrita, brush, descripcionRect, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
                descripcionY += fuenteNegrita.GetHeight(graphics);

                // 2️⃣ Componentes (si aplica)
                if (detalle.Articulo?.EsKit == true && detalle.Componentes != null && detalle.Componentes.Count > 0)
                {
                    var componentesTexto = new StringBuilder();
                    componentesTexto.AppendLine("Guarniciones:");
                    foreach (var componente in detalle.Componentes)
                    {
                        if (componente.Visible.Trim() == "S")
                        {
                            decimal cantidadresumen = componente.Cantidad * detalle.Cantidad;
                            componentesTexto.AppendLine($"• {componente.NombreArticulo} {cantidadresumen.ToString("N")} {componente.Articulo.UnidadMedida}(s)");
                        }

                    }

                    var componenteRect = new RectangleF(descripcionRect.Left + 10, descripcionY, descripcionRect.Width - 10, filaAltura);
                    graphics.DrawString(componentesTexto.ToString(), fuenteNormal, brush, componenteRect, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
                }

                // --- Precio Unitario ---
                var precioRect = columnas.GetColumnRectangle(2, y, filaAltura);
                graphics.DrawString(detalle.PrecioUnitario.ToString("C2"), _detalleTablaFont, brush, precioRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                // --- Total ---
                var totalRect = columnas.GetColumnRectangle(3, y, filaAltura);
                graphics.DrawString(detalle.Total.ToString("C2"), _detalleTablaFont, brush, totalRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                // --- Línea divisoria y avanzar ---
                y += filaAltura;
                graphics.DrawLine(Pens.LightGray, bounds.Left, y, bounds.Right, y);

                _indiceDetalleActual++;
            }


            return false;
        }

        private float DibujarTotales(Graphics graphics, Rectangle bounds, float y)
        {
            var subtotal = _pedido.Subtotal > 0m ? _pedido.Subtotal : (_pedido.Detalles?.Sum(d => d.Total) ?? 0m);
            var descuento = Math.Max(0m, _pedido.Descuento);
            var total = Math.Max(0m, subtotal - descuento);
            var anticipo = Math.Max(0m, _pedido.MontoAbonado);
            var saldo = Math.Max(0m, total - anticipo);

            var columnaDerecha = bounds.Left + bounds.Width * 0.55f;
            var ancho = bounds.Right - columnaDerecha;

            var format = new StringFormat { Alignment = StringAlignment.Far };
            var rectSubtotal = new RectangleF(columnaDerecha, y, ancho, _textoRegularFont.GetHeight(graphics) + 2);
            graphics.DrawString($"Subtotal: {subtotal:C2}", _textoRegularFont, Brushes.Black, rectSubtotal, format);
            y += _textoRegularFont.GetHeight(graphics) + 2;

            if (descuento > 0m)
            {
                var rectDescuento = new RectangleF(columnaDerecha, y, ancho, _textoRegularFont.GetHeight(graphics) + 2);
                graphics.DrawString($"Descuento: -{descuento:C2}", _textoRegularFont, Brushes.Black, rectDescuento, format);
                y += _textoRegularFont.GetHeight(graphics) + 2;
            }

            var rectTotal = new RectangleF(columnaDerecha, y, ancho, _totalFont.GetHeight(graphics) + 2);
            graphics.DrawString($"Total final: {total:C2}", _totalFont, Brushes.Black, rectTotal, format);
            y += _totalFont.GetHeight(graphics) + 6;

            var rectAnticipo = new RectangleF(columnaDerecha, y, ancho, _textoRegularFont.GetHeight(graphics) + 2);
            graphics.DrawString($"Anticipo: {anticipo:C2}", _textoRegularFont, Brushes.Black, rectAnticipo, format);
            y += _textoRegularFont.GetHeight(graphics) + 2;


            var rectSaldo = new RectangleF(columnaDerecha, y, ancho, _totalFont.GetHeight(graphics) + 2);
            graphics.DrawString($"Saldo pendiente: {saldo:C2}", _totalFont, Brushes.Black, rectSaldo, format);
            y += _totalFont.GetHeight(graphics) + 40;

            if (!string.IsNullOrWhiteSpace(_pedido.FormaCobroUltima))
            {
                var rectForma = new RectangleF(columnaDerecha, y, ancho, _textoRegularFont.GetHeight(graphics) + 2);
                graphics.DrawString($"Forma de pago: {_pedido.FormaCobroUltima}", _textoRegularFont, Brushes.Black, rectForma, format);
                y += _textoRegularFont.GetHeight(graphics) + 2;
            }
            // 🟢 Si el saldo es 0, dibujar marca de agua PAGADO
            if (saldo == 0)
            {
                using (var fuenteAgua = new Font("Segoe UI", 90, FontStyle.Bold))
                using (var pincelAgua = new SolidBrush(Color.FromArgb(40, 0, 128, 0))) // Verde translúcido
                {
                    string textoAgua = "PAGADO";
                    var sizeAgua = graphics.MeasureString(textoAgua, fuenteAgua);

                    float xAgua = bounds.Left + (bounds.Width - sizeAgua.Width) / 2;
                    float yAgua = bounds.Top + (bounds.Height - sizeAgua.Height) / 2;

                    graphics.TranslateTransform(xAgua + sizeAgua.Width / 2, yAgua + sizeAgua.Height / 2);
                    graphics.RotateTransform(-30);
                    graphics.DrawString(textoAgua, fuenteAgua, pincelAgua, -sizeAgua.Width / 2, -sizeAgua.Height / 2);
                    graphics.ResetTransform();
                }
                }
                
                return y;
        }

        private void DibujarPie(Graphics graphics, Rectangle bounds, float y)
        {
            var leyenda = "***Antes de 72 horas del evento se podrá cancelar o cambiar la fecha de entrega del pedido. Pasado ese tiempo, no se aceptan cambios ni devoluciones.***";
            var leyendaRect = new RectangleF(bounds.Left, y, bounds.Width, graphics.MeasureString(leyenda, _textoPequenoFont, bounds.Width).Height + 4);

            using (var formatoCentrado = new StringFormat())
            {
                formatoCentrado.Alignment = StringAlignment.Center;       // Centrado horizontal
                formatoCentrado.LineAlignment = StringAlignment.Near;     // Alineado arriba dentro del rectángulo
                formatoCentrado.Trimming = StringTrimming.Word;           // Ajuste de texto por palabra
                formatoCentrado.FormatFlags = StringFormatFlags.LineLimit;

                graphics.DrawString(leyenda, _textoPequenoFont, Brushes.Black, leyendaRect,formatoCentrado);
            }

            y += leyendaRect.Height + 40;

            var firmaWidth = bounds.Width * 0.4f;
            var firmaLeft = bounds.Left + (bounds.Width - firmaWidth) / 2f;
            graphics.DrawLine(Pens.Black, firmaLeft, y, firmaLeft + firmaWidth, y);
            y += 4;

            var firmaRect = new RectangleF(firmaLeft, y, firmaWidth, _textoPequenoFont.GetHeight(graphics) + 2);
            var nombreCliente = _pedido.Cliente?.Nombre ?? string.Empty;
            graphics.DrawString(string.IsNullOrWhiteSpace(nombreCliente) ? "Nombre del cliente" : nombreCliente, _textoPequenoFont, Brushes.Black, firmaRect, new StringFormat { Alignment = StringAlignment.Center });
        }

        private string ConstruirDescripcionDetalle(PedidoDetalle detalle)
        {
            if (detalle == null)
            {
                return string.Empty;
            }

            if (detalle.Articulo?.EsKit != true || detalle.Componentes == null || detalle.Componentes.Count == 0)
            {
                return detalle.ArticuloNombre.ToUpper();
            }

            var builder = new StringBuilder();
            builder.AppendLine(detalle.ArticuloNombre.ToUpper());
            builder.AppendLine("Componentes:");
            foreach (var componente in detalle.Componentes)
            {
                builder.Append("• ");
                builder.AppendLine(componente.NombreArticulo);
            }

            return builder.ToString().TrimEnd();
        }

        private float CalcularEspacioTotalesYFooter(Graphics graphics)
        {
            var lineHeight = _textoRegularFont.GetHeight(graphics);
            var totalHeight = lineHeight * 6 + _totalFont.GetHeight(graphics);
            var leyendaHeight = graphics.MeasureString("Antes de 72 horas del evento se podrá cancelar o cambiar la fecha del pedido. Pasado ese tiempo, no se aceptan cambios ni devoluciones.", _textoPequenoFont, 1000).Height + 30;
            return totalHeight + leyendaHeight;
        }

        private void DibujarMarcaAguaPagado(Graphics graphics, Rectangle bounds)
        {
            using (var fuente = new Font("Segoe UI", 80, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.FromArgb(40, 0, 128, 0))) // Verde translúcido
            {
                var texto = "PAGADO";
                var size = graphics.MeasureString(texto, fuente);

                // Centrar el texto en la hoja
                float x = bounds.Left + (bounds.Width - size.Width) / 2;
                float y = bounds.Top + (bounds.Height - size.Height) / 2;

                // Aplicar rotación (diagonal) para efecto de marca de agua
                graphics.TranslateTransform(x + size.Width / 2, y + size.Height / 2);
                graphics.RotateTransform(-30);
                graphics.DrawString(texto, fuente, brush, -size.Width / 2, -size.Height / 2);
                graphics.ResetTransform();
            }
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
                // Ignoramos los errores de recursos para no romper la impresión.
            }

            return null;
        }

        private sealed class TablaColumnas
        {
            private readonly Rectangle _bounds;
            private readonly float[] _anchos;

            public TablaColumnas(Rectangle bounds, IReadOnlyList<float> proporciones)
            {
                _bounds = bounds;
                _anchos = new float[proporciones.Count];

                for (var i = 0; i < proporciones.Count; i++)
                {
                    _anchos[i] = bounds.Width * proporciones[i];
                }
            }

            public float GetColumnWidth(int index) => _anchos[index];

            public RectangleF GetColumnRectangle(int index, float y, float height)
            {
                var left = (float)_bounds.Left;
                for (var i = 0; i < index; i++)
                {
                    left += _anchos[i];
                }

                return new RectangleF(left, y, _anchos[index], height);
            }
        }
    }
}
