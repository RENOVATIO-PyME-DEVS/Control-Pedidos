using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Models;

namespace Control_Pedidos.Printing
{
    /// <summary>
    /// Documento de impresión genérico para mostrar cualquier reporte cargado en un DataGridView.
    /// Respeta el diseño corporativo, agrega encabezados y pies y controla el paginado automáticamente.
    /// </summary>
    public sealed class ReportPrintDocument : PrintDocument
    {
        // === Campos obligatorios recibidos por el constructor ===
        private readonly DataGridView _grid;
        private readonly Empresa _empresa;
        private readonly string _nombreReporte;
        private readonly string _parametros;
        private readonly Image _logoEmpresa;

        // === Fuentes y recursos de dibujo ===
        private readonly Font _fuenteNombreEmpresa = new Font("Segoe UI", 12f, FontStyle.Bold);
        private readonly Font _fuenteDatosEmpresa = new Font("Segoe UI", 9f, FontStyle.Regular);
        private readonly Font _fuenteTituloReporte = new Font("Segoe UI", 16f, FontStyle.Bold);
        private readonly Font _fuenteParametros = new Font("Segoe UI", 9f, FontStyle.Regular);
        private readonly Font _fuenteEncabezadoTabla = new Font("Segoe UI", 9f, FontStyle.Bold);
        private readonly Font _fuenteCeldas = new Font("Segoe UI", 9f, FontStyle.Regular);
        private readonly Font _fuentePie = new Font("Segoe UI", 8f, FontStyle.Regular);

        // === Control interno de paginación ===
        private int _filaActual;
        private int _numeroPagina;

        /// <summary>
        /// Crea un documento de impresión listo para renderizar el contenido de un DataGridView.
        /// </summary>
        /// <param name="grid">Grid que contiene el reporte a imprimir.</param>
        /// <param name="empresa">Datos de la empresa dueña del reporte.</param>
        /// <param name="nombreReporte">Nombre legible del reporte.</param>
        /// <param name="parametros">Parámetros usados al generar el reporte.</param>
        /// <param name="logoEmpresa">Logo corporativo a mostrar en el encabezado.</param>
        public ReportPrintDocument(
            DataGridView grid,
            Empresa empresa,
            string nombreReporte,
            string parametros,
            Image logoEmpresa)
        {
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            _empresa = empresa ?? new Empresa();
            _nombreReporte = nombreReporte ?? string.Empty;
            _parametros = parametros ?? string.Empty;
            _logoEmpresa = logoEmpresa;

            DefaultPageSettings = new PageSettings
            {
                Margins = new Margins(50, 50, 50, 50)
            };
        }

        /// <summary>
        /// Reinicia contadores antes de comenzar la impresión.
        /// </summary>
        /// <param name="e">Argumentos de impresión.</param>
        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            _filaActual = 0;       // Reinicia el índice de fila
            _numeroPagina = 1;     // Siempre se empieza en página 1
        }

        /// <summary>
        /// Dibuja cada página del documento controlando saltos y repetición de encabezados.
        /// </summary>
        /// <param name="e">Argumentos de impresión.</param>
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PageUnit = GraphicsUnit.Display;

            Rectangle marginBounds = e.MarginBounds;
            float y = marginBounds.Top;

            // 🔹 Prepara columnas visibles y anchos escalados al área imprimible
            var columnasVisibles = ObtenerColumnasVisibles();
            var anchosColumnas = CalcularAnchosColumnas(columnasVisibles, marginBounds.Width);

            // 🔹 Altura reservada para el pie de página (fecha + número de página)
            float alturaPie = CalcularAlturaPie(g);

            // === Encabezado corporativo (logo + datos + título) ===
            y = DibujarEncabezado(g, marginBounds, y);

            // === Encabezados de tabla ===
            y = DibujarEncabezadosTabla(g, marginBounds, columnasVisibles, anchosColumnas, y);

            // === Cuerpo de la tabla con paginación automática ===
            bool hayMasPaginas = DibujarFilas(g, marginBounds, columnasVisibles, anchosColumnas, ref y, alturaPie);

            // === Pie de página: fecha/hora y número de página ===
            DibujarPie(g, marginBounds, alturaPie);

            if (hayMasPaginas)
            {
                // Solicita otra página y avanza contador interno
                e.HasMorePages = true;
                _numeroPagina++;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        /// <summary>
        /// Libera los recursos gráficos creados manualmente.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fuenteNombreEmpresa.Dispose();
                _fuenteDatosEmpresa.Dispose();
                _fuenteTituloReporte.Dispose();
                _fuenteParametros.Dispose();
                _fuenteEncabezadoTabla.Dispose();
                _fuenteCeldas.Dispose();
                _fuentePie.Dispose();
            }

