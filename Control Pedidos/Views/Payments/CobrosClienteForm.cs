using System;
using System.ComponentModel;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Printing;

namespace Control_Pedidos.Views.Payments
{
    public partial class CobrosClienteForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly Cliente _cliente;
        private readonly CobroDao _cobroDao;
        private readonly CobroPrintingService _printingService = new CobroPrintingService();
        private readonly BindingList<Cobro> _cobros = new BindingList<Cobro>();
        private readonly BindingSource _bindingSource = new BindingSource();

        public CobrosClienteForm(DatabaseConnectionFactory connectionFactory, Cliente cliente)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            _cobroDao = new CobroDao(_connectionFactory);

            Text = $"Cobros de {ObtenerNombreCliente()}";
            clienteLabel.Text = $"Cliente: {ObtenerNombreCliente()}";

            ConfigureGrid();
            cobrosGrid.CellMouseDown += cobrosGrid_CellMouseDown;
        }

        private string ObtenerNombreCliente()
        {
            if (_cliente == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(_cliente.NombreComercial))
            {
                return _cliente.NombreComercial;
            }

            return _cliente.Nombre;
        }

        private void CobrosClienteForm_Load(object sender, EventArgs e)
        {
            CargarCobros();
        }

        private void ConfigureGrid()
        {
            cobrosGrid.AutoGenerateColumns = false;
            cobrosGrid.Columns.Clear();

            cobrosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cobro.CobroPedidoId),
                HeaderText = "Folio",
                Width = 70,
                ReadOnly = true
            });

            cobrosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cobro.Fecha),
                HeaderText = "Fecha",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" },
                ReadOnly = true
            });

            cobrosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cobro.FormaCobroNombre),
                HeaderText = "Forma de cobro",
                Width = 160,
                ReadOnly = true
            });

            cobrosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cobro.Monto),
                HeaderText = "Monto",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                },
                ReadOnly = true
            });

            cobrosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cobro.Estatus),
                HeaderText = "Estatus",
                Width = 120,
                ReadOnly = true
            });

            cobrosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Cobro.Impreso),
                HeaderText = "Impreso",
                Width = 90,
                ReadOnly = true
            });

            _bindingSource.DataSource = _cobros;
            cobrosGrid.DataSource = _bindingSource;
        }

        private void CargarCobros()
        {
            try
            {
                _cobros.Clear();
                foreach (var cobro in _cobroDao.ObtenerCobrosPorCliente(_cliente.Id))
                {
                    _cobros.Add(cobro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo obtener la lista de cobros: {ex.Message}", "Cobros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Cobro ObtenerCobroSeleccionado()
        {
            if (cobrosGrid.CurrentRow?.DataBoundItem is Cobro cobro)
            {
                return cobro;
            }

            return null;
        }

        private void reimprimirCobroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerCobroSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un cobro para reimprimir.", "Cobros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cobro cobroCompleto;
            try
            {
                cobroCompleto = _cobroDao.ObtenerCobroPorId(seleccionado.CobroPedidoId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo obtener el detalle del cobro: {ex.Message}", "Cobros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cobroCompleto == null)
            {
                MessageBox.Show("No se encontró la información del cobro seleccionado.", "Cobros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cobroCompleto.MostrarLeyendaCopia = cobroCompleto.EstaImpreso;

            var resultado = _printingService.Print(cobroCompleto, this);

            if (resultado.Printed)
            {
                _cobroDao.MarcarCobroImpreso(cobroCompleto.CobroPedidoId, true);
                seleccionado.Impreso = "S";
                cobroCompleto.Impreso = "S";
                _bindingSource.ResetBindings(false);
                MessageBox.Show("Ticket impreso correctamente.", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _cobroDao.MarcarCobroImpreso(cobroCompleto.CobroPedidoId, false);
            seleccionado.Impreso = "N";
            cobroCompleto.Impreso = "N";

            var mensaje = resultado.CancelledByUser ? "Impresión cancelada." : "Error al imprimir.";

            if (!string.IsNullOrWhiteSpace(resultado.PdfPath))
            {
                mensaje += "\n\nSe generó un archivo PDF con el ticket.";
                mensaje += $"\nRuta del archivo: {resultado.PdfPath}";
            }
            else if (!resultado.CancelledByUser)
            {
                mensaje += "\nNo se pudo generar el PDF automáticamente.";
            }

            if (resultado.Error != null && !resultado.CancelledByUser)
            {
                mensaje += $"\nDetalle: {resultado.Error.Message}";
            }

            if (resultado.PdfError != null)
            {
                mensaje += $"\nPDF: {resultado.PdfError.Message}";
            }

            MessageBox.Show(mensaje, "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _bindingSource.ResetBindings(false);
        }

        private void cerrarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cobrosGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                cobrosGrid.ClearSelection();
                cobrosGrid.Rows[e.RowIndex].Selected = true;
                cobrosGrid.CurrentCell = cobrosGrid.Rows[e.RowIndex].Cells[0];
            }
        }

        private void cobrosGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                reimprimirCobroToolStripMenuItem_Click(sender, EventArgs.Empty);
            }
        }

    }
}
