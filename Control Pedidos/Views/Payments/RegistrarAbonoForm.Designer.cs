namespace Control_Pedidos.Views.Payments
{
    partial class RegistrarAbonoForm
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

        private void InitializeComponent()
        {
            this.clienteLabel = new System.Windows.Forms.Label();
            this.clienteTextBox = new System.Windows.Forms.TextBox();
            this.saldoActualLabel = new System.Windows.Forms.Label();
            this.saldoActualValueLabel = new System.Windows.Forms.Label();
            this.saldoDespuesLabel = new System.Windows.Forms.Label();
            this.saldoDespuesValueLabel = new System.Windows.Forms.Label();
            this.formaCobroLabel = new System.Windows.Forms.Label();
            this.formaCobroComboBox = new System.Windows.Forms.ComboBox();
            this.montoLabel = new System.Windows.Forms.Label();
            this.montoNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pedidosLabel = new System.Windows.Forms.Label();
            this.pedidosGrid = new System.Windows.Forms.DataGridView();
            this.resumenLabel = new System.Windows.Forms.Label();
            this.resumenTextBox = new System.Windows.Forms.TextBox();
            this.registrarButton = new System.Windows.Forms.Button();
            this.cancelarButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.montoNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // clienteLabel
            // 
            this.clienteLabel.AutoSize = true;
            this.clienteLabel.Location = new System.Drawing.Point(12, 15);
            this.clienteLabel.Name = "clienteLabel";
            this.clienteLabel.Size = new System.Drawing.Size(50, 15);
            this.clienteLabel.TabIndex = 0;
            this.clienteLabel.Text = "Cliente:";
            // 
            // clienteTextBox
            // 
            this.clienteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clienteTextBox.Location = new System.Drawing.Point(120, 12);
            this.clienteTextBox.Name = "clienteTextBox";
            this.clienteTextBox.ReadOnly = true;
            this.clienteTextBox.Size = new System.Drawing.Size(448, 23);
            this.clienteTextBox.TabIndex = 1;
            this.clienteTextBox.TabStop = false;
            // 
            // saldoActualLabel
            // 
            this.saldoActualLabel.AutoSize = true;
            this.saldoActualLabel.Location = new System.Drawing.Point(12, 45);
            this.saldoActualLabel.Name = "saldoActualLabel";
            this.saldoActualLabel.Size = new System.Drawing.Size(77, 15);
            this.saldoActualLabel.TabIndex = 2;
            this.saldoActualLabel.Text = "Saldo actual:";
            // 
            // saldoActualValueLabel
            // 
            this.saldoActualValueLabel.AutoSize = true;
            this.saldoActualValueLabel.Location = new System.Drawing.Point(118, 45);
            this.saldoActualValueLabel.Name = "saldoActualValueLabel";
            this.saldoActualValueLabel.Size = new System.Drawing.Size(34, 15);
            this.saldoActualValueLabel.TabIndex = 3;
            this.saldoActualValueLabel.Text = "$0.00";
            // 
            // saldoDespuesLabel
            // 
            this.saldoDespuesLabel.AutoSize = true;
            this.saldoDespuesLabel.Location = new System.Drawing.Point(284, 45);
            this.saldoDespuesLabel.Name = "saldoDespuesLabel";
            this.saldoDespuesLabel.Size = new System.Drawing.Size(121, 15);
            this.saldoDespuesLabel.TabIndex = 4;
            this.saldoDespuesLabel.Text = "Saldo después abono:";
            // 
            // saldoDespuesValueLabel
            // 
            this.saldoDespuesValueLabel.AutoSize = true;
            this.saldoDespuesValueLabel.Location = new System.Drawing.Point(411, 45);
            this.saldoDespuesValueLabel.Name = "saldoDespuesValueLabel";
            this.saldoDespuesValueLabel.Size = new System.Drawing.Size(34, 15);
            this.saldoDespuesValueLabel.TabIndex = 5;
            this.saldoDespuesValueLabel.Text = "$0.00";
            // 
            // formaCobroLabel
            // 
            this.formaCobroLabel.AutoSize = true;
            this.formaCobroLabel.Location = new System.Drawing.Point(12, 78);
            this.formaCobroLabel.Name = "formaCobroLabel";
            this.formaCobroLabel.Size = new System.Drawing.Size(102, 15);
            this.formaCobroLabel.TabIndex = 6;
            this.formaCobroLabel.Text = "Forma de cobro:";
            // 
            // formaCobroComboBox
            // 
            this.formaCobroComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formaCobroComboBox.FormattingEnabled = true;
            this.formaCobroComboBox.Location = new System.Drawing.Point(120, 75);
            this.formaCobroComboBox.Name = "formaCobroComboBox";
            this.formaCobroComboBox.Size = new System.Drawing.Size(220, 23);
            this.formaCobroComboBox.TabIndex = 0;
            this.formaCobroComboBox.SelectedIndexChanged += new System.EventHandler(this.formaCobroComboBox_SelectedIndexChanged);
            // 
            // montoLabel
            // 
            this.montoLabel.AutoSize = true;
            this.montoLabel.Location = new System.Drawing.Point(360, 78);
            this.montoLabel.Name = "montoLabel";
            this.montoLabel.Size = new System.Drawing.Size(96, 15);
            this.montoLabel.TabIndex = 7;
            this.montoLabel.Text = "Monto del abono:";
            // 
            // montoNumericUpDown
            // 
            this.montoNumericUpDown.DecimalPlaces = 2;
            this.montoNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.montoNumericUpDown.Location = new System.Drawing.Point(462, 75);
            this.montoNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.montoNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.montoNumericUpDown.Name = "montoNumericUpDown";
            this.montoNumericUpDown.Size = new System.Drawing.Size(106, 23);
            this.montoNumericUpDown.TabIndex = 1;
            this.montoNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.montoNumericUpDown.ValueChanged += new System.EventHandler(this.montoNumericUpDown_ValueChanged);
            // 
            // pedidosLabel
            // 
            this.pedidosLabel.AutoSize = true;
            this.pedidosLabel.Location = new System.Drawing.Point(12, 111);
            this.pedidosLabel.Name = "pedidosLabel";
            this.pedidosLabel.Size = new System.Drawing.Size(125, 15);
            this.pedidosLabel.TabIndex = 8;
            this.pedidosLabel.Text = "Pedidos con saldo (N):";
            // 
            // pedidosGrid
            // 
            this.pedidosGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pedidosGrid.Location = new System.Drawing.Point(15, 129);
            this.pedidosGrid.Name = "pedidosGrid";
            this.pedidosGrid.Size = new System.Drawing.Size(553, 190);
            this.pedidosGrid.TabIndex = 9;
            this.pedidosGrid.TabStop = false;
            // 
            // resumenLabel
            // 
            this.resumenLabel.AutoSize = true;
            this.resumenLabel.Location = new System.Drawing.Point(12, 329);
            this.resumenLabel.Name = "resumenLabel";
            this.resumenLabel.Size = new System.Drawing.Size(60, 15);
            this.resumenLabel.TabIndex = 10;
            this.resumenLabel.Text = "Resumen:";
            // 
            // resumenTextBox
            // 
            this.resumenTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resumenTextBox.Location = new System.Drawing.Point(15, 347);
            this.resumenTextBox.Multiline = true;
            this.resumenTextBox.Name = "resumenTextBox";
            this.resumenTextBox.ReadOnly = true;
            this.resumenTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resumenTextBox.Size = new System.Drawing.Size(553, 90);
            this.resumenTextBox.TabIndex = 11;
            this.resumenTextBox.TabStop = false;
            // 
            // registrarButton
            // 
            this.registrarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.registrarButton.Location = new System.Drawing.Point(372, 448);
            this.registrarButton.Name = "registrarButton";
            this.registrarButton.Size = new System.Drawing.Size(95, 27);
            this.registrarButton.TabIndex = 2;
            this.registrarButton.Text = "Registrar";
            this.registrarButton.UseVisualStyleBackColor = true;
            this.registrarButton.Click += new System.EventHandler(this.registrarButton_Click);
            // 
            // cancelarButton
            // 
            this.cancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelarButton.Location = new System.Drawing.Point(473, 448);
            this.cancelarButton.Name = "cancelarButton";
            this.cancelarButton.Size = new System.Drawing.Size(95, 27);
            this.cancelarButton.TabIndex = 3;
            this.cancelarButton.Text = "Cancelar";
            this.cancelarButton.UseVisualStyleBackColor = true;
            this.cancelarButton.Click += new System.EventHandler(this.cancelarButton_Click);
            // 
            // RegistrarAbonoForm
            // 
            this.AcceptButton = this.registrarButton;
            this.CancelButton = this.cancelarButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 487);
            this.Controls.Add(this.cancelarButton);
            this.Controls.Add(this.registrarButton);
            this.Controls.Add(this.resumenTextBox);
            this.Controls.Add(this.resumenLabel);
            this.Controls.Add(this.pedidosGrid);
            this.Controls.Add(this.pedidosLabel);
            this.Controls.Add(this.montoNumericUpDown);
            this.Controls.Add(this.montoLabel);
            this.Controls.Add(this.formaCobroComboBox);
            this.Controls.Add(this.formaCobroLabel);
            this.Controls.Add(this.saldoDespuesValueLabel);
            this.Controls.Add(this.saldoDespuesLabel);
            this.Controls.Add(this.saldoActualValueLabel);
            this.Controls.Add(this.saldoActualLabel);
            this.Controls.Add(this.clienteTextBox);
            this.Controls.Add(this.clienteLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegistrarAbonoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar abono";
            ((System.ComponentModel.ISupportInitialize)(this.montoNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label clienteLabel;
        private System.Windows.Forms.TextBox clienteTextBox;
        private System.Windows.Forms.Label saldoActualLabel;
        private System.Windows.Forms.Label saldoActualValueLabel;
        private System.Windows.Forms.Label saldoDespuesLabel;
        private System.Windows.Forms.Label saldoDespuesValueLabel;
        private System.Windows.Forms.Label formaCobroLabel;
        private System.Windows.Forms.ComboBox formaCobroComboBox;
        private System.Windows.Forms.Label montoLabel;
        private System.Windows.Forms.NumericUpDown montoNumericUpDown;
        private System.Windows.Forms.Label pedidosLabel;
        private System.Windows.Forms.DataGridView pedidosGrid;
        private System.Windows.Forms.Label resumenLabel;
        private System.Windows.Forms.TextBox resumenTextBox;
        private System.Windows.Forms.Button registrarButton;
        private System.Windows.Forms.Button cancelarButton;
    }
}
