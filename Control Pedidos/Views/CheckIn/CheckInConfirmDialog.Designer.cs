using System.Windows.Forms;

namespace Control_Pedidos.Views.CheckIn
{
    partial class CheckInConfirmDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.datosTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblFolio = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblEntrega = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.lblFolioValor = new System.Windows.Forms.Label();
            this.lblClienteValor = new System.Windows.Forms.Label();
            this.lblEntregaValor = new System.Windows.Forms.Label();
            this.lblEventoValor = new System.Windows.Forms.Label();
            this.lblTotalValor = new System.Windows.Forms.Label();
            this.lblSaldoValor = new System.Windows.Forms.Label();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.buttonsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.mainTableLayoutPanel.SuspendLayout();
            this.datosTableLayoutPanel.SuspendLayout();
            this.buttonsFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.lblTitulo, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.datosTableLayoutPanel, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.lblPregunta, 0, 2);
            this.mainTableLayoutPanel.Controls.Add(this.buttonsFlowLayoutPanel, 0, 3);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 4;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(805, 397);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(799, 41);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Confirmar CheckIN";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // datosTableLayoutPanel
            // 
            this.datosTableLayoutPanel.ColumnCount = 2;
            this.datosTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.datosTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.datosTableLayoutPanel.Controls.Add(this.lblFolio, 0, 0);
            this.datosTableLayoutPanel.Controls.Add(this.lblCliente, 0, 1);
            this.datosTableLayoutPanel.Controls.Add(this.lblEntrega, 0, 2);
            this.datosTableLayoutPanel.Controls.Add(this.lblEvento, 0, 3);
            this.datosTableLayoutPanel.Controls.Add(this.lblTotal, 0, 4);
            this.datosTableLayoutPanel.Controls.Add(this.lblSaldo, 0, 5);
            this.datosTableLayoutPanel.Controls.Add(this.lblFolioValor, 1, 0);
            this.datosTableLayoutPanel.Controls.Add(this.lblClienteValor, 1, 1);
            this.datosTableLayoutPanel.Controls.Add(this.lblEntregaValor, 1, 2);
            this.datosTableLayoutPanel.Controls.Add(this.lblEventoValor, 1, 3);
            this.datosTableLayoutPanel.Controls.Add(this.lblTotalValor, 1, 4);
            this.datosTableLayoutPanel.Controls.Add(this.lblSaldoValor, 1, 5);
            this.datosTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datosTableLayoutPanel.Location = new System.Drawing.Point(3, 51);
            this.datosTableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.datosTableLayoutPanel.Name = "datosTableLayoutPanel";
            this.datosTableLayoutPanel.RowCount = 6;
            this.datosTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.datosTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.datosTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.datosTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.datosTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.datosTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.datosTableLayoutPanel.Size = new System.Drawing.Size(799, 230);
            this.datosTableLayoutPanel.TabIndex = 1;
            // 
            // lblFolio
            // 
            this.lblFolio.AutoSize = true;
            this.lblFolio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFolio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFolio.Location = new System.Drawing.Point(3, 0);
            this.lblFolio.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(273, 33);
            this.lblFolio.TabIndex = 0;
            this.lblFolio.Text = "Folio";
            this.lblFolio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCliente.Location = new System.Drawing.Point(3, 38);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(273, 33);
            this.lblCliente.TabIndex = 1;
            this.lblCliente.Text = "Cliente";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEntrega
            // 
            this.lblEntrega.AutoSize = true;
            this.lblEntrega.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntrega.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblEntrega.Location = new System.Drawing.Point(3, 76);
            this.lblEntrega.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblEntrega.Name = "lblEntrega";
            this.lblEntrega.Size = new System.Drawing.Size(273, 33);
            this.lblEntrega.TabIndex = 2;
            this.lblEntrega.Text = "Fecha y hora";
            this.lblEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblEvento.Location = new System.Drawing.Point(3, 114);
            this.lblEvento.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(273, 33);
            this.lblEvento.TabIndex = 3;
            this.lblEvento.Text = "Evento";
            this.lblEvento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(3, 152);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(273, 33);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSaldo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSaldo.Location = new System.Drawing.Point(3, 190);
            this.lblSaldo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(273, 35);
            this.lblSaldo.TabIndex = 5;
            this.lblSaldo.Text = "Saldo pendiente";
            this.lblSaldo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFolioValor
            // 
            this.lblFolioValor.AutoSize = true;
            this.lblFolioValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFolioValor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblFolioValor.Location = new System.Drawing.Point(282, 0);
            this.lblFolioValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblFolioValor.Name = "lblFolioValor";
            this.lblFolioValor.Size = new System.Drawing.Size(514, 33);
            this.lblFolioValor.TabIndex = 6;
            this.lblFolioValor.Text = "-";
            this.lblFolioValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClienteValor
            // 
            this.lblClienteValor.AutoSize = true;
            this.lblClienteValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClienteValor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblClienteValor.Location = new System.Drawing.Point(282, 38);
            this.lblClienteValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblClienteValor.Name = "lblClienteValor";
            this.lblClienteValor.Size = new System.Drawing.Size(514, 33);
            this.lblClienteValor.TabIndex = 7;
            this.lblClienteValor.Text = "-";
            this.lblClienteValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEntregaValor
            // 
            this.lblEntregaValor.AutoSize = true;
            this.lblEntregaValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntregaValor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblEntregaValor.Location = new System.Drawing.Point(282, 76);
            this.lblEntregaValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblEntregaValor.Name = "lblEntregaValor";
            this.lblEntregaValor.Size = new System.Drawing.Size(514, 33);
            this.lblEntregaValor.TabIndex = 8;
            this.lblEntregaValor.Text = "-";
            this.lblEntregaValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEventoValor
            // 
            this.lblEventoValor.AutoSize = true;
            this.lblEventoValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEventoValor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblEventoValor.Location = new System.Drawing.Point(282, 114);
            this.lblEventoValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblEventoValor.Name = "lblEventoValor";
            this.lblEventoValor.Size = new System.Drawing.Size(514, 33);
            this.lblEventoValor.TabIndex = 9;
            this.lblEventoValor.Text = "-";
            this.lblEventoValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalValor
            // 
            this.lblTotalValor.AutoSize = true;
            this.lblTotalValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalValor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTotalValor.Location = new System.Drawing.Point(282, 152);
            this.lblTotalValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblTotalValor.Name = "lblTotalValor";
            this.lblTotalValor.Size = new System.Drawing.Size(514, 33);
            this.lblTotalValor.TabIndex = 10;
            this.lblTotalValor.Text = "-";
            this.lblTotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSaldoValor
            // 
            this.lblSaldoValor.AutoSize = true;
            this.lblSaldoValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSaldoValor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSaldoValor.Location = new System.Drawing.Point(282, 190);
            this.lblSaldoValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblSaldoValor.Name = "lblSaldoValor";
            this.lblSaldoValor.Size = new System.Drawing.Size(514, 35);
            this.lblSaldoValor.TabIndex = 11;
            this.lblSaldoValor.Text = "-";
            this.lblSaldoValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPregunta
            // 
            this.lblPregunta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPregunta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.Location = new System.Drawing.Point(3, 291);
            this.lblPregunta.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(799, 34);
            this.lblPregunta.TabIndex = 2;
            this.lblPregunta.Text = "¿Desea registrar el CheckIN para este pedido?";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonsFlowLayoutPanel
            // 
            this.buttonsFlowLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonsFlowLayoutPanel.AutoSize = true;
            this.buttonsFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonsFlowLayoutPanel.Controls.Add(this.btnConfirmar);
            this.buttonsFlowLayoutPanel.Controls.Add(this.btnCancelar);
            this.buttonsFlowLayoutPanel.Location = new System.Drawing.Point(243, 335);
            this.buttonsFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.buttonsFlowLayoutPanel.Name = "buttonsFlowLayoutPanel";
            this.buttonsFlowLayoutPanel.Size = new System.Drawing.Size(319, 52);
            this.buttonsFlowLayoutPanel.TabIndex = 3;
            this.buttonsFlowLayoutPanel.WrapContents = false;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.AutoSize = true;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.Location = new System.Drawing.Point(3, 2);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(3, 2, 12, 2);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Padding = new System.Windows.Forms.Padding(12, 5, 12, 5);
            this.btnConfirmar.Size = new System.Drawing.Size(178, 48);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnCancelar.Location = new System.Drawing.Point(196, 2);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(12, 5, 12, 5);
            this.btnCancelar.Size = new System.Drawing.Size(120, 48);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // CheckInConfirmDialog
            // 
            this.AcceptButton = this.btnConfirmar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(805, 397);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckInConfirmDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Confirmar CheckIN";
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.datosTableLayoutPanel.ResumeLayout(false);
            this.datosTableLayoutPanel.PerformLayout();
            this.buttonsFlowLayoutPanel.ResumeLayout(false);
            this.buttonsFlowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TableLayoutPanel datosTableLayoutPanel;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblEntrega;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSaldo;
        private System.Windows.Forms.Label lblFolioValor;
        private System.Windows.Forms.Label lblClienteValor;
        private System.Windows.Forms.Label lblEntregaValor;
        private System.Windows.Forms.Label lblEventoValor;
        private System.Windows.Forms.Label lblTotalValor;
        private System.Windows.Forms.Label lblSaldoValor;
        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.FlowLayoutPanel buttonsFlowLayoutPanel;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
