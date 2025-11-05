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
            this.nombreComercialTextBox = new System.Windows.Forms.TextBox();
            this.rfcTextBox = new System.Windows.Forms.TextBox();
            this.telefonoTextBox = new System.Windows.Forms.TextBox();
            this.correoTextBox = new System.Windows.Forms.TextBox();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.nombreComercialLabel = new System.Windows.Forms.Label();
            this.rfcLabel = new System.Windows.Forms.Label();
            this.telefonoLabel = new System.Windows.Forms.Label();
            this.correoLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.chkRequiereFactura = new System.Windows.Forms.CheckBox();
            this.codigoPostalLabel = new System.Windows.Forms.Label();
            this.codigoPostalTextBox = new System.Windows.Forms.TextBox();
            this.regimenFiscalLabel = new System.Windows.Forms.Label();
            this.cmbRegimenFiscal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.direccionTextBox = new System.Windows.Forms.TextBox();
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
            this.clientsGrid.Location = new System.Drawing.Point(24, 355);
            this.clientsGrid.MultiSelect = false;
            this.clientsGrid.Name = "clientsGrid";
            this.clientsGrid.ReadOnly = true;
            this.clientsGrid.RowHeadersWidth = 51;
            this.clientsGrid.RowTemplate.Height = 24;
            this.clientsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientsGrid.Size = new System.Drawing.Size(1298, 225);
            this.clientsGrid.TabIndex = 0;
            this.clientsGrid.SelectionChanged += new System.EventHandler(this.clientsGrid_SelectionChanged);
            // 
            // nombreComercialTextBox
            // 
            this.nombreComercialTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.nombreComercialTextBox.Location = new System.Drawing.Point(24, 81);
            this.nombreComercialTextBox.Name = "nombreComercialTextBox";
            this.nombreComercialTextBox.Size = new System.Drawing.Size(536, 22);
            this.nombreComercialTextBox.TabIndex = 2;
            // 
            // rfcTextBox
            // 
            this.rfcTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.rfcTextBox.Enabled = false;
            this.rfcTextBox.Location = new System.Drawing.Point(24, 138);
            this.rfcTextBox.Name = "rfcTextBox";
            this.rfcTextBox.Size = new System.Drawing.Size(260, 22);
            this.rfcTextBox.TabIndex = 3;
            this.rfcTextBox.TextChanged += new System.EventHandler(this.rfcTextBox_TextChanged);
            // 
            // telefonoTextBox
            // 
            this.telefonoTextBox.Location = new System.Drawing.Point(320, 138);
            this.telefonoTextBox.Name = "telefonoTextBox";
            this.telefonoTextBox.Size = new System.Drawing.Size(240, 22);
            this.telefonoTextBox.TabIndex = 6;
            // 
            // correoTextBox
            // 
            this.correoTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.correoTextBox.Location = new System.Drawing.Point(320, 194);
            this.correoTextBox.Name = "correoTextBox";
            this.correoTextBox.Size = new System.Drawing.Size(240, 22);
            this.correoTextBox.TabIndex = 7;
            // 
            // statusComboBox
            // 
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Location = new System.Drawing.Point(320, 250);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(240, 24);
            this.statusComboBox.TabIndex = 9;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(586, 187);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(140, 36);
            this.addButton.TabIndex = 10;
            this.addButton.Text = "Agregar";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(747, 187);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(140, 36);
            this.updateButton.TabIndex = 11;
            this.updateButton.Text = "Actualizar";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(747, 243);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(140, 36);
            this.deleteButton.TabIndex = 12;
            this.deleteButton.Text = "Eliminar";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(586, 243);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(140, 36);
            this.clearButton.TabIndex = 13;
            this.clearButton.Text = "Limpiar";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // nombreComercialLabel
            // 
            this.nombreComercialLabel.AutoSize = true;
            this.nombreComercialLabel.Location = new System.Drawing.Point(21, 61);
            this.nombreComercialLabel.Name = "nombreComercialLabel";
            this.nombreComercialLabel.Size = new System.Drawing.Size(115, 16);
            this.nombreComercialLabel.TabIndex = 13;
            this.nombreComercialLabel.Text = "Nombre completo";
            // 
            // rfcLabel
            // 
            this.rfcLabel.AutoSize = true;
            this.rfcLabel.Location = new System.Drawing.Point(21, 118);
            this.rfcLabel.Name = "rfcLabel";
            this.rfcLabel.Size = new System.Drawing.Size(34, 16);
            this.rfcLabel.TabIndex = 14;
            this.rfcLabel.Text = "RFC";
            // 
            // telefonoLabel
            // 
            this.telefonoLabel.AutoSize = true;
            this.telefonoLabel.Location = new System.Drawing.Point(317, 118);
            this.telefonoLabel.Name = "telefonoLabel";
            this.telefonoLabel.Size = new System.Drawing.Size(61, 16);
            this.telefonoLabel.TabIndex = 15;
            this.telefonoLabel.Text = "Teléfono";
            // 
            // correoLabel
            // 
            this.correoLabel.AutoSize = true;
            this.correoLabel.Location = new System.Drawing.Point(317, 174);
            this.correoLabel.Name = "correoLabel";
            this.correoLabel.Size = new System.Drawing.Size(48, 16);
            this.correoLabel.TabIndex = 16;
            this.correoLabel.Text = "Correo";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(317, 230);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(51, 16);
            this.statusLabel.TabIndex = 18;
            this.statusLabel.Text = "Estatus";
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(19, 331);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(52, 16);
            this.searchLabel.TabIndex = 19;
            this.searchLabel.Text = "Buscar:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(80, 327);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(260, 22);
            this.searchTextBox.TabIndex = 20;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(192, 163);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(92, 16);
            this.linkLabel1.TabIndex = 21;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "RFC Generico";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // chkRequiereFactura
            // 
            this.chkRequiereFactura.AutoSize = true;
            this.chkRequiereFactura.Location = new System.Drawing.Point(156, 109);
            this.chkRequiereFactura.Name = "chkRequiereFactura";
            this.chkRequiereFactura.Size = new System.Drawing.Size(128, 20);
            this.chkRequiereFactura.TabIndex = 1;
            this.chkRequiereFactura.Text = "Requiere factura";
            this.chkRequiereFactura.UseVisualStyleBackColor = true;
            this.chkRequiereFactura.CheckedChanged += new System.EventHandler(this.chkRequiereFactura_CheckedChanged);
            // 
            // codigoPostalLabel
            // 
            this.codigoPostalLabel.AutoSize = true;
            this.codigoPostalLabel.Location = new System.Drawing.Point(21, 174);
            this.codigoPostalLabel.Name = "codigoPostalLabel";
            this.codigoPostalLabel.Size = new System.Drawing.Size(91, 16);
            this.codigoPostalLabel.TabIndex = 22;
            this.codigoPostalLabel.Text = "Código postal";
            // 
            // codigoPostalTextBox
            // 
            this.codigoPostalTextBox.Enabled = false;
            this.codigoPostalTextBox.Location = new System.Drawing.Point(24, 194);
            this.codigoPostalTextBox.Name = "codigoPostalTextBox";
            this.codigoPostalTextBox.Size = new System.Drawing.Size(260, 22);
            this.codigoPostalTextBox.TabIndex = 4;
            // 
            // regimenFiscalLabel
            // 
            this.regimenFiscalLabel.AutoSize = true;
            this.regimenFiscalLabel.Location = new System.Drawing.Point(21, 230);
            this.regimenFiscalLabel.Name = "regimenFiscalLabel";
            this.regimenFiscalLabel.Size = new System.Drawing.Size(96, 16);
            this.regimenFiscalLabel.TabIndex = 24;
            this.regimenFiscalLabel.Text = "Régimen fiscal";
            // 
            // cmbRegimenFiscal
            // 
            this.cmbRegimenFiscal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegimenFiscal.Enabled = false;
            this.cmbRegimenFiscal.FormattingEnabled = true;
            this.cmbRegimenFiscal.Location = new System.Drawing.Point(24, 250);
            this.cmbRegimenFiscal.Name = "cmbRegimenFiscal";
            this.cmbRegimenFiscal.Size = new System.Drawing.Size(260, 24);
            this.cmbRegimenFiscal.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(583, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Domicilio";
            // 
            // direccionTextBox
            // 
            this.direccionTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.direccionTextBox.Location = new System.Drawing.Point(586, 81);
            this.direccionTextBox.Multiline = true;
            this.direccionTextBox.Name = "direccionTextBox";
            this.direccionTextBox.Size = new System.Drawing.Size(301, 79);
            this.direccionTextBox.TabIndex = 25;
            // 
            // ClientManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 604);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.direccionTextBox);
            this.Controls.Add(this.cmbRegimenFiscal);
            this.Controls.Add(this.regimenFiscalLabel);
            this.Controls.Add(this.codigoPostalTextBox);
            this.Controls.Add(this.codigoPostalLabel);
            this.Controls.Add(this.chkRequiereFactura);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.correoLabel);
            this.Controls.Add(this.telefonoLabel);
            this.Controls.Add(this.rfcLabel);
            this.Controls.Add(this.nombreComercialLabel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.correoTextBox);
            this.Controls.Add(this.telefonoTextBox);
            this.Controls.Add(this.rfcTextBox);
            this.Controls.Add(this.nombreComercialTextBox);
            this.Controls.Add(this.clientsGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Banquetes - Gestión de clientes";
            ((System.ComponentModel.ISupportInitialize)(this.clientsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView clientsGrid;
        private System.Windows.Forms.TextBox nombreComercialTextBox;
        private System.Windows.Forms.TextBox rfcTextBox;
        private System.Windows.Forms.TextBox telefonoTextBox;
        private System.Windows.Forms.TextBox correoTextBox;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label nombreComercialLabel;
        private System.Windows.Forms.Label rfcLabel;
        private System.Windows.Forms.Label telefonoLabel;
        private System.Windows.Forms.Label correoLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chkRequiereFactura;
        private System.Windows.Forms.Label codigoPostalLabel;
        private System.Windows.Forms.TextBox codigoPostalTextBox;
        private System.Windows.Forms.Label regimenFiscalLabel;
        private System.Windows.Forms.ComboBox cmbRegimenFiscal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox direccionTextBox;
    }
}
