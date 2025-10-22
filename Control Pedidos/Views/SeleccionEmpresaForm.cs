using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views
{
    public partial class SeleccionEmpresaForm : Form
    {
        private readonly List<Empresa> _empresas;

        public Empresa EmpresaSeleccionada { get; private set; }

        public SeleccionEmpresaForm(IEnumerable<Empresa> empresas)
        {
            if (empresas == null)
            {
                throw new ArgumentNullException(nameof(empresas));
            }

            _empresas = empresas.ToList();
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            empresasComboBox.DisplayMember = nameof(Empresa.Nombre);
            empresasComboBox.ValueMember = nameof(Empresa.Id);
            empresasComboBox.DataSource = _empresas;

            if (_empresas.Count > 0)
            {
                empresasComboBox.SelectedIndex = 0;
            }
        }

        private void confirmarButton_Click(object sender, EventArgs e)
        {
            if (empresasComboBox.SelectedItem is Empresa empresa)
            {
                EmpresaSeleccionada = empresa;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una empresa.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
