using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Control_Pedidos.Controllers;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Views.Articles;
using Control_Pedidos.Views.CheckIn;
using Control_Pedidos.Views.CheckOut;
using Control_Pedidos.Views.Clients;
using Control_Pedidos.Views.Events;
using Control_Pedidos.Views.CorteCaja;
using Control_Pedidos.Views.Reports;
using Control_Pedidos.Views.Users;

namespace Control_Pedidos.Views
{
    public partial class DashboardForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly OrderController _orderController;
        private readonly string _role;
        private readonly string _user;
        private readonly string _userId;
        private readonly string _userEmail;
        private readonly int _empresaId;
        private readonly string _empresaNombre;
        private readonly ForecastDao _forecastDao;
        private readonly EventoDao _eventoDao;
        private readonly List<Evento> _eventosForecast = new List<Evento>();
        private bool _isHorizontalForecastView;

        public DashboardForm(string usernameid, string usernamename, string usernamecorreo, string role, DatabaseConnectionFactory connectionFactory, int empresaId, string empresaNombre)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _connectionFactory = connectionFactory;
            _orderController = new OrderController(connectionFactory);
            _role = role;
            _user = usernamename;
            _userId = usernameid;
            _userEmail = usernamecorreo;
            _empresaId = empresaId;
            _empresaNombre = empresaNombre;
            _forecastDao = new ForecastDao(_connectionFactory);
            _eventoDao = new EventoDao(_connectionFactory);

            // Personalizamos la cabecera con la info del usuario autenticado.
            welcomeLabel.Text = $"Bienvenido, {usernamename}";
            roleLabel.Text = $"Rol: {role}";
            companyLabel.Text = $"Empresa: {empresaNombre}";

            //btnCheckInPedidos.Enabled = false;
            //btnCheckOutPedidos.Enabled = false;
            //btnCheckInPedidos.Visible = false;
            //btnCheckOutPedidos.Visible = false;


            // Solo los administradores pueden ver la sección de catálogos.
            var isAdmin = string.Equals(role, "Administrador", StringComparison.OrdinalIgnoreCase);            
            if (isAdmin) {
                usersButton.Enabled = isAdmin;
                clientsButton.Enabled = isAdmin;
                articlesButton.Enabled = isAdmin;
            }

