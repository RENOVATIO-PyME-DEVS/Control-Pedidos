using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using Control_Pedidos.Models;
using Control_Pedidos.Printing;
using MySql.Data.MySqlClient;

namespace Control_Pedidos.Views.CorteCaja
{
    /// <summary>
    /// Formulario principal del módulo "Corte de Caja". Permite consultar los movimientos
    /// del día con base en el procedimiento almacenado SP_CorteCaja y ofrece la opción de
    /// imprimir o generar un PDF con el resumen.
    /// </summary>
    public partial class CorteCajaForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;
        private readonly CorteCajaDao _corteCajaDao;
        private readonly CorteCajaPrintingService _printingService;
        private readonly bool _esAdministrador;
        private readonly int _empresaActualId;
        private readonly string _empresaActualNombre;
        private readonly int _usuarioActualId;
        private readonly string _usuarioActualNombre;

        private readonly Dictionary<int, EmpresaItem> _empresas = new Dictionary<int, EmpresaItem>();
        private readonly Dictionary<int, UsuarioItem> _usuarios = new Dictionary<int, UsuarioItem>();
        private DataTable _resultadoActual;

        public CorteCajaForm(DatabaseConnectionFactory connectionFactory, bool esAdministrador, int empresaId, string empresaNombre, int usuarioId, string usuarioNombre)
        {
            InitializeComponent();
            UIStyles.ApplyTheme(this);

            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _corteCajaDao = new CorteCajaDao(connectionFactory);
            _printingService = new CorteCajaPrintingService();
            _esAdministrador = esAdministrador;
            _empresaActualId = empresaId;
            _empresaActualNombre = empresaNombre ?? string.Empty;
            _usuarioActualId = usuarioId;
            _usuarioActualNombre = usuarioNombre ?? string.Empty;

            ConfigurarGrid();
        }

