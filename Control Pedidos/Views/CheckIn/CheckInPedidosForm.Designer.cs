using System.Windows.Forms;

namespace Control_Pedidos.Views.CheckIn
{
    partial class CheckInPedidosForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador.
        /// No se puede modificar el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.filtersPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblEvento = new System.Windows.Forms.Label();
            this.cmbEventos = new System.Windows.Forms.ComboBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnPedidosEscaneados = new System.Windows.Forms.Button();
            this.lblEscanear = new System.Windows.Forms.Label();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.cmsPedidos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.registrarCheckInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTableLayoutPanel.SuspendLayout();
            this.filtersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.cmsPedidos.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.filtersPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.dgvPedidos, 0, 1);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(1378, 609);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // filtersPanel
            // 
            this.filtersPanel.AutoSize = true;
            this.filtersPanel.ColumnCount = 6;
            this.filtersPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filtersPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 372F));
            this.filtersPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filtersPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 212F));
            this.filtersPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filtersPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filtersPanel.Controls.Add(this.lblEvento, 0, 0);
            this.filtersPanel.Controls.Add(this.cmbEventos, 1, 0);
            this.filtersPanel.Controls.Add(this.lblBuscar, 2, 0);
            this.filtersPanel.Controls.Add(this.txtBuscar, 3, 0);
            this.filtersPanel.Controls.Add(this.btnPedidosEscaneados, 5, 0);
            this.filtersPanel.Controls.Add(this.lblEscanear, 0, 1);
            this.filtersPanel.Controls.Add(this.txtCodigoBarras, 1, 1);
            this.filtersPanel.Controls.Add(this.btnRefrescar, 4, 0);
            this.filtersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtersPanel.Location = new System.Drawing.Point(4, 3);
            this.filtersPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.filtersPanel.Name = "filtersPanel";
            this.filtersPanel.RowCount = 2;
            this.filtersPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.filtersPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.filtersPanel.Size = new System.Drawing.Size(1370, 79);
            this.filtersPanel.TabIndex = 0;
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEvento.Location = new System.Drawing.Point(4, 6);
            this.lblEvento.Margin = new System.Windows.Forms.Padding(4, 6, 8, 0);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(64, 23);
            this.lblEvento.TabIndex = 0;
            this.lblEvento.Text = "Evento";
            // 
            // cmbEventos
            // 
            this.cmbEventos.DisplayMember = "Descripcion";
            this.cmbEventos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEventos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEventos.FormattingEnabled = true;
            this.cmbEventos.Location = new System.Drawing.Point(260, 3);
            this.cmbEventos.Margin = new System.Windows.Forms.Padding(4, 3, 16, 3);
            this.cmbEventos.Name = "cmbEventos";
            this.cmbEventos.Size = new System.Drawing.Size(352, 31);
            this.cmbEventos.TabIndex = 1;
            this.cmbEventos.SelectedIndexChanged += new System.EventHandler(this.cmbEventos_SelectedIndexChanged);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBuscar.Location = new System.Drawing.Point(628, 6);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(0, 6, 8, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(62, 23);
            this.lblBuscar.TabIndex = 2;
            this.lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(701, 3);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(197, 30);
            this.txtBuscar.TabIndex = 3;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnPedidosEscaneados
            // 
            this.btnPedidosEscaneados.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPedidosEscaneados.AutoSize = true;
            this.btnPedidosEscaneados.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPedidosEscaneados.Location = new System.Drawing.Point(1085, 3);
            this.btnPedidosEscaneados.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPedidosEscaneados.Name = "btnPedidosEscaneados";
            this.btnPedidosEscaneados.Size = new System.Drawing.Size(172, 33);
            this.btnPedidosEscaneados.TabIndex = 5;
            this.btnPedidosEscaneados.Text = "Pedidos Escaneados";
            this.btnPedidosEscaneados.UseVisualStyleBackColor = true;
            this.btnPedidosEscaneados.Click += new System.EventHandler(this.btnPedidosEscaneados_Click);
            // 
            // lblEscanear
            // 
            this.lblEscanear.AutoSize = true;
            this.lblEscanear.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEscanear.Location = new System.Drawing.Point(4, 49);
            this.lblEscanear.Margin = new System.Windows.Forms.Padding(4, 10, 8, 3);
            this.lblEscanear.Name = "lblEscanear";
            this.lblEscanear.Size = new System.Drawing.Size(244, 25);
            this.lblEscanear.TabIndex = 6;
            this.lblEscanear.Text = "Escanear código de barras";
            // 
            // txtCodigoBarras
            // 
            this.filtersPanel.SetColumnSpan(this.txtCodigoBarras, 5);
            this.txtCodigoBarras.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtCodigoBarras.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtCodigoBarras.Location = new System.Drawing.Point(260, 42);
            this.txtCodigoBarras.Margin = new System.Windows.Forms.Padding(4, 3, 4, 6);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(320, 31);
            this.txtCodigoBarras.TabIndex = 7;
            this.txtCodigoBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoBarras_KeyDown);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRefrescar.AutoSize = true;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefrescar.Location = new System.Drawing.Point(914, 3);
            this.btnRefrescar.Margin = new System.Windows.Forms.Padding(4, 3, 16, 3);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(151, 33);
            this.btnRefrescar.TabIndex = 4;
            this.btnRefrescar.Text = "Refrescar lista";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.ContextMenuStrip = this.cmsPedidos;
            this.dgvPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPedidos.Location = new System.Drawing.Point(4, 88);
            this.dgvPedidos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvPedidos.MultiSelect = false;
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.ReadOnly = true;
            this.dgvPedidos.RowHeadersVisible = false;
            this.dgvPedidos.RowHeadersWidth = 51;
            this.dgvPedidos.RowTemplate.Height = 29;
            this.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedidos.Size = new System.Drawing.Size(1370, 518);
            this.dgvPedidos.TabIndex = 1;
            this.dgvPedidos.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPedidos_CellMouseDown);
            // 
            // cmsPedidos
            // 
            this.cmsPedidos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsPedidos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarCheckInMenuItem});
            this.cmsPedidos.Name = "cmsPedidos";
            this.cmsPedidos.Size = new System.Drawing.Size(196, 28);
            // 
            // registrarCheckInMenuItem
            // 
            this.registrarCheckInMenuItem.Name = "registrarCheckInMenuItem";
            this.registrarCheckInMenuItem.Size = new System.Drawing.Size(195, 24);
            this.registrarCheckInMenuItem.Text = "Registrar CheckIN";
            this.registrarCheckInMenuItem.Click += new System.EventHandler(this.registrarCheckInMenuItem_Click);
            // 
            // CheckInPedidosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 609);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CheckInPedidosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CheckIN de pedidos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CheckInPedidosForm_Load);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.filtersPanel.ResumeLayout(false);
            this.filtersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.cmsPedidos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel filtersPanel;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.ComboBox cmbEventos;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnPedidosEscaneados;
        private System.Windows.Forms.Label lblEscanear;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.ContextMenuStrip cmsPedidos;
        private System.Windows.Forms.ToolStripMenuItem registrarCheckInMenuItem;
        private System.Windows.Forms.Button btnRefrescar;
    }
}
