namespace Control_Pedidos.Views.Orders
{
    partial class OrderManagementForm
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
            this.clientNameLabel = new System.Windows.Forms.Label();
            this.clientNameTextBox = new System.Windows.Forms.TextBox();
            this.clientPhoneLabel = new System.Windows.Forms.Label();
            this.clientPhoneTextBox = new System.Windows.Forms.TextBox();
            this.clientAddressLabel = new System.Windows.Forms.Label();
            this.clientAddressTextBox = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.userRoleLabel = new System.Windows.Forms.Label();
            this.userRoleTextBox = new System.Windows.Forms.TextBox();
            this.companyLabel = new System.Windows.Forms.Label();
            this.companyComboBox = new System.Windows.Forms.ComboBox();
            this.eventLabel = new System.Windows.Forms.Label();
            this.eventComboBox = new System.Windows.Forms.ComboBox();
            this.folioLabel = new System.Windows.Forms.Label();
            this.folioTextBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.fechaLabel = new System.Windows.Forms.Label();
            this.fechaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fechaEntregaLabel = new System.Windows.Forms.Label();
            this.fechaEntregaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.horaEntregaLabel = new System.Windows.Forms.Label();
            this.horaEntregaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.articuloLabel = new System.Windows.Forms.Label();
            this.articuloComboBox = new System.Windows.Forms.ComboBox();
            this.cantidadLabel = new System.Windows.Forms.Label();
            this.cantidadNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.precioLabel = new System.Windows.Forms.Label();
            this.precioNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.totalArticuloLabel = new System.Windows.Forms.Label();
            this.totalArticuloTextBox = new System.Windows.Forms.TextBox();
            this.agregarArticuloButton = new System.Windows.Forms.Button();
            this.detallesGrid = new System.Windows.Forms.DataGridView();
            this.eliminarArticuloButton = new System.Windows.Forms.Button();
            this.totalGeneralLabel = new System.Windows.Forms.Label();
            this.totalGeneralValueLabel = new System.Windows.Forms.Label();
            this.cerrarPedidoButton = new System.Windows.Forms.Button();
            this.cancelarPedidoButton = new System.Windows.Forms.Button();
            this.cerrarVentanaButton = new System.Windows.Forms.Button();
            this.clientRfcLabel = new System.Windows.Forms.Label();
            this.clientRfcTextBox = new System.Windows.Forms.TextBox();
            this.clientEmailLabel = new System.Windows.Forms.Label();
            this.clientEmailTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cantidadNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.precioNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detallesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // clientNameLabel
            // 
            this.clientNameLabel.AutoSize = true;
            this.clientNameLabel.Location = new System.Drawing.Point(22, 22);
            this.clientNameLabel.Name = "clientNameLabel";
            this.clientNameLabel.Size = new System.Drawing.Size(44, 13);
            this.clientNameLabel.TabIndex = 0;
            this.clientNameLabel.Text = "Cliente";
            // 
            // clientNameTextBox
            // 
            this.clientNameTextBox.Location = new System.Drawing.Point(110, 19);
            this.clientNameTextBox.Name = "clientNameTextBox";
            this.clientNameTextBox.ReadOnly = true;
            this.clientNameTextBox.Size = new System.Drawing.Size(280, 20);
            this.clientNameTextBox.TabIndex = 1;
            // 
            // clientPhoneLabel
            // 
            this.clientPhoneLabel.AutoSize = true;
            this.clientPhoneLabel.Location = new System.Drawing.Point(22, 52);
            this.clientPhoneLabel.Name = "clientPhoneLabel";
            this.clientPhoneLabel.Size = new System.Drawing.Size(49, 13);
            this.clientPhoneLabel.TabIndex = 2;
            this.clientPhoneLabel.Text = "Teléfono";
            // 
            // clientPhoneTextBox
            // 
            this.clientPhoneTextBox.Location = new System.Drawing.Point(110, 49);
            this.clientPhoneTextBox.Name = "clientPhoneTextBox";
            this.clientPhoneTextBox.ReadOnly = true;
            this.clientPhoneTextBox.Size = new System.Drawing.Size(280, 20);
            this.clientPhoneTextBox.TabIndex = 3;
            // 
            // clientAddressLabel
            // 
            this.clientAddressLabel.AutoSize = true;
            this.clientAddressLabel.Location = new System.Drawing.Point(22, 82);
            this.clientAddressLabel.Name = "clientAddressLabel";
            this.clientAddressLabel.Size = new System.Drawing.Size(52, 13);
            this.clientAddressLabel.TabIndex = 4;
            this.clientAddressLabel.Text = "Domicilio";
            // 
            // clientAddressTextBox
            // 
            this.clientAddressTextBox.Location = new System.Drawing.Point(110, 79);
            this.clientAddressTextBox.Multiline = true;
            this.clientAddressTextBox.Name = "clientAddressTextBox";
            this.clientAddressTextBox.ReadOnly = true;
            this.clientAddressTextBox.Size = new System.Drawing.Size(280, 42);
            this.clientAddressTextBox.TabIndex = 5;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(421, 22);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(49, 13);
            this.userNameLabel.TabIndex = 6;
            this.userNameLabel.Text = "Usuario";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(490, 19);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.ReadOnly = true;
            this.userNameTextBox.Size = new System.Drawing.Size(240, 20);
            this.userNameTextBox.TabIndex = 7;
            // 
            // userRoleLabel
            // 
            this.userRoleLabel.AutoSize = true;
            this.userRoleLabel.Location = new System.Drawing.Point(421, 52);
            this.userRoleLabel.Name = "userRoleLabel";
            this.userRoleLabel.Size = new System.Drawing.Size(29, 13);
            this.userRoleLabel.TabIndex = 8;
            this.userRoleLabel.Text = "Rol";
            // 
            // userRoleTextBox
            // 
            this.userRoleTextBox.Location = new System.Drawing.Point(490, 49);
            this.userRoleTextBox.Name = "userRoleTextBox";
            this.userRoleTextBox.ReadOnly = true;
            this.userRoleTextBox.Size = new System.Drawing.Size(240, 20);
            this.userRoleTextBox.TabIndex = 9;
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Location = new System.Drawing.Point(421, 112);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(51, 13);
            this.companyLabel.TabIndex = 10;
            this.companyLabel.Text = "Empresa";
            // 
            // companyComboBox
            // 
            this.companyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companyComboBox.FormattingEnabled = true;
            this.companyComboBox.Location = new System.Drawing.Point(490, 109);
            this.companyComboBox.Name = "companyComboBox";
            this.companyComboBox.Size = new System.Drawing.Size(240, 21);
            this.companyComboBox.TabIndex = 11;
            this.companyComboBox.SelectedIndexChanged += new System.EventHandler(this.companyComboBox_SelectedIndexChanged);
            // 
            // eventLabel
            // 
            this.eventLabel.AutoSize = true;
            this.eventLabel.Location = new System.Drawing.Point(421, 142);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(44, 13);
            this.eventLabel.TabIndex = 12;
            this.eventLabel.Text = "Evento";
            // 
            // eventComboBox
            // 
            this.eventComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventComboBox.FormattingEnabled = true;
            this.eventComboBox.Location = new System.Drawing.Point(490, 139);
            this.eventComboBox.Name = "eventComboBox";
            this.eventComboBox.Size = new System.Drawing.Size(240, 21);
            this.eventComboBox.TabIndex = 13;
            // 
            // folioLabel
            // 
            this.folioLabel.AutoSize = true;
            this.folioLabel.Location = new System.Drawing.Point(756, 112);
            this.folioLabel.Name = "folioLabel";
            this.folioLabel.Size = new System.Drawing.Size(29, 13);
            this.folioLabel.TabIndex = 14;
            this.folioLabel.Text = "Folio";
            // 
            // folioTextBox
            // 
            this.folioTextBox.Location = new System.Drawing.Point(811, 109);
            this.folioTextBox.Name = "folioTextBox";
            this.folioTextBox.ReadOnly = true;
            this.folioTextBox.Size = new System.Drawing.Size(180, 20);
            this.folioTextBox.TabIndex = 15;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(756, 142);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(45, 13);
            this.statusLabel.TabIndex = 16;
            this.statusLabel.Text = "Estatus";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(811, 139);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ReadOnly = true;
            this.statusTextBox.Size = new System.Drawing.Size(180, 20);
            this.statusTextBox.TabIndex = 17;
            // 
            // fechaLabel
            // 
            this.fechaLabel.AutoSize = true;
            this.fechaLabel.Location = new System.Drawing.Point(421, 82);
            this.fechaLabel.Name = "fechaLabel";
            this.fechaLabel.Size = new System.Drawing.Size(37, 13);
            this.fechaLabel.TabIndex = 18;
            this.fechaLabel.Text = "Fecha";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaDateTimePicker.Location = new System.Drawing.Point(490, 79);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.Size = new System.Drawing.Size(116, 20);
            this.fechaDateTimePicker.TabIndex = 19;
            // 
            // fechaEntregaLabel
            // 
            this.fechaEntregaLabel.AutoSize = true;
            this.fechaEntregaLabel.Location = new System.Drawing.Point(622, 82);
            this.fechaEntregaLabel.Name = "fechaEntregaLabel";
            this.fechaEntregaLabel.Size = new System.Drawing.Size(76, 13);
            this.fechaEntregaLabel.TabIndex = 20;
            this.fechaEntregaLabel.Text = "Fecha entrega";
            // 
            // fechaEntregaDateTimePicker
            // 
            this.fechaEntregaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaEntregaDateTimePicker.Location = new System.Drawing.Point(704, 79);
            this.fechaEntregaDateTimePicker.Name = "fechaEntregaDateTimePicker";
            this.fechaEntregaDateTimePicker.Size = new System.Drawing.Size(116, 20);
            this.fechaEntregaDateTimePicker.TabIndex = 21;
            // 
            // horaEntregaLabel
            // 
            this.horaEntregaLabel.AutoSize = true;
            this.horaEntregaLabel.Location = new System.Drawing.Point(836, 82);
            this.horaEntregaLabel.Name = "horaEntregaLabel";
            this.horaEntregaLabel.Size = new System.Drawing.Size(70, 13);
            this.horaEntregaLabel.TabIndex = 22;
            this.horaEntregaLabel.Text = "Hora entrega";
            // 
            // horaEntregaDateTimePicker
            // 
            this.horaEntregaDateTimePicker.CustomFormat = "HH:mm";
            this.horaEntregaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.horaEntregaDateTimePicker.Location = new System.Drawing.Point(912, 79);
            this.horaEntregaDateTimePicker.Name = "horaEntregaDateTimePicker";
            this.horaEntregaDateTimePicker.ShowCheckBox = true;
            this.horaEntregaDateTimePicker.ShowUpDown = true;
            this.horaEntregaDateTimePicker.Size = new System.Drawing.Size(79, 20);
            this.horaEntregaDateTimePicker.TabIndex = 23;
            // 
            // articuloLabel
            // 
            this.articuloLabel.AutoSize = true;
            this.articuloLabel.Location = new System.Drawing.Point(22, 194);
            this.articuloLabel.Name = "articuloLabel";
            this.articuloLabel.Size = new System.Drawing.Size(45, 13);
            this.articuloLabel.TabIndex = 24;
            this.articuloLabel.Text = "Artículo";
            // 
            // articuloComboBox
            // 
            this.articuloComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.articuloComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.articuloComboBox.FormattingEnabled = true;
            this.articuloComboBox.Location = new System.Drawing.Point(110, 191);
            this.articuloComboBox.Name = "articuloComboBox";
            this.articuloComboBox.Size = new System.Drawing.Size(320, 21);
            this.articuloComboBox.TabIndex = 25;
            this.articuloComboBox.SelectedIndexChanged += new System.EventHandler(this.articuloComboBox_SelectedIndexChanged);
            // 
            // cantidadLabel
            // 
            this.cantidadLabel.AutoSize = true;
            this.cantidadLabel.Location = new System.Drawing.Point(448, 194);
            this.cantidadLabel.Name = "cantidadLabel";
            this.cantidadLabel.Size = new System.Drawing.Size(52, 13);
            this.cantidadLabel.TabIndex = 26;
            this.cantidadLabel.Text = "Cantidad";
            // 
            // cantidadNumericUpDown
            // 
            this.cantidadNumericUpDown.DecimalPlaces = 2;
            this.cantidadNumericUpDown.Location = new System.Drawing.Point(506, 191);
            this.cantidadNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.cantidadNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.cantidadNumericUpDown.Name = "cantidadNumericUpDown";
            this.cantidadNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.cantidadNumericUpDown.TabIndex = 27;
            this.cantidadNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cantidadNumericUpDown.ValueChanged += new System.EventHandler(this.DetalleValueChanged);
            // 
            // precioLabel
            // 
            this.precioLabel.AutoSize = true;
            this.precioLabel.Location = new System.Drawing.Point(604, 194);
            this.precioLabel.Name = "precioLabel";
            this.precioLabel.Size = new System.Drawing.Size(37, 13);
            this.precioLabel.TabIndex = 28;
            this.precioLabel.Text = "Precio";
            // 
            // precioNumericUpDown
            // 
            this.precioNumericUpDown.DecimalPlaces = 2;
            this.precioNumericUpDown.Location = new System.Drawing.Point(647, 191);
            this.precioNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.precioNumericUpDown.Name = "precioNumericUpDown";
            this.precioNumericUpDown.Size = new System.Drawing.Size(100, 20);
            this.precioNumericUpDown.TabIndex = 29;
            this.precioNumericUpDown.ValueChanged += new System.EventHandler(this.DetalleValueChanged);
            // 
            // totalArticuloLabel
            // 
            this.totalArticuloLabel.AutoSize = true;
            this.totalArticuloLabel.Location = new System.Drawing.Point(765, 194);
            this.totalArticuloLabel.Name = "totalArticuloLabel";
            this.totalArticuloLabel.Size = new System.Drawing.Size(31, 13);
            this.totalArticuloLabel.TabIndex = 30;
            this.totalArticuloLabel.Text = "Total";
            // 
            // totalArticuloTextBox
            // 
            this.totalArticuloTextBox.Location = new System.Drawing.Point(802, 191);
            this.totalArticuloTextBox.Name = "totalArticuloTextBox";
            this.totalArticuloTextBox.ReadOnly = true;
            this.totalArticuloTextBox.Size = new System.Drawing.Size(106, 20);
            this.totalArticuloTextBox.TabIndex = 31;
            // 
            // agregarArticuloButton
            // 
            this.agregarArticuloButton.Location = new System.Drawing.Point(928, 189);
            this.agregarArticuloButton.Name = "agregarArticuloButton";
            this.agregarArticuloButton.Size = new System.Drawing.Size(133, 23);
            this.agregarArticuloButton.TabIndex = 32;
            this.agregarArticuloButton.Text = "Agregar artículo";
            this.agregarArticuloButton.UseVisualStyleBackColor = true;
            this.agregarArticuloButton.Click += new System.EventHandler(this.agregarArticuloButton_Click);
            // 
            // detallesGrid
            // 
            this.detallesGrid.AllowUserToAddRows = false;
            this.detallesGrid.AllowUserToDeleteRows = false;
            this.detallesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detallesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.detallesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detallesGrid.Location = new System.Drawing.Point(25, 226);
            this.detallesGrid.MultiSelect = false;
            this.detallesGrid.Name = "detallesGrid";
            this.detallesGrid.ReadOnly = true;
            this.detallesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detallesGrid.Size = new System.Drawing.Size(1036, 276);
            this.detallesGrid.TabIndex = 33;
            // 
            // eliminarArticuloButton
            // 
            this.eliminarArticuloButton.Location = new System.Drawing.Point(25, 518);
            this.eliminarArticuloButton.Name = "eliminarArticuloButton";
            this.eliminarArticuloButton.Size = new System.Drawing.Size(133, 27);
            this.eliminarArticuloButton.TabIndex = 34;
            this.eliminarArticuloButton.Text = "Eliminar artículo";
            this.eliminarArticuloButton.UseVisualStyleBackColor = true;
            this.eliminarArticuloButton.Click += new System.EventHandler(this.eliminarArticuloButton_Click);
            // 
            // totalGeneralLabel
            // 
            this.totalGeneralLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalGeneralLabel.AutoSize = true;
            this.totalGeneralLabel.Location = new System.Drawing.Point(762, 525);
            this.totalGeneralLabel.Name = "totalGeneralLabel";
            this.totalGeneralLabel.Size = new System.Drawing.Size(74, 13);
            this.totalGeneralLabel.TabIndex = 35;
            this.totalGeneralLabel.Text = "Total pedido";
            // 
            // totalGeneralValueLabel
            // 
            this.totalGeneralValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalGeneralValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.totalGeneralValueLabel.Location = new System.Drawing.Point(842, 518);
            this.totalGeneralValueLabel.Name = "totalGeneralValueLabel";
            this.totalGeneralValueLabel.Size = new System.Drawing.Size(103, 23);
            this.totalGeneralValueLabel.TabIndex = 36;
            this.totalGeneralValueLabel.Text = "$0.00";
            this.totalGeneralValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cerrarPedidoButton
            // 
            this.cerrarPedidoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cerrarPedidoButton.Location = new System.Drawing.Point(290, 518);
            this.cerrarPedidoButton.Name = "cerrarPedidoButton";
            this.cerrarPedidoButton.Size = new System.Drawing.Size(133, 27);
            this.cerrarPedidoButton.TabIndex = 37;
            this.cerrarPedidoButton.Text = "Cerrar pedido";
            this.cerrarPedidoButton.UseVisualStyleBackColor = true;
            this.cerrarPedidoButton.Click += new System.EventHandler(this.cerrarPedidoButton_Click);
            // 
            // cancelarPedidoButton
            // 
            this.cancelarPedidoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelarPedidoButton.Location = new System.Drawing.Point(429, 518);
            this.cancelarPedidoButton.Name = "cancelarPedidoButton";
            this.cancelarPedidoButton.Size = new System.Drawing.Size(133, 27);
            this.cancelarPedidoButton.TabIndex = 38;
            this.cancelarPedidoButton.Text = "Cancelar pedido";
            this.cancelarPedidoButton.UseVisualStyleBackColor = true;
            this.cancelarPedidoButton.Click += new System.EventHandler(this.cancelarPedidoButton_Click);
            // 
            // cerrarVentanaButton
            // 
            this.cerrarVentanaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cerrarVentanaButton.Location = new System.Drawing.Point(968, 518);
            this.cerrarVentanaButton.Name = "cerrarVentanaButton";
            this.cerrarVentanaButton.Size = new System.Drawing.Size(93, 27);
            this.cerrarVentanaButton.TabIndex = 39;
            this.cerrarVentanaButton.Text = "Cerrar";
            this.cerrarVentanaButton.UseVisualStyleBackColor = true;
            this.cerrarVentanaButton.Click += new System.EventHandler(this.cerrarVentanaButton_Click);
            // 
            // clientRfcLabel
            // 
            this.clientRfcLabel.AutoSize = true;
            this.clientRfcLabel.Location = new System.Drawing.Point(22, 132);
            this.clientRfcLabel.Name = "clientRfcLabel";
            this.clientRfcLabel.Size = new System.Drawing.Size(28, 13);
            this.clientRfcLabel.TabIndex = 40;
            this.clientRfcLabel.Text = "RFC";
            // 
            // clientRfcTextBox
            // 
            this.clientRfcTextBox.Location = new System.Drawing.Point(110, 129);
            this.clientRfcTextBox.Name = "clientRfcTextBox";
            this.clientRfcTextBox.ReadOnly = true;
            this.clientRfcTextBox.Size = new System.Drawing.Size(280, 20);
            this.clientRfcTextBox.TabIndex = 41;
            // 
            // clientEmailLabel
            // 
            this.clientEmailLabel.AutoSize = true;
            this.clientEmailLabel.Location = new System.Drawing.Point(22, 162);
            this.clientEmailLabel.Name = "clientEmailLabel";
            this.clientEmailLabel.Size = new System.Drawing.Size(38, 13);
            this.clientEmailLabel.TabIndex = 42;
            this.clientEmailLabel.Text = "Correo";
            // 
            // clientEmailTextBox
            // 
            this.clientEmailTextBox.Location = new System.Drawing.Point(110, 159);
            this.clientEmailTextBox.Name = "clientEmailTextBox";
            this.clientEmailTextBox.ReadOnly = true;
            this.clientEmailTextBox.Size = new System.Drawing.Size(280, 20);
            this.clientEmailTextBox.TabIndex = 43;
            // 
            // OrderManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 565);
            this.Controls.Add(this.clientEmailTextBox);
            this.Controls.Add(this.clientEmailLabel);
            this.Controls.Add(this.clientRfcTextBox);
            this.Controls.Add(this.clientRfcLabel);
            this.Controls.Add(this.cerrarVentanaButton);
            this.Controls.Add(this.cancelarPedidoButton);
            this.Controls.Add(this.cerrarPedidoButton);
            this.Controls.Add(this.totalGeneralValueLabel);
            this.Controls.Add(this.totalGeneralLabel);
            this.Controls.Add(this.eliminarArticuloButton);
            this.Controls.Add(this.detallesGrid);
            this.Controls.Add(this.agregarArticuloButton);
            this.Controls.Add(this.totalArticuloTextBox);
            this.Controls.Add(this.totalArticuloLabel);
            this.Controls.Add(this.precioNumericUpDown);
            this.Controls.Add(this.precioLabel);
            this.Controls.Add(this.cantidadNumericUpDown);
            this.Controls.Add(this.cantidadLabel);
            this.Controls.Add(this.articuloComboBox);
            this.Controls.Add(this.articuloLabel);
            this.Controls.Add(this.horaEntregaDateTimePicker);
            this.Controls.Add(this.horaEntregaLabel);
            this.Controls.Add(this.fechaEntregaDateTimePicker);
            this.Controls.Add(this.fechaEntregaLabel);
            this.Controls.Add(this.fechaDateTimePicker);
            this.Controls.Add(this.fechaLabel);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.folioTextBox);
            this.Controls.Add(this.folioLabel);
            this.Controls.Add(this.eventComboBox);
            this.Controls.Add(this.eventLabel);
            this.Controls.Add(this.companyComboBox);
            this.Controls.Add(this.companyLabel);
            this.Controls.Add(this.userRoleTextBox);
            this.Controls.Add(this.userRoleLabel);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.clientAddressTextBox);
            this.Controls.Add(this.clientAddressLabel);
            this.Controls.Add(this.clientPhoneTextBox);
            this.Controls.Add(this.clientPhoneLabel);
            this.Controls.Add(this.clientNameTextBox);
            this.Controls.Add(this.clientNameLabel);
            this.MinimumSize = new System.Drawing.Size(1102, 604);
            this.Name = "OrderManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de pedidos";
            ((System.ComponentModel.ISupportInitialize)(this.cantidadNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.precioNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detallesGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label clientNameLabel;
        private System.Windows.Forms.TextBox clientNameTextBox;
        private System.Windows.Forms.Label clientPhoneLabel;
        private System.Windows.Forms.TextBox clientPhoneTextBox;
        private System.Windows.Forms.Label clientAddressLabel;
        private System.Windows.Forms.TextBox clientAddressTextBox;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label userRoleLabel;
        private System.Windows.Forms.TextBox userRoleTextBox;
        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.ComboBox companyComboBox;
        private System.Windows.Forms.Label eventLabel;
        private System.Windows.Forms.ComboBox eventComboBox;
        private System.Windows.Forms.Label folioLabel;
        private System.Windows.Forms.TextBox folioTextBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.Label fechaLabel;
        private System.Windows.Forms.DateTimePicker fechaDateTimePicker;
        private System.Windows.Forms.Label fechaEntregaLabel;
        private System.Windows.Forms.DateTimePicker fechaEntregaDateTimePicker;
        private System.Windows.Forms.Label horaEntregaLabel;
        private System.Windows.Forms.DateTimePicker horaEntregaDateTimePicker;
        private System.Windows.Forms.Label articuloLabel;
        private System.Windows.Forms.ComboBox articuloComboBox;
        private System.Windows.Forms.Label cantidadLabel;
        private System.Windows.Forms.NumericUpDown cantidadNumericUpDown;
        private System.Windows.Forms.Label precioLabel;
        private System.Windows.Forms.NumericUpDown precioNumericUpDown;
        private System.Windows.Forms.Label totalArticuloLabel;
        private System.Windows.Forms.TextBox totalArticuloTextBox;
        private System.Windows.Forms.Button agregarArticuloButton;
        private System.Windows.Forms.DataGridView detallesGrid;
        private System.Windows.Forms.Button eliminarArticuloButton;
        private System.Windows.Forms.Label totalGeneralLabel;
        private System.Windows.Forms.Label totalGeneralValueLabel;
        private System.Windows.Forms.Button cerrarPedidoButton;
        private System.Windows.Forms.Button cancelarPedidoButton;
        private System.Windows.Forms.Button cerrarVentanaButton;
        private System.Windows.Forms.Label clientRfcLabel;
        private System.Windows.Forms.TextBox clientRfcTextBox;
        private System.Windows.Forms.Label clientEmailLabel;
        private System.Windows.Forms.TextBox clientEmailTextBox;
    }
}
