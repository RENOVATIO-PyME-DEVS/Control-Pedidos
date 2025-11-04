using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

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

        private readonly string _userId;

        public ArticleManagementForm(DatabaseConnectionFactory connectionFactory, string usernameid)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            _userId = usernameid;

            typeComboBox.DataSource = new[] { "Normal", "Produccion", "Kit" };
            statusComboBox.DataSource = new[] { "Activo", "Inactivo" };

            priceDatePicker.Value = DateTime.Today;

            ConfigureGrids();
            LoadArticulos();
            LoadComponentCatalog();
            ToggleKitControls();
        }

        private void ConfigureGrids()
        {
            // Configuramos la grilla de artículos con columnas amigables.
            _articlesSource.DataSource = _articulos;
            articlesGrid.AutoGenerateColumns = false;
            articlesGrid.Columns.Clear();

            articlesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Articulo.NombreCorto),
                HeaderText = "Clave",
                Width = 80
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

            // Y la grilla de componentes para armar kits.
            _componentsSource.DataSource = _componentes;
            componentsGrid.AutoGenerateColumns = false;
            componentsGrid.Columns.Clear();

            componentsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(KitDetalle.NombreArticulo),
                HeaderText = "Artículo Kit",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            componentsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(KitDetalle.Cantidad),
                HeaderText = "Cantidad",
                Width = 100
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

                countArticlesLabel.Text = $"Total articulos: {articlesGrid.Rows.Count}";
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
                // Cargamos opciones de unidades reutilizando datos existentes para evitar escribir manualmente.
                CargarUnidadesComboBox(unitMeasureComboBox, "unidad_medida");
                CargarUnidadesComboBox(unitControlComboBox, "unidad_control");

                // 🔹 Guardamos los artículos base para referencia (esta lista se usa en addComponentButton_Click)
                _catalogoComponentes = Articulo.Listar(_connectionFactory, string.Empty)
                    .Where(a => a.Estatus == "N" && (a.TipoArticulo == "N" || a.TipoArticulo == "P"))
                    .OrderBy(a => a.NombreCorto)
                    .ToList();

                // Para mostrar en el ComboBox con formato "<clave> - <nombre>"
                var articulosVisual = _catalogoComponentes
                    .Select(a => new
                    {
                        a.Id,
                        NombreCompleto = $"{a.NombreCorto} - {a.Nombre}"
                    })
                    .OrderBy(a => a.NombreCompleto)
                    .ToList();

                componentComboBox.DataSource = articulosVisual;
                componentComboBox.DisplayMember = "NombreCompleto";
                componentComboBox.ValueMember = "Id";
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
            // Normalizamos el estatus a los códigos que maneja la base (N/B).
            string estatusSeleccionado = statusComboBox.SelectedItem?.ToString() ?? "Activo";

            string tipoSeleccionado = typeComboBox.SelectedItem?.ToString()?.Trim() ?? string.Empty;
            string tipoArticulo;

            if (tipoSeleccionado.Equals("Produccion", StringComparison.OrdinalIgnoreCase))
                tipoArticulo = "P";
            else if (tipoSeleccionado.Equals("Kit", StringComparison.OrdinalIgnoreCase))
                tipoArticulo = "K";
            else
                tipoArticulo = "N";

            var articulo = new Articulo
            {
                Nombre = nameTextBox.Text.Trim(),
                NombreCorto = shortNameTextBox.Text.Trim(),
                TipoArticulo = tipoArticulo,
                UnidadMedida = unitMeasureComboBox.SelectedItem?.ToString() == "Otra..."
                        ? customUnitMeasureTextBox.Text
                        : unitMeasureComboBox.SelectedItem?.ToString(),
                UnidadControl = unitControlComboBox.SelectedItem?.ToString() == "Otra..."
                        ? customUnitControlTextBox.Text
                        : unitControlComboBox.SelectedItem?.ToString(),
                ContenidoControl = ParseDecimal(contentControlTextBox.Text.Trim()),
                Precio = ParseDecimal(priceTextBox.Text.Trim()),
                FechaPrecio = priceDatePicker.Value.Date,
                UsuarioPrecioId = int.TryParse(_userId, out var usuarioId) ? usuarioId : (int?)null,
                Estatus = estatusSeleccionado.Equals("Activo", StringComparison.OrdinalIgnoreCase) ? "N" : estatusSeleccionado.Equals("Inactivo", StringComparison.OrdinalIgnoreCase) ? "B" : "N",
                Personas = int.TryParse(personsTextBox.Text.ToString(), out var intPersonas) ? intPersonas : (int?)null
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
                // Reflejamos el artículo seleccionado en el formulario para edición.
                _selectedArticulo = articulo;
                nameTextBox.Text = articulo.Nombre;
                shortNameTextBox.Text = articulo.NombreCorto;

                switch (articulo.TipoArticulo)
                {
                    case "P":
                        typeComboBox.SelectedItem = "Produccion";
                        break;
                    case "K":
                        typeComboBox.SelectedItem = "Kit";
                        break;
                    default:
                        typeComboBox.SelectedItem = "Normal";
                        break;
                }

                unitMeasureComboBox.Text = articulo.UnidadMedida;
                unitControlComboBox.Text = articulo.UnidadControl;
                contentControlTextBox.Text = articulo.ContenidoControl.ToString(CultureInfo.CurrentCulture);
                priceTextBox.Text = articulo.Precio.ToString(CultureInfo.CurrentCulture);
                priceDatePicker.Value = articulo.FechaPrecio;
                personsTextBox.Text = articulo.Personas.ToString();

                switch (articulo.Estatus)
                {
                    case "N":
                    case "P":
                        statusComboBox.SelectedItem = "Activo";
                        deleteButton.Enabled = true;
                        break;

                    case "B":
                        statusComboBox.SelectedItem = "Inactivo";
                        deleteButton.Enabled = false;
                        break;

                    default:
                        statusComboBox.SelectedItem = "Activo";
                        deleteButton.Enabled = true;
                        break;
                }

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
            // Reseteamos todos los campos y estados a sus valores iniciales.
            nameTextBox.Clear();
            shortNameTextBox.Clear();
            typeComboBox.SelectedIndex = 0;

            unitMeasureComboBox.SelectedIndex = -1;
            customUnitMeasureTextBox.Clear();
            customUnitMeasureTextBox.Visible = false;

            unitControlComboBox.SelectedIndex = -1;
            customUnitControlTextBox.Clear();
            customUnitControlTextBox.Visible = false;

            contentControlTextBox.Clear();
            priceTextBox.Clear();
            priceDatePicker.Value = DateTime.Today;
            statusComboBox.SelectedIndex = 0;
            personsTextBox.Clear();
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

            if (componentComboBox.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un artículo para el componente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int articuloId = Convert.ToInt32(componentComboBox.SelectedValue);
            var articulo = _catalogoComponentes.FirstOrDefault(a => a.Id == articuloId);

            if (articulo == null)
            {
                MessageBox.Show("No se encontró el artículo en el catálogo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Habilitamos o no la sección de componentes dependiendo del tipo seleccionado.
            var esKit = string.Equals(typeComboBox.SelectedItem?.ToString(), "kit", StringComparison.OrdinalIgnoreCase);

            panelKit.Visible = esKit;

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

        private void ArticleManagementForm_Load(object sender, EventArgs e)
        {
        }

        private void CargarUnidadesComboBox(ComboBox comboBox, string columna)
        {
            comboBox.Items.Clear();

            try
            {
                using (var connection = _connectionFactory.Create())
                {
                    connection.Open();

                    string query = $"SELECT DISTINCT {columna} FROM articulos WHERE {columna} IS NOT NULL AND {columna} <> '' ORDER BY {columna}";
                    using (var cmd = new MySqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox.Items.Add(reader[columna].ToString());
                        }
                    }
                }

                comboBox.Items.Add("Otra...");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar {columna}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void unitMeasureComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            customUnitMeasureTextBox.Visible = (unitMeasureComboBox.SelectedItem?.ToString() == "Otra...");
            if (!customUnitMeasureTextBox.Visible) customUnitMeasureTextBox.Text = string.Empty;
        }

        private void unitControlComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            customUnitControlTextBox.Visible = (unitControlComboBox.SelectedItem?.ToString() == "Otra...");
            if (!customUnitControlTextBox.Visible) customUnitControlTextBox.Text = string.Empty;
        }

        private void personsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite tecla de retroceso (Backspace)
            if (char.IsControl(e.KeyChar))
                return;

            // Solo permite números
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void priceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir control (Backspace, Supr, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Permitir dígitos
            if (char.IsDigit(e.KeyChar))
                return;

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && !((TextBox)sender).Text.Contains('.'))
                return;

            // Bloquear todo lo demás
            e.Handled = true;
        }

        private void priceTextBox_Leave(object sender, EventArgs e)
        {
            string input = priceTextBox.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                priceTextBox.Text = "0.00";
                return;
            }

            // Intenta convertir a decimal y formatear con dos decimales.
            if (decimal.TryParse(input, out decimal value))
            {
                priceTextBox.Text = value.ToString("0.00");
            }
            else
            {
                priceTextBox.Text = "0.00";
            }
        }

        private void contentControlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite tecla de retroceso (Backspace)
            if (char.IsControl(e.KeyChar))
                return;

            // Solo permite números
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
