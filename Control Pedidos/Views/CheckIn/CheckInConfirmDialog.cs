using System;
using System.Drawing;
using System.Windows.Forms;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.CheckIn
{
    /// <summary>
    /// Diálogo grande que muestra la información del pedido y solicita confirmación para registrar el CheckIN.
    /// </summary>
    public partial class CheckInConfirmDialog : Form
    {
        private readonly PedidoCheckInfo _pedido;

        public CheckInConfirmDialog(PedidoCheckInfo pedido)
        {
            _pedido = pedido ?? throw new ArgumentNullException(nameof(pedido));

            InitializeComponent();
            CargarDatos();
        }

        /// <summary>
        /// Pinta en pantalla los datos clave del pedido para que el usuario pueda validarlos visualmente.
        /// </summary>
        private void CargarDatos()
        {
            lblFolioValor.Text = _pedido.FolioFormateado;
            lblClienteValor.Text = _pedido.ClienteNombre;
            lblEntregaValor.Text = _pedido.FechaEntregaDescripcion;
            lblEventoValor.Text = string.IsNullOrWhiteSpace(_pedido.EventoNombre) ? "Sin evento" : _pedido.EventoNombre;
            lblTotalValor.Text = _pedido.Total.ToString("C2");
            lblSaldoValor.Text = _pedido.SaldoPendiente.ToString("C2");

            if (!_pedido.EstaPagado)
            {
                lblSaldoValor.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                lblSaldoValor.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }

        /// <summary>
        /// Confirma la operación devolviendo DialogResult.OK al formulario padre.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
