using System.Windows.Forms;

namespace Control_Pedidos.Views.CheckOut
{
    partial class CheckOutPedidosForm
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

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.filtersTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblEvento = new System.Windows.Forms.Label();
            this.cmbEventos = new System.Windows.Forms.ComboBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnPedidosEntregados = new System.Windows.Forms.Button();
            this.lblEscaneo = new System.Windows.Forms.Label();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.cmsPedidos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.liberarPedidoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTableLayoutPanel.SuspendLayout();
            this.filtersTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.cmsPedidos.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.filtersTableLayoutPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.dgvPedidos, 0, 1);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(1284, 761);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // filtersTableLayoutPanel
            // 
            this.filtersTableLayoutPanel.AutoSize = true;
            this.filtersTableLayoutPanel.ColumnCount = 5;
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
            this.filtersTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filtersTableLayoutPanel.Controls.Add(this.lblEvento, 0, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.cmbEventos, 1, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.lblBuscar, 2, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.txtBuscar, 3, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.btnPedidosEntregados, 4, 0);
            this.filtersTableLayoutPanel.Controls.Add(this.lblEscaneo, 0, 1);
            this.filtersTableLayoutPanel.Controls.Add(this.txtCodigoBarras, 1, 1);
            this.filtersTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtersTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.filtersTableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.filtersTableLayoutPanel.Name = "filtersTableLayoutPanel";
            this.filtersTableLayoutPanel.RowCount = 2;
            this.filtersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.filtersTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.filtersTableLayoutPanel.Size = new System.Drawing.Size(1278, 122);
            this.filtersTableLayoutPanel.TabIndex = 0;
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEvento.Location = new System.Drawing.Point(3, 8);
            this.lblEvento.Margin = new System.Windows.Forms.Padding(3, 8, 8, 0);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(74, 25);
            this.lblEvento.TabIndex = 0;
            this.lblEvento.Text = "Evento";
            // 
            // cmbEventos
            // 
            this.cmbEventos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEventos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEventos.FormattingEnabled = true;
            this.cmbEventos.Location = new System.Drawing.Point(88, 4);
            this.cmbEventos.Margin = new System.Windows.Forms.Padding(3, 4, 16, 4);
            this.cmbEventos.Name = "cmbEventos";
            this.cmbEventos.Size = new System.Drawing.Size(260, 31);
            this.cmbEventos.TabIndex = 1;
            this.cmbEventos.SelectedIndexChanged += new System.EventHandler(this.cmbEventos_SelectedIndexChanged);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBuscar.Location = new System.Drawing.Point(364, 8);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(0, 8, 8, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(66, 25);
            this.lblBuscar.TabIndex = 2;
            this.lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(438, 4);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 4, 16, 4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PlaceholderText = "Cliente o folio";
            this.txtBuscar.Size = new System.Drawing.Size(244, 30);
            this.txtBuscar.TabIndex = 3;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnPedidosEntregados
            // 
            this.btnPedidosEntregados.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPedidosEntregados.AutoSize = true;
            this.btnPedidosEntregados.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPedidosEntregados.Location = new System.Drawing.Point(701, 6);
            this.btnPedidosEntregados.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPedidosEntregados.Name = "btnPedidosEntregados";
            this.btnPedidosEntregados.Size = new System.Drawing.Size(190, 33);
            this.btnPedidosEntregados.TabIndex = 4;
            this.btnPedidosEntregados.Text = "Pedidos Entregados";
            this.btnPedidosEntregados.UseVisualStyleBackColor = true;
            this.btnPedidosEntregados.Click += new System.EventHandler(this.btnPedidosEntregados_Click);
            // 
            // lblEscaneo
            // 
            this.lblEscaneo.AutoSize = true;
            this.lblEscaneo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEscaneo.Location = new System.Drawing.Point(3, 69);
            this.lblEscaneo.Margin = new System.Windows.Forms.Padding(3, 12, 8, 4);
            this.lblEscaneo.Name = "lblEscaneo";
            this.lblEscaneo.Size = new System.Drawing.Size(241, 25);
            this.lblEscaneo.TabIndex = 5;
            this.lblEscaneo.Text = "Escanear código de barras";
            // 
            // txtCodigoBarras
            // 
            this.filtersTableLayoutPanel.SetColumnSpan(this.txtCodigoBarras, 4);
            this.txtCodigoBarras.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtCodigoBarras.Location = new System.Drawing.Point(88, 100);
            this.txtCodigoBarras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 8);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(320, 31);
            this.txtCodigoBarras.TabIndex = 6;
            this.txtCodigoBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoBarras_KeyDown);
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPedidos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.ContextMenuStrip = this.cmsPedidos;
            this.dgvPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPedidos.Location = new System.Drawing.Point(3, 140);
            this.dgvPedidos.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.dgvPedidos.MultiSelect = false;
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.ReadOnly = true;
            this.dgvPedidos.RowHeadersVisible = false;
            this.dgvPedidos.RowTemplate.Height = 60;
            this.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedidos.Size = new System.Drawing.Size(1278, 609);
            this.dgvPedidos.TabIndex = 1;
            this.dgvPedidos.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPedidos_CellMouseDown);
            // 
            // cmsPedidos
            // 
            this.cmsPedidos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsPedidos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liberarPedidoMenuItem});
            this.cmsPedidos.Name = "cmsPedidos";
            this.cmsPedidos.Size = new System.Drawing.Size(189, 28);
            // 
            // liberarPedidoMenuItem
            // 
            this.liberarPedidoMenuItem.Name = "liberarPedidoMenuItem";
            this.liberarPedidoMenuItem.Size = new System.Drawing.Size(188, 24);
            this.liberarPedidoMenuItem.Text = "Liberar pedido";
            this.liberarPedidoMenuItem.Click += new System.EventHandler(this.liberarPedidoMenuItem_Click);
            // 
            // CheckOutPedidosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 761);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "CheckOutPedidosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CheckOUT de pedidos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CheckOutPedidosForm_FormClosed);
            this.Load += new System.EventHandler(this.CheckOutPedidosForm_Load);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.filtersTableLayoutPanel.ResumeLayout(false);
            this.filtersTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.cmsPedidos.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel filtersTableLayoutPanel;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.ComboBox cmbEventos;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnPedidosEntregados;
        private System.Windows.Forms.Label lblEscaneo;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.ContextMenuStrip cmsPedidos;
        private System.Windows.Forms.ToolStripMenuItem liberarPedidoMenuItem;
    }
}
