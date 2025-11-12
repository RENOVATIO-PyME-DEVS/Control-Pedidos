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
            this.nombreLabel.Location = new System.Drawing.Point(29, 85);
            this.nombreLabel.Name = "nombreLabel";
            this.nombreLabel.Size = new System.Drawing.Size(122, 16);
            this.nombreLabel.TabIndex = 0;
            this.nombreLabel.Text = "Nombre del evento";
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.Location = new System.Drawing.Point(29, 105);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.Size = new System.Drawing.Size(366, 22);
            this.nombreTextBox.TabIndex = 1;
            // 
            // empresaLabel
            // 
            this.empresaLabel.AutoSize = true;
            this.empresaLabel.Location = new System.Drawing.Point(438, 85);
            this.empresaLabel.Name = "empresaLabel";
            this.empresaLabel.Size = new System.Drawing.Size(62, 16);
            this.empresaLabel.TabIndex = 2;
            this.empresaLabel.Text = "Empresa";
            // 
            // empresaComboBox
            // 
            this.empresaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.empresaComboBox.FormattingEnabled = true;
            this.empresaComboBox.Location = new System.Drawing.Point(438, 105);
            this.empresaComboBox.Name = "empresaComboBox";
            this.empresaComboBox.Size = new System.Drawing.Size(274, 24);
            this.empresaComboBox.TabIndex = 3;
            // 
            // fechaLabel
            // 
            this.fechaLabel.AutoSize = true;
            this.fechaLabel.Location = new System.Drawing.Point(732, 85);
            this.fechaLabel.Name = "fechaLabel";
            this.fechaLabel.Size = new System.Drawing.Size(111, 16);
            this.fechaLabel.TabIndex = 4;
            this.fechaLabel.Text = "Fecha del evento";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaDateTimePicker.Location = new System.Drawing.Point(735, 107);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.Size = new System.Drawing.Size(183, 22);
            this.fechaDateTimePicker.TabIndex = 5;
            // 
            // tieneSerieCheckBox
            // 
            this.tieneSerieCheckBox.AutoSize = true;
            this.tieneSerieCheckBox.Location = new System.Drawing.Point(29, 149);
            this.tieneSerieCheckBox.Name = "tieneSerieCheckBox";
            this.tieneSerieCheckBox.Size = new System.Drawing.Size(97, 20);
            this.tieneSerieCheckBox.TabIndex = 6;
            this.tieneSerieCheckBox.Text = "Tiene serie";
            this.tieneSerieCheckBox.UseVisualStyleBackColor = true;
            this.tieneSerieCheckBox.CheckedChanged += new System.EventHandler(this.tieneSerieCheckBox_CheckedChanged);
            // 
            // serieLabel
            // 
            this.serieLabel.AutoSize = true;
            this.serieLabel.Location = new System.Drawing.Point(139, 149);
            this.serieLabel.Name = "serieLabel";
            this.serieLabel.Size = new System.Drawing.Size(39, 16);
            this.serieLabel.TabIndex = 7;
            this.serieLabel.Text = "Serie";
            // 
            // serieTextBox
            // 
            this.serieTextBox.Enabled = false;
            this.serieTextBox.Location = new System.Drawing.Point(139, 169);
            this.serieTextBox.Name = "serieTextBox";
            this.serieTextBox.Size = new System.Drawing.Size(137, 22);
            this.serieTextBox.TabIndex = 8;
            // 
            // siguienteFolioLabel
            // 
            this.siguienteFolioLabel.AutoSize = true;
            this.siguienteFolioLabel.Location = new System.Drawing.Point(304, 149);
            this.siguienteFolioLabel.Name = "siguienteFolioLabel";
            this.siguienteFolioLabel.Size = new System.Drawing.Size(91, 16);
            this.siguienteFolioLabel.TabIndex = 9;
            this.siguienteFolioLabel.Text = "Siguiente folio";
            // 
            // siguienteFolioNumericUpDown
            // 
            this.siguienteFolioNumericUpDown.Location = new System.Drawing.Point(304, 169);
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
            this.siguienteFolioNumericUpDown.Size = new System.Drawing.Size(137, 22);
            this.siguienteFolioNumericUpDown.TabIndex = 10;
            this.siguienteFolioNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // agregarButton
            // 
            this.agregarButton.Location = new System.Drawing.Point(487, 165);
            this.agregarButton.Name = "agregarButton";
            this.agregarButton.Size = new System.Drawing.Size(136, 29);
            this.agregarButton.TabIndex = 11;
            this.agregarButton.Text = "Agregar";
            this.agregarButton.UseVisualStyleBackColor = true;
            this.agregarButton.Click += new System.EventHandler(this.agregarButton_Click);
            // 
            // actualizarButton
            // 
            this.actualizarButton.Location = new System.Drawing.Point(636, 165);
            this.actualizarButton.Name = "actualizarButton";
            this.actualizarButton.Size = new System.Drawing.Size(126, 29);
            this.actualizarButton.TabIndex = 12;
            this.actualizarButton.Text = "Actualizar";
            this.actualizarButton.UseVisualStyleBackColor = true;
            this.actualizarButton.Click += new System.EventHandler(this.actualizarButton_Click);
            // 
            // limpiarButton
            // 
            this.limpiarButton.Location = new System.Drawing.Point(784, 165);
            this.limpiarButton.Name = "limpiarButton";
            this.limpiarButton.Size = new System.Drawing.Size(134, 29);
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
            this.eventosGrid.Location = new System.Drawing.Point(27, 230);
            this.eventosGrid.MultiSelect = false;
            this.eventosGrid.Name = "eventosGrid";
            this.eventosGrid.ReadOnly = true;
            this.eventosGrid.RowHeadersVisible = false;
            this.eventosGrid.RowHeadersWidth = 51;
            this.eventosGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.eventosGrid.Size = new System.Drawing.Size(891, 287);
            this.eventosGrid.TabIndex = 14;
            this.eventosGrid.SelectionChanged += new System.EventHandler(this.eventosGrid_SelectionChanged);
            // 
            // EventManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 546);
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
            this.Load += new System.EventHandler(this.EventManagementForm_Load);
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