            // Solo los administradores pueden ver la sección de catálogos.
            var isCajero = string.Equals(role, "Cajero", StringComparison.OrdinalIgnoreCase);
            if (isCajero)
            {
                button1.Enabled = !isCajero; //reportes
                usersButton.Enabled = !isCajero;
                btnCheckInPedidos.Enabled = !isCajero;
                btnCheckOutPedidos.Enabled = !isCajero;

                clientsButton.Enabled = isCajero;
                articlesButton.Enabled = isCajero;
            }

            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                // Falta integrar métricas reales, por ahora dejamos una tabla vacía para evitar romper la UI.
                var table = _orderController.GetOrderTable(_empresaId);
                activeOrdersGrid.DataSource = table;
                activeOrdersCountLabel.Text = table.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar el dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task InitializeForecastModuleAsync()
        {
            ConfigureForecastChart();
            await LoadEventosForecastAsync();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void ordersButton_Click(object sender, EventArgs e)
        {
            using (var ordersForm = new Orders.OrderDeliveryDashboardForm(_orderController, _connectionFactory, _empresaId, _empresaNombre))
            {
                ordersForm.ShowDialog();
            }
        }

        private void usersButton_Click(object sender, EventArgs e)
        {
            using (var form = new UserManagementForm(_connectionFactory))
            {
                form.ShowDialog();
            }
        }

        private void clientsButton_Click(object sender, EventArgs e)
        {
            var usuarioActual = new Usuario
            {
                Id = int.TryParse(_userId, out var parsedId) ? parsedId : 0,
                Nombre = _user,
                Correo = _userEmail
            };

            if (!string.IsNullOrWhiteSpace(_role))
            {
                usuarioActual.Roles.Add(new Rol { Nombre = _role });
            }

            var empresaActual = new Empresa
            {
                Id = _empresaId,
                Nombre = _empresaNombre
            };

            using (var form = new ClientManagementForm(_connectionFactory, usuarioActual, empresaActual))
            {
                form.ShowDialog();
            }
        }

        private void articlesButton_Click(object sender, EventArgs e)
        {
            using (var form = new ArticleManagementForm(_connectionFactory, _userId))
            {
                form.ShowDialog();
            }
        }

        private void eventsButton_Click(object sender, EventArgs e)
        {
            using (var form = new EventManagementForm(_connectionFactory, _empresaId))
            {
                form.ShowDialog();
            }
        }

        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            await InitializeForecastModuleAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new ReportsForm(_connectionFactory))
            {
                form.ShowDialog();
            }
        }

        private void btnCorteCaja_Click(object sender, EventArgs e)
        {
            var esAdministrador = string.Equals(_role, "Administrador", StringComparison.OrdinalIgnoreCase);
            if (!int.TryParse(_userId, out var usuarioId))
            {
                usuarioId = 0;
            }

            using (var form = new CorteCajaForm(_connectionFactory, esAdministrador, _empresaId, _empresaNombre, usuarioId, _user))
            {
                form.ShowDialog();
            }
        }

        private void btnCheckInPedidos_Click(object sender, EventArgs e)
        {
            // Abrimos el módulo de CheckIN dentro de un using para asegurar la liberación correcta de recursos.
            using (var form = new CheckInPedidosForm(_connectionFactory, _empresaId))
            {
                form.ShowDialog(this);
            }
        }

        private void btnCheckOutPedidos_Click(object sender, EventArgs e)
        {
            // Abrimos el módulo de CheckOUT para liberar pedidos con CheckIN registrado previamente.
            using (var form = new CheckOutPedidosForm(_connectionFactory, _empresaId))
            {
                form.ShowDialog(this);
            }
        }

        private async Task LoadEventosForecastAsync()
        {
            try
            {
                var eventos = await Task.Run(() => _eventoDao.ObtenerEventosActivosPorEmpresa(_empresaId));

                comboEventosForecast.SelectedIndexChanged -= comboEventosForecast_SelectedIndexChanged;
                comboEventosForecast.DataSource = null;
                _eventosForecast.Clear();
                _eventosForecast.AddRange(eventos);

                if (_eventosForecast.Count > 0)
                {
                    comboEventosForecast.DisplayMember = nameof(Evento.Nombre);
                    comboEventosForecast.ValueMember = nameof(Evento.Id);
                    comboEventosForecast.DataSource = new List<Evento>(_eventosForecast);
                }

                int todayIndex = _eventosForecast.FindIndex(e => e.FechaEvento.Date == DateTime.Today);
                comboEventosForecast.SelectedIndex = todayIndex;
                comboEventosForecast.SelectedIndexChanged += comboEventosForecast_SelectedIndexChanged;

                if (todayIndex >= 0)
                {
                    await LoadForecastDataAsync(_eventosForecast[todayIndex]);
                }
                else
                {
                    ClearForecastChart();
                    UpdateForecastTitle(null, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los eventos para el forecast: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadForecastDataAsync(Evento evento)
        {
            if (evento == null)
            {
                ClearForecastChart();
                UpdateForecastTitle(null, false);
                return;
            }

            try
            {
                var data = await Task.Run(() => _forecastDao.ObtenerForecast(_empresaId, evento.Id));
                await RenderForecastChartAsync(data, evento);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar la información del forecast: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureForecastChart()
        {
            if (chartForecast.ChartAreas.Count == 0)
            {
                return;
            }

            var chartArea = chartForecast.ChartAreas[0];
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Angle = -25;
            chartArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(70, 84, 102);
            chartArea.AxisY.LabelStyle.ForeColor = Color.FromArgb(70, 84, 102);
            chartArea.AxisX.Title = "Artículo";
            chartArea.AxisY.Title = "Cantidad";
            chartArea.AxisX.TitleForeColor = Color.FromArgb(30, 45, 68);
            chartArea.AxisY.TitleForeColor = Color.FromArgb(30, 45, 68);
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(225, 230, 235);
            chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(225, 230, 235);
            chartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea.BackColor = Color.FromArgb(250, 252, 255);
            chartArea.AxisX.LineColor = Color.FromArgb(210, 214, 220);
            chartArea.AxisY.LineColor = Color.FromArgb(210, 214, 220);

            chartForecast.BorderlineColor = Color.FromArgb(210, 214, 220);
            chartForecast.BorderlineDashStyle = ChartDashStyle.Solid;
            chartForecast.BorderlineWidth = 1;

            if (chartForecast.Legends.Count > 0)
            {
                chartForecast.Legends[0].BackColor = Color.Transparent;
                chartForecast.Legends[0].ForeColor = Color.FromArgb(45, 60, 82);
            }

            foreach (var seriesName in new[] { "Stock", "Vendido", "Diferencia", "Sobrevendido" })
            {
                var series = chartForecast.Series[seriesName];
                series.BorderWidth = 2;
                series.IsValueShownAsLabel = false;
                series["PointWidth"] = "0.55";
                series["DrawingStyle"] = "Cylinder";
            }

            var promedioSeries = chartForecast.Series["Promedio"];
            promedioSeries.BorderWidth = 3;
            promedioSeries.Color = Color.FromArgb(108, 117, 125);
            promedioSeries.LegendText = "Promedio vendido";
            promedioSeries.IsVisibleInLegend = true;

            ApplyChartOrientation();
        }

        private void ApplyChartOrientation()
        {
            if (chartForecast.ChartAreas.Count == 0)
            {
                return;
            }

            var chartArea = chartForecast.ChartAreas[0];
            var chartType = _isHorizontalForecastView ? SeriesChartType.Bar : SeriesChartType.Column;

            foreach (var seriesName in new[] { "Stock", "Vendido", "Diferencia", "Sobrevendido" })
            {
                if (chartForecast.Series.IndexOf(seriesName) < 0)
                {
                    continue;
                }

                chartForecast.Series[seriesName].ChartType = chartType;
            }

            chartArea.AxisX.MajorGrid.Enabled = _isHorizontalForecastView;
            chartArea.AxisY.MajorGrid.Enabled = !_isHorizontalForecastView;
            chartArea.AxisX.Title = _isHorizontalForecastView ? "Cantidad" : "Artículo";
            chartArea.AxisY.Title = _isHorizontalForecastView ? "Artículo" : "Cantidad";
            chartArea.AxisX.LabelStyle.Angle = _isHorizontalForecastView ? 0 : -25;
            chartArea.AxisX.Interval = 1;
        }

        private async Task RenderForecastChartAsync(DataTable data, Evento evento)
        {
            var registros = new List<ForecastRegistro>();

            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    var nombreCorto = row.Table.Columns.Contains("nombre_corto") ? row["nombre_corto"].ToString() : string.Empty;
                    var articulo = row.Table.Columns.Contains("Articulo") ? row["Articulo"].ToString() : string.Empty;
                    var stock = ConvertToDecimal(row["CantidadStock"]);
                    var vendido = ConvertToDecimal(row["CantidadVendidaPedidos"]);

                    registros.Add(new ForecastRegistro
                    {
                        Nombre = string.IsNullOrWhiteSpace(nombreCorto) ? articulo : nombreCorto,
                        Articulo = articulo,
                        Stock = stock,
                        Vendido = vendido
                    });
                }
            }

            var stockSeries = chartForecast.Series["Stock"];
            var vendidoSeries = chartForecast.Series["Vendido"];
            var diferenciaSeries = chartForecast.Series["Diferencia"];
            var sobreventaSeries = chartForecast.Series["Sobrevendido"];
            var promedioSeries = chartForecast.Series["Promedio"];

            stockSeries.Points.Clear();
            vendidoSeries.Points.Clear();
            diferenciaSeries.Points.Clear();
            sobreventaSeries.Points.Clear();
            promedioSeries.Points.Clear();

            if (registros.Count == 0)
            {
                UpdateForecastTitle(evento?.Nombre, false);
                chartForecast.Titles.Clear();
                chartForecast.Titles.Add(new Title("Sin datos disponibles", Docking.Top, new Font("Segoe UI", 11F, FontStyle.Bold), Color.FromArgb(120, 120, 120)));
                chartForecast.Invalidate();
                return;
            }

            UpdateForecastTitle(evento?.Nombre, true);
            chartForecast.Titles.Clear();
            chartForecast.Titles.Add(new Title(lblForecastTitle.Text, Docking.Top, new Font("Segoe UI", 11F, FontStyle.Bold), Color.FromArgb(30, 45, 68)));

            var promedio = registros.Average(r => r.Vendido);

            foreach (var registro in registros)
            {
                var tooltip = $"Artículo: {registro.Articulo}\nStock: {registro.Stock:N2}\nVendido: {registro.Vendido:N2}\nDiferencia: {registro.Diferencia:N2}";

                var stockIndex = stockSeries.Points.AddXY(registro.Nombre, registro.Stock);
                stockSeries.Points[stockIndex].ToolTip = tooltip;

                var vendidoIndex = vendidoSeries.Points.AddXY(registro.Nombre, registro.Vendido);
                vendidoSeries.Points[vendidoIndex].ToolTip = tooltip;

                if (registro.Diferencia > 0)
                {
                    var diffIndex = diferenciaSeries.Points.AddXY(registro.Nombre, registro.Diferencia);
                    diferenciaSeries.Points[diffIndex].ToolTip = tooltip;
                }
                else if (registro.Sobrevendido > 0)
                {
                    var sobreIndex = sobreventaSeries.Points.AddXY(registro.Nombre, registro.Sobrevendido);
                    sobreventaSeries.Points[sobreIndex].ToolTip = tooltip + $"\nSobrevendido: {registro.Sobrevendido:N2}";
                }

                var promedioIndex = promedioSeries.Points.AddXY(registro.Nombre, promedio);
                promedioSeries.Points[promedioIndex].ToolTip = $"Promedio vendido: {promedio:N2}";

                await Task.Delay(60);
            }

            foreach (var point in promedioSeries.Points)
            {
                point.MarkerStyle = MarkerStyle.Circle;
                point.MarkerSize = 7;
                point.MarkerColor = promedioSeries.Color;
            }

            ApplyChartOrientation();
            chartForecast.Invalidate();
        }

        private static decimal ConvertToDecimal(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return 0m;
            }

            return decimal.TryParse(value.ToString(), out var result) ? result : 0m;
        }

        private void UpdateForecastTitle(string eventoNombre, bool hasData)
        {
            var descriptor = string.IsNullOrWhiteSpace(eventoNombre) ? "Selecciona un evento" : eventoNombre;

            if (!hasData && !string.IsNullOrWhiteSpace(eventoNombre))
            {
                descriptor = $"{eventoNombre} (sin datos)";
            }

            lblForecastTitle.Text = $"Avance de Forecast — Evento: {descriptor}";
        }

        private void ClearForecastChart()
        {
            foreach (var seriesName in new[] { "Stock", "Vendido", "Diferencia", "Sobrevendido", "Promedio" })
            {
                if (chartForecast.Series.IndexOf(seriesName) >= 0)
                {
                    chartForecast.Series[seriesName].Points.Clear();
                }
            }

            chartForecast.Titles.Clear();
        }

        private async void comboEventosForecast_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboEventosForecast.SelectedItem is Evento evento)
            {
                await LoadForecastDataAsync(evento);
            }
            else
            {
                ClearForecastChart();
                UpdateForecastTitle(null, false);
            }
        }

        private void toggleOrientacionForecast_CheckedChanged(object sender, EventArgs e)
        {
            _isHorizontalForecastView = toggleOrientacionForecast.Checked;
            toggleOrientacionForecast.Text = _isHorizontalForecastView ? "Barras horizontales" : "Barras verticales";
            toggleOrientacionForecast.BackColor = _isHorizontalForecastView ? Color.FromArgb(0, 123, 255) : Color.FromArgb(52, 58, 64);
            ApplyChartOrientation();
        }

        private void btnExportarForecast_Click(object sender, EventArgs e)
        {
            if (chartForecast.Series["Stock"].Points.Count == 0 && chartForecast.Series["Vendido"].Points.Count == 0)
            {
                MessageBox.Show("No hay información para exportar el gráfico.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var evento = comboEventosForecast.SelectedItem as Evento;
            var eventoNombre = evento?.Nombre ?? "General";
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var directory = Path.Combine(desktopPath, "Banquetes", "Graficos");

            try
            {
                Directory.CreateDirectory(directory);
                var fileName = $"Forecast_{SanitizeFileName(eventoNombre)}_{DateTime.Now:yyyyMMdd_HHmm}.png";
                var fullPath = Path.Combine(directory, fileName);
                chartForecast.SaveImage(fullPath, ChartImageFormat.Png);
                MessageBox.Show($"Gráfico exportado correctamente en:{Environment.NewLine}{fullPath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo exportar el gráfico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string SanitizeFileName(string fileName)
        {
            foreach (var invalid in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalid, '_');
            }

            return fileName;
        }

        private class ForecastRegistro
        {
            public string Nombre { get; set; } = string.Empty;
            public string Articulo { get; set; } = string.Empty;
            public decimal Stock { get; set; }
            public decimal Vendido { get; set; }
            public decimal Diferencia => Stock - Vendido;
            public decimal Sobrevendido => Vendido > Stock ? Vendido - Stock : 0m;
        }
    }
}
