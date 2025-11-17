namespace Control_Pedidos.Views.Orders
{
    partial class PedidoPreviewForm
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
            this.components = new System.ComponentModel.Container();
            this.watermarkPanel = new Control_Pedidos.Views.Orders.PedidoPreviewForm.StatusWatermarkPanel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerTable = new System.Windows.Forms.TableLayoutPanel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.generalInfoTable = new System.Windows.Forms.TableLayoutPanel();
            this.folioTitleLabel = new System.Windows.Forms.Label();
            this.folioValueLabel = new System.Windows.Forms.Label();
            this.clientTitleLabel = new System.Windows.Forms.Label();
            this.clientValueLabel = new System.Windows.Forms.Label();
            this.phoneTitleLabel = new System.Windows.Forms.Label();
            this.phoneValueLabel = new System.Windows.Forms.Label();
            this.deliveryTitleLabel = new System.Windows.Forms.Label();
            this.deliveryValueLabel = new System.Windows.Forms.Label();
            this.addressTitleLabel = new System.Windows.Forms.Label();
            this.addressValueLabel = new System.Windows.Forms.Label();
            this.eventTitleLabel = new System.Windows.Forms.Label();
            this.eventValueLabel = new System.Windows.Forms.Label();
            this.userTitleLabel = new System.Windows.Forms.Label();
            this.userValueLabel = new System.Windows.Forms.Label();
            this.fechaCapturaTitleLabel = new System.Windows.Forms.Label();
            this.fechaCapturaValueLabel = new System.Windows.Forms.Label();
            this.notesPanel = new System.Windows.Forms.Panel();
            this.notesValueLabel = new System.Windows.Forms.Label();
            this.notesTitleLabel = new System.Windows.Forms.Label();
            this.totalsTable = new System.Windows.Forms.TableLayoutPanel();
            this.totalTitleLabel = new System.Windows.Forms.Label();
            this.totalValueLabel = new System.Windows.Forms.Label();
            this.abonosTitleLabel = new System.Windows.Forms.Label();
            this.abonosValueLabel = new System.Windows.Forms.Label();
            this.descuentoTitleLabel = new System.Windows.Forms.Label();
            this.descuentoValueLabel = new System.Windows.Forms.Label();
            this.saldoTitleLabel = new System.Windows.Forms.Label();
            this.saldoValueLabel = new System.Windows.Forms.Label();
            this.articlesLabel = new System.Windows.Forms.Label();
            this.articlesGrid = new System.Windows.Forms.DataGridView();
            this.kitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreCortoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentsLabel = new System.Windows.Forms.Label();
            this.componentsGrid = new System.Windows.Forms.DataGridView();
            this.componentNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentQuantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentUnitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertLabel = new System.Windows.Forms.Label();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.closeButton = new System.Windows.Forms.Button();
            this.reprintButton = new System.Windows.Forms.Button();
            this.watermarkPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.mainLayout.SuspendLayout();
            this.headerTable.SuspendLayout();
            this.generalInfoTable.SuspendLayout();
            this.notesPanel.SuspendLayout();
            this.totalsTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.articlesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentsGrid)).BeginInit();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // watermarkPanel
            // 
            this.watermarkPanel.BackColor = System.Drawing.Color.White;
            this.watermarkPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.watermarkPanel.Location = new System.Drawing.Point(0, 0);
            this.watermarkPanel.Name = "watermarkPanel";
            this.watermarkPanel.Size = new System.Drawing.Size(1080, 740);
            this.watermarkPanel.TabIndex = 0;
            this.watermarkPanel.WatermarkFontSize = 72F;
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.contentPanel.Controls.Add(this.mainLayout);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(24);
            this.contentPanel.Size = new System.Drawing.Size(1080, 740);
            this.contentPanel.TabIndex = 0;
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.headerTable, 0, 0);
            this.mainLayout.Controls.Add(this.generalInfoTable, 0, 1);
            this.mainLayout.Controls.Add(this.notesPanel, 0, 2);
            this.mainLayout.Controls.Add(this.totalsTable, 0, 3);
            this.mainLayout.Controls.Add(this.articlesLabel, 0, 4);
            this.mainLayout.Controls.Add(this.articlesGrid, 0, 5);
            this.mainLayout.Controls.Add(this.componentsLabel, 0, 6);
            this.mainLayout.Controls.Add(this.componentsGrid, 0, 7);
            this.mainLayout.Controls.Add(this.alertLabel, 0, 8);
            this.mainLayout.Controls.Add(this.buttonsPanel, 0, 9);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(24, 24);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 10;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainLayout.Size = new System.Drawing.Size(1032, 692);
            this.mainLayout.TabIndex = 0;
            // 
            // headerTable
            // 
            this.headerTable.ColumnCount = 2;
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.headerTable.Controls.Add(this.titleLabel, 0, 0);
            this.headerTable.Controls.Add(this.statusLabel, 1, 0);
            this.headerTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerTable.Location = new System.Drawing.Point(0, 0);
            this.headerTable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.headerTable.Name = "headerTable";
            this.headerTable.RowCount = 1;
            this.headerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.headerTable.Size = new System.Drawing.Size(1032, 54);
            this.headerTable.TabIndex = 0;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.titleLabel.Location = new System.Drawing.Point(3, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(718, 54);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Vista previa del pedido";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.BackColor = System.Drawing.Color.DimGray;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(766, 12);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(3, 12, 0, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this.statusLabel.Size = new System.Drawing.Size(263, 31);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "ESTATUS";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // generalInfoTable
            // 
            this.generalInfoTable.AutoSize = true;
            this.generalInfoTable.ColumnCount = 4;
            this.generalInfoTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.generalInfoTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.generalInfoTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.generalInfoTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.generalInfoTable.Controls.Add(this.folioTitleLabel, 0, 0);
            this.generalInfoTable.Controls.Add(this.folioValueLabel, 1, 0);
            this.generalInfoTable.Controls.Add(this.clientTitleLabel, 2, 0);
            this.generalInfoTable.Controls.Add(this.clientValueLabel, 3, 0);
            this.generalInfoTable.Controls.Add(this.phoneTitleLabel, 0, 1);
            this.generalInfoTable.Controls.Add(this.phoneValueLabel, 1, 1);
            this.generalInfoTable.Controls.Add(this.deliveryTitleLabel, 2, 1);
            this.generalInfoTable.Controls.Add(this.deliveryValueLabel, 3, 1);
            this.generalInfoTable.Controls.Add(this.addressTitleLabel, 0, 2);
            this.generalInfoTable.Controls.Add(this.addressValueLabel, 1, 2);
            this.generalInfoTable.Controls.Add(this.eventTitleLabel, 0, 3);
            this.generalInfoTable.Controls.Add(this.eventValueLabel, 1, 3);
            this.generalInfoTable.Controls.Add(this.userTitleLabel, 2, 3);
            this.generalInfoTable.Controls.Add(this.userValueLabel, 3, 3);
            this.generalInfoTable.Controls.Add(this.fechaCapturaTitleLabel, 0, 4);
            this.generalInfoTable.Controls.Add(this.fechaCapturaValueLabel, 1, 4);
            this.generalInfoTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalInfoTable.Location = new System.Drawing.Point(0, 66);
            this.generalInfoTable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.generalInfoTable.Name = "generalInfoTable";
            this.generalInfoTable.RowCount = 5;
            this.generalInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.generalInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.generalInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.generalInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.generalInfoTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.generalInfoTable.Size = new System.Drawing.Size(1032, 158);
            this.generalInfoTable.TabIndex = 1;
            // 
            // folioTitleLabel
            // 
            this.folioTitleLabel.AutoSize = true;
            this.folioTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.folioTitleLabel.Location = new System.Drawing.Point(3, 0);
            this.folioTitleLabel.Name = "folioTitleLabel";
            this.folioTitleLabel.Size = new System.Drawing.Size(42, 15);
            this.folioTitleLabel.TabIndex = 0;
            this.folioTitleLabel.Text = "Folio";
            // 
            // folioValueLabel
            // 
            this.folioValueLabel.AutoSize = true;
            this.folioValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.folioValueLabel.Location = new System.Drawing.Point(189, 0);
            this.folioValueLabel.Name = "folioValueLabel";
            this.folioValueLabel.Size = new System.Drawing.Size(46, 17);
            this.folioValueLabel.TabIndex = 1;
            this.folioValueLabel.Text = "label1";
            // 
            // clientTitleLabel
            // 
            this.clientTitleLabel.AutoSize = true;
            this.clientTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.clientTitleLabel.Location = new System.Drawing.Point(517, 0);
            this.clientTitleLabel.Name = "clientTitleLabel";
            this.clientTitleLabel.Size = new System.Drawing.Size(46, 15);
            this.clientTitleLabel.TabIndex = 2;
            this.clientTitleLabel.Text = "Cliente";
            // 
            // clientValueLabel
            // 
            this.clientValueLabel.AutoSize = true;
            this.clientValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clientValueLabel.Location = new System.Drawing.Point(703, 0);
            this.clientValueLabel.Name = "clientValueLabel";
            this.clientValueLabel.Size = new System.Drawing.Size(46, 17);
            this.clientValueLabel.TabIndex = 3;
            this.clientValueLabel.Text = "label2";
            // 
            // phoneTitleLabel
            // 
            this.phoneTitleLabel.AutoSize = true;
            this.phoneTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.phoneTitleLabel.Location = new System.Drawing.Point(3, 34);
            this.phoneTitleLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.phoneTitleLabel.Name = "phoneTitleLabel";
            this.phoneTitleLabel.Size = new System.Drawing.Size(56, 15);
            this.phoneTitleLabel.TabIndex = 4;
            this.phoneTitleLabel.Text = "Teléfono";
            // 
            // phoneValueLabel
            // 
            this.phoneValueLabel.AutoSize = true;
            this.phoneValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.phoneValueLabel.Location = new System.Drawing.Point(189, 34);
            this.phoneValueLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.phoneValueLabel.Name = "phoneValueLabel";
            this.phoneValueLabel.Size = new System.Drawing.Size(46, 17);
            this.phoneValueLabel.TabIndex = 5;
            this.phoneValueLabel.Text = "label3";
            // 
            // deliveryTitleLabel
            // 
            this.deliveryTitleLabel.AutoSize = true;
            this.deliveryTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.deliveryTitleLabel.Location = new System.Drawing.Point(517, 34);
            this.deliveryTitleLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.deliveryTitleLabel.Name = "deliveryTitleLabel";
            this.deliveryTitleLabel.Size = new System.Drawing.Size(94, 15);
            this.deliveryTitleLabel.TabIndex = 6;
            this.deliveryTitleLabel.Text = "Fecha de entrega";
            // 
            // deliveryValueLabel
            // 
            this.deliveryValueLabel.AutoSize = true;
            this.deliveryValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.deliveryValueLabel.Location = new System.Drawing.Point(703, 34);
            this.deliveryValueLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.deliveryValueLabel.Name = "deliveryValueLabel";
            this.deliveryValueLabel.Size = new System.Drawing.Size(46, 17);
            this.deliveryValueLabel.TabIndex = 7;
            this.deliveryValueLabel.Text = "label4";
            // 
            // addressTitleLabel
            // 
            this.addressTitleLabel.AutoSize = true;
            this.addressTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.addressTitleLabel.Location = new System.Drawing.Point(3, 68);
            this.addressTitleLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.addressTitleLabel.Name = "addressTitleLabel";
            this.addressTitleLabel.Size = new System.Drawing.Size(58, 15);
            this.addressTitleLabel.TabIndex = 8;
            this.addressTitleLabel.Text = "Domicilio";
            // 
            // addressValueLabel
            // 
            this.addressValueLabel.AutoSize = true;
            this.generalInfoTable.SetColumnSpan(this.addressValueLabel, 3);
            this.addressValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addressValueLabel.Location = new System.Drawing.Point(189, 68);
            this.addressValueLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.addressValueLabel.MaximumSize = new System.Drawing.Size(700, 0);
            this.addressValueLabel.Name = "addressValueLabel";
            this.addressValueLabel.Size = new System.Drawing.Size(46, 17);
            this.addressValueLabel.TabIndex = 9;
            this.addressValueLabel.Text = "label5";
            // 
            // eventTitleLabel
            // 
            this.eventTitleLabel.AutoSize = true;
            this.eventTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.eventTitleLabel.Location = new System.Drawing.Point(3, 102);
            this.eventTitleLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.eventTitleLabel.Name = "eventTitleLabel";
            this.eventTitleLabel.Size = new System.Drawing.Size(44, 15);
            this.eventTitleLabel.TabIndex = 10;
            this.eventTitleLabel.Text = "Evento";
            // 
            // eventValueLabel
            // 
            this.eventValueLabel.AutoSize = true;
            this.eventValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.eventValueLabel.Location = new System.Drawing.Point(189, 102);
            this.eventValueLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.eventValueLabel.Name = "eventValueLabel";
            this.eventValueLabel.Size = new System.Drawing.Size(46, 17);
            this.eventValueLabel.TabIndex = 11;
            this.eventValueLabel.Text = "label6";
            // 
            // userTitleLabel
            // 
            this.userTitleLabel.AutoSize = true;
            this.userTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.userTitleLabel.Location = new System.Drawing.Point(517, 102);
            this.userTitleLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.userTitleLabel.Name = "userTitleLabel";
            this.userTitleLabel.Size = new System.Drawing.Size(108, 15);
            this.userTitleLabel.TabIndex = 12;
            this.userTitleLabel.Text = "Usuario que captura";
            // 
            // userValueLabel
            // 
            this.userValueLabel.AutoSize = true;
            this.userValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.userValueLabel.Location = new System.Drawing.Point(703, 102);
            this.userValueLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.userValueLabel.Name = "userValueLabel";
            this.userValueLabel.Size = new System.Drawing.Size(46, 17);
            this.userValueLabel.TabIndex = 13;
            this.userValueLabel.Text = "label7";
            // 
            // fechaCapturaTitleLabel
            // 
            this.fechaCapturaTitleLabel.AutoSize = true;
            this.fechaCapturaTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.fechaCapturaTitleLabel.Location = new System.Drawing.Point(3, 136);
            this.fechaCapturaTitleLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.fechaCapturaTitleLabel.Name = "fechaCapturaTitleLabel";
            this.fechaCapturaTitleLabel.Size = new System.Drawing.Size(112, 15);
            this.fechaCapturaTitleLabel.TabIndex = 14;
            this.fechaCapturaTitleLabel.Text = "Fecha de captura";
            // 
            // fechaCapturaValueLabel
            // 
            this.fechaCapturaValueLabel.AutoSize = true;
            this.generalInfoTable.SetColumnSpan(this.fechaCapturaValueLabel, 3);
            this.fechaCapturaValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fechaCapturaValueLabel.Location = new System.Drawing.Point(189, 136);
            this.fechaCapturaValueLabel.Margin = new System.Windows.Forms.Padding(3, 17, 3, 0);
            this.fechaCapturaValueLabel.Name = "fechaCapturaValueLabel";
            this.fechaCapturaValueLabel.Size = new System.Drawing.Size(46, 17);
            this.fechaCapturaValueLabel.TabIndex = 15;
            this.fechaCapturaValueLabel.Text = "label8";
            // 
            // notesPanel
            // 
            this.notesPanel.AutoSize = true;
            this.notesPanel.Controls.Add(this.notesValueLabel);
            this.notesPanel.Controls.Add(this.notesTitleLabel);
            this.notesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notesPanel.Location = new System.Drawing.Point(0, 224);
            this.notesPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.notesPanel.Name = "notesPanel";
            this.notesPanel.Size = new System.Drawing.Size(1032, 59);
            this.notesPanel.TabIndex = 2;
            // 
            // notesValueLabel
            // 
            this.notesValueLabel.AutoSize = true;
            this.notesValueLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.notesValueLabel.Location = new System.Drawing.Point(0, 28);
            this.notesValueLabel.MaximumSize = new System.Drawing.Size(1000, 0);
            this.notesValueLabel.Name = "notesValueLabel";
            this.notesValueLabel.Size = new System.Drawing.Size(46, 17);
            this.notesValueLabel.TabIndex = 1;
            this.notesValueLabel.Text = "label9";
            // 
            // notesTitleLabel
            // 
            this.notesTitleLabel.AutoSize = true;
            this.notesTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.notesTitleLabel.Location = new System.Drawing.Point(0, 0);
            this.notesTitleLabel.Name = "notesTitleLabel";
            this.notesTitleLabel.Size = new System.Drawing.Size(43, 15);
            this.notesTitleLabel.TabIndex = 0;
            this.notesTitleLabel.Text = "Notas";
            // 
            // totalsTable
            // 
            this.totalsTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.totalsTable.ColumnCount = 4;
            this.totalsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.totalsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.totalsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.totalsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.totalsTable.Controls.Add(this.totalTitleLabel, 0, 0);
            this.totalsTable.Controls.Add(this.totalValueLabel, 0, 1);
            this.totalsTable.Controls.Add(this.abonosTitleLabel, 1, 0);
            this.totalsTable.Controls.Add(this.abonosValueLabel, 1, 1);
            this.totalsTable.Controls.Add(this.descuentoTitleLabel, 2, 0);
            this.totalsTable.Controls.Add(this.descuentoValueLabel, 2, 1);
            this.totalsTable.Controls.Add(this.saldoTitleLabel, 3, 0);
            this.totalsTable.Controls.Add(this.saldoValueLabel, 3, 1);
            this.totalsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalsTable.Location = new System.Drawing.Point(0, 283);
            this.totalsTable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.totalsTable.Name = "totalsTable";
            this.totalsTable.RowCount = 2;
            this.totalsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.totalsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.totalsTable.Size = new System.Drawing.Size(1032, 82);
            this.totalsTable.TabIndex = 3;
            // 
            // totalTitleLabel
            // 
            this.totalTitleLabel.AutoSize = true;
            this.totalTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.totalTitleLabel.Location = new System.Drawing.Point(4, 1);
            this.totalTitleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.totalTitleLabel.Name = "totalTitleLabel";
            this.totalTitleLabel.Size = new System.Drawing.Size(252, 15);
            this.totalTitleLabel.TabIndex = 0;
            this.totalTitleLabel.Text = "Total";
            this.totalTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalValueLabel
            // 
            this.totalValueLabel.AutoSize = true;
            this.totalValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.totalValueLabel.Location = new System.Drawing.Point(4, 17);
            this.totalValueLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 6);
            this.totalValueLabel.Name = "totalValueLabel";
            this.totalValueLabel.Size = new System.Drawing.Size(252, 58);
            this.totalValueLabel.TabIndex = 1;
            this.totalValueLabel.Text = "$0.00";
            this.totalValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // abonosTitleLabel
            // 
            this.abonosTitleLabel.AutoSize = true;
            this.abonosTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abonosTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.abonosTitleLabel.Location = new System.Drawing.Point(262, 1);
            this.abonosTitleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.abonosTitleLabel.Name = "abonosTitleLabel";
            this.abonosTitleLabel.Size = new System.Drawing.Size(252, 15);
            this.abonosTitleLabel.TabIndex = 2;
            this.abonosTitleLabel.Text = "Abonos";
            this.abonosTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // abonosValueLabel
            // 
            this.abonosValueLabel.AutoSize = true;
            this.abonosValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abonosValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.abonosValueLabel.Location = new System.Drawing.Point(262, 17);
            this.abonosValueLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 6);
            this.abonosValueLabel.Name = "abonosValueLabel";
            this.abonosValueLabel.Size = new System.Drawing.Size(252, 58);
            this.abonosValueLabel.TabIndex = 3;
            this.abonosValueLabel.Text = "$0.00";
            this.abonosValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // descuentoTitleLabel
            // 
            this.descuentoTitleLabel.AutoSize = true;
            this.descuentoTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descuentoTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.descuentoTitleLabel.Location = new System.Drawing.Point(520, 1);
            this.descuentoTitleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.descuentoTitleLabel.Name = "descuentoTitleLabel";
            this.descuentoTitleLabel.Size = new System.Drawing.Size(252, 15);
            this.descuentoTitleLabel.TabIndex = 4;
            this.descuentoTitleLabel.Text = "Descuento";
            this.descuentoTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // descuentoValueLabel
            // 
            this.descuentoValueLabel.AutoSize = true;
            this.descuentoValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descuentoValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.descuentoValueLabel.Location = new System.Drawing.Point(520, 17);
            this.descuentoValueLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 6);
            this.descuentoValueLabel.Name = "descuentoValueLabel";
            this.descuentoValueLabel.Size = new System.Drawing.Size(252, 58);
            this.descuentoValueLabel.TabIndex = 5;
            this.descuentoValueLabel.Text = "$0.00";
            this.descuentoValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saldoTitleLabel
            // 
            this.saldoTitleLabel.AutoSize = true;
            this.saldoTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saldoTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saldoTitleLabel.Location = new System.Drawing.Point(778, 1);
            this.saldoTitleLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.saldoTitleLabel.Name = "saldoTitleLabel";
            this.saldoTitleLabel.Size = new System.Drawing.Size(251, 15);
            this.saldoTitleLabel.TabIndex = 6;
            this.saldoTitleLabel.Text = "Saldo pendiente";
            this.saldoTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saldoValueLabel
            // 
            this.saldoValueLabel.AutoSize = true;
            this.saldoValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saldoValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saldoValueLabel.Location = new System.Drawing.Point(778, 17);
            this.saldoValueLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 6);
            this.saldoValueLabel.Name = "saldoValueLabel";
            this.saldoValueLabel.Size = new System.Drawing.Size(251, 58);
            this.saldoValueLabel.TabIndex = 7;
            this.saldoValueLabel.Text = "$0.00";
            this.saldoValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // articlesLabel
            // 
            this.articlesLabel.AutoSize = true;
            this.articlesLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.articlesLabel.Location = new System.Drawing.Point(3, 381);
            this.articlesLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.articlesLabel.Name = "articlesLabel";
            this.articlesLabel.Size = new System.Drawing.Size(160, 19);
            this.articlesLabel.TabIndex = 4;
            this.articlesLabel.Text = "Artículos y/o kits";
            // 
            // articlesGrid
            // 
            this.articlesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.articlesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.kitColumn,
            this.nombreColumn,
            this.nombreCortoColumn,
            this.cantidadColumn,
            this.precioColumn,
            this.subtotalColumn});
            this.articlesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.articlesGrid.Location = new System.Drawing.Point(3, 410);
            this.articlesGrid.MultiSelect = false;
            this.articlesGrid.Name = "articlesGrid";
            this.articlesGrid.RowTemplate.Height = 28;
            this.articlesGrid.Size = new System.Drawing.Size(1026, 177);
            this.articlesGrid.TabIndex = 5;
            this.articlesGrid.SelectionChanged += new System.EventHandler(this.articlesGrid_SelectionChanged);
            // 
            // kitColumn
            // 
            this.kitColumn.DataPropertyName = "TipoIcono";
            this.kitColumn.HeaderText = "";
            this.kitColumn.Name = "kitColumn";
            this.kitColumn.Width = 40;
            // 
            // nombreColumn
            // 
            this.nombreColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombreColumn.DataPropertyName = "Nombre";
            this.nombreColumn.HeaderText = "Descripción";
            this.nombreColumn.Name = "nombreColumn";
            // 
            // nombreCortoColumn
            // 
            this.nombreCortoColumn.DataPropertyName = "NombreCorto";
            this.nombreCortoColumn.HeaderText = "Nombre corto";
            this.nombreCortoColumn.Name = "nombreCortoColumn";
            this.nombreCortoColumn.Width = 160;
            // 
            // cantidadColumn
            // 
            this.cantidadColumn.DataPropertyName = "Cantidad";
            this.cantidadColumn.HeaderText = "Cantidad";
            this.cantidadColumn.Name = "cantidadColumn";
            this.cantidadColumn.Width = 120;
            // 
            // precioColumn
            // 
            this.precioColumn.DataPropertyName = "Precio";
            this.precioColumn.HeaderText = "Precio";
            this.precioColumn.Name = "precioColumn";
            this.precioColumn.Width = 120;
            // 
            // subtotalColumn
            // 
            this.subtotalColumn.DataPropertyName = "Subtotal";
            this.subtotalColumn.HeaderText = "Subtotal";
            this.subtotalColumn.Name = "subtotalColumn";
            this.subtotalColumn.Width = 140;
            // 
            // componentsLabel
            // 
            this.componentsLabel.AutoSize = true;
            this.componentsLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.componentsLabel.Location = new System.Drawing.Point(3, 590);
            this.componentsLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.componentsLabel.Name = "componentsLabel";
            this.componentsLabel.Size = new System.Drawing.Size(263, 19);
            this.componentsLabel.TabIndex = 6;
            this.componentsLabel.Text = "Componentes del kit seleccionado";
            // 
            // componentsGrid
            // 
            this.componentsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.componentsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.componentNameColumn,
            this.componentQuantityColumn,
            this.componentUnitColumn});
            this.componentsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.componentsGrid.Location = new System.Drawing.Point(3, 619);
            this.componentsGrid.Name = "componentsGrid";
            this.componentsGrid.RowTemplate.Height = 28;
            this.componentsGrid.Size = new System.Drawing.Size(1026, 70);
            this.componentsGrid.TabIndex = 7;
            // 
            // componentNameColumn
            // 
            this.componentNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.componentNameColumn.DataPropertyName = "Nombre";
            this.componentNameColumn.HeaderText = "Componente";
            this.componentNameColumn.Name = "componentNameColumn";
            // 
            // componentQuantityColumn
            // 
            this.componentQuantityColumn.DataPropertyName = "Cantidad";
            this.componentQuantityColumn.HeaderText = "Cantidad";
            this.componentQuantityColumn.Name = "componentQuantityColumn";
            this.componentQuantityColumn.Width = 140;
            // 
            // componentUnitColumn
            // 
            this.componentUnitColumn.DataPropertyName = "UnidadMedida";
            this.componentUnitColumn.HeaderText = "Unidad";
            this.componentUnitColumn.Name = "componentUnitColumn";
            this.componentUnitColumn.Width = 140;
            // 
            // alertLabel
            // 
            this.alertLabel.AutoSize = true;
            this.alertLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.alertLabel.ForeColor = System.Drawing.Color.Firebrick;
            this.alertLabel.Location = new System.Drawing.Point(3, 692);
            this.alertLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.alertLabel.Name = "alertLabel";
            this.alertLabel.Size = new System.Drawing.Size(1026, 15);
            this.alertLabel.TabIndex = 8;
            this.alertLabel.Text = "El pedido está cancelado";
            this.alertLabel.Visible = false;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.AutoSize = true;
            this.buttonsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonsPanel.Controls.Add(this.closeButton);
            this.buttonsPanel.Controls.Add(this.reprintButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 707);
            this.buttonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(1032, 33);
            this.buttonsPanel.TabIndex = 9;
            this.buttonsPanel.WrapContents = false;
            // 
            // closeButton
            // 
            this.closeButton.AutoSize = true;
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(234)))), ((int)(((byte)(239)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.closeButton.Location = new System.Drawing.Point(918, 3);
            this.closeButton.Margin = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(114, 27);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Cerrar";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // reprintButton
            // 
            this.reprintButton.AutoSize = true;
            this.reprintButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(123)))), ((int)(((byte)(213)))));
            this.reprintButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(90)))), ((int)(((byte)(157)))));
            this.reprintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reprintButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.reprintButton.ForeColor = System.Drawing.Color.White;
            this.reprintButton.Location = new System.Drawing.Point(784, 3);
            this.reprintButton.Margin = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.reprintButton.Name = "reprintButton";
            this.reprintButton.Size = new System.Drawing.Size(126, 27);
            this.reprintButton.TabIndex = 0;
            this.reprintButton.Text = "Reimprimir";
            this.reprintButton.UseVisualStyleBackColor = false;
            this.reprintButton.Click += new System.EventHandler(this.reprintButton_Click);
            // 
            // watermarkPanel Controls
            // 
            this.watermarkPanel.Controls.Add(this.contentPanel);
            // 
            // PedidoPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(1080, 740);
            this.Controls.Add(this.watermarkPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "PedidoPreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vista previa del pedido";
            this.Load += new System.EventHandler(this.PedidoPreviewForm_Load);
            this.watermarkPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            this.headerTable.ResumeLayout(false);
            this.headerTable.PerformLayout();
            this.generalInfoTable.ResumeLayout(false);
            this.generalInfoTable.PerformLayout();
            this.notesPanel.ResumeLayout(false);
            this.notesPanel.PerformLayout();
            this.totalsTable.ResumeLayout(false);
            this.totalsTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.articlesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentsGrid)).EndInit();
            this.buttonsPanel.ResumeLayout(false);
            this.buttonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PedidoPreviewForm.StatusWatermarkPanel watermarkPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TableLayoutPanel headerTable;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.TableLayoutPanel generalInfoTable;
        private System.Windows.Forms.Label folioTitleLabel;
        private System.Windows.Forms.Label folioValueLabel;
        private System.Windows.Forms.Label clientTitleLabel;
        private System.Windows.Forms.Label clientValueLabel;
        private System.Windows.Forms.Label phoneTitleLabel;
        private System.Windows.Forms.Label phoneValueLabel;
        private System.Windows.Forms.Label deliveryTitleLabel;
        private System.Windows.Forms.Label deliveryValueLabel;
        private System.Windows.Forms.Label addressTitleLabel;
        private System.Windows.Forms.Label addressValueLabel;
        private System.Windows.Forms.Label eventTitleLabel;
        private System.Windows.Forms.Label eventValueLabel;
        private System.Windows.Forms.Label userTitleLabel;
        private System.Windows.Forms.Label userValueLabel;
        private System.Windows.Forms.Label fechaCapturaTitleLabel;
        private System.Windows.Forms.Label fechaCapturaValueLabel;
        private System.Windows.Forms.Panel notesPanel;
        private System.Windows.Forms.Label notesValueLabel;
        private System.Windows.Forms.Label notesTitleLabel;
        private System.Windows.Forms.TableLayoutPanel totalsTable;
        private System.Windows.Forms.Label totalTitleLabel;
        private System.Windows.Forms.Label totalValueLabel;
        private System.Windows.Forms.Label abonosTitleLabel;
        private System.Windows.Forms.Label abonosValueLabel;
        private System.Windows.Forms.Label descuentoTitleLabel;
        private System.Windows.Forms.Label descuentoValueLabel;
        private System.Windows.Forms.Label saldoTitleLabel;
        private System.Windows.Forms.Label saldoValueLabel;
        private System.Windows.Forms.Label articlesLabel;
        private System.Windows.Forms.DataGridView articlesGrid;
        private System.Windows.Forms.Label componentsLabel;
        private System.Windows.Forms.DataGridView componentsGrid;
        private System.Windows.Forms.Label alertLabel;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button reprintButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn kitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreCortoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentQuantityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentUnitColumn;
    }
}
