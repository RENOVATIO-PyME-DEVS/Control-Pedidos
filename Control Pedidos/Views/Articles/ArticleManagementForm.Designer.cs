namespace Control_Pedidos.Views.Articles
{
    partial class ArticleManagementForm
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
            this.articlesGrid = new System.Windows.Forms.DataGridView();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.shortNameTextBox = new System.Windows.Forms.TextBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.unitMeasureTextBox = new System.Windows.Forms.TextBox();
            this.unitControlTextBox = new System.Windows.Forms.TextBox();
            this.contentControlTextBox = new System.Windows.Forms.TextBox();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.priceDatePicker = new System.Windows.Forms.DateTimePicker();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.componentsGrid = new System.Windows.Forms.DataGridView();
            this.componentComboBox = new System.Windows.Forms.ComboBox();
            this.componentQuantityTextBox = new System.Windows.Forms.TextBox();
            this.addComponentButton = new System.Windows.Forms.Button();
            this.removeComponentButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.shortNameLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.unitMeasureLabel = new System.Windows.Forms.Label();
            this.unitControlLabel = new System.Windows.Forms.Label();
            this.contentControlLabel = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            this.priceDateLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.componentLabel = new System.Windows.Forms.Label();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.inventoryCheckBox = new System.Windows.Forms.CheckBox();
            this.inventoryLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.articlesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // articlesGrid
            // 
            this.articlesGrid.AllowUserToAddRows = false;
            this.articlesGrid.AllowUserToDeleteRows = false;
            this.articlesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.articlesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.articlesGrid.Location = new System.Drawing.Point(24, 447);
            this.articlesGrid.MultiSelect = false;
            this.articlesGrid.Name = "articlesGrid";
            this.articlesGrid.ReadOnly = true;
            this.articlesGrid.RowHeadersWidth = 51;
            this.articlesGrid.RowTemplate.Height = 24;
            this.articlesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.articlesGrid.Size = new System.Drawing.Size(1155, 246);
            this.articlesGrid.TabIndex = 0;
            this.articlesGrid.SelectionChanged += new System.EventHandler(this.articlesGrid_SelectionChanged);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(24, 83);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(260, 22);
            this.nameTextBox.TabIndex = 1;
            // 
            // shortNameTextBox
            // 
            this.shortNameTextBox.Location = new System.Drawing.Point(24, 139);
            this.shortNameTextBox.Name = "shortNameTextBox";
            this.shortNameTextBox.Size = new System.Drawing.Size(260, 22);
            this.shortNameTextBox.TabIndex = 2;
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(24, 195);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(260, 24);
            this.typeComboBox.TabIndex = 3;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
            // 
            // unitMeasureTextBox
            // 
            this.unitMeasureTextBox.Location = new System.Drawing.Point(320, 83);
            this.unitMeasureTextBox.Name = "unitMeasureTextBox";
            this.unitMeasureTextBox.Size = new System.Drawing.Size(240, 22);
            this.unitMeasureTextBox.TabIndex = 4;
            // 
            // unitControlTextBox
            // 
            this.unitControlTextBox.Location = new System.Drawing.Point(320, 139);
            this.unitControlTextBox.Name = "unitControlTextBox";
            this.unitControlTextBox.Size = new System.Drawing.Size(240, 22);
            this.unitControlTextBox.TabIndex = 5;
            // 
            // contentControlTextBox
            // 
            this.contentControlTextBox.Location = new System.Drawing.Point(320, 195);
            this.contentControlTextBox.Name = "contentControlTextBox";
            this.contentControlTextBox.Size = new System.Drawing.Size(240, 22);
            this.contentControlTextBox.TabIndex = 6;
            // 
            // priceTextBox
            // 
            this.priceTextBox.Location = new System.Drawing.Point(320, 251);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(240, 22);
            this.priceTextBox.TabIndex = 7;
            // 
            // priceDatePicker
            // 
            this.priceDatePicker.Enabled = false;
            this.priceDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.priceDatePicker.Location = new System.Drawing.Point(320, 307);
            this.priceDatePicker.Name = "priceDatePicker";
            this.priceDatePicker.Size = new System.Drawing.Size(240, 22);
            this.priceDatePicker.TabIndex = 8;
            this.priceDatePicker.Value = new System.DateTime(2025, 10, 27, 0, 0, 0, 0);
            // 
            // statusComboBox
            // 
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Location = new System.Drawing.Point(24, 307);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(260, 24);
            this.statusComboBox.TabIndex = 10;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(600, 302);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(140, 36);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "Agregar";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(747, 302);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(140, 36);
            this.updateButton.TabIndex = 13;
            this.updateButton.Text = "Actualizar";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(893, 302);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(140, 36);
            this.deleteButton.TabIndex = 14;
            this.deleteButton.Text = "Eliminar";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(1039, 302);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(140, 36);
            this.clearButton.TabIndex = 15;
            this.clearButton.Text = "Limpiar";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // componentsGrid
            // 
            this.componentsGrid.AllowUserToAddRows = false;
            this.componentsGrid.AllowUserToDeleteRows = false;
            this.componentsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.componentsGrid.Location = new System.Drawing.Point(600, 139);
            this.componentsGrid.MultiSelect = false;
            this.componentsGrid.Name = "componentsGrid";
            this.componentsGrid.ReadOnly = true;
            this.componentsGrid.RowHeadersWidth = 51;
            this.componentsGrid.RowTemplate.Height = 24;
            this.componentsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.componentsGrid.Size = new System.Drawing.Size(579, 134);
            this.componentsGrid.TabIndex = 16;
            // 
            // componentComboBox
            // 
            this.componentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.componentComboBox.FormattingEnabled = true;
            this.componentComboBox.Location = new System.Drawing.Point(600, 99);
            this.componentComboBox.Name = "componentComboBox";
            this.componentComboBox.Size = new System.Drawing.Size(224, 24);
            this.componentComboBox.TabIndex = 17;
            // 
            // componentQuantityTextBox
            // 
            this.componentQuantityTextBox.Location = new System.Drawing.Point(845, 99);
            this.componentQuantityTextBox.Name = "componentQuantityTextBox";
            this.componentQuantityTextBox.Size = new System.Drawing.Size(60, 22);
            this.componentQuantityTextBox.TabIndex = 18;
            // 
            // addComponentButton
            // 
            this.addComponentButton.Location = new System.Drawing.Point(922, 96);
            this.addComponentButton.Name = "addComponentButton";
            this.addComponentButton.Size = new System.Drawing.Size(60, 28);
            this.addComponentButton.TabIndex = 19;
            this.addComponentButton.Text = "+";
            this.addComponentButton.UseVisualStyleBackColor = true;
            this.addComponentButton.Click += new System.EventHandler(this.addComponentButton_Click);
            // 
            // removeComponentButton
            // 
            this.removeComponentButton.Location = new System.Drawing.Point(1119, 96);
            this.removeComponentButton.Name = "removeComponentButton";
            this.removeComponentButton.Size = new System.Drawing.Size(60, 28);
            this.removeComponentButton.TabIndex = 20;
            this.removeComponentButton.Text = "-";
            this.removeComponentButton.UseVisualStyleBackColor = true;
            this.removeComponentButton.Click += new System.EventHandler(this.removeComponentButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(21, 63);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(56, 16);
            this.nameLabel.TabIndex = 21;
            this.nameLabel.Text = "Nombre";
            // 
            // shortNameLabel
            // 
            this.shortNameLabel.AutoSize = true;
            this.shortNameLabel.Location = new System.Drawing.Point(21, 119);
            this.shortNameLabel.Name = "shortNameLabel";
            this.shortNameLabel.Size = new System.Drawing.Size(89, 16);
            this.shortNameLabel.TabIndex = 22;
            this.shortNameLabel.Text = "Nombre corto";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(21, 175);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(35, 16);
            this.typeLabel.TabIndex = 23;
            this.typeLabel.Text = "Tipo";
            // 
            // unitMeasureLabel
            // 
            this.unitMeasureLabel.AutoSize = true;
            this.unitMeasureLabel.Location = new System.Drawing.Point(317, 63);
            this.unitMeasureLabel.Name = "unitMeasureLabel";
            this.unitMeasureLabel.Size = new System.Drawing.Size(100, 16);
            this.unitMeasureLabel.TabIndex = 24;
            this.unitMeasureLabel.Text = "Unidad medida";
            // 
            // unitControlLabel
            // 
            this.unitControlLabel.AutoSize = true;
            this.unitControlLabel.Location = new System.Drawing.Point(317, 119);
            this.unitControlLabel.Name = "unitControlLabel";
            this.unitControlLabel.Size = new System.Drawing.Size(94, 16);
            this.unitControlLabel.TabIndex = 25;
            this.unitControlLabel.Text = "Unidad control";
            // 
            // contentControlLabel
            // 
            this.contentControlLabel.AutoSize = true;
            this.contentControlLabel.Location = new System.Drawing.Point(317, 175);
            this.contentControlLabel.Name = "contentControlLabel";
            this.contentControlLabel.Size = new System.Drawing.Size(111, 16);
            this.contentControlLabel.TabIndex = 26;
            this.contentControlLabel.Text = "Contenido control";
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.Location = new System.Drawing.Point(317, 231);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(46, 16);
            this.priceLabel.TabIndex = 27;
            this.priceLabel.Text = "Precio";
            // 
            // priceDateLabel
            // 
            this.priceDateLabel.AutoSize = true;
            this.priceDateLabel.Location = new System.Drawing.Point(317, 287);
            this.priceDateLabel.Name = "priceDateLabel";
            this.priceDateLabel.Size = new System.Drawing.Size(86, 16);
            this.priceDateLabel.TabIndex = 28;
            this.priceDateLabel.Text = "Fecha precio";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(21, 287);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(51, 16);
            this.statusLabel.TabIndex = 30;
            this.statusLabel.Text = "Estatus";
            // 
            // componentLabel
            // 
            this.componentLabel.AutoSize = true;
            this.componentLabel.Location = new System.Drawing.Point(597, 79);
            this.componentLabel.Name = "componentLabel";
            this.componentLabel.Size = new System.Drawing.Size(84, 16);
            this.componentLabel.TabIndex = 32;
            this.componentLabel.Text = "Componente";
            // 
            // quantityLabel
            // 
            this.quantityLabel.AutoSize = true;
            this.quantityLabel.Location = new System.Drawing.Point(842, 79);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(61, 16);
            this.quantityLabel.TabIndex = 33;
            this.quantityLabel.Text = "Cantidad";
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(21, 423);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(52, 16);
            this.searchLabel.TabIndex = 34;
            this.searchLabel.Text = "Buscar:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(82, 419);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(260, 22);
            this.searchTextBox.TabIndex = 35;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // inventoryCheckBox
            // 
            this.inventoryCheckBox.AutoSize = true;
            this.inventoryCheckBox.Location = new System.Drawing.Point(24, 364);
            this.inventoryCheckBox.Name = "inventoryCheckBox";
            this.inventoryCheckBox.Size = new System.Drawing.Size(99, 20);
            this.inventoryCheckBox.TabIndex = 11;
            this.inventoryCheckBox.Text = "Tiene stock";
            this.inventoryCheckBox.UseVisualStyleBackColor = true;
            // 
            // inventoryLabel
            // 
            this.inventoryLabel.AutoSize = true;
            this.inventoryLabel.Location = new System.Drawing.Point(21, 344);
            this.inventoryLabel.Name = "inventoryLabel";
            this.inventoryLabel.Size = new System.Drawing.Size(110, 16);
            this.inventoryLabel.TabIndex = 31;
            this.inventoryLabel.Text = "Control inventario";
            // 
            // ArticleManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 731);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.quantityLabel);
            this.Controls.Add(this.componentLabel);
            this.Controls.Add(this.inventoryLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.priceDateLabel);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.contentControlLabel);
            this.Controls.Add(this.unitControlLabel);
            this.Controls.Add(this.unitMeasureLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.shortNameLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.removeComponentButton);
            this.Controls.Add(this.addComponentButton);
            this.Controls.Add(this.componentQuantityTextBox);
            this.Controls.Add(this.componentComboBox);
            this.Controls.Add(this.componentsGrid);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.inventoryCheckBox);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.priceDatePicker);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.contentControlTextBox);
            this.Controls.Add(this.unitControlTextBox);
            this.Controls.Add(this.unitMeasureTextBox);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.shortNameTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.articlesGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArticleManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de artículos";
            this.Load += new System.EventHandler(this.ArticleManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.articlesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView articlesGrid;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox shortNameTextBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.TextBox unitMeasureTextBox;
        private System.Windows.Forms.TextBox unitControlTextBox;
        private System.Windows.Forms.TextBox contentControlTextBox;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.DateTimePicker priceDatePicker;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.DataGridView componentsGrid;
        private System.Windows.Forms.ComboBox componentComboBox;
        private System.Windows.Forms.TextBox componentQuantityTextBox;
        private System.Windows.Forms.Button addComponentButton;
        private System.Windows.Forms.Button removeComponentButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label shortNameLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label unitMeasureLabel;
        private System.Windows.Forms.Label unitControlLabel;
        private System.Windows.Forms.Label contentControlLabel;
        private System.Windows.Forms.Label priceLabel;
        private System.Windows.Forms.Label priceDateLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label componentLabel;
        private System.Windows.Forms.Label quantityLabel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.CheckBox inventoryCheckBox;
        private System.Windows.Forms.Label inventoryLabel;
    }
}
