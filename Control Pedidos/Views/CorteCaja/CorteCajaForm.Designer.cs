namespace Control_Pedidos.Views.CorteCaja
{
    partial class CorteCajaForm
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
            this.filtersTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.empresaLabel = new System.Windows.Forms.Label();
            this.empresaComboBox = new System.Windows.Forms.ComboBox();
            this.usuarioLabel = new System.Windows.Forms.Label();
            this.usuarioComboBox = new System.Windows.Forms.ComboBox();
            this.fechaLabel = new System.Windows.Forms.Label();
            this.fechaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.consultarButton = new System.Windows.Forms.Button();
            this.imprimirButton = new System.Windows.Forms.Button();
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            this.filtersTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // filtersTableLayoutPanel
            // 
            this.filtersTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filtersTableLayoutPanel.ColumnCount = 9;
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filtersTableLayoutPanel.Controls.Add(this.empresaLabel, 0, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.empresaComboBox, 1, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.usuarioLabel, 2, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.usuarioComboBox, 3, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.fechaLabel, 4, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.fechaDateTimePicker, 5, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.consultarButton, 7, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.imprimirButton, 8, 0);
            this.filtersTableLayoutPanel.Location = new System.Drawing.Point(12, 97);
            this.filtersTableLayoutPanel.Name = "filtersTableLayoutPanel";
            this.filtersTableLayoutPanel.RowCount = 1;
            this.filtersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.filtersTableLayoutPanel.Size = new System.Drawing.Size(1000, 40);
            this.filtersTableLayoutPanel.TabIndex = 0;
            // 
            // empresaLabel
            // 
            this.empresaLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.empresaLabel.AutoSize = true;
            this.empresaLabel.Location = new System.Drawing.Point(3, 12);
            this.empresaLabel.Name = "empresaLabel";
            this.empresaLabel.Size = new System.Drawing.Size(62, 16);
            this.empresaLabel.TabIndex = 0;
            this.empresaLabel.Text = "Empresa";
            // 
            // empresaComboBox
            // 
            this.empresaComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.empresaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.empresaComboBox.FormattingEnabled = true;
            this.empresaComboBox.Location = new System.Drawing.Point(71, 8);
            this.empresaComboBox.Name = "empresaComboBox";
            this.empresaComboBox.Size = new System.Drawing.Size(207, 24);
            this.empresaComboBox.TabIndex = 1;
            // 
            // usuarioLabel
            // 
            this.usuarioLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.usuarioLabel.AutoSize = true;
            this.usuarioLabel.Location = new System.Drawing.Point(284, 12);
            this.usuarioLabel.Name = "usuarioLabel";
            this.usuarioLabel.Size = new System.Drawing.Size(54, 16);
            this.usuarioLabel.TabIndex = 2;
            this.usuarioLabel.Text = "Usuario";
            // 
            // usuarioComboBox
            // 
            this.usuarioComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.usuarioComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usuarioComboBox.FormattingEnabled = true;
            this.usuarioComboBox.Location = new System.Drawing.Point(344, 8);
            this.usuarioComboBox.Name = "usuarioComboBox";
            this.usuarioComboBox.Size = new System.Drawing.Size(172, 24);
            this.usuarioComboBox.TabIndex = 3;
            // 
            // fechaLabel
            // 
            this.fechaLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fechaLabel.AutoSize = true;
            this.fechaLabel.Location = new System.Drawing.Point(522, 12);
            this.fechaLabel.Name = "fechaLabel";
            this.fechaLabel.Size = new System.Drawing.Size(45, 16);
            this.fechaLabel.TabIndex = 4;
            this.fechaLabel.Text = "Fecha";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaDateTimePicker.Location = new System.Drawing.Point(573, 9);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.Size = new System.Drawing.Size(100, 22);
            this.fechaDateTimePicker.TabIndex = 5;
            // 
            // consultarButton
            // 
            this.consultarButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.consultarButton.Location = new System.Drawing.Point(750, 4);
            this.consultarButton.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.consultarButton.Name = "consultarButton";
            this.consultarButton.Size = new System.Drawing.Size(120, 32);
            this.consultarButton.TabIndex = 6;
            this.consultarButton.Text = "Consultar";
            this.consultarButton.UseVisualStyleBackColor = true;
            this.consultarButton.Click += new System.EventHandler(this.consultarButton_Click);
            // 
            // imprimirButton
            // 
            this.imprimirButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.imprimirButton.Enabled = false;
            this.imprimirButton.Location = new System.Drawing.Point(884, 4);
            this.imprimirButton.Margin = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.imprimirButton.Name = "imprimirButton";
            this.imprimirButton.Size = new System.Drawing.Size(116, 32);
            this.imprimirButton.TabIndex = 7;
            this.imprimirButton.Text = "Imprimir Corte";
            this.imprimirButton.UseVisualStyleBackColor = true;
            this.imprimirButton.Click += new System.EventHandler(this.imprimirButton_Click);
            // 
            // resultDataGridView
            // 
            this.resultDataGridView.AllowUserToAddRows = false;
            this.resultDataGridView.AllowUserToDeleteRows = false;
            this.resultDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDataGridView.Location = new System.Drawing.Point(12, 143);
            this.resultDataGridView.MultiSelect = false;
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.ReadOnly = true;
            this.resultDataGridView.RowHeadersVisible = false;
            this.resultDataGridView.RowHeadersWidth = 51;
            this.resultDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultDataGridView.Size = new System.Drawing.Size(1000, 405);
            this.resultDataGridView.TabIndex = 1;
            this.resultDataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.resultDataGridView_DataBindingComplete);
            // 
            // CorteCajaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 560);
            this.Controls.Add(this.resultDataGridView);
            this.Controls.Add(this.filtersTableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "CorteCajaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Corte de Caja";
            this.Load += new System.EventHandler(this.CorteCajaForm_Load);
            this.filtersTableLayoutPanel.ResumeLayout(false);
            this.filtersTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel filtersTableLayoutPanel;
        private System.Windows.Forms.Label empresaLabel;
        private System.Windows.Forms.ComboBox empresaComboBox;
        private System.Windows.Forms.Label usuarioLabel;
        private System.Windows.Forms.ComboBox usuarioComboBox;
        private System.Windows.Forms.Label fechaLabel;
        private System.Windows.Forms.DateTimePicker fechaDateTimePicker;
        private System.Windows.Forms.Button consultarButton;
        private System.Windows.Forms.Button imprimirButton;
        private System.Windows.Forms.DataGridView resultDataGridView;
    }
}
