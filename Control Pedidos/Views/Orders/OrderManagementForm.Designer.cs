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
            this.notesLabel = new System.Windows.Forms.Label();
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.kitComponentsLabel = new System.Windows.Forms.Label();
            this.kitComponentsRichTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cantidadNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.precioNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detallesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // clientNameLabel
            // 
            this.clientNameLabel.AutoSize = true;
            this.clientNameLabel.Location = new System.Drawing.Point(35, 69);
            this.clientNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientNameLabel.Name = "clientNameLabel";
            this.clientNameLabel.Size = new System.Drawing.Size(48, 16);
            this.clientNameLabel.TabIndex = 0;
            this.clientNameLabel.Text = "Cliente";
            // 
            // clientNameTextBox
            // 
            this.clientNameTextBox.Location = new System.Drawing.Point(153, 65);
            this.clientNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.clientNameTextBox.Name = "clientNameTextBox";
            this.clientNameTextBox.ReadOnly = true;
            this.clientNameTextBox.Size = new System.Drawing.Size(372, 22);
            this.clientNameTextBox.TabIndex = 1;
            // 
            // clientPhoneLabel
            // 
            this.clientPhoneLabel.AutoSize = true;
            this.clientPhoneLabel.Location = new System.Drawing.Point(35, 106);
            this.clientPhoneLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientPhoneLabel.Name = "clientPhoneLabel";
            this.clientPhoneLabel.Size = new System.Drawing.Size(61, 16);
            this.clientPhoneLabel.TabIndex = 2;
            this.clientPhoneLabel.Text = "Teléfono";
            // 
            // clientPhoneTextBox
            // 
            this.clientPhoneTextBox.Location = new System.Drawing.Point(153, 102);
            this.clientPhoneTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.clientPhoneTextBox.Name = "clientPhoneTextBox";
            this.clientPhoneTextBox.ReadOnly = true;
            this.clientPhoneTextBox.Size = new System.Drawing.Size(372, 22);
            this.clientPhoneTextBox.TabIndex = 3;
            // 
            // clientAddressLabel
            // 
            this.clientAddressLabel.AutoSize = true;
            this.clientAddressLabel.Location = new System.Drawing.Point(35, 143);
            this.clientAddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientAddressLabel.Name = "clientAddressLabel";
            this.clientAddressLabel.Size = new System.Drawing.Size(63, 16);
            this.clientAddressLabel.TabIndex = 4;
            this.clientAddressLabel.Text = "Domicilio";
            // 
            // clientAddressTextBox
            // 
            this.clientAddressTextBox.Location = new System.Drawing.Point(153, 139);
            this.clientAddressTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.clientAddressTextBox.Multiline = true;
            this.clientAddressTextBox.Name = "clientAddressTextBox";
            this.clientAddressTextBox.ReadOnly = true;
            this.clientAddressTextBox.Size = new System.Drawing.Size(372, 51);
            this.clientAddressTextBox.TabIndex = 5;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(567, 69);
            this.userNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(54, 16);
            this.userNameLabel.TabIndex = 6;
            this.userNameLabel.Text = "Usuario";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(659, 65);
            this.userNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.ReadOnly = true;
            this.userNameTextBox.Size = new System.Drawing.Size(319, 22);
            this.userNameTextBox.TabIndex = 7;
            // 
            // userRoleLabel
            // 
            this.userRoleLabel.AutoSize = true;
            this.userRoleLabel.Location = new System.Drawing.Point(567, 106);
            this.userRoleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userRoleLabel.Name = "userRoleLabel";
            this.userRoleLabel.Size = new System.Drawing.Size(28, 16);
            this.userRoleLabel.TabIndex = 8;
            this.userRoleLabel.Text = "Rol";
            // 
            // userRoleTextBox
            // 
            this.userRoleTextBox.Location = new System.Drawing.Point(659, 102);
            this.userRoleTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.userRoleTextBox.Name = "userRoleTextBox";
            this.userRoleTextBox.ReadOnly = true;
            this.userRoleTextBox.Size = new System.Drawing.Size(319, 22);
            this.userRoleTextBox.TabIndex = 9;
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Location = new System.Drawing.Point(567, 180);
            this.companyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(62, 16);
            this.companyLabel.TabIndex = 10;
            this.companyLabel.Text = "Empresa";
            // 
            // companyComboBox
            // 
            this.companyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.companyComboBox.FormattingEnabled = true;
            this.companyComboBox.Location = new System.Drawing.Point(659, 176);
            this.companyComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.companyComboBox.Name = "companyComboBox";
            this.companyComboBox.Size = new System.Drawing.Size(319, 24);
            this.companyComboBox.TabIndex = 11;
            this.companyComboBox.SelectedIndexChanged += new System.EventHandler(this.companyComboBox_SelectedIndexChanged);
            // 
            // eventLabel
            // 
            this.eventLabel.AutoSize = true;
            this.eventLabel.Location = new System.Drawing.Point(567, 217);
            this.eventLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(49, 16);
            this.eventLabel.TabIndex = 12;
            this.eventLabel.Text = "Evento";
            // 
            // eventComboBox
            // 
            this.eventComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventComboBox.FormattingEnabled = true;
            this.eventComboBox.Location = new System.Drawing.Point(659, 213);
            this.eventComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.eventComboBox.Name = "eventComboBox";
            this.eventComboBox.Size = new System.Drawing.Size(319, 24);
            this.eventComboBox.TabIndex = 13;
            this.eventComboBox.SelectedIndexChanged += new System.EventHandler(this.eventComboBox_SelectedIndexChanged);
            // 
            // folioLabel
            // 
            this.folioLabel.AutoSize = true;
            this.folioLabel.Location = new System.Drawing.Point(1014, 180);
            this.folioLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.folioLabel.Name = "folioLabel";
            this.folioLabel.Size = new System.Drawing.Size(37, 16);
            this.folioLabel.TabIndex = 14;
            this.folioLabel.Text = "Folio";
            // 
            // folioTextBox
            // 
            this.folioTextBox.Location = new System.Drawing.Point(1087, 176);
            this.folioTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.folioTextBox.Name = "folioTextBox";
            this.folioTextBox.ReadOnly = true;
            this.folioTextBox.Size = new System.Drawing.Size(239, 22);
            this.folioTextBox.TabIndex = 15;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(1014, 217);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(51, 16);
            this.statusLabel.TabIndex = 16;
            this.statusLabel.Text = "Estatus";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(1087, 213);
            this.statusTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ReadOnly = true;
            this.statusTextBox.Size = new System.Drawing.Size(239, 22);
            this.statusTextBox.TabIndex = 17;
            // 
            // fechaLabel
            // 
            this.fechaLabel.AutoSize = true;
            this.fechaLabel.Location = new System.Drawing.Point(567, 143);
            this.fechaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fechaLabel.Name = "fechaLabel";
            this.fechaLabel.Size = new System.Drawing.Size(45, 16);
            this.fechaLabel.TabIndex = 18;
            this.fechaLabel.Text = "Fecha";
            // 
            // fechaDateTimePicker
            // 
            this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaDateTimePicker.Location = new System.Drawing.Point(659, 139);
            this.fechaDateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.fechaDateTimePicker.Name = "fechaDateTimePicker";
            this.fechaDateTimePicker.Size = new System.Drawing.Size(153, 22);
            this.fechaDateTimePicker.TabIndex = 19;
            // 
            // fechaEntregaLabel
            // 
            this.fechaEntregaLabel.AutoSize = true;
            this.fechaEntregaLabel.Location = new System.Drawing.Point(835, 143);
            this.fechaEntregaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fechaEntregaLabel.Name = "fechaEntregaLabel";
            this.fechaEntregaLabel.Size = new System.Drawing.Size(94, 16);
            this.fechaEntregaLabel.TabIndex = 20;
            this.fechaEntregaLabel.Text = "Fecha entrega";
            // 
            // fechaEntregaDateTimePicker
            // 
            this.fechaEntregaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaEntregaDateTimePicker.Location = new System.Drawing.Point(945, 139);
            this.fechaEntregaDateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.fechaEntregaDateTimePicker.Name = "fechaEntregaDateTimePicker";
            this.fechaEntregaDateTimePicker.Size = new System.Drawing.Size(153, 22);
            this.fechaEntregaDateTimePicker.TabIndex = 21;
            // 
            // horaEntregaLabel
            // 
            this.horaEntregaLabel.AutoSize = true;
            this.horaEntregaLabel.Location = new System.Drawing.Point(1121, 143);
            this.horaEntregaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.horaEntregaLabel.Name = "horaEntregaLabel";
            this.horaEntregaLabel.Size = new System.Drawing.Size(86, 16);
            this.horaEntregaLabel.TabIndex = 22;
            this.horaEntregaLabel.Text = "Hora entrega";
            // 
            // horaEntregaDateTimePicker
            // 
            this.horaEntregaDateTimePicker.CustomFormat = "HH:mm";
            this.horaEntregaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.horaEntregaDateTimePicker.Location = new System.Drawing.Point(1222, 139);
            this.horaEntregaDateTimePicker.Margin = new System.Windows.Forms.Padding(4);
            this.horaEntregaDateTimePicker.Name = "horaEntregaDateTimePicker";
            this.horaEntregaDateTimePicker.ShowCheckBox = true;
            this.horaEntregaDateTimePicker.ShowUpDown = true;
            this.horaEntregaDateTimePicker.Size = new System.Drawing.Size(104, 22);
            this.horaEntregaDateTimePicker.TabIndex = 23;
            // 
            // articuloLabel
            // 
            this.articuloLabel.AutoSize = true;
            this.articuloLabel.Location = new System.Drawing.Point(35, 281);
            this.articuloLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.articuloLabel.Name = "articuloLabel";
            this.articuloLabel.Size = new System.Drawing.Size(51, 16);
            this.articuloLabel.TabIndex = 24;
            this.articuloLabel.Text = "Artículo";
            // 
            // articuloComboBox
            // 
            this.articuloComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.articuloComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.articuloComboBox.FormattingEnabled = true;
            this.articuloComboBox.Location = new System.Drawing.Point(153, 277);
            this.articuloComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.articuloComboBox.Name = "articuloComboBox";
            this.articuloComboBox.Size = new System.Drawing.Size(372, 24);
            this.articuloComboBox.TabIndex = 25;
            this.articuloComboBox.SelectedIndexChanged += new System.EventHandler(this.articuloComboBox_SelectedIndexChanged);
            // 
            // cantidadLabel
            // 
            this.cantidadLabel.AutoSize = true;
            this.cantidadLabel.Location = new System.Drawing.Point(603, 281);
            this.cantidadLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cantidadLabel.Name = "cantidadLabel";
            this.cantidadLabel.Size = new System.Drawing.Size(61, 16);
            this.cantidadLabel.TabIndex = 26;
            this.cantidadLabel.Text = "Cantidad";
            // 
            // cantidadNumericUpDown
            // 
            this.cantidadNumericUpDown.DecimalPlaces = 2;
            this.cantidadNumericUpDown.Location = new System.Drawing.Point(681, 277);
            this.cantidadNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
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
            this.cantidadNumericUpDown.Size = new System.Drawing.Size(107, 22);
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
            this.precioLabel.Location = new System.Drawing.Point(811, 281);
            this.precioLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.precioLabel.Name = "precioLabel";
            this.precioLabel.Size = new System.Drawing.Size(46, 16);
            this.precioLabel.TabIndex = 28;
            this.precioLabel.Text = "Precio";
            // 
            // precioNumericUpDown
            // 
            this.precioNumericUpDown.DecimalPlaces = 2;
            this.precioNumericUpDown.Location = new System.Drawing.Point(869, 277);
            this.precioNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.precioNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.precioNumericUpDown.Name = "precioNumericUpDown";
            this.precioNumericUpDown.Size = new System.Drawing.Size(133, 22);
            this.precioNumericUpDown.TabIndex = 29;
            this.precioNumericUpDown.ValueChanged += new System.EventHandler(this.DetalleValueChanged);
            // 
            // totalArticuloLabel
            // 
            this.totalArticuloLabel.AutoSize = true;
            this.totalArticuloLabel.Location = new System.Drawing.Point(1026, 281);
            this.totalArticuloLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalArticuloLabel.Name = "totalArticuloLabel";
            this.totalArticuloLabel.Size = new System.Drawing.Size(38, 16);
            this.totalArticuloLabel.TabIndex = 30;
            this.totalArticuloLabel.Text = "Total";
            // 
            // totalArticuloTextBox
            // 
            this.totalArticuloTextBox.Location = new System.Drawing.Point(1075, 277);
            this.totalArticuloTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.totalArticuloTextBox.Name = "totalArticuloTextBox";
            this.totalArticuloTextBox.ReadOnly = true;
            this.totalArticuloTextBox.Size = new System.Drawing.Size(140, 22);
            this.totalArticuloTextBox.TabIndex = 31;
            // 
            // agregarArticuloButton
            // 
            this.agregarArticuloButton.Location = new System.Drawing.Point(1243, 275);
            this.agregarArticuloButton.Margin = new System.Windows.Forms.Padding(4);
            this.agregarArticuloButton.Name = "agregarArticuloButton";
            this.agregarArticuloButton.Size = new System.Drawing.Size(177, 28);
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
            this.detallesGrid.Location = new System.Drawing.Point(33, 322);
            this.detallesGrid.Margin = new System.Windows.Forms.Padding(4);
            this.detallesGrid.MultiSelect = false;
            this.detallesGrid.Name = "detallesGrid";
            this.detallesGrid.ReadOnly = true;
            this.detallesGrid.RowHeadersWidth = 51;
            this.detallesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detallesGrid.Size = new System.Drawing.Size(1384, 364);
            this.detallesGrid.TabIndex = 35;
            // 
            // eliminarArticuloButton
            // 
            this.eliminarArticuloButton.Location = new System.Drawing.Point(33, 694);
            this.eliminarArticuloButton.Margin = new System.Windows.Forms.Padding(4);
            this.eliminarArticuloButton.Name = "eliminarArticuloButton";
            this.eliminarArticuloButton.Size = new System.Drawing.Size(177, 33);
            this.eliminarArticuloButton.TabIndex = 36;
            this.eliminarArticuloButton.Text = "Eliminar artículo";
            this.eliminarArticuloButton.UseVisualStyleBackColor = true;
            this.eliminarArticuloButton.Click += new System.EventHandler(this.eliminarArticuloButton_Click);
            // 
            // totalGeneralLabel
            // 
            this.totalGeneralLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalGeneralLabel.AutoSize = true;
            this.totalGeneralLabel.Location = new System.Drawing.Point(1018, 702);
            this.totalGeneralLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalGeneralLabel.Name = "totalGeneralLabel";
            this.totalGeneralLabel.Size = new System.Drawing.Size(84, 16);
            this.totalGeneralLabel.TabIndex = 37;
            this.totalGeneralLabel.Text = "Total pedido";
            // 
            // totalGeneralValueLabel
            // 
            this.totalGeneralValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalGeneralValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.totalGeneralValueLabel.Location = new System.Drawing.Point(1125, 694);
            this.totalGeneralValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalGeneralValueLabel.Name = "totalGeneralValueLabel";
            this.totalGeneralValueLabel.Size = new System.Drawing.Size(137, 28);
            this.totalGeneralValueLabel.TabIndex = 38;
            this.totalGeneralValueLabel.Text = "$0.00";
            this.totalGeneralValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cerrarPedidoButton
            // 
            this.cerrarPedidoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cerrarPedidoButton.Location = new System.Drawing.Point(400, 694);
            this.cerrarPedidoButton.Margin = new System.Windows.Forms.Padding(4);
            this.cerrarPedidoButton.Name = "cerrarPedidoButton";
            this.cerrarPedidoButton.Size = new System.Drawing.Size(177, 33);
            this.cerrarPedidoButton.TabIndex = 39;
            this.cerrarPedidoButton.Text = "Cerrar pedido";
            this.cerrarPedidoButton.UseVisualStyleBackColor = true;
            this.cerrarPedidoButton.Click += new System.EventHandler(this.cerrarPedidoButton_Click);
            // 
            // cancelarPedidoButton
            // 
            this.cancelarPedidoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelarPedidoButton.Location = new System.Drawing.Point(585, 694);
            this.cancelarPedidoButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelarPedidoButton.Name = "cancelarPedidoButton";
            this.cancelarPedidoButton.Size = new System.Drawing.Size(177, 33);
            this.cancelarPedidoButton.TabIndex = 40;
            this.cancelarPedidoButton.Text = "Cancelar pedido";
            this.cancelarPedidoButton.UseVisualStyleBackColor = true;
            this.cancelarPedidoButton.Click += new System.EventHandler(this.cancelarPedidoButton_Click);
            // 
            // cerrarVentanaButton
            // 
            this.cerrarVentanaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cerrarVentanaButton.Location = new System.Drawing.Point(1293, 694);
            this.cerrarVentanaButton.Margin = new System.Windows.Forms.Padding(4);
            this.cerrarVentanaButton.Name = "cerrarVentanaButton";
            this.cerrarVentanaButton.Size = new System.Drawing.Size(124, 33);
            this.cerrarVentanaButton.TabIndex = 41;
            this.cerrarVentanaButton.Text = "Cerrar";
            this.cerrarVentanaButton.UseVisualStyleBackColor = true;
            this.cerrarVentanaButton.Click += new System.EventHandler(this.cerrarVentanaButton_Click);
            // 
            // clientRfcLabel
            // 
            this.clientRfcLabel.AutoSize = true;
            this.clientRfcLabel.Location = new System.Drawing.Point(35, 204);
            this.clientRfcLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientRfcLabel.Name = "clientRfcLabel";
            this.clientRfcLabel.Size = new System.Drawing.Size(34, 16);
            this.clientRfcLabel.TabIndex = 42;
            this.clientRfcLabel.Text = "RFC";
            // 
            // clientRfcTextBox
            // 
            this.clientRfcTextBox.Location = new System.Drawing.Point(153, 201);
            this.clientRfcTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.clientRfcTextBox.Name = "clientRfcTextBox";
            this.clientRfcTextBox.ReadOnly = true;
            this.clientRfcTextBox.Size = new System.Drawing.Size(372, 22);
            this.clientRfcTextBox.TabIndex = 43;
            // 
            // clientEmailLabel
            // 
            this.clientEmailLabel.AutoSize = true;
            this.clientEmailLabel.Location = new System.Drawing.Point(35, 241);
            this.clientEmailLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clientEmailLabel.Name = "clientEmailLabel";
            this.clientEmailLabel.Size = new System.Drawing.Size(48, 16);
            this.clientEmailLabel.TabIndex = 44;
            this.clientEmailLabel.Text = "Correo";
            // 
            // clientEmailTextBox
            // 
            this.clientEmailTextBox.Location = new System.Drawing.Point(153, 238);
            this.clientEmailTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.clientEmailTextBox.Name = "clientEmailTextBox";
            this.clientEmailTextBox.ReadOnly = true;
            this.clientEmailTextBox.Size = new System.Drawing.Size(372, 22);
            this.clientEmailTextBox.TabIndex = 45;
            // 
            // notesLabel
            // 
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(1014, 62);
            this.notesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(43, 16);
            this.notesLabel.TabIndex = 46;
            this.notesLabel.Text = "Notas";
            // 
            // notesTextBox
            // 
            this.notesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notesTextBox.Location = new System.Drawing.Point(1017, 82);
            this.notesTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.notesTextBox.Multiline = true;
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.notesTextBox.Size = new System.Drawing.Size(400, 40);
            this.notesTextBox.TabIndex = 47;
            // 
            // kitComponentsLabel
            // 
            this.kitComponentsLabel.AutoSize = true;
            this.kitComponentsLabel.Location = new System.Drawing.Point(13, 311);
            this.kitComponentsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.kitComponentsLabel.Name = "kitComponentsLabel";
            this.kitComponentsLabel.Size = new System.Drawing.Size(132, 16);
            this.kitComponentsLabel.TabIndex = 33;
            this.kitComponentsLabel.Text = "Componentes del kit:";
            this.kitComponentsLabel.Visible = false;
            // 
            // kitComponentsRichTextBox
            // 
            this.kitComponentsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kitComponentsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.kitComponentsRichTextBox.Location = new System.Drawing.Point(153, 306);
            this.kitComponentsRichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.kitComponentsRichTextBox.Name = "kitComponentsRichTextBox";
            this.kitComponentsRichTextBox.ReadOnly = true;
            this.kitComponentsRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.kitComponentsRichTextBox.Size = new System.Drawing.Size(372, 96);
            this.kitComponentsRichTextBox.TabIndex = 34;
            this.kitComponentsRichTextBox.TabStop = false;
            this.kitComponentsRichTextBox.Text = "";
            this.kitComponentsRichTextBox.Visible = false;
            // 
            // OrderManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1451, 838);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.notesLabel);
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
            this.Controls.Add(this.kitComponentsRichTextBox);
            this.Controls.Add(this.kitComponentsLabel);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1463, 733);
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
        private System.Windows.Forms.Label kitComponentsLabel;
        private System.Windows.Forms.RichTextBox kitComponentsRichTextBox;
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
        private System.Windows.Forms.Label notesLabel;
        private System.Windows.Forms.TextBox notesTextBox;
    }
}
