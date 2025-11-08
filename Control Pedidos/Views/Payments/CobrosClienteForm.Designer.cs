namespace Control_Pedidos.Views.Payments
{
    partial class CobrosClienteForm
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
            this.cobrosGrid = new System.Windows.Forms.DataGridView();
            this.cerrarButton = new System.Windows.Forms.Button();
            this.clienteLabel = new System.Windows.Forms.Label();
            this.cobrosContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reimprimirCobroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.cobrosGrid)).BeginInit();
            this.cobrosContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cobrosGrid
            // 
            this.cobrosGrid.AllowUserToAddRows = false;
            this.cobrosGrid.AllowUserToDeleteRows = false;
            this.cobrosGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cobrosGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cobrosGrid.ContextMenuStrip = this.cobrosContextMenu;
            this.cobrosGrid.Location = new System.Drawing.Point(16, 58);
            this.cobrosGrid.MultiSelect = false;
            this.cobrosGrid.Name = "cobrosGrid";
            this.cobrosGrid.ReadOnly = true;
            this.cobrosGrid.RowHeadersVisible = false;
            this.cobrosGrid.RowTemplate.Height = 24;
            this.cobrosGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cobrosGrid.Size = new System.Drawing.Size(746, 302);
            this.cobrosGrid.TabIndex = 1;
            this.cobrosGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cobrosGrid_CellDoubleClick);
            // 
            // cerrarButton
            // 
            this.cerrarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cerrarButton.Location = new System.Drawing.Point(657, 374);
            this.cerrarButton.Name = "cerrarButton";
            this.cerrarButton.Size = new System.Drawing.Size(105, 32);
            this.cerrarButton.TabIndex = 2;
            this.cerrarButton.Text = "Cerrar";
            this.cerrarButton.UseVisualStyleBackColor = true;
            this.cerrarButton.Click += new System.EventHandler(this.cerrarButton_Click);
            // 
            // clienteLabel
            // 
            this.clienteLabel.AutoSize = true;
            this.clienteLabel.Location = new System.Drawing.Point(13, 19);
            this.clienteLabel.Name = "clienteLabel";
            this.clienteLabel.Size = new System.Drawing.Size(57, 17);
            this.clienteLabel.TabIndex = 0;
            this.clienteLabel.Text = "Cliente";
            // 
            // cobrosContextMenu
            // 
            this.cobrosContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cobrosContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reimprimirCobroToolStripMenuItem});
            this.cobrosContextMenu.Name = "cobrosContextMenu";
            this.cobrosContextMenu.Size = new System.Drawing.Size(200, 28);
            // 
            // reimprimirCobroToolStripMenuItem
            // 
            this.reimprimirCobroToolStripMenuItem.Name = "reimprimirCobroToolStripMenuItem";
            this.reimprimirCobroToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.reimprimirCobroToolStripMenuItem.Text = "Reimprimir cobro";
            this.reimprimirCobroToolStripMenuItem.Click += new System.EventHandler(this.reimprimirCobroToolStripMenuItem_Click);
            // 
            // CobrosClienteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 418);
            this.Controls.Add(this.clienteLabel);
            this.Controls.Add(this.cerrarButton);
            this.Controls.Add(this.cobrosGrid);
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "CobrosClienteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cobros del cliente";
            this.Load += new System.EventHandler(this.CobrosClienteForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cobrosGrid)).EndInit();
            this.cobrosContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView cobrosGrid;
        private System.Windows.Forms.Button cerrarButton;
        private System.Windows.Forms.Label clienteLabel;
        private System.Windows.Forms.ContextMenuStrip cobrosContextMenu;
        private System.Windows.Forms.ToolStripMenuItem reimprimirCobroToolStripMenuItem;
    }
}
