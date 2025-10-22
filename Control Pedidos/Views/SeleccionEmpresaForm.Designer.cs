namespace Control_Pedidos.Views
{
    partial class SeleccionEmpresaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label instruccionLabel;
        private System.Windows.Forms.ComboBox empresasComboBox;
        private System.Windows.Forms.Button confirmarButton;
        private System.Windows.Forms.Button cancelarButton;

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
            this.instruccionLabel = new System.Windows.Forms.Label();
            this.empresasComboBox = new System.Windows.Forms.ComboBox();
            this.confirmarButton = new System.Windows.Forms.Button();
            this.cancelarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // instruccionLabel
            // 
            this.instruccionLabel.AutoSize = true;
            this.instruccionLabel.Location = new System.Drawing.Point(32, 32);
            this.instruccionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.instruccionLabel.Name = "instruccionLabel";
            this.instruccionLabel.Size = new System.Drawing.Size(250, 16);
            this.instruccionLabel.TabIndex = 0;
            this.instruccionLabel.Text = "Seleccione la empresa con la que trabajará";
            // 
            // empresasComboBox
            // 
            this.empresasComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.empresasComboBox.FormattingEnabled = true;
            this.empresasComboBox.Location = new System.Drawing.Point(36, 60);
            this.empresasComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.empresasComboBox.Name = "empresasComboBox";
            this.empresasComboBox.Size = new System.Drawing.Size(360, 24);
            this.empresasComboBox.TabIndex = 1;
            // 
            // confirmarButton
            // 
            this.confirmarButton.Location = new System.Drawing.Point(36, 104);
            this.confirmarButton.Margin = new System.Windows.Forms.Padding(4);
            this.confirmarButton.Name = "confirmarButton";
            this.confirmarButton.Size = new System.Drawing.Size(164, 36);
            this.confirmarButton.TabIndex = 2;
            this.confirmarButton.Text = "Aceptar";
            this.confirmarButton.UseVisualStyleBackColor = true;
            this.confirmarButton.Click += new System.EventHandler(this.confirmarButton_Click);
            // 
            // cancelarButton
            // 
            this.cancelarButton.Location = new System.Drawing.Point(232, 104);
            this.cancelarButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelarButton.Name = "cancelarButton";
            this.cancelarButton.Size = new System.Drawing.Size(164, 36);
            this.cancelarButton.TabIndex = 3;
            this.cancelarButton.Text = "Cancelar";
            this.cancelarButton.UseVisualStyleBackColor = true;
            this.cancelarButton.Click += new System.EventHandler(this.cancelarButton_Click);
            // 
            // SeleccionEmpresaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 169);
            this.Controls.Add(this.cancelarButton);
            this.Controls.Add(this.confirmarButton);
            this.Controls.Add(this.empresasComboBox);
            this.Controls.Add(this.instruccionLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeleccionEmpresaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccionar empresa";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
