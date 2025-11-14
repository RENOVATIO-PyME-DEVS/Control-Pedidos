using System;
using System.Drawing;
using System.Windows.Forms;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.CheckIn
{
    /// <summary>
    /// Ventana modal que solicita confirmación antes de registrar el CheckIN de un pedido.
    /// </summary>
    public class CheckInConfirmForm : Form
    {
        private readonly PedidoCheckInfo _pedido;
        private Label _folioLabel;
        private Label _clienteLabel;
        private Label _fechaLabel;
        private Label _eventoLabel;

        public CheckInConfirmForm(PedidoCheckInfo pedido)
        {
            _pedido = pedido ?? throw new ArgumentNullException(nameof(pedido));
            InitializeComponent();
            CargarDatos();
        }

        /// <summary>
        /// Configura todos los elementos visuales del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            Text = "Confirmar CheckIN";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Size = new Size(520, 360);
            BackColor = Color.FromArgb(250, 250, 250);

            var tituloLabel = new Label
            {
                Text = "Confirmación de registro",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                ForeColor = Color.FromArgb(33, 33, 33),
                Location = new Point(24, 24)
            };

            var descripcionLabel = new Label
            {
                Text = "Revise la información del pedido antes de confirmar el CheckIN.",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(26, 74)
            };

            _folioLabel = CrearEtiquetaDato(new Point(26, 120));
            _clienteLabel = CrearEtiquetaDato(new Point(26, 160));
            _fechaLabel = CrearEtiquetaDato(new Point(26, 200));
            _eventoLabel = CrearEtiquetaDato(new Point(26, 240));

            var preguntaLabel = new Label
            {
                Text = "¿Desea registrar el CheckIN de este pedido?",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                ForeColor = Color.FromArgb(33, 33, 33),
                Location = new Point(26, 280)
            };

            var confirmarButton = new Button
            {
                Text = "Registrar CheckIN",
                DialogResult = DialogResult.OK,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point),
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 36),
                Location = new Point(300, 300)
            };
            confirmarButton.FlatAppearance.BorderSize = 0;

            var cancelarButton = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
                BackColor = Color.FromArgb(189, 189, 189),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 36),
                Location = new Point(190, 300)
            };
            cancelarButton.FlatAppearance.BorderSize = 0;

            Controls.Add(tituloLabel);
            Controls.Add(descripcionLabel);
            Controls.Add(_folioLabel);
            Controls.Add(_clienteLabel);
            Controls.Add(_fechaLabel);
            Controls.Add(_eventoLabel);
            Controls.Add(preguntaLabel);
            Controls.Add(confirmarButton);
            Controls.Add(cancelarButton);

            AcceptButton = confirmarButton;
            CancelButton = cancelarButton;
        }

        private Label CrearEtiquetaDato(Point location)
        {
            return new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(66, 66, 66),
                Location = location
            };
        }

        /// <summary>
        /// Pinta los datos del pedido en pantalla para que el usuario pueda revisarlos.
        /// </summary>
        private void CargarDatos()
        {
            _folioLabel.Text = $"Folio: {_pedido.FolioFormateado}";
            _clienteLabel.Text = $"Cliente: {_pedido.ClienteNombre}";
            _fechaLabel.Text = $"Entrega: {_pedido.FechaEntregaDescripcion}";
            var evento = string.IsNullOrWhiteSpace(_pedido.EventoNombre) ? "Sin evento" : _pedido.EventoNombre;
            _eventoLabel.Text = $"Evento: {evento}";
        }
    }
}
