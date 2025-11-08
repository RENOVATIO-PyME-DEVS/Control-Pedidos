namespace Control_Pedidos.Views.Payments
{
    partial class RegisterAbonoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clienteLabel = new System.Windows.Forms.Label();
            this.clienteTextBox = new System.Windows.Forms.TextBox();
            this.saldoClienteLabel = new System.Windows.Forms.Label();
            this.saldoClienteTextBox = new System.Windows.Forms.TextBox();
            this.formaCobroLabel = new System.Windows.Forms.Label();
            this.formaCobroComboBox = new System.Windows.Forms.ComboBox();
            this.montoLabel = new System.Windows.Forms.Label();
            this.montoNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saldoRestanteTitleLabel = new System.Windows.Forms.Label();
            this.saldoRestanteLabel = new System.Windows.Forms.Label();
            this.pedidosGrid = new System.Windows.Forms.DataGridView();
            this.folioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaEntregaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.abonadoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.montoAsignadoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resumenLabel = new System.Windows.Forms.Label();
            this.resumenTextBox = new System.Windows.Forms.TextBox();
            this.cancelarButton = new System.Windows.Forms.Button();
            this.guardarButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.panelResumen = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.montoNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosGrid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.panelResumen.SuspendLayout();
            this.SuspendLayout();
            // 
            // clienteLabel
            // 
            this.clienteLabel.AutoSize = true;
            this.clienteLabel.Location = new System.Drawing.Point(12, 15);
            this.clienteLabel.Name = "clienteLabel";
            this.clienteLabel.Size = new System.Drawing.Size(45, 13);
            this.clienteLabel.TabIndex = 0;
            this.clienteLabel.Text = "Cliente:";
            // 
            // clienteTextBox
            // 
            this.clienteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clienteTextBox.Location = new System.Drawing.Point(110, 12);
            this.clienteTextBox.Name = "clienteTextBox";
            this.clienteTextBox.ReadOnly = true;
            this.clienteTextBox.Size = new System.Drawing.Size(348, 20);
            this.clienteTextBox.TabIndex = 1;
            this.clienteTextBox.TabStop = false;
            // 
            // saldoClienteLabel
            // 
            this.saldoClienteLabel.AutoSize = true;
            this.saldoClienteLabel.Location = new System.Drawing.Point(12, 44);
            this.saldoClienteLabel.Name = "saldoClienteLabel";
            this.saldoClienteLabel.Size = new System.Drawing.Size(77, 13);
            this.saldoClienteLabel.TabIndex = 2;
            this.saldoClienteLabel.Text = "Saldo cliente:";
            // 
            // saldoClienteTextBox
            // 
            this.saldoClienteTextBox.Location = new System.Drawing.Point(110, 41);
            this.saldoClienteTextBox.Name = "saldoClienteTextBox";
            this.saldoClienteTextBox.ReadOnly = true;
            this.saldoClienteTextBox.Size = new System.Drawing.Size(120, 20);
            this.saldoClienteTextBox.TabIndex = 3;
            this.saldoClienteTextBox.TabStop = false;
            // 
            // formaCobroLabel
            // 
            this.formaCobroLabel.AutoSize = true;
            this.formaCobroLabel.Location = new System.Drawing.Point(250, 44);
            this.formaCobroLabel.Name = "formaCobroLabel";
            this.formaCobroLabel.Size = new System.Drawing.Size(77, 13);
            this.formaCobroLabel.TabIndex = 4;
            this.formaCobroLabel.Text = "Forma cobro:";
            // 
            // formaCobroComboBox
            // 
            this.formaCobroComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.formaCobroComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formaCobroComboBox.FormattingEnabled = true;
            this.formaCobroComboBox.Location = new System.Drawing.Point(333, 41);
            this.formaCobroComboBox.Name = "formaCobroComboBox";
            this.formaCobroComboBox.Size = new System.Drawing.Size(125, 21);
            this.formaCobroComboBox.TabIndex = 5;
            this.formaCobroComboBox.SelectedIndexChanged += new System.EventHandler(this.formaCobroComboBox_SelectedIndexChanged);
            // 
            // montoLabel
            // 
            this.montoLabel.AutoSize = true;
            this.montoLabel.Location = new System.Drawing.Point(12, 74);
            this.montoLabel.Name = "montoLabel";
            this.montoLabel.Size = new System.Drawing.Size(73, 13);
            this.montoLabel.TabIndex = 6;
            this.montoLabel.Text = "Monto abono:";
            // 
            // montoNumericUpDown
            // 
            this.montoNumericUpDown.DecimalPlaces = 2;
            this.montoNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.montoNumericUpDown.Location = new System.Drawing.Point(110, 72);
            this.montoNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.montoNumericUpDown.Name = "montoNumericUpDown";
            this.montoNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.montoNumericUpDown.TabIndex = 7;
            this.montoNumericUpDown.ValueChanged += new System.EventHandler(this.montoNumericUpDown_ValueChanged);
            // 
            // saldoRestanteTitleLabel
            // 
            this.saldoRestanteTitleLabel.AutoSize = true;
            this.saldoRestanteTitleLabel.Location = new System.Drawing.Point(250, 74);
            this.saldoRestanteTitleLabel.Name = "saldoRestanteTitleLabel";
            this.saldoRestanteTitleLabel.Size = new System.Drawing.Size(82, 13);
            this.saldoRestanteTitleLabel.TabIndex = 8;
            this.saldoRestanteTitleLabel.Text = "Saldo restante:";
            // 
            // saldoRestanteLabel
            // 
            this.saldoRestanteLabel.AutoSize = true;
            this.saldoRestanteLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.saldoRestanteLabel.Location = new System.Drawing.Point(338, 72);
            this.saldoRestanteLabel.Name = "saldoRestanteLabel";
            this.saldoRestanteLabel.Size = new System.Drawing.Size(29, 15);
            this.saldoRestanteLabel.TabIndex = 9;
            this.saldoRestanteLabel.Text = "$0.0";
            // 
            // pedidosGrid
            // 
            this.pedidosGrid.AllowUserToAddRows = false;
            this.pedidosGrid.AllowUserToDeleteRows = false;
            this.pedidosGrid.AllowUserToResizeRows = false;
            this.pedidosGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.pedidosGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pedidosGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.folioColumn,
            this.fechaEntregaColumn,
            this.totalColumn,
            this.abonadoColumn,
            this.saldoColumn,
            this.montoAsignadoColumn});
            this.folioColumn.DataPropertyName = "Folio";
            this.folioColumn.HeaderText = "Folio";
            this.folioColumn.Name = "folioColumn";
            this.folioColumn.ReadOnly = true;
            this.fechaEntregaColumn.DataPropertyName = "FechaEntrega";
            this.fechaEntregaColumn.HeaderText = "Fecha entrega";
            this.fechaEntregaColumn.Name = "fechaEntregaColumn";
            this.fechaEntregaColumn.ReadOnly = true;
            this.fechaEntregaColumn.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "dd/MM/yyyy" };
            this.totalColumn.DataPropertyName = "Total";
            this.totalColumn.HeaderText = "Total";
            this.totalColumn.Name = "totalColumn";
            this.totalColumn.ReadOnly = true;
            this.totalColumn.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "C2" };
            this.abonadoColumn.DataPropertyName = "Abonado";
            this.abonadoColumn.HeaderText = "Abonado";
            this.abonadoColumn.Name = "abonadoColumn";
            this.abonadoColumn.ReadOnly = true;
            this.abonadoColumn.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "C2" };
            this.saldoColumn.DataPropertyName = "Saldo";
            this.saldoColumn.HeaderText = "Saldo";
            this.saldoColumn.Name = "saldoColumn";
            this.saldoColumn.ReadOnly = true;
            this.saldoColumn.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "C2" };
            this.montoAsignadoColumn.DataPropertyName = "MontoAsignado";
            this.montoAsignadoColumn.HeaderText = "Monto asignado";
            this.montoAsignadoColumn.Name = "montoAsignadoColumn";
            this.montoAsignadoColumn.ReadOnly = true;
            this.montoAsignadoColumn.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "C2" };
            this.pedidosGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pedidosGrid.Location = new System.Drawing.Point(3, 3);
            this.pedidosGrid.MultiSelect = false;
            this.pedidosGrid.Name = "pedidosGrid";
            this.pedidosGrid.ReadOnly = true;
            this.pedidosGrid.RowHeadersVisible = false;
            this.pedidosGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pedidosGrid.Size = new System.Drawing.Size(679, 230);
            this.pedidosGrid.TabIndex = 10;
            this.pedidosGrid.TabStop = false;
            // 
            // resumenLabel
            // 
            this.resumenLabel.AutoSize = true;
            this.resumenLabel.Location = new System.Drawing.Point(6, 8);
            this.resumenLabel.Name = "resumenLabel";
            this.resumenLabel.Size = new System.Drawing.Size(55, 13);
            this.resumenLabel.TabIndex = 11;
            this.resumenLabel.Text = "Resumen:";
            // 
            // resumenTextBox
            // 
            this.resumenTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resumenTextBox.Location = new System.Drawing.Point(9, 26);
            this.resumenTextBox.Multiline = true;
            this.resumenTextBox.Name = "resumenTextBox";
            this.resumenTextBox.ReadOnly = true;
            this.resumenTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resumenTextBox.Size = new System.Drawing.Size(664, 78);
            this.resumenTextBox.TabIndex = 12;
            this.resumenTextBox.TabStop = false;
            // 
            // cancelarButton
            // 
            this.cancelarButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cancelarButton.Location = new System.Drawing.Point(509, 9);
            this.cancelarButton.Name = "cancelarButton";
            this.cancelarButton.Size = new System.Drawing.Size(75, 30);
            this.cancelarButton.TabIndex = 14;
            this.cancelarButton.Text = "Cancelar";
            this.cancelarButton.UseVisualStyleBackColor = true;
            this.cancelarButton.Click += new System.EventHandler(this.cancelarButton_Click);
            // 
            // guardarButton
            // 
            this.guardarButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guardarButton.Location = new System.Drawing.Point(598, 9);
            this.guardarButton.Name = "guardarButton";
            this.guardarButton.Size = new System.Drawing.Size(75, 30);
            this.guardarButton.TabIndex = 13;
            this.guardarButton.Text = "Guardar";
            this.guardarButton.UseVisualStyleBackColor = true;
            this.guardarButton.Click += new System.EventHandler(this.guardarButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pedidosGrid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelResumen, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 107);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 340);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAcciones.Controls.Add(this.cancelarButton);
            this.panelAcciones.Controls.Add(this.guardarButton);
            this.panelAcciones.Location = new System.Drawing.Point(12, 453);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(685, 48);
            this.panelAcciones.TabIndex = 16;
            // 
            // panelResumen
            // 
            this.panelResumen.Controls.Add(this.resumenLabel);
            this.panelResumen.Controls.Add(this.resumenTextBox);
            this.panelResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResumen.Location = new System.Drawing.Point(3, 241);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Size = new System.Drawing.Size(679, 96);
            this.panelResumen.TabIndex = 17;
            // 
            // RegisterAbonoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 513);
            this.Controls.Add(this.panelAcciones);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.saldoRestanteLabel);
            this.Controls.Add(this.saldoRestanteTitleLabel);
            this.Controls.Add(this.montoNumericUpDown);
            this.Controls.Add(this.montoLabel);
            this.Controls.Add(this.formaCobroComboBox);
            this.Controls.Add(this.formaCobroLabel);
            this.Controls.Add(this.saldoClienteTextBox);
            this.Controls.Add(this.saldoClienteLabel);
            this.Controls.Add(this.clienteTextBox);
            this.Controls.Add(this.clienteLabel);
            this.MinimumSize = new System.Drawing.Size(725, 552);
            this.Name = "RegisterAbonoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar abono";
            ((System.ComponentModel.ISupportInitialize)(this.montoNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosGrid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelAcciones.ResumeLayout(false);
            this.panelResumen.ResumeLayout(false);
            this.panelResumen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clienteLabel;
        private System.Windows.Forms.TextBox clienteTextBox;
        private System.Windows.Forms.Label saldoClienteLabel;
        private System.Windows.Forms.TextBox saldoClienteTextBox;
        private System.Windows.Forms.Label formaCobroLabel;
        private System.Windows.Forms.ComboBox formaCobroComboBox;
        private System.Windows.Forms.Label montoLabel;
        private System.Windows.Forms.NumericUpDown montoNumericUpDown;
        private System.Windows.Forms.Label saldoRestanteTitleLabel;
        private System.Windows.Forms.Label saldoRestanteLabel;
        private System.Windows.Forms.DataGridView pedidosGrid;
        private System.Windows.Forms.Label resumenLabel;
        private System.Windows.Forms.TextBox resumenTextBox;
        private System.Windows.Forms.Button cancelarButton;
        private System.Windows.Forms.Button guardarButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Panel panelResumen;
        private System.Windows.Forms.DataGridViewTextBoxColumn folioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaEntregaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn abonadoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn montoAsignadoColumn;
    }
}
