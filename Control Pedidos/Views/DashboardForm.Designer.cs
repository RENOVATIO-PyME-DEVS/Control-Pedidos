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
            this.welcomeLabel.Location = new System.Drawing.Point(22, 18);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(93, 21);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Text = "Bienvenido";
            // 
            // roleLabel
            // 
            this.roleLabel.AutoSize = true;
            this.roleLabel.Location = new System.Drawing.Point(25, 48);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Size = new System.Drawing.Size(26, 13);
            this.roleLabel.TabIndex = 1;
            this.roleLabel.Text = "Rol";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(604, 27);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(137, 34);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Actualizar";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // ordersButton
            // 
            this.ordersButton.Location = new System.Drawing.Point(28, 82);
            this.ordersButton.Name = "ordersButton";
            this.ordersButton.Size = new System.Drawing.Size(137, 34);
            this.ordersButton.TabIndex = 3;
            this.ordersButton.Text = "Gestión de pedidos";
            this.ordersButton.UseVisualStyleBackColor = true;
            this.ordersButton.Click += new System.EventHandler(this.ordersButton_Click);
            // 
            // usersButton
            // 
            this.usersButton.Location = new System.Drawing.Point(181, 82);
            this.usersButton.Name = "usersButton";
            this.usersButton.Size = new System.Drawing.Size(137, 34);
            this.usersButton.TabIndex = 4;
            this.usersButton.Text = "Usuarios";
            this.usersButton.UseVisualStyleBackColor = true;
            // 
            // activeOrdersGroupBox
            // 
            this.activeOrdersGroupBox.Controls.Add(this.activeOrdersCountLabel);
            this.activeOrdersGroupBox.Controls.Add(this.countCaptionLabel);
            this.activeOrdersGroupBox.Controls.Add(this.activeOrdersGrid);
            this.activeOrdersGroupBox.Location = new System.Drawing.Point(28, 133);
            this.activeOrdersGroupBox.Name = "activeOrdersGroupBox";
            this.activeOrdersGroupBox.Size = new System.Drawing.Size(713, 283);
            this.activeOrdersGroupBox.TabIndex = 5;
            this.activeOrdersGroupBox.TabStop = false;
            this.activeOrdersGroupBox.Text = "Pedidos activos";
            // 
            // activeOrdersCountLabel
            // 
            this.activeOrdersCountLabel.AutoSize = true;
            this.activeOrdersCountLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.activeOrdersCountLabel.Location = new System.Drawing.Point(130, 27);
            this.activeOrdersCountLabel.Name = "activeOrdersCountLabel";
            this.activeOrdersCountLabel.Size = new System.Drawing.Size(17, 19);
            this.activeOrdersCountLabel.TabIndex = 2;
            this.activeOrdersCountLabel.Text = "0";
            // 
            // countCaptionLabel
            // 
            this.countCaptionLabel.AutoSize = true;
            this.countCaptionLabel.Location = new System.Drawing.Point(17, 31);
            this.countCaptionLabel.Name = "countCaptionLabel";
            this.countCaptionLabel.Size = new System.Drawing.Size(107, 13);
            this.countCaptionLabel.TabIndex = 1;
            this.countCaptionLabel.Text = "Total de pedidos:";
            // 
            // activeOrdersGrid
            // 
            this.activeOrdersGrid.AllowUserToAddRows = false;
            this.activeOrdersGrid.AllowUserToDeleteRows = false;
            this.activeOrdersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.activeOrdersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activeOrdersGrid.Location = new System.Drawing.Point(20, 60);
            this.activeOrdersGrid.MultiSelect = false;
            this.activeOrdersGrid.Name = "activeOrdersGrid";
            this.activeOrdersGrid.ReadOnly = true;
            this.activeOrdersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.activeOrdersGrid.Size = new System.Drawing.Size(674, 204);
            this.activeOrdersGrid.TabIndex = 0;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 441);
            this.Controls.Add(this.activeOrdersGroupBox);
            this.Controls.Add(this.usersButton);
            this.Controls.Add(this.ordersButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.roleLabel);
            this.Controls.Add(this.welcomeLabel);
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
        private System.Windows.Forms.GroupBox activeOrdersGroupBox;
        private System.Windows.Forms.DataGridView activeOrdersGrid;
        private System.Windows.Forms.Label activeOrdersCountLabel;
        private System.Windows.Forms.Label countCaptionLabel;
    }
}
