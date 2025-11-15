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
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estatusCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualizarEstatusColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.pedidosPendientesLabel = new System.Windows.Forms.Label();
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
            this.filtersPanel.Location = new System.Drawing.Point(29, 35);
            this.filtersPanel.Margin = new System.Windows.Forms.Padding(4);
            this.filtersPanel.Name = "filtersPanel";
            this.filtersPanel.Size = new System.Drawing.Size(803, 32);
            this.filtersPanel.TabIndex = 3;
            this.filtersPanel.Visible = false;
            this.filtersPanel.WrapContents = false;
            // 
            // clientFilterLabel
            // 
            this.clientFilterLabel.AutoSize = true;
            this.clientFilterLabel.Location = new System.Drawing.Point(4, 7);
            this.clientFilterLabel.Margin = new System.Windows.Forms.Padding(4, 7, 4, 0);
            this.clientFilterLabel.Name = "clientFilterLabel";
            this.clientFilterLabel.Size = new System.Drawing.Size(105, 16);
            this.clientFilterLabel.TabIndex = 0;
            this.clientFilterLabel.Text = "Filtrar por cliente";
            // 
            // clientFilterComboBox
            // 
            this.clientFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientFilterComboBox.FormattingEnabled = true;
            this.clientFilterComboBox.Location = new System.Drawing.Point(117, 4);
            this.clientFilterComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.clientFilterComboBox.Name = "clientFilterComboBox";
            this.clientFilterComboBox.Size = new System.Drawing.Size(292, 24);
            this.clientFilterComboBox.TabIndex = 1;
            // 
            // statusFilterLabel
            // 
            this.statusFilterLabel.AutoSize = true;
            this.statusFilterLabel.Location = new System.Drawing.Point(417, 7);
            this.statusFilterLabel.Margin = new System.Windows.Forms.Padding(4, 7, 4, 0);
            this.statusFilterLabel.Name = "statusFilterLabel";
            this.statusFilterLabel.Size = new System.Drawing.Size(109, 16);
            this.statusFilterLabel.TabIndex = 2;
            this.statusFilterLabel.Text = "Filtrar por estatus";
            // 
            // statusFilterComboBox
            // 
            this.statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusFilterComboBox.FormattingEnabled = true;
            this.statusFilterComboBox.Location = new System.Drawing.Point(534, 4);
            this.statusFilterComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.statusFilterComboBox.Name = "statusFilterComboBox";
            this.statusFilterComboBox.Size = new System.Drawing.Size(265, 24);
            this.statusFilterComboBox.TabIndex = 3;
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
            this.summaryPanel.Controls.Add(this.pedidosPendientesLabel);
            this.summaryPanel.Location = new System.Drawing.Point(30, 88);
            this.summaryPanel.Margin = new System.Windows.Forms.Padding(4);
            this.summaryPanel.Name = "summaryPanel";
            this.summaryPanel.Size = new System.Drawing.Size(991, 29);
            this.summaryPanel.TabIndex = 4;
            this.summaryPanel.WrapContents = false;
            // 
            // totalOrdersLabel
            // 
            this.totalOrdersLabel.AutoSize = true;
            this.totalOrdersLabel.Location = new System.Drawing.Point(4, 7);
            this.totalOrdersLabel.Margin = new System.Windows.Forms.Padding(4, 7, 24, 0);
            this.totalOrdersLabel.Name = "totalOrdersLabel";
            this.totalOrdersLabel.Size = new System.Drawing.Size(101, 16);
            this.totalOrdersLabel.TabIndex = 0;
            this.totalOrdersLabel.Text = "Pedidos totales";
            // 
            // waitingOrdersLabel
            // 
            this.waitingOrdersLabel.AutoSize = true;
            this.waitingOrdersLabel.Location = new System.Drawing.Point(133, 7);
            this.waitingOrdersLabel.Margin = new System.Windows.Forms.Padding(4, 7, 24, 0);
            this.waitingOrdersLabel.Name = "waitingOrdersLabel";
            this.waitingOrdersLabel.Size = new System.Drawing.Size(172, 16);
            this.waitingOrdersLabel.TabIndex = 1;
            this.waitingOrdersLabel.Text = "Pedidos espera de entrega";
            // 
            // assemblingOrdersLabel
            // 
            this.assemblingOrdersLabel.AutoSize = true;
            this.assemblingOrdersLabel.Location = new System.Drawing.Point(333, 7);
            this.assemblingOrdersLabel.Margin = new System.Windows.Forms.Padding(4, 7, 24, 0);
            this.assemblingOrdersLabel.Name = "assemblingOrdersLabel";
            this.assemblingOrdersLabel.Size = new System.Drawing.Size(122, 16);
            this.assemblingOrdersLabel.TabIndex = 2;
            this.assemblingOrdersLabel.Text = "Pedidos Devueltos";
            // 
            // deliveredOrdersLabel
            // 
            this.deliveredOrdersLabel.AutoSize = true;
            this.deliveredOrdersLabel.Location = new System.Drawing.Point(483, 7);
            this.deliveredOrdersLabel.Margin = new System.Windows.Forms.Padding(4, 7, 24, 0);
            this.deliveredOrdersLabel.Name = "deliveredOrdersLabel";
            this.deliveredOrdersLabel.Size = new System.Drawing.Size(130, 16);
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
            this.todaysOrdersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.todaysOrdersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.todaysOrdersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Folio,
            this.Cliente,
            this.Total,
            this.estatusCol,
            this.actualizarEstatusColumn});
            this.todaysOrdersGrid.Location = new System.Drawing.Point(29, 178);
            this.todaysOrdersGrid.Margin = new System.Windows.Forms.Padding(4);
            this.todaysOrdersGrid.MultiSelect = false;
            this.todaysOrdersGrid.Name = "todaysOrdersGrid";
            this.todaysOrdersGrid.RowHeadersVisible = false;
            this.todaysOrdersGrid.RowHeadersWidth = 51;
            this.todaysOrdersGrid.RowTemplate.Height = 40;
            this.todaysOrdersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.todaysOrdersGrid.Size = new System.Drawing.Size(1253, 397);
            this.todaysOrdersGrid.TabIndex = 5;
            // 
            // Folio
            // 
            this.Folio.DataPropertyName = "Folio";
            this.Folio.HeaderText = "Folio";
            this.Folio.MinimumWidth = 6;
            this.Folio.Name = "Folio";
            // 
            // Cliente
            // 
            this.Cliente.DataPropertyName = "Cliente";
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.MinimumWidth = 6;
            this.Cliente.Name = "Cliente";
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            this.Total.HeaderText = "Total";
            this.Total.MinimumWidth = 6;
            this.Total.Name = "Total";
            // 
            // estatusCol
            // 
            this.estatusCol.DataPropertyName = "Estatus";
            this.estatusCol.HeaderText = "Estatus";
            this.estatusCol.MinimumWidth = 6;
            this.estatusCol.Name = "estatusCol";
            // 
            // actualizarEstatusColumn
            // 
            this.actualizarEstatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.actualizarEstatusColumn.HeaderText = "Actualizar estatus";
            this.actualizarEstatusColumn.MinimumWidth = 6;
            this.actualizarEstatusColumn.Name = "actualizarEstatusColumn";
            this.actualizarEstatusColumn.Text = "Actualizar";
            this.actualizarEstatusColumn.UseColumnTextForButtonValue = true;
            this.actualizarEstatusColumn.Width = 105;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(88, 149);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(260, 22);
            this.searchTextBox.TabIndex = 22;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(27, 153);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(52, 16);
            this.searchLabel.TabIndex = 21;
            this.searchLabel.Text = "Buscar:";
            // 
            // pedidosPendientesLabel
            // 
            this.pedidosPendientesLabel.AutoSize = true;
            this.pedidosPendientesLabel.Location = new System.Drawing.Point(641, 7);
            this.pedidosPendientesLabel.Margin = new System.Windows.Forms.Padding(4, 7, 24, 0);
            this.pedidosPendientesLabel.Name = "pedidosPendientesLabel";
            this.pedidosPendientesLabel.Size = new System.Drawing.Size(128, 16);
            this.pedidosPendientesLabel.TabIndex = 4;
            this.pedidosPendientesLabel.Text = "Pedidos pendientes";
            // 
            // OrderDeliveryDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 604);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.filtersPanel);
            this.Controls.Add(this.summaryPanel);
            this.Controls.Add(this.todaysOrdersGrid);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn pedidoIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn folioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn horaEntregaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalColumn;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn estatusCol;
        private System.Windows.Forms.DataGridViewButtonColumn actualizarEstatusColumn;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Label pedidosPendientesLabel;
    }
}
