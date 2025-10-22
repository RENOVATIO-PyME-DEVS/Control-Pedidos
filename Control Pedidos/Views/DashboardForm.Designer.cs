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
            this.activeOrdersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activeOrdersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.welcomeLabel.Location = new System.Drawing.Point(29, 22);
            this.welcomeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(118, 28);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Text = "Bienvenido";
            // 
            // roleLabel
            // 
            this.roleLabel.AutoSize = true;
            this.roleLabel.Location = new System.Drawing.Point(33, 59);
            this.roleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Size = new System.Drawing.Size(28, 16);
            this.roleLabel.TabIndex = 1;
            this.roleLabel.Text = "Rol";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(792, 46);
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
            this.ordersButton.Location = new System.Drawing.Point(37, 101);
            this.ordersButton.Margin = new System.Windows.Forms.Padding(4);
            this.ordersButton.Name = "ordersButton";
            this.ordersButton.Size = new System.Drawing.Size(183, 42);
            this.ordersButton.TabIndex = 3;
            this.ordersButton.Text = "Gestión de pedidos";
            this.ordersButton.UseVisualStyleBackColor = true;
            this.ordersButton.Click += new System.EventHandler(this.ordersButton_Click);
            // 
            // usersButton
            // 
            this.usersButton.Location = new System.Drawing.Point(241, 101);
            this.usersButton.Margin = new System.Windows.Forms.Padding(4);
            this.usersButton.Name = "usersButton";
            this.usersButton.Size = new System.Drawing.Size(183, 42);
            this.usersButton.TabIndex = 4;
            this.usersButton.Text = "Usuarios";
            this.usersButton.UseVisualStyleBackColor = true;
            this.usersButton.Click += new System.EventHandler(this.usersButton_Click);
            // 
            // clientsButton
            // 
            this.clientsButton.Location = new System.Drawing.Point(445, 101);
            this.clientsButton.Margin = new System.Windows.Forms.Padding(4);
            this.clientsButton.Name = "clientsButton";
            this.clientsButton.Size = new System.Drawing.Size(183, 42);
            this.clientsButton.TabIndex = 5;
            this.clientsButton.Text = "Clientes";
            this.clientsButton.UseVisualStyleBackColor = true;
            this.clientsButton.Click += new System.EventHandler(this.clientsButton_Click);
            // 
            // articlesButton
            // 
            this.articlesButton.Location = new System.Drawing.Point(649, 101);
            this.articlesButton.Margin = new System.Windows.Forms.Padding(4);
            this.articlesButton.Name = "articlesButton";
            this.articlesButton.Size = new System.Drawing.Size(183, 42);
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
            this.activeOrdersGroupBox.Location = new System.Drawing.Point(37, 164);
            this.activeOrdersGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.activeOrdersGroupBox.Name = "activeOrdersGroupBox";
            this.activeOrdersGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.activeOrdersGroupBox.Size = new System.Drawing.Size(938, 511);
            this.activeOrdersGroupBox.TabIndex = 5;
            this.activeOrdersGroupBox.TabStop = false;
            this.activeOrdersGroupBox.Text = "Pedidos activos";
            // 
            // activeOrdersCountLabel
            // 
            this.activeOrdersCountLabel.AutoSize = true;
            this.activeOrdersCountLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.activeOrdersCountLabel.Location = new System.Drawing.Point(144, 33);
            this.activeOrdersCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.activeOrdersCountLabel.Name = "activeOrdersCountLabel";
            this.activeOrdersCountLabel.Size = new System.Drawing.Size(20, 23);
            this.activeOrdersCountLabel.TabIndex = 2;
            this.activeOrdersCountLabel.Text = "0";
            // 
            // countCaptionLabel
            // 
            this.countCaptionLabel.AutoSize = true;
            this.countCaptionLabel.Location = new System.Drawing.Point(23, 38);
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
            this.activeOrdersGrid.Location = new System.Drawing.Point(27, 74);
            this.activeOrdersGrid.Margin = new System.Windows.Forms.Padding(4);
            this.activeOrdersGrid.MultiSelect = false;
            this.activeOrdersGrid.Name = "activeOrdersGrid";
            this.activeOrdersGrid.ReadOnly = true;
            this.activeOrdersGrid.RowHeadersWidth = 51;
            this.activeOrdersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.activeOrdersGrid.Size = new System.Drawing.Size(886, 414);
            this.activeOrdersGrid.TabIndex = 0;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 706);
            this.Controls.Add(this.activeOrdersGroupBox);
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
            this.activeOrdersGroupBox.ResumeLayout(false);
            this.activeOrdersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activeOrdersGrid)).EndInit();
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
        private System.Windows.Forms.DataGridView activeOrdersGrid;
        private System.Windows.Forms.Label activeOrdersCountLabel;
        private System.Windows.Forms.Label countCaptionLabel;
    }
}
