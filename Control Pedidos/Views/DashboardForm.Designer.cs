namespace Control_Pedidos.Views
{
    partial class DashboardForm
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
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.roleLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.ordersButton = new System.Windows.Forms.Button();
            this.usersButton = new System.Windows.Forms.Button();
            this.clientsButton = new System.Windows.Forms.Button();
            this.articlesButton = new System.Windows.Forms.Button();
            this.activeOrdersGroupBox = new System.Windows.Forms.GroupBox();
            this.activeOrdersCountLabel = new System.Windows.Forms.Label();
            this.countCaptionLabel = new System.Windows.Forms.Label();
            this.activeOrdersGrid = new System.Windows.Forms.DataGridView();
            this.companyLabel = new System.Windows.Forms.Label();
            this.eventsButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCheckInPedidos = new System.Windows.Forms.Button();
            this.btnCheckOutPedidos = new System.Windows.Forms.Button();
            this.btnCorteCaja = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.activeOrdersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activeOrdersGrid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.welcomeLabel.Location = new System.Drawing.Point(29, 73);
            this.welcomeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(118, 28);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Text = "Bienvenido";
            // 
            // roleLabel
            // 
            this.roleLabel.AutoSize = true;
            this.roleLabel.Location = new System.Drawing.Point(33, 103);
            this.roleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Size = new System.Drawing.Size(28, 16);
            this.roleLabel.TabIndex = 1;
            this.roleLabel.Text = "Rol";
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(1629, 46);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(183, 42);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Actualizar";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // ordersButton
            // 
            this.ordersButton.Location = new System.Drawing.Point(37, 178);
            this.ordersButton.Margin = new System.Windows.Forms.Padding(4);
            this.ordersButton.Name = "ordersButton";
            this.ordersButton.Size = new System.Drawing.Size(147, 63);
            this.ordersButton.TabIndex = 3;
            this.ordersButton.Text = "Gestión de pedidos";
            this.ordersButton.UseVisualStyleBackColor = true;
            this.ordersButton.Click += new System.EventHandler(this.ordersButton_Click);
            // 
            // usersButton
            // 
            this.usersButton.Location = new System.Drawing.Point(213, 178);
            this.usersButton.Margin = new System.Windows.Forms.Padding(4);
            this.usersButton.Name = "usersButton";
            this.usersButton.Size = new System.Drawing.Size(147, 63);
            this.usersButton.TabIndex = 4;
            this.usersButton.Text = "Usuarios";
            this.usersButton.UseVisualStyleBackColor = true;
            this.usersButton.Click += new System.EventHandler(this.usersButton_Click);
            // 
            // clientsButton
            // 
            this.clientsButton.Location = new System.Drawing.Point(380, 178);
            this.clientsButton.Margin = new System.Windows.Forms.Padding(4);
            this.clientsButton.Name = "clientsButton";
            this.clientsButton.Size = new System.Drawing.Size(150, 63);
            this.clientsButton.TabIndex = 5;
            this.clientsButton.Text = "Clientes";
            this.clientsButton.UseVisualStyleBackColor = true;
            this.clientsButton.Click += new System.EventHandler(this.clientsButton_Click);
            // 
            // articlesButton
            // 
            this.articlesButton.Location = new System.Drawing.Point(552, 178);
            this.articlesButton.Margin = new System.Windows.Forms.Padding(4);
            this.articlesButton.Name = "articlesButton";
            this.articlesButton.Size = new System.Drawing.Size(147, 63);
            this.articlesButton.TabIndex = 6;
            this.articlesButton.Text = "Artículos";
            this.articlesButton.UseVisualStyleBackColor = true;
            this.articlesButton.Click += new System.EventHandler(this.articlesButton_Click);
            // 
            // activeOrdersGroupBox
            // 
            this.activeOrdersGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activeOrdersGroupBox.Controls.Add(this.activeOrdersCountLabel);
            this.activeOrdersGroupBox.Controls.Add(this.countCaptionLabel);
            this.activeOrdersGroupBox.Controls.Add(this.activeOrdersGrid);
            this.activeOrdersGroupBox.Location = new System.Drawing.Point(7, 7);
            this.activeOrdersGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.activeOrdersGroupBox.Name = "activeOrdersGroupBox";
            this.activeOrdersGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.activeOrdersGroupBox.Size = new System.Drawing.Size(1776, 387);
            this.activeOrdersGroupBox.TabIndex = 5;
            this.activeOrdersGroupBox.TabStop = false;
            this.activeOrdersGroupBox.Text = "Pedidos activos";
            this.activeOrdersGroupBox.Visible = false;
            // 
            // activeOrdersCountLabel
            // 
            this.activeOrdersCountLabel.AutoSize = true;
            this.activeOrdersCountLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.activeOrdersCountLabel.Location = new System.Drawing.Point(134, 24);
            this.activeOrdersCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.activeOrdersCountLabel.Name = "activeOrdersCountLabel";
            this.activeOrdersCountLabel.Size = new System.Drawing.Size(20, 23);
            this.activeOrdersCountLabel.TabIndex = 2;
            this.activeOrdersCountLabel.Text = "0";
            // 
            // countCaptionLabel
            // 
            this.countCaptionLabel.AutoSize = true;
            this.countCaptionLabel.Location = new System.Drawing.Point(23, 25);
            this.countCaptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.countCaptionLabel.Name = "countCaptionLabel";
            this.countCaptionLabel.Size = new System.Drawing.Size(113, 16);
            this.countCaptionLabel.TabIndex = 1;
            this.countCaptionLabel.Text = "Total de pedidos:";
            // 
            // activeOrdersGrid
            // 
            this.activeOrdersGrid.AllowUserToAddRows = false;
            this.activeOrdersGrid.AllowUserToDeleteRows = false;
            this.activeOrdersGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activeOrdersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.activeOrdersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activeOrdersGrid.Location = new System.Drawing.Point(27, 77);
            this.activeOrdersGrid.Margin = new System.Windows.Forms.Padding(4);
            this.activeOrdersGrid.MultiSelect = false;
            this.activeOrdersGrid.Name = "activeOrdersGrid";
            this.activeOrdersGrid.ReadOnly = true;
            this.activeOrdersGrid.RowHeadersWidth = 51;
            this.activeOrdersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.activeOrdersGrid.Size = new System.Drawing.Size(1724, 287);
            this.activeOrdersGrid.TabIndex = 0;
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Location = new System.Drawing.Point(33, 129);
            this.companyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(62, 16);
            this.companyLabel.TabIndex = 8;
            this.companyLabel.Text = "Empresa";
            // 
            // eventsButton
            // 
            this.eventsButton.Location = new System.Drawing.Point(723, 178);
            this.eventsButton.Margin = new System.Windows.Forms.Padding(4);
            this.eventsButton.Name = "eventsButton";
            this.eventsButton.Size = new System.Drawing.Size(147, 63);
            this.eventsButton.TabIndex = 9;
            this.eventsButton.Text = "Eventos";
            this.eventsButton.UseVisualStyleBackColor = true;
            this.eventsButton.Click += new System.EventHandler(this.eventsButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(891, 178);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 63);
            this.button1.TabIndex = 10;
            this.button1.Text = "Reportes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCheckInPedidos
            // 
            this.btnCheckInPedidos.Location = new System.Drawing.Point(1236, 178);
            this.btnCheckInPedidos.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckInPedidos.Name = "btnCheckInPedidos";
            this.btnCheckInPedidos.Size = new System.Drawing.Size(147, 63);
            this.btnCheckInPedidos.TabIndex = 11;
            this.btnCheckInPedidos.Text = "CheckIn Pedidos";
            this.btnCheckInPedidos.UseVisualStyleBackColor = true;
            this.btnCheckInPedidos.Click += new System.EventHandler(this.btnCheckInPedidos_Click);
            // 
            // btnCheckOutPedidos
            // 
            this.btnCheckOutPedidos.Location = new System.Drawing.Point(1406, 178);
            this.btnCheckOutPedidos.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckOutPedidos.Name = "btnCheckOutPedidos";
            this.btnCheckOutPedidos.Size = new System.Drawing.Size(147, 63);
            this.btnCheckOutPedidos.TabIndex = 12;
            this.btnCheckOutPedidos.Text = "CheckOut Pedidos";
            this.btnCheckOutPedidos.UseVisualStyleBackColor = true;
            this.btnCheckOutPedidos.Click += new System.EventHandler(this.btnCheckOutPedidos_Click);
            // 
            // btnCorteCaja
            // 
            this.btnCorteCaja.Location = new System.Drawing.Point(1062, 178);
            this.btnCorteCaja.Margin = new System.Windows.Forms.Padding(4);
            this.btnCorteCaja.Name = "btnCorteCaja";
            this.btnCorteCaja.Size = new System.Drawing.Size(147, 63);
            this.btnCorteCaja.TabIndex = 13;
            this.btnCorteCaja.Text = "Corte de Caja";
            this.btnCorteCaja.UseVisualStyleBackColor = true;
            this.btnCorteCaja.Click += new System.EventHandler(this.btnCorteCaja_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(37, 265);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1791, 427);
            this.tabControl1.TabIndex = 14;
            this.tabControl1.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1783, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Forecast";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.activeOrdersGroupBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1783, 398);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Historia pedidos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1840, 704);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCorteCaja);
            this.Controls.Add(this.btnCheckOutPedidos);
            this.Controls.Add(this.btnCheckInPedidos);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.eventsButton);
            this.Controls.Add(this.companyLabel);
            this.Controls.Add(this.articlesButton);
            this.Controls.Add(this.clientsButton);
            this.Controls.Add(this.usersButton);
            this.Controls.Add(this.ordersButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.roleLabel);
            this.Controls.Add(this.welcomeLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de pedidos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.activeOrdersGroupBox.ResumeLayout(false);
            this.activeOrdersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activeOrdersGrid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button ordersButton;
        private System.Windows.Forms.Button usersButton;
        private System.Windows.Forms.Button clientsButton;
        private System.Windows.Forms.Button articlesButton;
        private System.Windows.Forms.GroupBox activeOrdersGroupBox;
        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.Button eventsButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCheckInPedidos;
        private System.Windows.Forms.Button btnCheckOutPedidos;
        private System.Windows.Forms.Button btnCorteCaja;
        private System.Windows.Forms.Label activeOrdersCountLabel;
        private System.Windows.Forms.Label countCaptionLabel;
        private System.Windows.Forms.DataGridView activeOrdersGrid;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
