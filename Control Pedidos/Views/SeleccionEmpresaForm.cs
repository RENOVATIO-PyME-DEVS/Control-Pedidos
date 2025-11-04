using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views
{
    public partial class SeleccionEmpresaForm : Form
    {
        private readonly IList<Empresa> _empresas;

        public Empresa EmpresaSeleccionada { get; private set; }

        public SeleccionEmpresaForm(IList<Empresa> empresas)
        {
            _empresas = empresas ?? throw new ArgumentNullException(nameof(empresas));
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            PopulateEmpresas();
        }

        private void PopulateEmpresas()
        {
            // Configuramos el combo para que muestre el nombre pero conserve el id.
            empresaComboBox.DisplayMember = nameof(Empresa.Nombre);
            empresaComboBox.ValueMember = nameof(Empresa.Id);
            empresaComboBox.DataSource = new List<Empresa>(_empresas);

            if (_empresas.Count > 0)
            {
                empresaComboBox.SelectedIndex = 0;
            }
        }

        private void aceptarButton_Click(object sender, EventArgs e)
        {
            if (empresaComboBox.SelectedItem is Empresa empresa)
            {
                EmpresaSeleccionada = empresa;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Seleccione una empresa válida", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
