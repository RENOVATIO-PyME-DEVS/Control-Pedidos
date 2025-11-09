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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.panelResumen = new System.Windows.Forms.Panel();
            this.panelAcciones = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.montoNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosGrid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelResumen.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // clienteLabel
            // 
            this.clienteLabel.AutoSize = true;
            this.clienteLabel.Location = new System.Drawing.Point(18, 83);
            this.clienteLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clienteLabel.Name = "clienteLabel";
            this.clienteLabel.Size = new System.Drawing.Size(51, 16);
            this.clienteLabel.TabIndex = 0;
            this.clienteLabel.Text = "Cliente:";
            // 
            // clienteTextBox
            // 
            this.clienteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clienteTextBox.Location = new System.Drawing.Point(149, 80);
            this.clienteTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.clienteTextBox.Name = "clienteTextBox";
            this.clienteTextBox.ReadOnly = true;
            this.clienteTextBox.Size = new System.Drawing.Size(518, 22);
            this.clienteTextBox.TabIndex = 1;
            this.clienteTextBox.TabStop = false;
            // 
            // saldoClienteLabel
            // 
            this.saldoClienteLabel.AutoSize = true;
            this.saldoClienteLabel.Location = new System.Drawing.Point(18, 119);
            this.saldoClienteLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.saldoClienteLabel.Name = "saldoClienteLabel";
            this.saldoClienteLabel.Size = new System.Drawing.Size(88, 16);
            this.saldoClienteLabel.TabIndex = 2;
            this.saldoClienteLabel.Text = "Saldo cliente:";
            // 
            // saldoClienteTextBox
            // 
            this.saldoClienteTextBox.Location = new System.Drawing.Point(149, 115);
            this.saldoClienteTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.saldoClienteTextBox.Name = "saldoClienteTextBox";
            this.saldoClienteTextBox.ReadOnly = true;
            this.saldoClienteTextBox.Size = new System.Drawing.Size(159, 22);
            this.saldoClienteTextBox.TabIndex = 3;
            this.saldoClienteTextBox.TabStop = false;
            // 
            // formaCobroLabel
            // 
            this.formaCobroLabel.AutoSize = true;
            this.formaCobroLabel.Location = new System.Drawing.Point(335, 119);
            this.formaCobroLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.formaCobroLabel.Name = "formaCobroLabel";
            this.formaCobroLabel.Size = new System.Drawing.Size(87, 16);
            this.formaCobroLabel.TabIndex = 4;
            this.formaCobroLabel.Text = "Forma cobro:";
            // 
            // formaCobroComboBox
            // 
            this.formaCobroComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formaCobroComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formaCobroComboBox.FormattingEnabled = true;
            this.formaCobroComboBox.Location = new System.Drawing.Point(446, 115);
            this.formaCobroComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.formaCobroComboBox.Name = "formaCobroComboBox";
            this.formaCobroComboBox.Size = new System.Drawing.Size(221, 24);
            this.formaCobroComboBox.TabIndex = 5;
            this.formaCobroComboBox.SelectedIndexChanged += new System.EventHandler(this.formaCobroComboBox_SelectedIndexChanged);
            // 
            // montoLabel
            // 
            this.montoLabel.AutoSize = true;
            this.montoLabel.Location = new System.Drawing.Point(18, 156);
            this.montoLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.montoLabel.Name = "montoLabel";
            this.montoLabel.Size = new System.Drawing.Size(89, 16);
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
            this.montoNumericUpDown.Location = new System.Drawing.Point(149, 154);
            this.montoNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.montoNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.montoNumericUpDown.Name = "montoNumericUpDown";
            this.montoNumericUpDown.Size = new System.Drawing.Size(160, 22);
            this.montoNumericUpDown.TabIndex = 7;
            this.montoNumericUpDown.ValueChanged += new System.EventHandler(this.montoNumericUpDown_ValueChanged);
            // 
            // saldoRestanteTitleLabel
            // 
            this.saldoRestanteTitleLabel.AutoSize = true;
            this.saldoRestanteTitleLabel.Location = new System.Drawing.Point(335, 156);
            this.saldoRestanteTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.saldoRestanteTitleLabel.Name = "saldoRestanteTitleLabel";
            this.saldoRestanteTitleLabel.Size = new System.Drawing.Size(97, 16);
            this.saldoRestanteTitleLabel.TabIndex = 8;
            this.saldoRestanteTitleLabel.Text = "Saldo restante:";
            // 
            // saldoRestanteLabel
            // 
            this.saldoRestanteLabel.AutoSize = true;
            this.saldoRestanteLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.saldoRestanteLabel.Location = new System.Drawing.Point(453, 154);
            this.saldoRestanteLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.saldoRestanteLabel.Name = "saldoRestanteLabel";
            this.saldoRestanteLabel.Size = new System.Drawing.Size(40, 20);
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
            this.pedidosGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pedidosGrid.Location = new System.Drawing.Point(4, 4);
            this.pedidosGrid.Margin = new System.Windows.Forms.Padding(4);
            this.pedidosGrid.MultiSelect = false;
            this.pedidosGrid.Name = "pedidosGrid";
            this.pedidosGrid.ReadOnly = true;
            this.pedidosGrid.RowHeadersVisible = false;
            this.pedidosGrid.RowHeadersWidth = 51;
            this.pedidosGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pedidosGrid.Size = new System.Drawing.Size(905, 245);
            this.pedidosGrid.TabIndex = 10;
            this.pedidosGrid.TabStop = false;
            // 
            // folioColumn
            // 
            this.folioColumn.DataPropertyName = "Folio";
            this.folioColumn.HeaderText = "Folio";
            this.folioColumn.MinimumWidth = 6;
            this.folioColumn.Name = "folioColumn";
            this.folioColumn.ReadOnly = true;
            // 
            // fechaEntregaColumn
            // 
            this.fechaEntregaColumn.DataPropertyName = "FechaEntrega";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.fechaEntregaColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.fechaEntregaColumn.HeaderText = "Fecha entrega";
            this.fechaEntregaColumn.MinimumWidth = 6;
            this.fechaEntregaColumn.Name = "fechaEntregaColumn";
            this.fechaEntregaColumn.ReadOnly = true;
            // 
            // totalColumn
            // 
            this.totalColumn.DataPropertyName = "Total";
            dataGridViewCellStyle2.Format = "C2";
            this.totalColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.totalColumn.HeaderText = "Total";
            this.totalColumn.MinimumWidth = 6;
            this.totalColumn.Name = "totalColumn";
            this.totalColumn.ReadOnly = true;
            // 
            // abonadoColumn
            // 
            this.abonadoColumn.DataPropertyName = "Abonado";
            dataGridViewCellStyle3.Format = "C2";
            this.abonadoColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.abonadoColumn.HeaderText = "Abonado";
            this.abonadoColumn.MinimumWidth = 6;
            this.abonadoColumn.Name = "abonadoColumn";
            this.abonadoColumn.ReadOnly = true;
            // 
            // saldoColumn
            // 
            this.saldoColumn.DataPropertyName = "Saldo";
            dataGridViewCellStyle4.Format = "C2";
            this.saldoColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.saldoColumn.HeaderText = "Saldo";
            this.saldoColumn.MinimumWidth = 6;
            this.saldoColumn.Name = "saldoColumn";
            this.saldoColumn.ReadOnly = true;
            // 
            // montoAsignadoColumn
            // 
            this.montoAsignadoColumn.DataPropertyName = "MontoAsignado";
            dataGridViewCellStyle5.Format = "C2";
            this.montoAsignadoColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.montoAsignadoColumn.HeaderText = "Monto asignado";
            this.montoAsignadoColumn.MinimumWidth = 6;
            this.montoAsignadoColumn.Name = "montoAsignadoColumn";
            this.montoAsignadoColumn.ReadOnly = true;
            // 
            // resumenLabel
            // 
            this.resumenLabel.AutoSize = true;
            this.resumenLabel.Location = new System.Drawing.Point(8, 10);
            this.resumenLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.resumenLabel.Name = "resumenLabel";
            this.resumenLabel.Size = new System.Drawing.Size(68, 16);
            this.resumenLabel.TabIndex = 11;
            this.resumenLabel.Text = "Resumen:";
            // 
            // resumenTextBox
            // 
            this.resumenTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resumenTextBox.Location = new System.Drawing.Point(12, 32);
            this.resumenTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.resumenTextBox.Multiline = true;
            this.resumenTextBox.Name = "resumenTextBox";
            this.resumenTextBox.ReadOnly = true;
            this.resumenTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resumenTextBox.Size = new System.Drawing.Size(884, 78);
            this.resumenTextBox.TabIndex = 12;
            this.resumenTextBox.TabStop = false;
            // 
            // cancelarButton
            // 
            this.cancelarButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cancelarButton.Location = new System.Drawing.Point(546, 11);
            this.cancelarButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelarButton.Name = "cancelarButton";
            this.cancelarButton.Size = new System.Drawing.Size(151, 37);
            this.cancelarButton.TabIndex = 14;
            this.cancelarButton.Text = "Cancelar";
            this.cancelarButton.UseVisualStyleBackColor = true;
            this.cancelarButton.Click += new System.EventHandler(this.cancelarButton_Click);
            // 
            // guardarButton
            // 
            this.guardarButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.guardarButton.Location = new System.Drawing.Point(705, 11);
            this.guardarButton.Margin = new System.Windows.Forms.Padding(4);
            this.guardarButton.Name = "guardarButton";
            this.guardarButton.Size = new System.Drawing.Size(192, 37);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 188);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(913, 362);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // panelResumen
            // 
            this.panelResumen.Controls.Add(this.resumenLabel);
            this.panelResumen.Controls.Add(this.resumenTextBox);
            this.panelResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResumen.Location = new System.Drawing.Point(4, 257);
            this.panelResumen.Margin = new System.Windows.Forms.Padding(4);
            this.panelResumen.Name = "panelResumen";
            this.panelResumen.Size = new System.Drawing.Size(905, 101);
            this.panelResumen.TabIndex = 17;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAcciones.Controls.Add(this.cancelarButton);
            this.panelAcciones.Controls.Add(this.guardarButton);
            this.panelAcciones.Location = new System.Drawing.Point(16, 558);
            this.panelAcciones.Margin = new System.Windows.Forms.Padding(4);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(913, 59);
            this.panelAcciones.TabIndex = 16;
            // 
            // RegisterAbonoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 631);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(961, 669);
            this.Name = "RegisterAbonoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrar abono";
            this.Load += new System.EventHandler(this.RegisterAbonoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.montoNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosGrid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelResumen.ResumeLayout(false);
            this.panelResumen.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
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
