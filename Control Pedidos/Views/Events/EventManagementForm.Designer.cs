namespace Control_Pedidos.Views.Events
{
    partial class EventManagementForm
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
            this.nombreLabel = new System.Windows.Forms.Label();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.empresaLabel = new System.Windows.Forms.Label();
            this.empresaComboBox = new System.Windows.Forms.ComboBox();
            this.fechaLabel = new System.Windows.Forms.Label();
            this.fechaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tieneSerieCheckBox = new System.Windows.Forms.CheckBox();
            this.serieLabel = new System.Windows.Forms.Label();
            this.serieTextBox = new System.Windows.Forms.TextBox();
            this.siguienteFolioLabel = new System.Windows.Forms.Label();
            this.siguienteFolioNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.agregarButton = new System.Windows.Forms.Button();
            this.actualizarButton = new System.Windows.Forms.Button();
            this.limpiarButton = new System.Windows.Forms.Button();
            this.eventosGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.siguienteFolioNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventosGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // nombreLabel
            // 
            this.nombreLabel.AutoSize = true;
            this.nombreLabel.Location = new System.Drawing.Point(24, 20);
            this.nombreLabel.Name = "nombreLabel";
            this.nombreLabel.Size = new System.Drawing.Size(51, 15);
            this.nombreLabel.TabIndex = 0;
            this.nombreLabel.Text = "Nombre";
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.Location = new System.Drawing.Point(24, 38);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.Size = new System.Drawing.Size(260, 23);
            this.nombreTextBox.TabIndex = 1;
            // 
            // empresaLabel
            // 
            this.empresaLabel.AutoSize = true;
            this.empresaLabel.Location = new System.Drawing.Point(304, 20);
            this.empresaLabel.Name = "empresaLabel";
            this.empresaLabel.Size = new System.Drawing.Size(54, 15);
            this.empresaLabel.TabIndex = 2;
            this.empresaLabel.Text = "Empresa";
            // 
            // empresaComboBox
            // 
            this.empresaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.empresaComboBox.FormattingEnabled = true;
            this.empresaComboBox.Location = new System.Drawing.Point(304, 38);
            this.empresaComboBox.Name = "empresaComboBox";
            this.empresaComboBox.Size = new System.Drawing.Size(240, 23);
            this.empresaComboBox.TabIndex = 3;
            // 
            // fechaLabel
            // 
            this.fechaLabel.AutoSize = true;
            this.fechaLabel.Location = new System.Drawing.Point(568, 20);
            this.fechaLabel.Name = "fechaLabel";
            this.fechaLabel.Size = new System.Drawing.Size(84, 15);
            this.fechaLabel.TabIndex = 4;
            this.fechaLabel.Text = "Fecha del evento";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaDateTimePicker.Location = new System.Drawing.Point(568, 38);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.Size = new System.Drawing.Size(140, 23);
            this.fechaDateTimePicker.TabIndex = 5;
            // 
            // tieneSerieCheckBox
            // 
            this.tieneSerieCheckBox.AutoSize = true;
            this.tieneSerieCheckBox.Location = new System.Drawing.Point(24, 80);
            this.tieneSerieCheckBox.Name = "tieneSerieCheckBox";
            this.tieneSerieCheckBox.Size = new System.Drawing.Size(83, 19);
            this.tieneSerieCheckBox.TabIndex = 6;
            this.tieneSerieCheckBox.Text = "Tiene serie";
            this.tieneSerieCheckBox.UseVisualStyleBackColor = true;
            this.tieneSerieCheckBox.CheckedChanged += new System.EventHandler(this.tieneSerieCheckBox_CheckedChanged);
            // 
            // serieLabel
            // 
            this.serieLabel.AutoSize = true;
            this.serieLabel.Location = new System.Drawing.Point(120, 80);
            this.serieLabel.Name = "serieLabel";
            this.serieLabel.Size = new System.Drawing.Size(33, 15);
            this.serieLabel.TabIndex = 7;
            this.serieLabel.Text = "Serie";
            // 
            // serieTextBox
            // 
            this.serieTextBox.Enabled = false;
            this.serieTextBox.Location = new System.Drawing.Point(120, 98);
            this.serieTextBox.Name = "serieTextBox";
            this.serieTextBox.Size = new System.Drawing.Size(120, 23);
            this.serieTextBox.TabIndex = 8;
            // 
            // siguienteFolioLabel
            // 
            this.siguienteFolioLabel.AutoSize = true;
            this.siguienteFolioLabel.Location = new System.Drawing.Point(264, 80);
            this.siguienteFolioLabel.Name = "siguienteFolioLabel";
            this.siguienteFolioLabel.Size = new System.Drawing.Size(87, 15);
            this.siguienteFolioLabel.TabIndex = 9;
            this.siguienteFolioLabel.Text = "Siguiente folio";
            // 
            // siguienteFolioNumericUpDown
            // 
            this.siguienteFolioNumericUpDown.Location = new System.Drawing.Point(264, 98);
            this.siguienteFolioNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.siguienteFolioNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.siguienteFolioNumericUpDown.Name = "siguienteFolioNumericUpDown";
            this.siguienteFolioNumericUpDown.Size = new System.Drawing.Size(120, 23);
            this.siguienteFolioNumericUpDown.TabIndex = 10;
            this.siguienteFolioNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // agregarButton
            // 
            this.agregarButton.Location = new System.Drawing.Point(408, 96);
            this.agregarButton.Name = "agregarButton";
            this.agregarButton.Size = new System.Drawing.Size(90, 27);
            this.agregarButton.TabIndex = 11;
            this.agregarButton.Text = "Agregar";
            this.agregarButton.UseVisualStyleBackColor = true;
            this.agregarButton.Click += new System.EventHandler(this.agregarButton_Click);
            // 
            // actualizarButton
            // 
            this.actualizarButton.Location = new System.Drawing.Point(504, 96);
            this.actualizarButton.Name = "actualizarButton";
            this.actualizarButton.Size = new System.Drawing.Size(90, 27);
            this.actualizarButton.TabIndex = 12;
            this.actualizarButton.Text = "Actualizar";
            this.actualizarButton.UseVisualStyleBackColor = true;
            this.actualizarButton.Click += new System.EventHandler(this.actualizarButton_Click);
            // 
            // limpiarButton
            // 
            this.limpiarButton.Location = new System.Drawing.Point(600, 96);
            this.limpiarButton.Name = "limpiarButton";
            this.limpiarButton.Size = new System.Drawing.Size(90, 27);
            this.limpiarButton.TabIndex = 13;
            this.limpiarButton.Text = "Limpiar";
            this.limpiarButton.UseVisualStyleBackColor = true;
            this.limpiarButton.Click += new System.EventHandler(this.limpiarButton_Click);
            // 
            // eventosGrid
            // 
            this.eventosGrid.AllowUserToAddRows = false;
            this.eventosGrid.AllowUserToDeleteRows = false;
            this.eventosGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventosGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventosGrid.Location = new System.Drawing.Point(24, 144);
            this.eventosGrid.MultiSelect = false;
            this.eventosGrid.Name = "eventosGrid";
            this.eventosGrid.ReadOnly = true;
            this.eventosGrid.RowHeadersVisible = false;
            this.eventosGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.eventosGrid.Size = new System.Drawing.Size(684, 340);
            this.eventosGrid.TabIndex = 14;
            this.eventosGrid.SelectionChanged += new System.EventHandler(this.eventosGrid_SelectionChanged);
            // 
            // EventManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 512);
            this.Controls.Add(this.eventosGrid);
            this.Controls.Add(this.limpiarButton);
            this.Controls.Add(this.actualizarButton);
            this.Controls.Add(this.agregarButton);
            this.Controls.Add(this.siguienteFolioNumericUpDown);
            this.Controls.Add(this.siguienteFolioLabel);
            this.Controls.Add(this.serieTextBox);
            this.Controls.Add(this.serieLabel);
            this.Controls.Add(this.tieneSerieCheckBox);
            this.Controls.Add(this.fechaDateTimePicker);
            this.Controls.Add(this.fechaLabel);
            this.Controls.Add(this.empresaComboBox);
            this.Controls.Add(this.empresaLabel);
            this.Controls.Add(this.nombreTextBox);
            this.Controls.Add(this.nombreLabel);
            this.MinimizeBox = false;
            this.Name = "EventManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de eventos";
            ((System.ComponentModel.ISupportInitialize)(this.siguienteFolioNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventosGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nombreLabel;
        private System.Windows.Forms.TextBox nombreTextBox;
        private System.Windows.Forms.Label empresaLabel;
        private System.Windows.Forms.ComboBox empresaComboBox;
        private System.Windows.Forms.Label fechaLabel;
        private System.Windows.Forms.DateTimePicker fechaDateTimePicker;
        private System.Windows.Forms.CheckBox tieneSerieCheckBox;
        private System.Windows.Forms.Label serieLabel;
        private System.Windows.Forms.TextBox serieTextBox;
        private System.Windows.Forms.Label siguienteFolioLabel;
        private System.Windows.Forms.NumericUpDown siguienteFolioNumericUpDown;
        private System.Windows.Forms.Button agregarButton;
        private System.Windows.Forms.Button actualizarButton;
        private System.Windows.Forms.Button limpiarButton;
        private System.Windows.Forms.DataGridView eventosGrid;
    }
}
