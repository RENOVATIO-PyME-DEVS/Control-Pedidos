using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Views.Events
{
    public partial class EventManagementForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly BindingList<Evento> _eventos = new BindingList<Evento>();
        private readonly BindingSource _bindingSource = new BindingSource();
        private readonly List<Empresa> _empresas = new List<Empresa>();
        private Evento _selectedEvento;
        private bool _isLoadingEventos;

        public EventManagementForm(DatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            InitializeComponent();
            UIStyles.ApplyTheme(this);

            ConfigureGrid();
            LoadEmpresas();
            LoadEventos();
            ResetControlsState();
        }

        private void ConfigureGrid()
        {
            _bindingSource.DataSource = _eventos;
            eventosGrid.AutoGenerateColumns = false;
            eventosGrid.Columns.Clear();

            eventosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Evento.Nombre),
                HeaderText = "Evento",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 160
            });

            eventosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Evento.EmpresaNombre),
                HeaderText = "Empresa",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 130
            });

            eventosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Evento.FechaEvento),
                HeaderText = "Fecha",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "d" }
            });

            eventosGrid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = nameof(Evento.TieneSerie),
                HeaderText = "Serie",
                Width = 60
            });

            eventosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Evento.Serie),
                HeaderText = "Prefijo",
                Width = 90
            });

            eventosGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Evento.SiguienteFolio),
                HeaderText = "Siguiente folio",
                Width = 110
            });

            eventosGrid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = nameof(Evento.TienePedidos),
                HeaderText = "Tiene pedidos",
                Width = 110
            });

            eventosGrid.DataSource = _bindingSource;
        }

        private void LoadEmpresas()
        {
            _empresas.Clear();
            try
            {
                const string query = @"SELECT empresa_id, nombre
                                       FROM banquetes.empresas
                                      WHERE estatus IS NULL OR estatus = 'N'
                                      ORDER BY nombre;";

                using (var connection = _connectionFactory.Create())
                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _empresas.Add(new Empresa
                            {
                                Id = reader.GetInt32("empresa_id"),
                                Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre")
                            });
                        }
                    }
                }

                BindEmpresas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudieron cargar las empresas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEventos(string filtro = "")
        {
            try
            {
                _isLoadingEventos = true;
                _eventos.Clear();
                foreach (var evento in Evento.Listar(_connectionFactory, filtro))
                {
                    _eventos.Add(evento);
                }
                _selectedEvento = null;
                if (eventosGrid.Rows.Count > 0)
                {
                    eventosGrid.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo obtener la lista de eventos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingEventos = false;
            }
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            var evento = new Evento
            {
                Nombre = nombreTextBox.Text.Trim(),
                EmpresaId = GetSelectedEmpresaId(),
                EmpresaNombre = GetSelectedEmpresaNombre(),
                FechaEvento = fechaDateTimePicker.Value.Date,
                TieneSerie = tieneSerieCheckBox.Checked,
                Serie = tieneSerieCheckBox.Checked ? serieTextBox.Text.Trim() : string.Empty,
                SiguienteFolio = (int)siguienteFolioNumericUpDown.Value,
                TienePedidos = false
            };

            if (Evento.Agregar(_connectionFactory, evento, out var message))
            {
                MessageBox.Show("Evento agregado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEventos();
                ClearForm();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizarButton_Click(object sender, EventArgs e)
        {
            if (_selectedEvento == null)
            {
                MessageBox.Show("Seleccione un evento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedEvento.TienePedidos)
            {
                MessageBox.Show("El evento está relacionado con pedidos y no puede modificarse.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm())
            {
                return;
            }

            _selectedEvento.Nombre = nombreTextBox.Text.Trim();
            _selectedEvento.EmpresaId = GetSelectedEmpresaId();
            _selectedEvento.EmpresaNombre = GetSelectedEmpresaNombre();
            _selectedEvento.FechaEvento = fechaDateTimePicker.Value.Date;
            _selectedEvento.TieneSerie = tieneSerieCheckBox.Checked;
            _selectedEvento.Serie = tieneSerieCheckBox.Checked ? serieTextBox.Text.Trim() : string.Empty;
            _selectedEvento.SiguienteFolio = (int)siguienteFolioNumericUpDown.Value;

            if (Evento.Actualizar(_connectionFactory, _selectedEvento, out var message))
            {
                MessageBox.Show("Evento actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEventos();
                ClearForm();
            }
            else
            {
                MessageBox.Show(message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void limpiarButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void eventosGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (_isLoadingEventos || eventosGrid.CurrentRow == null)
            {
                return;
            }

            if (eventosGrid.CurrentRow.DataBoundItem is Evento evento)
            {
                _selectedEvento = evento;
                PopulateFormFromEvento(evento);
            }
        }

        private void PopulateFormFromEvento(Evento evento)
        {
            nombreTextBox.Text = evento.Nombre;
            fechaDateTimePicker.Value = evento.FechaEvento;
            tieneSerieCheckBox.Checked = evento.TieneSerie;
            serieTextBox.Text = evento.Serie;
            siguienteFolioNumericUpDown.Value = Math.Max(siguienteFolioNumericUpDown.Minimum, evento.SiguienteFolio);

            if (_empresas.All(e => e.Id != evento.EmpresaId))
            {
                _empresas.Add(new Empresa { Id = evento.EmpresaId, Nombre = evento.EmpresaNombre });
                BindEmpresas(evento.EmpresaId);
            }
            else
            {
                BindEmpresas(evento.EmpresaId);
            }

            ApplyEditingLock(evento.TienePedidos);
        }

        private void ApplyEditingLock(bool hasOrders)
        {
            var enabled = !hasOrders;

            nombreTextBox.Enabled = enabled;
            empresaComboBox.Enabled = enabled;
            fechaDateTimePicker.Enabled = enabled;
            tieneSerieCheckBox.Enabled = enabled;
            serieTextBox.Enabled = enabled && tieneSerieCheckBox.Checked;
            siguienteFolioNumericUpDown.Enabled = enabled;
            actualizarButton.Enabled = enabled && _selectedEvento != null;
        }

        private void ResetControlsState()
        {
            nombreTextBox.Enabled = true;
            empresaComboBox.Enabled = true;
            fechaDateTimePicker.Enabled = true;
            tieneSerieCheckBox.Enabled = true;
            serieTextBox.Enabled = tieneSerieCheckBox.Checked;
            siguienteFolioNumericUpDown.Enabled = true;
            actualizarButton.Enabled = false;
        }

        private void ClearForm()
        {
            eventosGrid.ClearSelection();
            _selectedEvento = null;

            nombreTextBox.Text = string.Empty;
            if (empresaComboBox.Items.Count > 0)
            {
                empresaComboBox.SelectedIndex = 0;
            }
            fechaDateTimePicker.Value = DateTime.Today;
            tieneSerieCheckBox.Checked = false;
            serieTextBox.Text = string.Empty;
            siguienteFolioNumericUpDown.Value = 1;

            ResetControlsState();
            nombreTextBox.Focus();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(nombreTextBox.Text))
            {
                MessageBox.Show("El nombre del evento es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nombreTextBox.Focus();
                return false;
            }

            if (empresaComboBox.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una empresa.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                empresaComboBox.Focus();
                return false;
            }

            if (tieneSerieCheckBox.Checked && string.IsNullOrWhiteSpace(serieTextBox.Text))
            {
                MessageBox.Show("Debe capturar la serie del evento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                serieTextBox.Focus();
                return false;
            }

            if (siguienteFolioNumericUpDown.Value < 1)
            {
                MessageBox.Show("El siguiente folio debe ser mayor o igual a 1.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                siguienteFolioNumericUpDown.Focus();
                return false;
            }

            return true;
        }

        private int GetSelectedEmpresaId()
        {
            return empresaComboBox.SelectedItem is Empresa empresa ? empresa.Id : 0;
        }

        private string GetSelectedEmpresaNombre()
        {
            return empresaComboBox.SelectedItem is Empresa empresa ? empresa.Nombre : string.Empty;
        }

        private void tieneSerieCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!tieneSerieCheckBox.Enabled)
            {
                return;
            }

            if (!tieneSerieCheckBox.Checked)
            {
                serieTextBox.Text = string.Empty;
            }

            serieTextBox.Enabled = tieneSerieCheckBox.Checked && (_selectedEvento == null || !_selectedEvento.TienePedidos);
        }

        private void BindEmpresas(int? selectedEmpresaId = null)
        {
            var ordered = _empresas
                .OrderBy(e => e.Nombre ?? string.Empty, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            empresaComboBox.DataSource = null;
            empresaComboBox.DisplayMember = nameof(Empresa.Nombre);
            empresaComboBox.ValueMember = nameof(Empresa.Id);
            empresaComboBox.DataSource = ordered;

            if (selectedEmpresaId.HasValue && ordered.Any(e => e.Id == selectedEmpresaId.Value))
            {
                empresaComboBox.SelectedValue = selectedEmpresaId.Value;
            }
        }
    }
}
