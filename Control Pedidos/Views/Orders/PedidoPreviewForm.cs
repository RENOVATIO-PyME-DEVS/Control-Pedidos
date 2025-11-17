using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;
using Control_Pedidos.Printing;

namespace Control_Pedidos.Views.Orders
{
    public partial class PedidoPreviewForm : Form
    {
        private readonly int _pedidoId;
        private readonly PedidoDao _pedidoDao;
        private readonly PedidoPrintingService _printingService;
        private PedidoDetalleDto _pedidoDetalle;

        private static readonly IReadOnlyDictionary<string, string> StatusNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"P", "En proceso de Captura"},
            {"N", "Normal"},
            {"C", "Cancelado"},
            {"D", "Devuelto"},
            {"CI", "En producción"},
            {"CO", "Entregado"}
        };

        private static readonly IReadOnlyDictionary<string, Color> StatusColors = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase)
        {
            {"P", Color.FromArgb(120, 130, 140)},
            {"N", Color.FromArgb(33, 150, 243)},
            {"C", Color.FromArgb(220, 53, 69)},
            {"D", Color.FromArgb(255, 152, 0)},
            {"CI", Color.FromArgb(255, 193, 7)},
            {"CO", Color.FromArgb(40, 167, 69)}
        };

        public PedidoPreviewForm(int pedidoId, DatabaseConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException(nameof(connectionFactory));
            }

            _pedidoId = pedidoId;
            _pedidoDao = new PedidoDao(connectionFactory);
            _printingService = new PedidoPrintingService();

            InitializeComponent();
            DoubleBuffered = true;
        }

        private void PedidoPreviewForm_Load(object sender, EventArgs e)
        {
            ConfigureGrids();
            LoadPedido();
        }

        private void ConfigureGrids()
        {
            articlesGrid.AutoGenerateColumns = false;
            articlesGrid.MultiSelect = false;
            articlesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            articlesGrid.ReadOnly = true;
            articlesGrid.AllowUserToAddRows = false;
            articlesGrid.AllowUserToDeleteRows = false;
            articlesGrid.AllowUserToResizeRows = false;
            articlesGrid.RowHeadersVisible = false;
            articlesGrid.BackgroundColor = Color.White;
            articlesGrid.GridColor = Color.Gainsboro;
            articlesGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            articlesGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 242, 255);
            articlesGrid.DefaultCellStyle.SelectionForeColor = Color.Black;
            articlesGrid.Columns["cantidadColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            articlesGrid.Columns["cantidadColumn"].DefaultCellStyle.Format = "N2";
            articlesGrid.Columns["precioColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            articlesGrid.Columns["precioColumn"].DefaultCellStyle.Format = "C2";
            articlesGrid.Columns["subtotalColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            articlesGrid.Columns["subtotalColumn"].DefaultCellStyle.Format = "C2";

            componentsGrid.AutoGenerateColumns = false;
            componentsGrid.MultiSelect = false;
            componentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            componentsGrid.ReadOnly = true;
            componentsGrid.AllowUserToAddRows = false;
            componentsGrid.AllowUserToDeleteRows = false;
            componentsGrid.AllowUserToResizeRows = false;
            componentsGrid.RowHeadersVisible = false;
            componentsGrid.BackgroundColor = Color.White;
            componentsGrid.GridColor = Color.Gainsboro;
            componentsGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            componentsGrid.Columns["componentQuantityColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            componentsGrid.Columns["componentQuantityColumn"].DefaultCellStyle.Format = "N2";
        }

        private void LoadPedido()
        {
            try
            {
                _pedidoDetalle = ObtenerPedidoCompleto(_pedidoId);
                if (_pedidoDetalle == null)
                {
                    MessageBox.Show("No se encontró la información del pedido seleccionado.", "Vista previa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                RenderPedido();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar la información del pedido: {ex.Message}", "Vista previa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void RenderPedido()
        {
            var folio = !string.IsNullOrWhiteSpace(_pedidoDetalle.FolioFormateado) ? _pedidoDetalle.FolioFormateado : _pedidoDetalle.Folio;
            titleLabel.Text = string.IsNullOrWhiteSpace(folio)
                ? "Vista previa del pedido"
                : $"Vista previa del pedido {folio}";
            Text = titleLabel.Text;

            folioValueLabel.Text = string.IsNullOrWhiteSpace(folio) ? $"#{_pedidoDetalle.PedidoId}" : folio;
            clientValueLabel.Text = string.IsNullOrWhiteSpace(_pedidoDetalle.ClienteNombre) ? "Sin cliente" : _pedidoDetalle.ClienteNombre;
            phoneValueLabel.Text = string.IsNullOrWhiteSpace(_pedidoDetalle.ClienteTelefono) ? "Sin teléfono" : _pedidoDetalle.ClienteTelefono;
            addressValueLabel.Text = string.IsNullOrWhiteSpace(_pedidoDetalle.ClienteDomicilio) ? "Sin domicilio registrado" : _pedidoDetalle.ClienteDomicilio;

            var fechaEntrega = _pedidoDetalle.FechaEntrega.ToString("dd 'de' MMMM yyyy");
            var horaEntrega = _pedidoDetalle.HoraEntrega.HasValue ? _pedidoDetalle.HoraEntrega.Value.ToString("hh\:mm") : "Sin hora";
            deliveryValueLabel.Text = $"{fechaEntrega} - {horaEntrega}";
            fechaCapturaValueLabel.Text = _pedidoDetalle.FechaPedido.ToString("dd 'de' MMMM yyyy HH:mm");
            eventValueLabel.Text = string.IsNullOrWhiteSpace(_pedidoDetalle.EventoNombre) ? "Sin evento" : _pedidoDetalle.EventoNombre;
            userValueLabel.Text = string.IsNullOrWhiteSpace(_pedidoDetalle.UsuarioNombre) ? "No registrado" : _pedidoDetalle.UsuarioNombre;
            notesValueLabel.Text = string.IsNullOrWhiteSpace(_pedidoDetalle.Notas) ? "Sin notas" : _pedidoDetalle.Notas;

            totalValueLabel.Text = _pedidoDetalle.Total.ToString("C2");
            abonosValueLabel.Text = _pedidoDetalle.TotalCobrado.ToString("C2");
            descuentoValueLabel.Text = _pedidoDetalle.Descuento.ToString("C2");
            saldoValueLabel.Text = _pedidoDetalle.SaldoPendiente.ToString("C2");

            articlesGrid.DataSource = _pedidoDetalle.Articulos;
            if (_pedidoDetalle.Articulos.Count > 0)
            {
                articlesGrid.Rows[0].Selected = true;
            }

            ActualizarEstatusVisual();
            MostrarComponentesSeleccionados();
        }

        private void ActualizarEstatusVisual()
        {
            if (_pedidoDetalle == null)
            {
                return;
            }

            var codigo = _pedidoDetalle.EstatusCodigo ?? string.Empty;
            var nombre = ObtenerNombreEstatus(codigo);
            var color = ObtenerColorEstatus(codigo);

            statusLabel.Text = nombre.ToUpperInvariant();
            statusLabel.BackColor = color;
            statusLabel.ForeColor = Color.White;

            watermarkPanel.WatermarkText = nombre.ToUpperInvariant();
            watermarkPanel.WatermarkColor = Color.FromArgb(40, color);
            watermarkPanel.Invalidate();

            var esEditable = !string.Equals(codigo, "C", StringComparison.OrdinalIgnoreCase) && !string.Equals(codigo, "D", StringComparison.OrdinalIgnoreCase);
            articlesGrid.ReadOnly = !esEditable;
            articlesGrid.Enabled = esEditable;

            alertLabel.Visible = !esEditable;
            if (!esEditable)
            {
                alertLabel.Text = $"Este pedido está {nombre.ToUpperInvariant()}. No se puede modificar.";
            }
        }

        private void MostrarComponentesSeleccionados()
        {
            if (articlesGrid.CurrentRow?.DataBoundItem is PedidoDetalleArticuloDto articulo && articulo.EsKit)
            {
                componentsGrid.DataSource = articulo.Componentes.ToList();
                componentsLabel.Text = $"Componentes del kit \"{articulo.Nombre}\"";
                componentsLabel.Visible = true;
                componentsGrid.Visible = true;
            }
            else
            {
                componentsGrid.DataSource = null;
                componentsLabel.Text = "Selecciona un kit para visualizar sus componentes";
                componentsLabel.Visible = true;
                componentsGrid.Visible = false;
            }
        }

        private void articlesGrid_SelectionChanged(object sender, EventArgs e)
        {
            MostrarComponentesSeleccionados();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void reprintButton_Click(object sender, EventArgs e)
        {
            try
            {
                var pedido = _pedidoDao.ObtenerPedidoCompleto(_pedidoId);
                if (pedido == null)
                {
                    MessageBox.Show("No se encontró el pedido para reimprimir.", "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var resultado = _printingService.Print(pedido, this, pedido.EstaImpreso);
                if (resultado.Printed)
                {
                    _pedidoDao.ActualizarImpresion(pedido.Id, true);
                    MessageBox.Show("Pedido impreso correctamente.", "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (resultado.SavedPdf)
                {
                    _pedidoDao.ActualizarImpresion(pedido.Id, false);
                    var mensaje = new StringBuilder();
                    if (resultado.CancelledByUser)
                    {
                        mensaje.AppendLine("La impresión fue cancelada por el usuario.");
                    }
                    else if (resultado.Error != null)
                    {
                        mensaje.AppendLine("Ocurrió un problema al imprimir el pedido.");
                        mensaje.AppendLine(resultado.Error.Message);
                    }

                    if (!string.IsNullOrWhiteSpace(resultado.PdfPath))
                    {
                        mensaje.AppendLine();
                        mensaje.AppendLine("Se generó automáticamente un PDF en:");
                        mensaje.AppendLine(resultado.PdfPath);
                    }

                    MessageBox.Show(mensaje.ToString(), "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo reimprimir el pedido: {ex.Message}", "Reimpresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PedidoDetalleDto ObtenerPedidoCompleto(int pedidoId)
        {
            var pedido = _pedidoDao.ObtenerPedidoCompleto(pedidoId);
            if (pedido == null)
            {
                return null;
            }

            var dto = new PedidoDetalleDto
            {
                PedidoId = pedido.Id,
                Folio = pedido.Folio,
                FolioFormateado = pedido.FolioFormateado,
                ClienteNombre = pedido.Cliente?.Nombre ?? string.Empty,
                ClienteTelefono = pedido.Cliente?.Telefono ?? string.Empty,
                ClienteDomicilio = pedido.Cliente?.Domicilio ?? string.Empty,
                FechaPedido = pedido.Fecha,
                FechaEntrega = pedido.FechaEntrega,
                HoraEntrega = pedido.HoraEntrega,
                EventoNombre = pedido.Evento?.Nombre ?? string.Empty,
                UsuarioNombre = pedido.Usuario?.Nombre ?? string.Empty,
                Notas = pedido.Notas ?? string.Empty,
                Total = pedido.Total,
                TotalCobrado = pedido.MontoAbonado,
                Descuento = pedido.Descuento,
                SaldoPendiente = pedido.SaldoPendiente,
                EstatusCodigo = pedido.Estatus,
                EstatusNombre = ObtenerNombreEstatus(pedido.Estatus)
            };

            foreach (var detalle in pedido.Detalles ?? Enumerable.Empty<PedidoDetalle>())
            {
                var articulo = detalle.Articulo;
                var articuloDto = new PedidoDetalleArticuloDto
                {
                    ArticuloId = detalle.ArticuloId,
                    Nombre = articulo?.Nombre ?? string.Empty,
                    NombreCorto = articulo?.NombreCorto ?? string.Empty,
                    Cantidad = detalle.Cantidad,
                    Precio = detalle.PrecioUnitario,
                    Subtotal = detalle.Total,
                    EsKit = articulo?.EsKit ?? false
                };

                if (detalle.Componentes != null && detalle.Componentes.Count > 0)
                {
                    foreach (var componente in detalle.Componentes)
                    {
                        if (string.Equals(componente.Visible, "S", StringComparison.OrdinalIgnoreCase))
                        {
                            articuloDto.Componentes.Add(new PedidoDetalleComponenteDto
                            {
                                Nombre = componente.Articulo?.Nombre ?? string.Empty,
                                Cantidad = componente.Cantidad,
                                UnidadMedida = componente.Articulo?.UnidadMedida ?? string.Empty
                            });
                        }
                    }
                }

                dto.Articulos.Add(articuloDto);
            }

            return dto;
        }

        private static string ObtenerNombreEstatus(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return "Sin estatus";
            }

            return StatusNames.TryGetValue(codigo, out var nombre) ? nombre : codigo;
        }

        private static Color ObtenerColorEstatus(string codigo)
        {
            return StatusColors.TryGetValue(codigo ?? string.Empty, out var color)
                ? color
                : Color.FromArgb(96, 125, 139);
        }

        private sealed class StatusWatermarkPanel : Panel
        {
            public string WatermarkText { get; set; } = string.Empty;
            public Color WatermarkColor { get; set; } = Color.FromArgb(40, Color.Gray);
            public float WatermarkFontSize { get; set; } = 64f;

            public StatusWatermarkPanel()
            {
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                base.OnPaintBackground(e);

                if (string.IsNullOrWhiteSpace(WatermarkText))
                {
                    return;
                }

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(WatermarkColor))
                using (var font = new Font("Segoe UI", WatermarkFontSize, FontStyle.Bold, GraphicsUnit.Point))
                {
                    var size = e.Graphics.MeasureString(WatermarkText, font);
                    var center = new PointF(Width / 2f, Height / 2f);
                    e.Graphics.TranslateTransform(center.X, center.Y);
                    e.Graphics.RotateTransform(-30f);
                    var location = new PointF(-size.Width / 2f, -size.Height / 2f);
                    e.Graphics.DrawString(WatermarkText, font, brush, location);
                    e.Graphics.ResetTransform();
                }
            }
        }
    }
}
