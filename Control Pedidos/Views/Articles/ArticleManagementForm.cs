using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Models;

namespace Control_Pedidos.Views.Articles
{
    public partial class ArticleManagementForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly BindingList<Articulo> _articulos = new BindingList<Articulo>();
        private readonly BindingSource _articlesSource = new BindingSource();
        private readonly BindingList<KitDetalle> _componentes = new BindingList<KitDetalle>();
        private readonly BindingSource _componentsSource = new BindingSource();
        private IList<Articulo> _catalogoComponentes = new List<Articulo>();
        private Articulo _selectedArticulo;

        public ArticleManagementForm(DatabaseConnectionFactory connectionFactory)
        {
            InitializeComponent();
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            typeComboBox.DataSource = new[] { "normal", "kit" };
            statusComboBox.DataSource = new[] { "Activo", "Inactivo" };

            ConfigureGrids();
            LoadArticulos();
            LoadComponentCatalog();
            ToggleKitControls();
        }

        private void ConfigureGrids()
        {
            _articlesSource.DataSource = _articulos;
            articlesGrid.AutoGenerateColumns = false;
            articlesGrid.Columns.Clear();

            articlesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Articulo.Id),
                HeaderText = "ID",
                Width = 60
            });

            articlesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Articulo.Nombre),
                HeaderText = "Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            articlesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Articulo.TipoArticulo),
                HeaderText = "Tipo",
                Width = 80
            });

            articlesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Articulo.Precio),
                HeaderText = "Precio",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                Width = 120
            });

            articlesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Articulo.Estatus),
                HeaderText = "Estatus",
                Width = 90
            });

            articlesGrid.DataSource = _articlesSource;

            _componentsSource.DataSource = _componentes;
            componentsGrid.AutoGenerateColumns = false;
            componentsGrid.Columns.Clear();

            componentsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(KitDetalle.ArticuloId),
                HeaderText = "ID",
                Width = 60
            });

            componentsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(KitDetalle.NombreArticulo),
                HeaderText = "Artículo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            componentsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(KitDetalle.Cantidad),
                HeaderText = "Cantidad",
                Width = 90
            });

            componentsGrid.DataSource = _componentsSource;
        }

        private void LoadArticulos(string filtro = "")
        {
            try
            {
                _articulos.Clear();
                foreach (var articulo in Articulo.Listar(_connectionFactory, filtro))
                {
                    _articulos.Add(articulo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar los artículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComponentCatalog()
        {
            try
            {
                _catalogoComponentes = Articulo.Listar(_connectionFactory, string.Empty)
                    .Where(a => !a.EsKit)
                    .ToList();

                componentComboBox.DataSource = _catalogoComponentes;
                componentComboBox.DisplayMember = nameof(Articulo.Nombre);
                componentComboBox.ValueMember = nameof(Articulo.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo cargar el catálogo de componentes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            var articulo = BuildArticuloFromForm();

            if (Articulo.Agregar(_connectionFactory, articulo, out var message))
            {
                MessageBox.Show("Artículo agregado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadArticulos();
                LoadComponentCatalog();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (_selectedArticulo == null)
            {
                MessageBox.Show("Seleccione un artículo", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm())
            {
                return;
            }

            var articuloActualizado = BuildArticuloFromForm();
            articuloActualizado.Id = _selectedArticulo.Id;

            if (Articulo.Actualizar(_connectionFactory, articuloActualizado, out var message))
            {
                MessageBox.Show("Artículo actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadArticulos(searchTextBox.Text.Trim());
                LoadComponentCatalog();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (_selectedArticulo == null)
            {
                MessageBox.Show("Seleccione un artículo", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea inactivar este artículo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Articulo.Eliminar(_connectionFactory, _selectedArticulo.Id, out var message))
                {
                    MessageBox.Show("Artículo inactivado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadArticulos(searchTextBox.Text.Trim());
                    LoadComponentCatalog();
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Articulo BuildArticuloFromForm()
        {
            var articulo = new Articulo
            {
                Nombre = nameTextBox.Text.Trim(),
                NombreCorto = shortNameTextBox.Text.Trim(),
                TipoArticulo = typeComboBox.SelectedItem?.ToString() ?? "normal",
                UnidadMedida = unitMeasureTextBox.Text.Trim(),
                UnidadControl = unitControlTextBox.Text.Trim(),
                ContenidoControl = ParseDecimal(contentControlTextBox.Text.Trim()),
                Precio = ParseDecimal(priceTextBox.Text.Trim()),
                FechaPrecio = priceDatePicker.Value.Date,
                UsuarioPrecioId = int.TryParse(usuarioPrecioTextBox.Text.Trim(), out var usuarioId) ? usuarioId : (int?)null,
                Estatus = statusComboBox.SelectedItem?.ToString() ?? "Activo",
                TieneInventario = inventoryCheckBox.Checked
            };

            if (articulo.EsKit)
            {
                articulo.Componentes = _componentes.Select(c => new KitDetalle
                {
                    KitId = articulo.Id,
                    ArticuloId = c.ArticuloId,
                    Cantidad = c.Cantidad
                }).ToList();
            }

            return articulo;
        }

        private decimal ParseDecimal(string value)
        {
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
            {
                return result;
            }

            return 0;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Ingrese el nombre del artículo", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(priceTextBox.Text.Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out _) &&
                !decimal.TryParse(priceTextBox.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("Ingrese un precio válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.Equals(typeComboBox.SelectedItem?.ToString(), "kit", StringComparison.OrdinalIgnoreCase) && _componentes.Count == 0)
            {
                MessageBox.Show("Agregue al menos un componente al kit", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void articlesGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (articlesGrid.CurrentRow?.DataBoundItem is Articulo articulo)
            {
                _selectedArticulo = articulo;
                nameTextBox.Text = articulo.Nombre;
                shortNameTextBox.Text = articulo.NombreCorto;
                typeComboBox.SelectedItem = articulo.TipoArticulo;
                unitMeasureTextBox.Text = articulo.UnidadMedida;
                unitControlTextBox.Text = articulo.UnidadControl;
                contentControlTextBox.Text = articulo.ContenidoControl.ToString(CultureInfo.CurrentCulture);
                priceTextBox.Text = articulo.Precio.ToString(CultureInfo.CurrentCulture);
                priceDatePicker.Value = articulo.FechaPrecio;
                usuarioPrecioTextBox.Text = articulo.UsuarioPrecioId?.ToString() ?? string.Empty;
                statusComboBox.SelectedItem = articulo.Estatus;
                inventoryCheckBox.Checked = articulo.TieneInventario;

                _componentes.Clear();
                if (articulo.EsKit && articulo.Componentes != null)
                {
                    foreach (var componente in articulo.Componentes)
                    {
                        var detalle = new KitDetalle
                        {
                            KitId = articulo.Id,
                            ArticuloId = componente.ArticuloId,
                            Cantidad = componente.Cantidad,
                            Articulo = componente.Articulo ?? _catalogoComponentes.FirstOrDefault(a => a.Id == componente.ArticuloId)
                        };
                        _componentes.Add(detalle);
                    }
                }

                ToggleKitControls();
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            nameTextBox.Clear();
            shortNameTextBox.Clear();
            typeComboBox.SelectedIndex = 0;
            unitMeasureTextBox.Clear();
            unitControlTextBox.Clear();
            contentControlTextBox.Clear();
            priceTextBox.Clear();
            priceDatePicker.Value = DateTime.Today;
            usuarioPrecioTextBox.Clear();
            statusComboBox.SelectedIndex = 0;
            inventoryCheckBox.Checked = false;
            _componentes.Clear();
            articlesGrid.ClearSelection();
            _selectedArticulo = null;
            ToggleKitControls();
        }

        private void addComponentButton_Click(object sender, EventArgs e)
        {
            if (!string.Equals(typeComboBox.SelectedItem?.ToString(), "kit", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Solo los kits pueden contener componentes", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(componentComboBox.SelectedItem is Articulo articulo))
            {
                MessageBox.Show("Seleccione un artículo para el componente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(componentQuantityTextBox.Text.Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out var cantidad) &&
                !decimal.TryParse(componentQuantityTextBox.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out cantidad))
            {
                MessageBox.Show("Ingrese una cantidad válida", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_componentes.Any(c => c.ArticuloId == articulo.Id))
            {
                MessageBox.Show("El componente ya ha sido agregado", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _componentes.Add(new KitDetalle
            {
                ArticuloId = articulo.Id,
                Cantidad = cantidad,
                Articulo = articulo
            });

            componentQuantityTextBox.Clear();
        }

        private void removeComponentButton_Click(object sender, EventArgs e)
        {
            if (componentsGrid.CurrentRow?.DataBoundItem is KitDetalle detalle)
            {
                _componentes.Remove(detalle);
            }
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleKitControls();
        }

        private void ToggleKitControls()
        {
            var esKit = string.Equals(typeComboBox.SelectedItem?.ToString(), "kit", StringComparison.OrdinalIgnoreCase);
            componentComboBox.Enabled = esKit;
            componentQuantityTextBox.Enabled = esKit;
            addComponentButton.Enabled = esKit;
            removeComponentButton.Enabled = esKit;
            componentsGrid.Enabled = esKit;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            LoadArticulos(searchTextBox.Text.Trim());
        }
    }
}
