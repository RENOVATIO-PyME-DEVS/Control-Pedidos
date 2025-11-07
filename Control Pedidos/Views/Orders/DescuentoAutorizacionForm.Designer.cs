namespace Control_Pedidos.Views.Orders
{
    partial class DescuentoAutorizacionForm
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
            this.correoLabel = new System.Windows.Forms.Label();
            this.correoTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.autorizarButton = new System.Windows.Forms.Button();
            this.cancelarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // correoLabel
            // 
            this.correoLabel.AutoSize = true;
            this.correoLabel.Location = new System.Drawing.Point(28, 66);
            this.correoLabel.Name = "correoLabel";
            this.correoLabel.Size = new System.Drawing.Size(133, 16);
            this.correoLabel.TabIndex = 0;
            this.correoLabel.Text = "Correo administrador";
            // 
            // correoTextBox
            // 
            this.correoTextBox.Location = new System.Drawing.Point(31, 85);
            this.correoTextBox.MaxLength = 150;
            this.correoTextBox.Name = "correoTextBox";
            this.correoTextBox.Size = new System.Drawing.Size(346, 22);
            this.correoTextBox.TabIndex = 1;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(28, 132);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(76, 16);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Contraseña";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(31, 151);
            this.passwordTextBox.MaxLength = 100;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '•';
            this.passwordTextBox.Size = new System.Drawing.Size(346, 22);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // autorizarButton
            // 
            this.autorizarButton.Location = new System.Drawing.Point(167, 209);
            this.autorizarButton.Name = "autorizarButton";
            this.autorizarButton.Size = new System.Drawing.Size(210, 30);
            this.autorizarButton.TabIndex = 4;
            this.autorizarButton.Text = "Autorizar";
            this.autorizarButton.UseVisualStyleBackColor = true;
            this.autorizarButton.Click += new System.EventHandler(this.autorizarButton_Click);
            // 
            // cancelarButton
            // 
            this.cancelarButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelarButton.Location = new System.Drawing.Point(31, 209);
            this.cancelarButton.Name = "cancelarButton";
            this.cancelarButton.Size = new System.Drawing.Size(130, 30);
            this.cancelarButton.TabIndex = 5;
            this.cancelarButton.Text = "Cancelar";
            this.cancelarButton.UseVisualStyleBackColor = true;
            this.cancelarButton.Click += new System.EventHandler(this.cancelarButton_Click);
            // 
            // DescuentoAutorizacionForm
            // 
            this.AcceptButton = this.autorizarButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelarButton;
            this.ClientSize = new System.Drawing.Size(409, 263);
            this.Controls.Add(this.cancelarButton);
            this.Controls.Add(this.autorizarButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.correoTextBox);
            this.Controls.Add(this.correoLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DescuentoAutorizacionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Autorización de descuento";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label correoLabel;
        private System.Windows.Forms.TextBox correoTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button autorizarButton;
        private System.Windows.Forms.Button cancelarButton;
    }
}
