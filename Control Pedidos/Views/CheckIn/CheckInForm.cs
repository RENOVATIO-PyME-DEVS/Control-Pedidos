using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.CheckIn
{
    /// <summary>
    /// Pantalla principal para realizar el CheckIN de los pedidos.
    /// </summary>
    public class CheckInForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly PedidoCheckDao _pedidoCheckDao;
        private readonly int _empresaId;

        private readonly BindingList<PedidoCheckInfo> _pedidos = new BindingList<PedidoCheckInfo>();
        private readonly ToolTip _toolTip = new ToolTip();

        private ComboBox _eventComboBox;
        private TextBox _searchTextBox;
        private TextBox _scanTextBox;
        private DataGridView _ordersGrid;
        private ContextMenuStrip _contextMenu;
        private bool _loadingEventos;

        public CheckInForm(DatabaseConnectionFactory connectionFactory, int empresaId)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _pedidoCheckDao = new PedidoCheckDao(connectionFactory);
            _empresaId = empresaId;

            InitializeComponent();
            ConfigurarGrid();
            CargarEventos();
            FocusScanInput();
        }

        /// <summary>
        /// Construye visualmente la pantalla.
        /// </summary>
        private void InitializeComponent()
        {
            Text = "CheckIN de pedidos";
            StartPosition = FormStartPosition.CenterParent;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.WhiteSmoke;

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(16)
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var filtrosPanel = new TableLayoutPanel
            {
                ColumnCount = 6,
                RowCount = 2,
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            filtrosPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var eventoLabel = new Label
            {
                Text = "Evento:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(0, 8, 8, 0)
            };

            _eventComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 16, 0),
                Width = 240
            };
            _eventComboBox.SelectedIndexChanged += EventComboBox_SelectedIndexChanged;

            var searchLabel = new Label
            {
                Text = "Buscar:",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(0, 8, 8, 0)
            };

            _searchTextBox = new TextBox
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 8, 0),
                Width = 240
            };
            _searchTextBox.KeyDown += SearchTextBox_KeyDown;

            var searchButton = new Button
            {
                Text = "Buscar",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 8, 0),
                AutoSize = true
            };
            searchButton.Click += (s, e) => CargarPedidos();

            var scannedButton = new Button
            {
                Text = "Pedidos Escaneados",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 4, 0, 0),
                AutoSize = true
            };
            scannedButton.Click += ScannedButton_Click;

            var scanLabel = new Label
            {
                Text = "Escanear código de barras:",
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point),
                Margin = new Padding(0, 12, 0, 4)
            };

            _scanTextBox = new TextBox
            {
                Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point),
                Margin = new Padding(0, 0, 0, 12),
                Width = 320
            };
            _scanTextBox.KeyDown += ScanTextBox_KeyDown;

            filtrosPanel.Controls.Add(eventoLabel, 0, 0);
            filtrosPanel.Controls.Add(_eventComboBox, 1, 0);
            filtrosPanel.Controls.Add(searchLabel, 2, 0);
            filtrosPanel.Controls.Add(_searchTextBox, 3, 0);
            filtrosPanel.Controls.Add(searchButton, 4, 0);
            filtrosPanel.Controls.Add(scannedButton, 5, 0);
            filtrosPanel.SetColumnSpan(scanLabel, 6);
            filtrosPanel.Controls.Add(scanLabel, 0, 1);
            filtrosPanel.Controls.Add(_scanTextBox, 1, 1);

            _ordersGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                DataSource = _pedidos
            };
            _ordersGrid.CellMouseDown += OrdersGrid_CellMouseDown;

            _contextMenu = new ContextMenuStrip();
            var checkInMenuItem = new ToolStripMenuItem("Registrar CheckIN");
            checkInMenuItem.Click += (s, e) => RegistrarCheckInManual();
            _contextMenu.Items.Add(checkInMenuItem);
            _ordersGrid.ContextMenuStrip = _contextMenu;

            mainPanel.Controls.Add(filtrosPanel, 0, 0);
            mainPanel.Controls.Add(_ordersGrid, 0, 1);
            Controls.Add(mainPanel);

            _toolTip.SetToolTip(_eventComboBox, "Seleccione el evento del que desea ver los pedidos. Si existe un evento del día se seleccionará automáticamente.");
            _toolTip.SetToolTip(_searchTextBox, "Ingrese parte del nombre del cliente o el folio del pedido y presione Enter o el botón Buscar.");
            _toolTip.SetToolTip(scannedButton, "Abre una vista con los pedidos que ya fueron escaneados (estatus CI).");
            _toolTip.SetToolTip(_scanTextBox, "Coloque el cursor aquí y escanee el código de barras del pedido. Presione Enter si el lector no envía el Enter automáticamente.");
        }

        /// <summary>
        /// Define las columnas del DataGridView para mostrar la información relevante.
        /// </summary>
        private void ConfigurarGrid()
        {
            _ordersGrid.AutoGenerateColumns = false;
            _ordersGrid.Columns.Clear();

            _ordersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FolioFormateado),
                HeaderText = "Folio",
                Width = 120,
                MinimumWidth = 100
            });

            _ordersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.ClienteNombre),
                HeaderText = "Cliente",
                Width = 220,
                MinimumWidth = 180
            });

            _ordersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.FechaEntregaDescripcion),
                HeaderText = "Entrega",
                Width = 160,
                MinimumWidth = 140
            });

            _ordersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.EventoNombre),
                HeaderText = "Evento",
                Width = 200,
                MinimumWidth = 150
            });

            var saldoColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(PedidoCheckInfo.SaldoPendiente),
                HeaderText = "Saldo",
                Width = 120,
                MinimumWidth = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            };
            _ordersGrid.Columns.Add(saldoColumn);
        }

        /// <summary>
        /// Obtiene el listado de eventos y establece el evento del día si existe.
        /// </summary>
        private void CargarEventos()
        {
            try
            {
                _loadingEventos = true;
                _eventComboBox.Items.Clear();
                var eventos = _pedidoCheckDao.ObtenerEventosPorEmpresa(_empresaId)
                    .OrderByDescending(e => e.FechaEvento)
                    .ToList();

                _eventComboBox.Items.Add(new EventoComboItem(null, "Sin evento", true));

                EventoComboItem eventoHoyItem = null;
                foreach (var evento in eventos)
                {
                    var descripcion = $"{evento.Nombre} ({evento.FechaEvento:dd/MM/yyyy})";
                    var item = new EventoComboItem(evento.Id, descripcion, false);
                    _eventComboBox.Items.Add(item);

                    if (evento.FechaEvento.Date == DateTime.Today && eventoHoyItem == null)
                    {
                        eventoHoyItem = item;
                    }
                }

                if (eventoHoyItem != null)
                {
                    _eventComboBox.SelectedItem = eventoHoyItem;
                }
                else
                {
                    _eventComboBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los eventos: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingEventos = false;
                CargarPedidos();
            }
        }

        /// <summary>
        /// Recupera la lista de pedidos pendientes (estatus N) aplicando los filtros de búsqueda.
        /// </summary>
        private void CargarPedidos()
        {
            if (_loadingEventos)
            {
                return;
            }

            try
            {
                var filtro = _searchTextBox.Text?.Trim() ?? string.Empty;
                var eventoId = ObtenerEventoSeleccionado(out var sinEvento);

                var pedidos = _pedidoCheckDao.ObtenerPedidosPorEstatus(
                    _empresaId,
                    "N",
                    eventoId,
                    sinEvento,
                    filtro,
                    incluirProductos: false,
                    ordenarPorFechaCheckIn: false);

                _pedidos.Clear();
                foreach (var pedido in pedidos)
                {
                    _pedidos.Add(pedido);
                }

                if (_pedidos.Count == 0)
                {
                    _ordersGrid.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los pedidos: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? ObtenerEventoSeleccionado(out bool sinEvento)
        {
            sinEvento = false;
            if (_eventComboBox.SelectedItem is EventoComboItem item)
            {
                sinEvento = item.EsSinEvento;
                return item.EventoId;
            }

            return null;
        }

        private void EventComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingEventos)
            {
                return;
            }

            CargarPedidos();
            FocusScanInput();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                CargarPedidos();
            }
        }

        private void ScanTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ProcesarEscaneo();
            }
        }

        private void ProcesarEscaneo()
        {
            var codigo = _scanTextBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(codigo))
            {
                return;
            }

            try
            {
                var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
                var pedido = _pedidoCheckDao.ObtenerPedidoPorCodigo(_empresaId, codigo, "N");
                if (pedido == null)
                {
                    MessageBox.Show("No se encontró un pedido pendiente con ese folio.", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!_pedidoCheckDao.ValidarPedidoParaCheckIn(_empresaId, pedido.PedidoId, eventoId, sinEvento, out var mensaje, out var informacionActualizada))
                {
                    MessageBox.Show(mensaje, "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var confirmForm = new CheckInConfirmForm(informacionActualizada))
                {
                    if (confirmForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (_pedidoCheckDao.RegistrarCheckIn(informacionActualizada.PedidoId, out var error))
                        {
                            MessageBox.Show("CheckIN registrado correctamente.", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarPedidos();
                        }
                        else
                        {
                            MessageBox.Show(error, "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el CheckIN: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _scanTextBox.SelectAll();
            }
        }

        private void OrdersGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                _ordersGrid.ClearSelection();
                _ordersGrid.Rows[e.RowIndex].Selected = true;
            }
        }

        private void RegistrarCheckInManual()
        {
            //if (_ordersGrid.CurrentRow?.DataBoundItem is not PedidoCheckInfo pedidoSeleccionado)
            //{
            //    return;
            //}
            if (!(_ordersGrid.CurrentRow?.DataBoundItem is PedidoCheckInfo pedidoSeleccionado))
            {
                return;
            }


            try
            {
                var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
                if (!_pedidoCheckDao.ValidarPedidoParaCheckIn(_empresaId, pedidoSeleccionado.PedidoId, eventoId, sinEvento, out var mensaje, out var informacion))
                {
                    MessageBox.Show(mensaje, "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var confirmForm = new CheckInConfirmForm(informacion))
                {
                    if (confirmForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (_pedidoCheckDao.RegistrarCheckIn(informacion.PedidoId, out var error))
                        {
                            MessageBox.Show("CheckIN registrado correctamente.", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarPedidos();
                        }
                        else
                        {
                            MessageBox.Show(error, "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el CheckIN: {ex.Message}", "CheckIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScannedButton_Click(object sender, EventArgs e)
        {
            var eventoId = ObtenerEventoSeleccionado(out var sinEvento);
            var descripcion = _eventComboBox.SelectedItem?.ToString() ?? "Todos los eventos";
            using (var form = new CheckInScannedForm(_connectionFactory, _empresaId, eventoId, sinEvento, descripcion))
            {
                form.ShowDialog(this);
            }
        }

        private void FocusScanInput()
        {
            BeginInvoke(new Action(() =>
            {
                _scanTextBox.Focus();
                _scanTextBox.SelectAll();
            }));
        }

        private sealed class EventoComboItem
        {
            public EventoComboItem(int? eventoId, string descripcion, bool esSinEvento)
            {
                EventoId = eventoId;
                Descripcion = descripcion;
                EsSinEvento = esSinEvento;
            }

            public int? EventoId { get; }
            public string Descripcion { get; }
            public bool EsSinEvento { get; }

            public override string ToString() => Descripcion;
        }
    }
}
