namespace Control_Pedidos.Views.Clients
{
    partial class ClientManagementForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.clientsGrid = new System.Windows.Forms.DataGridView();
            this.razonSocialTextBox = new System.Windows.Forms.TextBox();
            this.nombreComercialTextBox = new System.Windows.Forms.TextBox();
            this.rfcTextBox = new System.Windows.Forms.TextBox();
            this.telefonoTextBox = new System.Windows.Forms.TextBox();
            this.correoTextBox = new System.Windows.Forms.TextBox();
            this.direccionTextBox = new System.Windows.Forms.TextBox();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.razonSocialLabel = new System.Windows.Forms.Label();
            this.nombreComercialLabel = new System.Windows.Forms.Label();
            this.rfcLabel = new System.Windows.Forms.Label();
            this.telefonoLabel = new System.Windows.Forms.Label();
            this.correoLabel = new System.Windows.Forms.Label();
            this.direccionLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.clientsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // clientsGrid
            // 
            this.clientsGrid.AllowUserToAddRows = false;
            this.clientsGrid.AllowUserToDeleteRows = false;
            this.clientsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsGrid.Location = new System.Drawing.Point(24, 320);
            this.clientsGrid.MultiSelect = false;
            this.clientsGrid.Name = "clientsGrid";
            this.clientsGrid.ReadOnly = true;
            this.clientsGrid.RowHeadersWidth = 51;
            this.clientsGrid.RowTemplate.Height = 24;
            this.clientsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientsGrid.Size = new System.Drawing.Size(888, 260);
            this.clientsGrid.TabIndex = 0;
            this.clientsGrid.SelectionChanged += new System.EventHandler(this.clientsGrid_SelectionChanged);
            // 
            // razonSocialTextBox
            // 
            this.razonSocialTextBox.Location = new System.Drawing.Point(24, 48);
            this.razonSocialTextBox.Name = "razonSocialTextBox";
            this.razonSocialTextBox.Size = new System.Drawing.Size(260, 22);
            this.razonSocialTextBox.TabIndex = 1;
            // 
            // nombreComercialTextBox
            // 
            this.nombreComercialTextBox.Location = new System.Drawing.Point(24, 104);
            this.nombreComercialTextBox.Name = "nombreComercialTextBox";
            this.nombreComercialTextBox.Size = new System.Drawing.Size(260, 22);
            this.nombreComercialTextBox.TabIndex = 2;
            // 
            // rfcTextBox
            // 
            this.rfcTextBox.Location = new System.Drawing.Point(320, 48);
            this.rfcTextBox.Name = "rfcTextBox";
            this.rfcTextBox.Size = new System.Drawing.Size(240, 22);
            this.rfcTextBox.TabIndex = 3;
            // 
            // telefonoTextBox
            // 
            this.telefonoTextBox.Location = new System.Drawing.Point(320, 104);
            this.telefonoTextBox.Name = "telefonoTextBox";
            this.telefonoTextBox.Size = new System.Drawing.Size(240, 22);
            this.telefonoTextBox.TabIndex = 4;
            // 
            // correoTextBox
            // 
            this.correoTextBox.Location = new System.Drawing.Point(320, 160);
            this.correoTextBox.Name = "correoTextBox";
            this.correoTextBox.Size = new System.Drawing.Size(240, 22);
            this.correoTextBox.TabIndex = 5;
            // 
            // direccionTextBox
            // 
            this.direccionTextBox.Location = new System.Drawing.Point(24, 160);
            this.direccionTextBox.Multiline = true;
            this.direccionTextBox.Name = "direccionTextBox";
            this.direccionTextBox.Size = new System.Drawing.Size(260, 80);
            this.direccionTextBox.TabIndex = 6;
            // 
            // statusComboBox
            // 
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Location = new System.Drawing.Point(320, 216);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(240, 24);
            this.statusComboBox.TabIndex = 7;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(600, 40);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(140, 36);
            this.addButton.TabIndex = 8;
            this.addButton.Text = "Agregar";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(600, 88);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(140, 36);
            this.updateButton.TabIndex = 9;
            this.updateButton.Text = "Actualizar";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(600, 136);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(140, 36);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = "Eliminar";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(600, 184);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(140, 36);
            this.clearButton.TabIndex = 11;
            this.clearButton.Text = "Limpiar";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // razonSocialLabel
            // 
            this.razonSocialLabel.AutoSize = true;
            this.razonSocialLabel.Location = new System.Drawing.Point(21, 28);
            this.razonSocialLabel.Name = "razonSocialLabel";
            this.razonSocialLabel.Size = new System.Drawing.Size(87, 16);
            this.razonSocialLabel.TabIndex = 12;
            this.razonSocialLabel.Text = "Razón social";
            // 
            // nombreComercialLabel
            // 
            this.nombreComercialLabel.AutoSize = true;
            this.nombreComercialLabel.Location = new System.Drawing.Point(21, 84);
            this.nombreComercialLabel.Name = "nombreComercialLabel";
            this.nombreComercialLabel.Size = new System.Drawing.Size(120, 16);
            this.nombreComercialLabel.TabIndex = 13;
            this.nombreComercialLabel.Text = "Nombre comercial";
            // 
            // rfcLabel
            // 
            this.rfcLabel.AutoSize = true;
            this.rfcLabel.Location = new System.Drawing.Point(317, 28);
            this.rfcLabel.Name = "rfcLabel";
            this.rfcLabel.Size = new System.Drawing.Size(32, 16);
            this.rfcLabel.TabIndex = 14;
            this.rfcLabel.Text = "RFC";
            // 
            // telefonoLabel
            // 
            this.telefonoLabel.AutoSize = true;
            this.telefonoLabel.Location = new System.Drawing.Point(317, 84);
            this.telefonoLabel.Name = "telefonoLabel";
            this.telefonoLabel.Size = new System.Drawing.Size(58, 16);
            this.telefonoLabel.TabIndex = 15;
            this.telefonoLabel.Text = "Teléfono";
            // 
            // correoLabel
            // 
            this.correoLabel.AutoSize = true;
            this.correoLabel.Location = new System.Drawing.Point(317, 140);
            this.correoLabel.Name = "correoLabel";
            this.correoLabel.Size = new System.Drawing.Size(47, 16);
            this.correoLabel.TabIndex = 16;
            this.correoLabel.Text = "Correo";
            // 
            // direccionLabel
            // 
            this.direccionLabel.AutoSize = true;
            this.direccionLabel.Location = new System.Drawing.Point(21, 140);
            this.direccionLabel.Name = "direccionLabel";
            this.direccionLabel.Size = new System.Drawing.Size(62, 16);
            this.direccionLabel.TabIndex = 17;
            this.direccionLabel.Text = "Dirección";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(317, 196);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(52, 16);
            this.statusLabel.TabIndex = 18;
            this.statusLabel.Text = "Estatus";
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(21, 284);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(55, 16);
            this.searchLabel.TabIndex = 19;
            this.searchLabel.Text = "Buscar:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(82, 280);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(260, 22);
            this.searchTextBox.TabIndex = 20;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // ClientManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 604);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.direccionLabel);
            this.Controls.Add(this.correoLabel);
            this.Controls.Add(this.telefonoLabel);
            this.Controls.Add(this.rfcLabel);
            this.Controls.Add(this.nombreComercialLabel);
            this.Controls.Add(this.razonSocialLabel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.direccionTextBox);
            this.Controls.Add(this.correoTextBox);
            this.Controls.Add(this.telefonoTextBox);
            this.Controls.Add(this.rfcTextBox);
            this.Controls.Add(this.nombreComercialTextBox);
            this.Controls.Add(this.razonSocialTextBox);
            this.Controls.Add(this.clientsGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de clientes";
            ((System.ComponentModel.ISupportInitialize)(this.clientsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView clientsGrid;
        private System.Windows.Forms.TextBox razonSocialTextBox;
        private System.Windows.Forms.TextBox nombreComercialTextBox;
        private System.Windows.Forms.TextBox rfcTextBox;
        private System.Windows.Forms.TextBox telefonoTextBox;
        private System.Windows.Forms.TextBox correoTextBox;
        private System.Windows.Forms.TextBox direccionTextBox;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label razonSocialLabel;
        private System.Windows.Forms.Label nombreComercialLabel;
        private System.Windows.Forms.Label rfcLabel;
        private System.Windows.Forms.Label telefonoLabel;
        private System.Windows.Forms.Label correoLabel;
        private System.Windows.Forms.Label direccionLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchTextBox;
    }
}
