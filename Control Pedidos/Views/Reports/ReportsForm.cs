using Control_Pedidos.Data;
using Control_Pedidos.Helpers;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_Pedidos.Views.Reports
{
    public partial class ReportsForm : Form
    {
        private readonly DatabaseConnectionFactory _connectionFactory;


        DataTable dtER = new DataTable();

        DataTable _dtReportes = new DataTable();   // reportes disponibles
        DataTable _dtResultado = new DataTable();  // resultados del SP

        DataTable listareportes = new DataTable();
        DataTable reporte = new DataTable();
        string _storedProcedure;
        string _parametros;
        string _rfcSeleccionado;
        string procedimiento;
        string variablesprocedimiento;

        public ReportsForm(DatabaseConnectionFactory connectionFactory)
        {
            InitializeComponent();
            //UIStyles.ApplyTheme(this);


            _connectionFactory = connectionFactory;

            InicializarGrids();
            CargarReportes();


        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {

        }

        private void InicializarGrids()
        {
            /*We remove the first column in datagridview*/
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersVisible = false;

            /*We set the columns to take the entire length of the datagridview*/
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            /*We remove the last row of the datagridview*/
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToAddRows = false;

            /*Read onluy*/
            this.dataGridView3.ReadOnly = true;

            /*We selected all row*/
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            /*We enabled copy*/
            //this.dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView2.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;



            /*We dont eduit*/
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            this.comboBox2.SelectedIndex = 0;

            //textBox2.Enabled = false;
            // textBox2.Text = "C:\\RENOVATIO\\BANQUETES - EXCEL";

            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string ruta = Path.Combine(escritorio, "Banquetes", "Reportes");

            // Crea la carpeta si no existe
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            textBox2.Text = ruta;

            dataGridView2.RowHeadersVisible = false;
            dataGridView3.RowHeadersVisible = false;

            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;

            dataGridView3.ReadOnly = true;

            dataGridView2.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
        }

        private void CargarReportes()
        {
            const string query = "SELECT * FROM banquetes.reportes_app_banquetes ORDER BY nombre ASC";

            using (var conn = _connectionFactory.Create())
            using (var cmd = new MySqlCommand(query, conn))
            using (var da = new MySqlDataAdapter(cmd))
            {
                _dtReportes.Clear();
                da.Fill(_dtReportes);

                dataGridView3.DataSource = _dtReportes;

                dataGridView3.Columns["nombre_sp"].Visible = false;
                dataGridView3.Columns["campos"].Visible = false;
                dataGridView3.Columns["arreglo_campos"].Visible = false;
                dataGridView3.Columns["estatus"].Visible = false;
                dataGridView3.Columns["reporte_app_banquetes_id"].Visible = false;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // fuera de filas

            // Selecciona fila
            dataGridView3.Rows[e.RowIndex].Selected = true;

            // Limpia el panel (dispose de todo)
            foreach (Control c in panel2.Controls.OfType<Control>().ToList())
            {
                panel2.Controls.Remove(c);
                c.Dispose();
            }

            // Lee JSON de la celda
            var row = dataGridView3.Rows[e.RowIndex];
            string json = Convert.ToString(row.Cells["arreglo_campos"].Value);
            procedimiento = Convert.ToString(row.Cells["nombre_sp"].Value);
            //RfcSeleccionado = Convert.ToString(row.Cells["nombre"].Value);

            if (string.IsNullOrWhiteSpace(json))
            {
                MessageBox.Show("La celda 'arreglo_campos' está vacía o nula.", "Aviso");
                return;
            }

            VariablesObject variablesObject;
            try
            {
                variablesObject = JsonConvert.DeserializeObject<VariablesObject>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("JSON inválido en 'arreglo_campos': " + ex.Message, "Error");
                return;
            }

            if (variablesObject?.Variables == null || variablesObject.Variables.Count == 0)
            {
                MessageBox.Show("No hay variables definidas en el JSON.", "Aviso");
                return;
            }

            // Layout básico
            int marginLeft = 10;
            int labelWidth = 150;
            int controlWidth = Math.Max(200, panel2.Width - (marginLeft + labelWidth + 30));
            int y = 12;
            int rowHeight = 32;
            int tabIndex = 0;

            foreach (var v in variablesObject.Variables)
            {
                string tipo = (v.tipo ?? "").Trim().ToLowerInvariant();
                string nombreSeguro = MakeSafeName(v.Nombre ?? "campo");
                string labelText = (v.Nombre ?? nombreSeguro) + ":";

                // Label
                var label = new Label
                {
                    Text = labelText,
                    AutoSize = false,
                    Width = labelWidth,
                    Height = 22,
                    Left = marginLeft,
                    Top = y + 4
                };
                panel2.Controls.Add(label);

                // Control según tipo
                Control inputControl = null;

                switch (tipo)
                {
                    case "combobox":
                        var cb = new ComboBox
                        {
                            Name = $"cmb_{nombreSeguro}",
                            Left = marginLeft + labelWidth + 10,
                            Top = y,
                            Width = controlWidth,
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            TabIndex = tabIndex++
                        };

                        // llena si hay queryInfo
                        if (!string.IsNullOrWhiteSpace(v.queryInfo))
                        {
                            try
                            {
                                using (var conn = _connectionFactory.Create())
                                using (var cmd = new MySqlCommand(v.queryInfo, conn))
                                {
                                    conn.Open();

                                    using (var reader = cmd.ExecuteReader())
                                    {
                                        var table = new DataTable();
                                        table.Load(reader);

                                        if (table.Rows.Count == 0)
                                        {
                                            MessageBox.Show(
                                                $"La consulta para {v.Nombre} no devolvió datos.",
                                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information
                                            );
                                            return;
                                        }

                                        cb.DataSource = table;
                                        cb.DisplayMember = table.Columns[1].ColumnName;
                                        cb.ValueMember = table.Columns[0].ColumnName;

                                        if (cb.Items.Count > 0)
                                            cb.SelectedIndex = 1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(
                                    $"Error al ejecutar queryInfo de {v.Nombre}: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                                );
                            }
                        }

                        inputControl = cb;
                        break;

                    case "date":
                        var dtp = new DateTimePicker
                        {
                            Name = $"dtp_{nombreSeguro}",
                            Left = marginLeft + labelWidth + 10,
                            Top = y,
                            Width = 160,
                            Format = DateTimePickerFormat.Short,
                            TabIndex = tabIndex++
                        };
                        inputControl = dtp;
                        break;

                    case "string":
                    default:
                        var txt = new TextBox
                        {
                            Name = $"txt_{nombreSeguro}",
                            Left = marginLeft + labelWidth + 10,
                            Top = y,
                            Width = controlWidth,
                            TabIndex = tabIndex++
                        };
                        inputControl = txt;
                        break;
                }

                // Guarda metadata por si luego quieres leer fácil
                inputControl.Tag = v;
                panel2.Controls.Add(inputControl);

                y += rowHeight;
            }
        }



        private async void button7_Click(object sender, EventArgs e)
        {
            //_parametros = ConstruirParametros();

            //string query = $"CALL {_storedProcedure}({_parametros})";

            //using (var conn = _connectionFactory.Create())
            //using (var cmd = new MySqlCommand(query, conn))
            //using (var da = new MySqlDataAdapter(cmd))
            //{
            //    _dtResultado.Clear();
            //    da.Fill(_dtResultado);

            //    dataGridView2.DataSource = _dtResultado;
            //}

            // Mostrar el ProgressBar y asegurarse de que esté visible
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Visible = true;


            variablesprocedimiento = "";
            foreach (Control control in panel2.Controls)/*quitar todos los objetos del panel*/
            {
                if (control is TextBox || control is DateTimePicker || control is ComboBox)
                {
                    if (string.IsNullOrEmpty(control.Text.ToString()))
                    {
                        MessageBox.Show("Debe indicar un valor en el campo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        control.Focus();
                        progressBar1.Visible = false; // Ocultar el ProgressBar si hay error
                        return;
                    }
                    string valor = "";

                    if (control is DateTimePicker dtp)
                    {
                        valor = dtp.Value.ToString("yyyy-MM-dd");
                    }
                    else if (control is ComboBox cb)
                    {
                        // ⚠️ Si tiene ValueMember, usa SelectedValue
                        // Si no tiene, usa Text
                        valor = cb.SelectedValue != null ? cb.SelectedValue.ToString() : cb.Text;
                    }
                    else // TextBox
                    {
                        valor = control.Text.ToString();
                    }

                    //variablesprocedimiento = variablesprocedimiento + "'" + (control is DateTimePicker dtp ? dtp.Value.ToString("yyyy-MM-dd") : control.Text.ToString()) + "',";
                    variablesprocedimiento += $"'{valor}',";
                }
            }

            reporte.Clear();
            reporte.Columns.Clear();
            string query = "call " + procedimiento + "(" + variablesprocedimiento.Substring(0, variablesprocedimiento.Length - 1) + ");";
            reporte.Clear();

            try
            {
                // Ejecutar la consulta en un hilo de fondo para no bloquear el UI
                await Task.Run(() =>
                {
                    //if (!conMysql.consulta_entabla_mysql(query, reporte))
                    //{
                    //    throw new Exception("Error: " + conMysql.Mensaje);
                    //}

                    using (var conn = _connectionFactory.Create())
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        conn.Open();
                        da.Fill(reporte);
                    }
                });


                dataGridView2.DataSource = null;
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.DataSource = reporte;
                dtER = reporte.Copy();


                this.label6.Text = $"Total de registros encontrados: {dataGridView2.Rows.Count.ToString()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Listado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ocultar el ProgressBar después de completar la tarea
                progressBar1.Visible = false;
            }

        }
        public static string srtParamPipes(String original)
        {
            // Paso 1: Dividir por comas
            string[] partes = original.Split(',');

            // Paso 2: Quitar las comillas simples de cada parte
            for (int i = 0; i < partes.Length; i++)
            {
                partes[i] = partes[i].Trim('\''); // Elimina las comillas simples
            }

            // Paso 3: Unir usando " | "
            string resultado = string.Join(" - ", partes);

            string sufijo = " - ";

            if (resultado.EndsWith(sufijo))
            {
                resultado = resultado.Substring(0, resultado.Length - sufijo.Length);
            }

            return resultado;
        }
        public class Variable
        {
            public string Nombre { get; set; }
            public string tipo { get; set; }        // "combobox" | "string" | "date"
            public string queryInfo { get; set; }   // opcional
        }
        // === Modelos para el JSON ===
        public class VariablesObject
        {
            public List<Variable> Variables { get; set; }
        }

        // Normaliza un nombre para usarlo como Name de control
        private static string MakeSafeName(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return "Campo";
            // quita acentos/espacios/caracteres raros
            string s = raw.Trim();
            s = s.ToLowerInvariant();
            s = s.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n");
            s = Regex.Replace(s, @"\s+", "_");
            s = Regex.Replace(s, @"[^a-z0-9_]", "");
            if (char.IsDigit(s[0])) s = "_" + s;
            return s;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = $"{textBox2.Text.Trim()}\\";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Crear una instancia de FolderBrowserDialog
            FolderBrowserDialog folder = new FolderBrowserDialog();

            // Establecer propiedades del diálogo
            folder.Description = "Seleccione una carpeta.";

            // Mostrar el diálogo y comprobar si el usuario seleccionó una carpeta
            if (folder.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta de la carpeta seleccionada
                textBox2.Text = folder.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(dataGridView2.Rows.Count.ToString());
            if (dtER.Rows.Count <= 0)
            {
                MessageBox.Show("Nada por exportar.", "RENOVATIO PyME");
            }
            else
            {
                DataTable dt = dtER;


                try
                {
                    Directory.CreateDirectory(textBox2.Text.Trim());
                    string ruot = $"{textBox2.Text.Trim()}\\ - ({srtParamPipes(variablesprocedimiento)}).xlsx";

                    label8.Text = $"Path completo:{ruot}";
                    //if (tabla_a_excel(dt, ruot, $"{comboBox1.Text}"))
                    if (TablaAExcel(dt, ruot, $"Hoja1"))
                    {
                        MessageBox.Show($"Datos exportados exitosamente a Excel, su archivo se encuentra en: {ruot}", "RENOVATIO PyME");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Valio ocurrio el siguiente error: {ex.Message}", "RENOVATIO PyME");
                }
            }
        }

        public static bool TablaAExcel(DataTable tabla, string rutaArchivo, string nombreHoja)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

            try
            {
                // Inicializar Excel
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                workbook = excelApp.Workbooks.Add();
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = nombreHoja;

                // Escribir encabezados
                for (int i = 0; i < tabla.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = tabla.Columns[i].ColumnName;
                }

                // Escribir datos
                object[,] data = new object[tabla.Rows.Count, tabla.Columns.Count];
                for (int row = 0; row < tabla.Rows.Count; row++)
                {
                    for (int col = 0; col < tabla.Columns.Count; col++)
                    {
                        data[row, col] = tabla.Rows[row][col];
                    }
                }

                // Usar Range para escribir todos los datos de una vez (más eficiente)
                Microsoft.Office.Interop.Excel.Range range = worksheet.Range[
                    worksheet.Cells[2, 1],
                    worksheet.Cells[tabla.Rows.Count + 1, tabla.Columns.Count]];
                range.Value = data;

                // Formatear encabezados
                Microsoft.Office.Interop.Excel.Range headerRange = worksheet.Range[
                    worksheet.Cells[1, 1],
                    worksheet.Cells[1, tabla.Columns.Count]];
                headerRange.Font.Bold = true;
                headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Ajustar columnas
                worksheet.Columns.AutoFit();

                // Guardar y cerrar
                workbook.SaveAs(rutaArchivo);
                workbook.Close(true);
                excelApp.Quit();

                return true;
            }
            catch (Exception ex)
            {
                // Limpiar en caso de error
                workbook?.Close(false);
                excelApp?.Quit();

                MessageBox.Show($"Error al exportar a Excel: {ex.Message}", "RENOVATIO PyME");
                return false;
            }
            finally
            {
                // Liberar recursos COM
                if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Asegúrate de que tu DataTable esté lleno
                if (dtER == null || dtER.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para copiar.");
                    return;
                }

                // Construir el contenido como texto separado por tabulaciones
                StringBuilder sb = new StringBuilder();

                // Agregar los encabezados
                foreach (DataColumn col in dtER.Columns)
                {
                    sb.Append(col.ColumnName + "\t");
                }
                sb.AppendLine();

                // Agregar los datos fila por fila
                foreach (DataRow row in dtER.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        sb.Append(item?.ToString() + "\t");
                    }
                    sb.AppendLine();
                }

                // Copiar al portapapeles
                Clipboard.SetText(sb.ToString());

                MessageBox.Show("Datos copiados al portapapeles. ¡Pégalos en Excel!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar datos en portapapeles: {ex.Message}", "RENOVATIO PyME");
            }
            
        }
    }
}