        /// <summary>
        /// Configura el DataGridView con estilos básicos para que los resultados del SP se lean mejor.
        /// </summary>
        private void ConfigurarGrid()
        {
            resultDataGridView.AutoGenerateColumns = true;
            resultDataGridView.AllowUserToAddRows = false;
            resultDataGridView.AllowUserToDeleteRows = false;
            resultDataGridView.MultiSelect = false;
            resultDataGridView.ReadOnly = true;
            resultDataGridView.RowHeadersVisible = false;
            resultDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            resultDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            resultDataGridView.EnableHeadersVisualStyles = false;
            resultDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(resultDataGridView.Font, FontStyle.Bold);
            resultDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235);
            resultDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);

            try
            {
                // Activar DoubleBuffered reduce el parpadeo cuando se asigna un DataTable con muchas filas.
                typeof(DataGridView)
                    .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(resultDataGridView, true, null);
            }
            catch
            {
                // En algunas versiones de .NET este truco puede fallar, pero no es crítico.
            }
        }

        private void CorteCajaForm_Load(object sender, EventArgs e)
        {
            fechaDateTimePicker.Value = DateTime.Today;

            try
            {
                CargarEmpresas();
                CargarUsuarios();
                AplicarPermisos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"No se pudieron cargar los catálogos iniciales: {ex.Message}", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Consulta la tabla de empresas. Los administradores reciben el catálogo completo, mientras
        /// que los usuarios normales obtienen únicamente la empresa asociada a su sesión.
        /// </summary>
        private void CargarEmpresas()
        {
            _empresas.Clear();

            const string queryAdmin = @"SELECT empresa_id, nombre, IFNULL(rfc, '') AS rfc, IFNULL(domicilio, '') AS domicilio
                                        FROM banquetes.empresas
                                        ORDER BY nombre;";

            const string queryUsuario = @"SELECT e.empresa_id, e.nombre, IFNULL(e.rfc, '') AS rfc, IFNULL(e.domicilio, '') AS domicilio
                                           FROM banquetes.usuarios_empresas ue
                                           INNER JOIN banquetes.empresas e ON e.empresa_id = ue.empresa_id
                                           WHERE ue.usuario_id = @usuarioId
                                             AND (ue.estatus IS NULL OR ue.estatus <> 'B')
                                           ORDER BY e.nombre;";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(_esAdministrador ? queryAdmin : queryUsuario, connection))
            {
                if (!_esAdministrador)
                {
                    command.Parameters.AddWithValue("@usuarioId", _usuarioActualId);
                }

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var empresaId = reader.GetInt32("empresa_id");
                        var empresa = new EmpresaItem
                        {
                            Id = empresaId,
                            Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                            Rfc = reader.IsDBNull(reader.GetOrdinal("rfc")) ? string.Empty : reader.GetString("rfc"),
                            Direccion = reader.IsDBNull(reader.GetOrdinal("domicilio")) ? string.Empty : reader.GetString("domicilio"),
                            LogoPath = BuscarLogoEmpresa(empresaId)
                        };

                        _empresas[empresa.Id] = empresa;
                    }
                }
            }

            // Si el usuario no es administrador y por alguna razón no se devolvió la empresa, usamos la del login.
            if (!_esAdministrador && _empresaActualId > 0 && !_empresas.ContainsKey(_empresaActualId))
            {
                _empresas[_empresaActualId] = new EmpresaItem
                {
                    Id = _empresaActualId,
                    Nombre = _empresaActualNombre,
                    Rfc = string.Empty,
                    Direccion = string.Empty,
                    LogoPath = BuscarLogoEmpresa(_empresaActualId)
                };
            }

            var listaEmpresas = _empresas.Values
                .OrderBy(e => e.Nombre, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            empresaComboBox.DataSource = listaEmpresas;
            empresaComboBox.DisplayMember = nameof(EmpresaItem.Nombre);
            empresaComboBox.ValueMember = nameof(EmpresaItem.Id);

            if (_empresas.ContainsKey(_empresaActualId))
            {
                empresaComboBox.SelectedValue = _empresaActualId;
            }
            else if (empresaComboBox.Items.Count > 0)
            {
                empresaComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Carga la lista de usuarios disponibles. Los administradores pueden elegir cualquier usuario,
        /// mientras que los usuarios normales solo ven su propio registro.
        /// </summary>
        private void CargarUsuarios()
        {
            _usuarios.Clear();

            const string queryAdmin = @"SELECT usuario_id, nombre, IFNULL(correo, '') AS correo
                                         FROM banquetes.usuarios
                                         WHERE (estatus IS NULL OR estatus <> 'B')
                                         ORDER BY nombre;";

            const string queryUsuario = @"SELECT usuario_id, nombre, IFNULL(correo, '') AS correo
                                            FROM banquetes.usuarios
                                            WHERE usuario_id = @usuarioId
                                            LIMIT 1;";

            using (var connection = _connectionFactory.Create())
            using (var command = new MySqlCommand(_esAdministrador ? queryAdmin : queryUsuario, connection))
            {
                if (!_esAdministrador)
                {
                    command.Parameters.AddWithValue("@usuarioId", _usuarioActualId);
                }

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuarioId = reader.GetInt32("usuario_id");
                        var usuario = new UsuarioItem
                        {
                            Id = usuarioId,
                            Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString("nombre"),
                            Correo = reader.IsDBNull(reader.GetOrdinal("correo")) ? string.Empty : reader.GetString("correo")
                        };

                        _usuarios[usuario.Id] = usuario;
                    }
                }
            }

            // Si por alguna razón no encontramos al usuario (por ejemplo, parseo fallido del ID), lo agregamos manualmente.
            if (_usuarioActualId > 0 && !_usuarios.ContainsKey(_usuarioActualId))
            {
                _usuarios[_usuarioActualId] = new UsuarioItem
                {
                    Id = _usuarioActualId,
                    Nombre = _usuarioActualNombre,
                    Correo = string.Empty
                };
            }

            var listaUsuarios = _usuarios.Values
                .OrderBy(u => u.Nombre, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            usuarioComboBox.DataSource = listaUsuarios;
            usuarioComboBox.DisplayMember = nameof(UsuarioItem.Nombre);
            usuarioComboBox.ValueMember = nameof(UsuarioItem.Id);

            if (_usuarios.ContainsKey(_usuarioActualId))
            {
                usuarioComboBox.SelectedValue = _usuarioActualId;
            }
            else if (usuarioComboBox.Items.Count > 0)
            {
                usuarioComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Aplica las restricciones de acuerdo al rol del usuario loggeado.
        /// </summary>
        private void AplicarPermisos()
        {
            empresaComboBox.Enabled = _esAdministrador;
            usuarioComboBox.Enabled = _esAdministrador;
            fechaDateTimePicker.Enabled = _esAdministrador;

            if (!_esAdministrador)
            {
                fechaDateTimePicker.Value = DateTime.Today;
            }
        }

        private void consultarButton_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros())
            {
                return;
            }

            var empresa = empresaComboBox.SelectedItem as EmpresaItem;
            var usuario = usuarioComboBox.SelectedItem as UsuarioItem;
            if (empresa == null || usuario == null)
            {
                MessageBox.Show(this, "Seleccione una empresa y un usuario válidos.", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var fecha = fechaDateTimePicker.Value.Date;
                var tabla = _corteCajaDao.EjecutarCorte(empresa.Id, usuario.Id, fecha);

                _resultadoActual = tabla;
                resultDataGridView.DataSource = tabla;

                if (tabla == null || tabla.Rows.Count == 0)
                {
                    imprimirButton.Enabled = false;
                    MessageBox.Show(this, "No existen movimientos para la fecha seleccionada.", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    imprimirButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                imprimirButton.Enabled = false;
                MessageBox.Show(this, $"Ocurrió un error al ejecutar el corte: {ex.Message}", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool ValidarFiltros()
        {
            if (_esAdministrador && empresaComboBox.SelectedItem == null)
            {
                MessageBox.Show(this, "Seleccione una empresa antes de continuar.", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                empresaComboBox.Focus();
                return false;
            }

            if (_esAdministrador && usuarioComboBox.SelectedItem == null)
            {
                MessageBox.Show(this, "Seleccione un usuario antes de continuar.", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usuarioComboBox.Focus();
                return false;
            }

            return true;
        }

        private void imprimirButton_Click(object sender, EventArgs e)
        {
            if (_resultadoActual == null || _resultadoActual.Rows.Count == 0)
            {
                MessageBox.Show(this, "No hay información para imprimir. Ejecute primero la consulta.", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var empresa = empresaComboBox.SelectedItem as EmpresaItem;
            var usuario = usuarioComboBox.SelectedItem as UsuarioItem;
            if (empresa == null || usuario == null)
            {
                MessageBox.Show(this, "Seleccione una empresa y un usuario válidos antes de imprimir.", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var reporte = new CorteCajaReporte
            {
                EmpresaId = empresa.Id,
                EmpresaNombre = empresa.Nombre,
                EmpresaDireccion = empresa.Direccion,
                EmpresaRfc = empresa.Rfc,
                UsuarioNombre = usuario.Nombre,
                FechaCorte = fechaDateTimePicker.Value.Date,
                Movimientos = _resultadoActual.Copy(),
                LogoPath = empresa.LogoPath
            };

            var resultadoImpresion = _printingService.Print(reporte, this);

            if (resultadoImpresion.Printed)
            {
                MessageBox.Show(this, "El corte de caja se envió a la impresora correctamente.", "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var mensaje = "No se pudo imprimir el corte de caja. Se generó un PDF con la información.";

            if (!string.IsNullOrWhiteSpace(resultadoImpresion.PdfPath))
            {
                mensaje += $"\nRuta del archivo: {resultadoImpresion.PdfPath}";
            }

            if (resultadoImpresion.PrintError != null)
            {
                mensaje += $"\nImpresión: {resultadoImpresion.PrintError.Message}";
            }

            if (resultadoImpresion.PdfError != null)
            {
                mensaje += $"\nPDF: {resultadoImpresion.PdfError.Message}";
            }

            MessageBox.Show(this, mensaje, "Corte de caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void resultDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_resultadoActual == null)
            {
                return;
            }

            foreach (DataGridViewColumn column in resultDataGridView.Columns)
            {
                column.HeaderText = FormatearEncabezado(column.HeaderText);

                if (column.Index < _resultadoActual.Columns.Count)
                {
                    var dataColumn = _resultadoActual.Columns[column.Index];
                    if (EsColumnaNumerica(dataColumn))
                    {
                        column.DefaultCellStyle.Format = "N2";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                }
            }
        }

        private static string FormatearEncabezado(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return string.Empty;
            }

            // Reemplaza guiones bajos y aplica capitalización tipo título para mejorar la lectura.
            var limpio = texto.Replace('_', ' ');
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(limpio.ToLowerInvariant());
        }

        private static bool EsColumnaNumerica(DataColumn column)
        {
            if (column == null)
            {
                return false;
            }

            var type = column.DataType;
            return type == typeof(decimal) || type == typeof(double) || type == typeof(float) || type == typeof(int) || type == typeof(long);
        }

        /// <summary>
        /// Busca un posible logo en disco con base en el identificador de la empresa.
        /// </summary>
        private static string BuscarLogoEmpresa(int empresaId)
        {
            try
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var logosDirectory = Path.Combine(baseDirectory, "Resources", "LogosEmpresas");

                if (!Directory.Exists(logosDirectory))
                {
                    return string.Empty;
                }

                var candidatos = new[]
                {
                    $"logo_{empresaId}.png",
                    $"logo_{empresaId}.jpg",
                    $"logo_{empresaId}.jpeg",
                    $"empresa_{empresaId}.png",
                    $"empresa_{empresaId}.jpg",
                    $"empresa_{empresaId}.jpeg"
                };

                foreach (var candidato in candidatos)
                {
                    var ruta = Path.Combine(logosDirectory, candidato);
                    if (File.Exists(ruta))
                    {
                        return ruta;
                    }
                }

                var coincidencia = Directory.EnumerateFiles(logosDirectory)
                    .FirstOrDefault(path => Path.GetFileNameWithoutExtension(path)
                        .Equals($"empresa_{empresaId}", StringComparison.OrdinalIgnoreCase));

                return coincidencia ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private class EmpresaItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Rfc { get; set; }
            public string Direccion { get; set; }
            public string LogoPath { get; set; }

            public override string ToString() => Nombre;
        }

        private class UsuarioItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Correo { get; set; }

            public override string ToString() => Nombre;
        }
    }
}
