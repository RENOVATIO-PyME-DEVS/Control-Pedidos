namespace Control_Pedidos.Views.Orders
{
    partial class OrderDeliveryDashboardForm
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
            this.filtersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.clientFilterLabel = new System.Windows.Forms.Label();
            this.clientFilterComboBox = new System.Windows.Forms.ComboBox();
            this.statusFilterLabel = new System.Windows.Forms.Label();
            this.statusFilterComboBox = new System.Windows.Forms.ComboBox();
            this.summaryPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.totalOrdersLabel = new System.Windows.Forms.Label();
            this.waitingOrdersLabel = new System.Windows.Forms.Label();
            this.assemblingOrdersLabel = new System.Windows.Forms.Label();
            this.deliveredOrdersLabel = new System.Windows.Forms.Label();
            this.todaysOrdersGrid = new System.Windows.Forms.DataGridView();
            this.pedidoIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clienteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horaEntregaColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualizarEstatusColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.filtersPanel.SuspendLayout();
            this.summaryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.todaysOrdersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // filtersPanel
            // 
            this.filtersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filtersPanel.AutoSize = true;
            this.filtersPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.filtersPanel.Controls.Add(this.clientFilterLabel);
            this.filtersPanel.Controls.Add(this.clientFilterComboBox);
            this.filtersPanel.Controls.Add(this.statusFilterLabel);
            this.filtersPanel.Controls.Add(this.statusFilterComboBox);
            this.filtersPanel.Location = new System.Drawing.Point(22, 24);
            this.filtersPanel.Name = "filtersPanel";
            this.filtersPanel.Size = new System.Drawing.Size(0, 0);
            this.filtersPanel.TabIndex = 0;
            this.filtersPanel.WrapContents = false;
            // 
            // clientFilterLabel
            // 
            this.clientFilterLabel.AutoSize = true;
            this.clientFilterLabel.Location = new System.Drawing.Point(3, 6);
            this.clientFilterLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.clientFilterLabel.Name = "clientFilterLabel";
            this.clientFilterLabel.Size = new System.Drawing.Size(90, 13);
            this.clientFilterLabel.TabIndex = 0;
            this.clientFilterLabel.Text = "Filtrar por cliente";
            // 
            // clientFilterComboBox
            // 
            this.clientFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientFilterComboBox.FormattingEnabled = true;
            this.clientFilterComboBox.Location = new System.Drawing.Point(99, 3);
            this.clientFilterComboBox.Name = "clientFilterComboBox";
            this.clientFilterComboBox.Size = new System.Drawing.Size(220, 21);
            this.clientFilterComboBox.TabIndex = 1;
            this.clientFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.clientFilterComboBox_SelectedIndexChanged);
            // 
            // statusFilterLabel
            // 
            this.statusFilterLabel.AutoSize = true;
            this.statusFilterLabel.Location = new System.Drawing.Point(325, 6);
            this.statusFilterLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.statusFilterLabel.Name = "statusFilterLabel";
            this.statusFilterLabel.Size = new System.Drawing.Size(88, 13);
            this.statusFilterLabel.TabIndex = 2;
            this.statusFilterLabel.Text = "Filtrar por estatus";
            // 
            // statusFilterComboBox
            // 
            this.statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusFilterComboBox.FormattingEnabled = true;
            this.statusFilterComboBox.Location = new System.Drawing.Point(419, 3);
            this.statusFilterComboBox.Name = "statusFilterComboBox";
            this.statusFilterComboBox.Size = new System.Drawing.Size(200, 21);
            this.statusFilterComboBox.TabIndex = 3;
            this.statusFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.statusFilterComboBox_SelectedIndexChanged);
            // 
            // summaryPanel
            // 
            this.summaryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.summaryPanel.AutoSize = true;
            this.summaryPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.summaryPanel.Controls.Add(this.totalOrdersLabel);
            this.summaryPanel.Controls.Add(this.waitingOrdersLabel);
            this.summaryPanel.Controls.Add(this.assemblingOrdersLabel);
            this.summaryPanel.Controls.Add(this.deliveredOrdersLabel);
            this.summaryPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.summaryPanel.Location = new System.Drawing.Point(22, 64);
            this.summaryPanel.Name = "summaryPanel";
            this.summaryPanel.Size = new System.Drawing.Size(0, 0);
            this.summaryPanel.TabIndex = 1;
            this.summaryPanel.WrapContents = false;
            // 
            // totalOrdersLabel
            // 
            this.totalOrdersLabel.AutoSize = true;
            this.totalOrdersLabel.Location = new System.Drawing.Point(3, 6);
            this.totalOrdersLabel.Margin = new System.Windows.Forms.Padding(3, 6, 18, 0);
            this.totalOrdersLabel.Name = "totalOrdersLabel";
            this.totalOrdersLabel.Size = new System.Drawing.Size(110, 13);
            this.totalOrdersLabel.TabIndex = 0;
            this.totalOrdersLabel.Text = "Pedidos totales del día";
            // 
            // waitingOrdersLabel
            // 
            this.waitingOrdersLabel.AutoSize = true;
            this.waitingOrdersLabel.Location = new System.Drawing.Point(134, 6);
            this.waitingOrdersLabel.Margin = new System.Windows.Forms.Padding(3, 6, 18, 0);
            this.waitingOrdersLabel.Name = "waitingOrdersLabel";
            this.waitingOrdersLabel.Size = new System.Drawing.Size(97, 13);
            this.waitingOrdersLabel.TabIndex = 1;
            this.waitingOrdersLabel.Text = "Pedidos en espera";
            // 
            // assemblingOrdersLabel
            // 
            this.assemblingOrdersLabel.AutoSize = true;
            this.assemblingOrdersLabel.Location = new System.Drawing.Point(255, 6);
            this.assemblingOrdersLabel.Margin = new System.Windows.Forms.Padding(3, 6, 18, 0);
            this.assemblingOrdersLabel.Name = "assemblingOrdersLabel";
            this.assemblingOrdersLabel.Size = new System.Drawing.Size(110, 13);
            this.assemblingOrdersLabel.TabIndex = 2;
            this.assemblingOrdersLabel.Text = "Pedidos en ensamble";
            // 
            // deliveredOrdersLabel
            // 
            this.deliveredOrdersLabel.AutoSize = true;
            this.deliveredOrdersLabel.Location = new System.Drawing.Point(386, 6);
            this.deliveredOrdersLabel.Margin = new System.Windows.Forms.Padding(3, 6, 18, 0);
            this.deliveredOrdersLabel.Name = "deliveredOrdersLabel";
            this.deliveredOrdersLabel.Size = new System.Drawing.Size(101, 13);
            this.deliveredOrdersLabel.TabIndex = 3;
            this.deliveredOrdersLabel.Text = "Pedidos entregados";
            // 
            // todaysOrdersGrid
            // 
            this.todaysOrdersGrid.AllowUserToAddRows = false;
            this.todaysOrdersGrid.AllowUserToDeleteRows = false;
            this.todaysOrdersGrid.AllowUserToResizeRows = false;
            this.todaysOrdersGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.todaysOrdersGrid.AutoGenerateColumns = false;
            this.todaysOrdersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.todaysOrdersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.todaysOrdersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pedidoIdColumn,
            this.folioColumn,
            this.clienteColumn,
            this.horaEntregaColumn,
            this.estatusColumn,
            this.totalColumn,
            this.actualizarEstatusColumn});
            this.todaysOrdersGrid.Location = new System.Drawing.Point(22, 112);
            this.todaysOrdersGrid.MultiSelect = false;
            this.todaysOrdersGrid.Name = "todaysOrdersGrid";
            this.todaysOrdersGrid.ReadOnly = false;
            this.todaysOrdersGrid.RowHeadersVisible = false;
            this.todaysOrdersGrid.RowTemplate.Height = 40;
            this.todaysOrdersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.todaysOrdersGrid.Size = new System.Drawing.Size(741, 324);
            this.todaysOrdersGrid.TabIndex = 2;
            this.todaysOrdersGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.todaysOrdersGrid_CellContentClick);
            this.todaysOrdersGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.todaysOrdersGrid_DataBindingComplete);
            // 
            // pedidoIdColumn
            // 
            this.pedidoIdColumn.DataPropertyName = "PedidoId";
            this.pedidoIdColumn.HeaderText = "PedidoId";
            this.pedidoIdColumn.Name = "pedidoIdColumn";
            this.pedidoIdColumn.ReadOnly = true;
            this.pedidoIdColumn.Visible = false;
            // 
            // folioColumn
            // 
            this.folioColumn.DataPropertyName = "Folio";
            this.folioColumn.HeaderText = "Folio";
            this.folioColumn.Name = "folioColumn";
            this.folioColumn.ReadOnly = true;
            // 
            // clienteColumn
            // 
            this.clienteColumn.DataPropertyName = "Cliente";
            this.clienteColumn.HeaderText = "Cliente";
            this.clienteColumn.Name = "clienteColumn";
            this.clienteColumn.ReadOnly = true;
            // 
            // horaEntregaColumn
            // 
            this.horaEntregaColumn.DataPropertyName = "HoraEntrega";
            this.horaEntregaColumn.HeaderText = "Hora de entrega";
            this.horaEntregaColumn.Name = "horaEntregaColumn";
            this.horaEntregaColumn.ReadOnly = true;
            this.horaEntregaColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // estatusColumn
            // 
            this.estatusColumn.DataPropertyName = "Estatus";
            this.estatusColumn.HeaderText = "Estatus";
            this.estatusColumn.Name = "estatusColumn";
            this.estatusColumn.ReadOnly = true;
            this.estatusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // totalColumn
            // 
            this.totalColumn.DataPropertyName = "Total";
            this.totalColumn.HeaderText = "Total";
            this.totalColumn.Name = "totalColumn";
            this.totalColumn.ReadOnly = true;
            this.totalColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // actualizarEstatusColumn
            // 
            this.actualizarEstatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.actualizarEstatusColumn.HeaderText = "Actualizar estatus";
            this.actualizarEstatusColumn.Name = "actualizarEstatusColumn";
            this.actualizarEstatusColumn.ReadOnly = false;
            this.actualizarEstatusColumn.Text = "Actualizar";
            this.actualizarEstatusColumn.UseColumnTextForButtonValue = true;
            // 
            // OrderDeliveryDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 460);
            this.Controls.Add(this.todaysOrdersGrid);
            this.Controls.Add(this.summaryPanel);
            this.Controls.Add(this.filtersPanel);
            this.Name = "OrderDeliveryDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pedidos del día";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrderDeliveryDashboardForm_FormClosing);
            this.Load += new System.EventHandler(this.OrderDeliveryDashboardForm_Load);
            this.filtersPanel.ResumeLayout(false);
            this.filtersPanel.PerformLayout();
            this.summaryPanel.ResumeLayout(false);
            this.summaryPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.todaysOrdersGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel filtersPanel;
        private System.Windows.Forms.Label clientFilterLabel;
        private System.Windows.Forms.ComboBox clientFilterComboBox;
        private System.Windows.Forms.Label statusFilterLabel;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.FlowLayoutPanel summaryPanel;
        private System.Windows.Forms.Label totalOrdersLabel;
        private System.Windows.Forms.Label waitingOrdersLabel;
        private System.Windows.Forms.Label assemblingOrdersLabel;
        private System.Windows.Forms.Label deliveredOrdersLabel;
        private System.Windows.Forms.DataGridView todaysOrdersGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn pedidoIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn folioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn horaEntregaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalColumn;
        private System.Windows.Forms.DataGridViewButtonColumn actualizarEstatusColumn;
    }
}
