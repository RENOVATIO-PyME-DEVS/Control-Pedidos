namespace Control_Pedidos.Views.Orders
{
    partial class DevolucionPedidoForm
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
            this.clienteLabel = new System.Windows.Forms.Label();
            this.clienteTextBox = new System.Windows.Forms.TextBox();
            this.pedidosDataGridView = new System.Windows.Forms.DataGridView();
            this.detalleGroupBox = new System.Windows.Forms.GroupBox();
            this.detallesDataGridView = new System.Windows.Forms.DataGridView();
            this.resumenGroupBox = new System.Windows.Forms.GroupBox();
            this.resumenTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.totalPedidoLabel = new System.Windows.Forms.Label();
            this.totalPedidoTextBox = new System.Windows.Forms.TextBox();
            this.totalCobrosLabel = new System.Windows.Forms.Label();
            this.totalCobrosTextBox = new System.Windows.Forms.TextBox();
            this.saldoLabel = new System.Windows.Forms.Label();
            this.saldoTextBox = new System.Windows.Forms.TextBox();
            this.formaCobroActualLabel = new System.Windows.Forms.Label();
            this.formaCobroActualTextBox = new System.Windows.Forms.TextBox();
            this.formaCobroDevolucionLabel = new System.Windows.Forms.Label();
            this.formaCobroComboBox = new System.Windows.Forms.ComboBox();
            this.devolverButton = new System.Windows.Forms.Button();
            this.cancelarButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pedidosDataGridView)).BeginInit();
            this.detalleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detallesDataGridView)).BeginInit();
            this.resumenGroupBox.SuspendLayout();
            this.resumenTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // clienteLabel
            // 
            this.clienteLabel.AutoSize = true;
            this.clienteLabel.Location = new System.Drawing.Point(12, 15);
            this.clienteLabel.Name = "clienteLabel";
            this.clienteLabel.Size = new System.Drawing.Size(51, 15);
            this.clienteLabel.TabIndex = 0;
            this.clienteLabel.Text = "Cliente:";
            // 
            // clienteTextBox
            // 
            this.clienteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clienteTextBox.Location = new System.Drawing.Point(69, 12);
            this.clienteTextBox.Name = "clienteTextBox";
            this.clienteTextBox.ReadOnly = true;
            this.clienteTextBox.Size = new System.Drawing.Size(739, 23);
            this.clienteTextBox.TabIndex = 1;
            // 
            // pedidosDataGridView
            // 
            this.pedidosDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pedidosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pedidosDataGridView.Location = new System.Drawing.Point(12, 50);
            this.pedidosDataGridView.MultiSelect = false;
            this.pedidosDataGridView.Name = "pedidosDataGridView";
            this.pedidosDataGridView.RowHeadersVisible = false;
            this.pedidosDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pedidosDataGridView.Size = new System.Drawing.Size(796, 180);
            this.pedidosDataGridView.TabIndex = 2;
            // 
            // detalleGroupBox
            // 
            this.detalleGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detalleGroupBox.Controls.Add(this.detallesDataGridView);
            this.detalleGroupBox.Location = new System.Drawing.Point(12, 236);
            this.detalleGroupBox.Name = "detalleGroupBox";
            this.detalleGroupBox.Size = new System.Drawing.Size(796, 180);
            this.detalleGroupBox.TabIndex = 3;
            this.detalleGroupBox.TabStop = false;
            this.detalleGroupBox.Text = "Detalle del pedido";
            // 
            // detallesDataGridView
            // 
            this.detallesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.detallesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detallesDataGridView.Location = new System.Drawing.Point(3, 19);
            this.detallesDataGridView.MultiSelect = false;
            this.detallesDataGridView.Name = "detallesDataGridView";
            this.detallesDataGridView.ReadOnly = true;
            this.detallesDataGridView.RowHeadersVisible = false;
            this.detallesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detallesDataGridView.Size = new System.Drawing.Size(790, 158);
            this.detallesDataGridView.TabIndex = 0;
            // 
            // resumenGroupBox
            // 
            this.resumenGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resumenGroupBox.Controls.Add(this.resumenTableLayoutPanel);
            this.resumenGroupBox.Location = new System.Drawing.Point(12, 422);
            this.resumenGroupBox.Name = "resumenGroupBox";
            this.resumenGroupBox.Size = new System.Drawing.Size(796, 152);
            this.resumenGroupBox.TabIndex = 4;
            this.resumenGroupBox.TabStop = false;
            this.resumenGroupBox.Text = "Resumen";
            // 
            // resumenTableLayoutPanel
            // 
            this.resumenTableLayoutPanel.ColumnCount = 2;
            this.resumenTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.resumenTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.resumenTableLayoutPanel.Controls.Add(this.totalPedidoLabel, 0, 0);
            this.resumenTableLayoutPanel.Controls.Add(this.totalPedidoTextBox, 1, 0);
            this.resumenTableLayoutPanel.Controls.Add(this.totalCobrosLabel, 0, 1);
            this.resumenTableLayoutPanel.Controls.Add(this.totalCobrosTextBox, 1, 1);
            this.resumenTableLayoutPanel.Controls.Add(this.saldoLabel, 0, 2);
            this.resumenTableLayoutPanel.Controls.Add(this.saldoTextBox, 1, 2);
            this.resumenTableLayoutPanel.Controls.Add(this.formaCobroActualLabel, 0, 3);
            this.resumenTableLayoutPanel.Controls.Add(this.formaCobroActualTextBox, 1, 3);
            this.resumenTableLayoutPanel.Controls.Add(this.formaCobroDevolucionLabel, 0, 4);
            this.resumenTableLayoutPanel.Controls.Add(this.formaCobroComboBox, 1, 4);
            this.resumenTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resumenTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.resumenTableLayoutPanel.Name = "resumenTableLayoutPanel";
            this.resumenTableLayoutPanel.RowCount = 5;
            this.resumenTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.resumenTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.resumenTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.resumenTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.resumenTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.resumenTableLayoutPanel.Size = new System.Drawing.Size(790, 130);
            this.resumenTableLayoutPanel.TabIndex = 0;
            // 
            // totalPedidoLabel
            // 
            this.totalPedidoLabel.AutoSize = true;
            this.totalPedidoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalPedidoLabel.Location = new System.Drawing.Point(3, 0);
            this.totalPedidoLabel.Name = "totalPedidoLabel";
            this.totalPedidoLabel.Size = new System.Drawing.Size(270, 28);
            this.totalPedidoLabel.TabIndex = 0;
            this.totalPedidoLabel.Text = "Total del pedido:";
            this.totalPedidoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalPedidoTextBox
            // 
            this.totalPedidoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalPedidoTextBox.Location = new System.Drawing.Point(279, 3);
            this.totalPedidoTextBox.Name = "totalPedidoTextBox";
            this.totalPedidoTextBox.ReadOnly = true;
            this.totalPedidoTextBox.Size = new System.Drawing.Size(508, 23);
            this.totalPedidoTextBox.TabIndex = 1;
            // 
            // totalCobrosLabel
            // 
            this.totalCobrosLabel.AutoSize = true;
            this.totalCobrosLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalCobrosLabel.Location = new System.Drawing.Point(3, 28);
            this.totalCobrosLabel.Name = "totalCobrosLabel";
            this.totalCobrosLabel.Size = new System.Drawing.Size(270, 28);
            this.totalCobrosLabel.TabIndex = 2;
            this.totalCobrosLabel.Text = "Total cobros registrados:";
            this.totalCobrosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalCobrosTextBox
            // 
            this.totalCobrosTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalCobrosTextBox.Location = new System.Drawing.Point(279, 31);
            this.totalCobrosTextBox.Name = "totalCobrosTextBox";
            this.totalCobrosTextBox.ReadOnly = true;
            this.totalCobrosTextBox.Size = new System.Drawing.Size(508, 23);
            this.totalCobrosTextBox.TabIndex = 3;
            // 
            // saldoLabel
            // 
            this.saldoLabel.AutoSize = true;
            this.saldoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saldoLabel.Location = new System.Drawing.Point(3, 56);
            this.saldoLabel.Name = "saldoLabel";
            this.saldoLabel.Size = new System.Drawing.Size(270, 28);
            this.saldoLabel.TabIndex = 4;
            this.saldoLabel.Text = "Saldo pendiente:";
            this.saldoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saldoTextBox
            // 
            this.saldoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saldoTextBox.Location = new System.Drawing.Point(279, 59);
            this.saldoTextBox.Name = "saldoTextBox";
            this.saldoTextBox.ReadOnly = true;
            this.saldoTextBox.Size = new System.Drawing.Size(508, 23);
            this.saldoTextBox.TabIndex = 5;
            // 
            // formaCobroActualLabel
            // 
            this.formaCobroActualLabel.AutoSize = true;
            this.formaCobroActualLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formaCobroActualLabel.Location = new System.Drawing.Point(3, 84);
            this.formaCobroActualLabel.Name = "formaCobroActualLabel";
            this.formaCobroActualLabel.Size = new System.Drawing.Size(270, 28);
            this.formaCobroActualLabel.TabIndex = 6;
            this.formaCobroActualLabel.Text = "Última forma de cobro registrada:";
            this.formaCobroActualLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // formaCobroActualTextBox
            // 
            this.formaCobroActualTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formaCobroActualTextBox.Location = new System.Drawing.Point(279, 87);
            this.formaCobroActualTextBox.Name = "formaCobroActualTextBox";
            this.formaCobroActualTextBox.ReadOnly = true;
            this.formaCobroActualTextBox.Size = new System.Drawing.Size(508, 23);
            this.formaCobroActualTextBox.TabIndex = 7;
            // 
            // formaCobroDevolucionLabel
            // 
            this.formaCobroDevolucionLabel.AutoSize = true;
            this.formaCobroDevolucionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formaCobroDevolucionLabel.Location = new System.Drawing.Point(3, 112);
            this.formaCobroDevolucionLabel.Name = "formaCobroDevolucionLabel";
            this.formaCobroDevolucionLabel.Size = new System.Drawing.Size(270, 28);
            this.formaCobroDevolucionLabel.TabIndex = 8;
            this.formaCobroDevolucionLabel.Text = "Forma de cobro devolución:";
            this.formaCobroDevolucionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // formaCobroComboBox
            // 
            this.formaCobroComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formaCobroComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formaCobroComboBox.FormattingEnabled = true;
            this.formaCobroComboBox.Location = new System.Drawing.Point(279, 115);
            this.formaCobroComboBox.Name = "formaCobroComboBox";
            this.formaCobroComboBox.Size = new System.Drawing.Size(508, 23);
            this.formaCobroComboBox.TabIndex = 9;
            // 
            // devolverButton
            // 
            this.devolverButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.devolverButton.Location = new System.Drawing.Point(542, 586);
            this.devolverButton.Name = "devolverButton";
            this.devolverButton.Size = new System.Drawing.Size(130, 30);
            this.devolverButton.TabIndex = 5;
            this.devolverButton.Text = "Devolver pedido";
            this.devolverButton.UseVisualStyleBackColor = true;
            this.devolverButton.Click += new System.EventHandler(this.devolverButton_Click);
            // 
            // cancelarButton
            // 
            this.cancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelarButton.Location = new System.Drawing.Point(678, 586);
            this.cancelarButton.Name = "cancelarButton";
            this.cancelarButton.Size = new System.Drawing.Size(130, 30);
            this.cancelarButton.TabIndex = 6;
            this.cancelarButton.Text = "Cerrar";
            this.cancelarButton.UseVisualStyleBackColor = true;
            this.cancelarButton.Click += new System.EventHandler(this.cancelarButton_Click);
            // 
            // DevolucionPedidoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 628);
            this.Controls.Add(this.cancelarButton);
            this.Controls.Add(this.devolverButton);
            this.Controls.Add(this.resumenGroupBox);
            this.Controls.Add(this.detalleGroupBox);
            this.Controls.Add(this.pedidosDataGridView);
            this.Controls.Add(this.clienteTextBox);
            this.Controls.Add(this.clienteLabel);
            this.MinimizeBox = false;
            this.Name = "DevolucionPedidoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Devolver pedido";
            this.Load += new System.EventHandler(this.DevolucionPedidoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pedidosDataGridView)).EndInit();
            this.detalleGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detallesDataGridView)).EndInit();
            this.resumenGroupBox.ResumeLayout(false);
            this.resumenTableLayoutPanel.ResumeLayout(false);
            this.resumenTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clienteLabel;
        private System.Windows.Forms.TextBox clienteTextBox;
        private System.Windows.Forms.DataGridView pedidosDataGridView;
        private System.Windows.Forms.GroupBox detalleGroupBox;
        private System.Windows.Forms.DataGridView detallesDataGridView;
        private System.Windows.Forms.GroupBox resumenGroupBox;
        private System.Windows.Forms.TableLayoutPanel resumenTableLayoutPanel;
        private System.Windows.Forms.Label totalPedidoLabel;
        private System.Windows.Forms.TextBox totalPedidoTextBox;
        private System.Windows.Forms.Label totalCobrosLabel;
        private System.Windows.Forms.TextBox totalCobrosTextBox;
        private System.Windows.Forms.Label saldoLabel;
        private System.Windows.Forms.TextBox saldoTextBox;
        private System.Windows.Forms.Label formaCobroActualLabel;
        private System.Windows.Forms.TextBox formaCobroActualTextBox;
        private System.Windows.Forms.Label formaCobroDevolucionLabel;
        private System.Windows.Forms.ComboBox formaCobroComboBox;
        private System.Windows.Forms.Button devolverButton;
        private System.Windows.Forms.Button cancelarButton;
    }
}