            base.Dispose(disposing);
        }

        // ======================= MÉTODOS DE DIBUJO =======================

        /// <summary>
        /// Dibuja el encabezado corporativo: logo, datos de empresa, nombre de reporte y parámetros.
        /// </summary>
        private float DibujarEncabezado(Graphics g, Rectangle bounds, float y)
        {
            float espacioHorizontal = bounds.Width;
            float xActual = bounds.Left;

            // -- Logo alineado a la izquierda manteniendo aspecto --
            if (_logoEmpresa != null)
            {
                const float alturaLogo = 70f; // Altura solicitada
                float proporcion = _logoEmpresa.Width / (float)Math.Max(1, _logoEmpresa.Height);
                float anchoLogo = alturaLogo * proporcion;

                RectangleF rectLogo = new RectangleF(xActual, y, anchoLogo, alturaLogo);
                g.DrawImage(_logoEmpresa, rectLogo);

                xActual += anchoLogo + 10; // espacio entre logo y texto
                espacioHorizontal -= anchoLogo + 10;
            }

            // -- Datos de empresa al lado derecho --
            float alturaNombre = _fuenteNombreEmpresa.GetHeight(g);
            float alturaLinea = _fuenteDatosEmpresa.GetHeight(g);
            float yTexto = y;

            RectangleF rectEmpresa = new RectangleF(xActual, yTexto, espacioHorizontal, alturaNombre);
            g.DrawString(_empresa?.Nombre ?? string.Empty, _fuenteNombreEmpresa, Brushes.Black, rectEmpresa,
                new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
            yTexto += alturaNombre + 2;

            var lineasEmpresa = new List<string>
            {
                string.IsNullOrWhiteSpace(_empresa?.Rfc) ? string.Empty : $"RFC: {_empresa.Rfc}",
                string.IsNullOrWhiteSpace(_empresa?.Direccion) ? string.Empty : _empresa.Direccion,
                string.IsNullOrWhiteSpace(_empresa?.Telefono) ? string.Empty : $"Tel: {_empresa.Telefono}"
            };

            foreach (string linea in lineasEmpresa.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                RectangleF rectLinea = new RectangleF(xActual, yTexto, espacioHorizontal, alturaLinea);
                g.DrawString(linea, _fuenteDatosEmpresa, Brushes.Black, rectLinea,
                    new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
                yTexto += alturaLinea + 2;
            }

            // -- Título del reporte centrado --
            string tituloReporte = _nombreReporte ?? string.Empty;
            if (tituloReporte.StartsWith("Reporte:", StringComparison.OrdinalIgnoreCase))
            {
                tituloReporte = tituloReporte.Substring("Reporte:".Length).Trim();
            }
            if (string.IsNullOrWhiteSpace(tituloReporte))
            {
                tituloReporte = "Reporte";
            }

            RectangleF rectTitulo = new RectangleF(bounds.Left, yTexto + 6, bounds.Width, _fuenteTituloReporte.GetHeight(g));
            g.DrawString(tituloReporte, _fuenteTituloReporte, Brushes.Black, rectTitulo,
                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near });
            yTexto = rectTitulo.Bottom + 4;

            // -- Parámetros utilizados --
            RectangleF rectParametros = new RectangleF(bounds.Left, yTexto, bounds.Width, _fuenteParametros.GetHeight(g));
            string parametrosTexto = string.IsNullOrWhiteSpace(_parametros)
                ? "Parámetros: Sin información"
                : _parametros;
            if (!parametrosTexto.StartsWith("Parámetros", StringComparison.OrdinalIgnoreCase))
            {
                parametrosTexto = $"Parámetros: {parametrosTexto}";
            }
            g.DrawString(parametrosTexto, _fuenteParametros, Brushes.Black, rectParametros,
                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near });

            // -- Línea divisoria inferior --
            float yLinea = rectParametros.Bottom + 8;
            g.DrawLine(Pens.Gray, bounds.Left, yLinea, bounds.Right, yLinea);

            return yLinea + 6; // devuelve la siguiente coordenada Y libre
        }

        /// <summary>
        /// Dibuja el encabezado de la tabla (nombres de columna) con fondo gris.
        /// </summary>
        private float DibujarEncabezadosTabla(Graphics g, Rectangle bounds, IList<DataGridViewColumn> columnas, IList<float> anchos, float y)
        {
            float alturaEncabezado = _fuenteEncabezadoTabla.GetHeight(g) + 10;
            using (Brush fondo = new SolidBrush(Color.FromArgb(230, 230, 230)))
            using (Pen borde = new Pen(Color.DarkGray, 1))
            {
                float x = bounds.Left;
                for (int i = 0; i < columnas.Count; i++)
                {
                    RectangleF celda = new RectangleF(x, y, anchos[i], alturaEncabezado);
                    g.FillRectangle(fondo, celda);
                    g.DrawRectangle(borde, celda.X, celda.Y, celda.Width, celda.Height);
                    g.DrawString(columnas[i].HeaderText, _fuenteEncabezadoTabla, Brushes.Black, celda,
                        new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
                    x += anchos[i];
                }
            }

            return y + alturaEncabezado;
        }

        /// <summary>
        /// Dibuja las filas visibles respetando saltos de página y devuelve si existen más páginas.
        /// </summary>
        private bool DibujarFilas(Graphics g, Rectangle bounds, IList<DataGridViewColumn> columnas, IList<float> anchos, ref float y, float alturaPie)
        {
            using (Pen borde = new Pen(Color.LightGray, 1))
            {
                while (_filaActual < _grid.Rows.Count)
                {
                    DataGridViewRow fila = _grid.Rows[_filaActual];
                    if (fila.IsNewRow)
                    {
                        _filaActual++;
                        continue;
                    }

                    float alturaCelda = _fuenteCeldas.GetHeight(g) + 10;

                    // 🔸 Verifica espacio disponible (restando el pie de página)
                    if (y + alturaCelda + alturaPie > bounds.Bottom)
                    {
                        return true; // hay más páginas por imprimir
                    }

                    float x = bounds.Left;
                    for (int i = 0; i < columnas.Count; i++)
                    {
                        DataGridViewColumn col = columnas[i];
                        object valor = fila.Cells[col.Index].FormattedValue;
                        string texto = valor?.ToString() ?? string.Empty;

                        RectangleF celda = new RectangleF(x, y, anchos[i], alturaCelda);
                        g.DrawRectangle(borde, celda.X, celda.Y, celda.Width, celda.Height);
                        g.DrawString(texto, _fuenteCeldas, Brushes.Black, celda,
                            new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });

                        x += anchos[i];
                    }

                    y += alturaCelda;
                    _filaActual++;
                }
            }

            return false; // No hay más filas que imprimir
        }

        /// <summary>
        /// Dibuja el pie de página con fecha/hora y número de página.
        /// </summary>
        private void DibujarPie(Graphics g, Rectangle bounds, float alturaPie)
        {
            float yPie = bounds.Bottom - alturaPie + 4;
            string fechaTexto = $"Impreso el: {DateTime.Now:dd/MM/yyyy HH:mm}";
            string paginaTexto = $"Página {_numeroPagina}";

            RectangleF rectFecha = new RectangleF(bounds.Left, yPie, bounds.Width / 2f, alturaPie);
            RectangleF rectPagina = new RectangleF(bounds.Left + bounds.Width / 2f, yPie, bounds.Width / 2f, alturaPie);

            g.DrawString(fechaTexto, _fuentePie, Brushes.Black, rectFecha,
                new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            g.DrawString(paginaTexto, _fuentePie, Brushes.Black, rectPagina,
                new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
        }

        // ======================= FUNCIONES DE APOYO =======================

        /// <summary>
        /// Obtiene las columnas visibles del grid en su orden actual.
        /// </summary>
        private IList<DataGridViewColumn> ObtenerColumnasVisibles()
        {
            return _grid.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();
        }

        /// <summary>
        /// Calcula anchos proporcionados al espacio disponible, respetando el ancho visual del grid.
        /// </summary>
        private IList<float> CalcularAnchosColumnas(IList<DataGridViewColumn> columnas, float anchoDisponible)
        {
            float anchoOriginal = columnas.Sum(c => (float)c.Width);
            float factorEscala = anchoOriginal > 0 ? Math.Min(1f, anchoDisponible / anchoOriginal) : 1f;

            return columnas.Select(c => c.Width * factorEscala).ToList();
        }

        /// <summary>
        /// Calcula la altura requerida para el pie de página.
        /// </summary>
        private float CalcularAlturaPie(Graphics g)
        {
            return _fuentePie.GetHeight(g) + 12; // margen visual
        }
    }
}
