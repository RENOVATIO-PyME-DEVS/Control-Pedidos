using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Control_Pedidos.Helpers
{
    public static class UIStyles
    {
        private const string StyledTag = "__ui_styled__";
        private const int HeaderHeight = 58;

        public static readonly Color BackgroundColor = Color.FromArgb(244, 247, 252);
        public static readonly Color SurfaceColor = Color.White;
        public static readonly Color AccentColor = Color.FromArgb(24, 66, 156);
        public static readonly Color AccentHoverColor = Color.FromArgb(59, 130, 246);
        public static readonly Color AccentActiveColor = Color.FromArgb(29, 78, 216);
        public static readonly Color NeutralButtonColor = Color.White;
        public static readonly Color DangerColor = Color.FromArgb(220, 38, 38);
        public static readonly Color DangerHoverColor = Color.FromArgb(239, 68, 68);
        public static readonly Color DangerActiveColor = Color.FromArgb(185, 28, 28);
        public static readonly Color TextPrimaryColor = Color.FromArgb(30, 41, 59);
        public static readonly Color TextSecondaryColor = Color.FromArgb(100, 116, 139);
        public static readonly Color DividerColor = Color.FromArgb(226, 232, 240);

        public static readonly Font DefaultFont = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        public static readonly Font SemiBoldFont = new Font("Segoe UI Semibold", 10F, FontStyle.Regular, GraphicsUnit.Point);
        public static readonly Font TitleFont = new Font("Segoe UI Semibold", 18F, FontStyle.Regular, GraphicsUnit.Point);
        public static readonly Font SubtitleFont = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);

        public static void ApplyTheme(Form form)
        {
            if (form == null || form.IsDisposed)
                return;

            if (form.Tag is string tag && tag == StyledTag)
                return;

            form.SuspendLayout();

            // Activamos doble buffer y seteamos colores base para evitar parpadeos.
            EnableDoubleBuffering(form);
            form.BackColor = BackgroundColor;
            form.ForeColor = TextPrimaryColor;
            form.Font = DefaultFont;

            // Dibuja encabezado visual sin modificar estructura
            AddHeaderPanel(form);

            // Aplica estilos a los controles existentes
            foreach (Control control in form.Controls)
            {

                if (control.Tag != null && control.Tag.ToString() == "no_style")
                    continue;

                StyleControl(control);
            }

            form.Tag = StyledTag;
            form.ResumeLayout();
        }

        private static void AddHeaderPanel(Form form)
        {
            // Header genérico para todas las pantallas con título y subtítulo.
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = HeaderHeight,
                BackColor = AccentColor,
                Tag = StyledTag
            };

            var title = new Label
            {
                AutoSize = true,
                Text = form.Text,
                ForeColor = Color.White,
                Font = TitleFont,
                Location = new Point(20, 10),
                Tag = StyledTag
            };

            var subtitle = new Label
            {
                AutoSize = true,
                Text = "Control de Pedidos",
                ForeColor = Color.FromArgb(226, 232, 240),
                Font = SubtitleFont,
                Location = new Point(22, 40),
                Tag = StyledTag
            };

            header.Controls.Add(title);
            header.Controls.Add(subtitle);

            // Insertar el header en la parte superior sin reordenar controles
            form.Controls.Add(header);
            header.BringToFront();

            // Agregar una sombra sutil debajo
            header.Paint += (s, e) =>
            {
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, header.Height - 18, header.Width, 18),
                    Color.FromArgb(80, Color.Black),
                    Color.Transparent,
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, 0, header.Height - 16, header.Width, 16);
                }
            };
        }

        private static void StyleControl(Control control)
        {
            if (control == null)
                return;

            if (control.Tag is string tag && tag == StyledTag)
                return;

            switch (control)
            {
                case Panel panel:
                    panel.BackColor = SurfaceColor;
                    EnableDoubleBuffering(panel);
                    break;
                case GroupBox groupBox:
                    groupBox.BackColor = SurfaceColor;
                    groupBox.ForeColor = TextPrimaryColor;
                    groupBox.Font = SemiBoldFont;
                    break;
                case Label label:
                    label.ForeColor = TextSecondaryColor;
                    if (Math.Abs(label.Font.Size - DefaultFont.Size) > 0.1f)
                        label.Font = new Font("Segoe UI Semibold", DefaultFont.Size - 0.5f, FontStyle.Regular, GraphicsUnit.Point);
                    break;
                case TextBox textBox:
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = TextPrimaryColor;
                    textBox.Font = DefaultFont;
                    break;
                case ComboBox comboBox:
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.BackColor = Color.White;
                    comboBox.ForeColor = TextPrimaryColor;
                    comboBox.Font = DefaultFont;
                    break;
                case DateTimePicker dateTimePicker:
                    dateTimePicker.Font = DefaultFont;
                    dateTimePicker.CalendarMonthBackground = Color.White;
                    dateTimePicker.CalendarForeColor = TextPrimaryColor;
                    dateTimePicker.CalendarTitleBackColor = AccentColor;
                    dateTimePicker.CalendarTitleForeColor = Color.White;
                    break;
                case NumericUpDown numericUpDown:
                    numericUpDown.Font = DefaultFont;
                    numericUpDown.ForeColor = TextPrimaryColor;
                    numericUpDown.BackColor = Color.White;
                    numericUpDown.BorderStyle = BorderStyle.FixedSingle;
                    break;
                case CheckBox checkBox:
                    checkBox.Font = DefaultFont;
                    checkBox.ForeColor = TextSecondaryColor;
                    break;
                case RadioButton radioButton:
                    radioButton.Font = DefaultFont;
                    radioButton.ForeColor = TextSecondaryColor;
                    break;
                case ListBox listBox:
                    listBox.BorderStyle = BorderStyle.FixedSingle;
                    listBox.BackColor = Color.FromArgb(248, 250, 252);
                    listBox.ForeColor = TextPrimaryColor;
                    listBox.Font = DefaultFont;
                    break;
                case DataGridView dataGridView:
                    StyleDataGridView(dataGridView);
                    break;
                case Button button:
                    StyleButton(button);
                    break;
            }

            foreach (Control child in control.Controls)
            {
                StyleControl(child);
            }
        }

        private static void StyleButton(Button button)
        {
            // Botones planos con padding y cursor de mano para dar feedback moderno.
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.ForeColor = Color.White;
            button.Font = SemiBoldFont;
            button.Height = Math.Max(button.Height, 38);
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.Padding = new Padding(12, 6, 12, 6);

            var text = button.Text?.ToLowerInvariant() ?? string.Empty;
            if (text.Contains("eliminar") || text.Contains("inactivar") || text.Contains("baja"))
            {
                ApplyButtonStateColors(button, DangerColor, DangerHoverColor, DangerActiveColor, Color.White);
            }
            else if (text.Contains("limpiar") || text.Contains("cancelar"))
            {
                button.BackColor = NeutralButtonColor;
                button.ForeColor = AccentColor;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = AccentColor;
                ApplyButtonStateColors(button, NeutralButtonColor, Color.FromArgb(226, 232, 240), Color.FromArgb(209, 213, 219), AccentColor);
            }
            else
            {
                ApplyButtonStateColors(button, AccentColor, AccentHoverColor, AccentActiveColor, Color.White);
            }
        }

        private static void ApplyButtonStateColors(Button button, Color normal, Color hover, Color active, Color textColor)
        {
            button.BackColor = normal;
            button.ForeColor = textColor;

            button.MouseEnter += (_, __) =>
            {
                if (button.Enabled)
                    button.BackColor = hover;
            };

            button.MouseLeave += (_, __) =>
            {
                if (button.Enabled)
                    button.BackColor = normal;
            };

            button.MouseDown += (_, __) =>
            {
                if (button.Enabled)
                    button.BackColor = active;
            };

            button.MouseUp += (_, __) =>
            {
                if (button.Enabled)
                    button.BackColor = hover;
            };
        }

        private static void StyleDataGridView(DataGridView grid)
        {
            // Configuramos apariencia consistente con el resto de la interfaz.
            EnableDoubleBuffering(grid);

            grid.BackgroundColor = SurfaceColor;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = DividerColor;

            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            grid.ColumnHeadersDefaultCellStyle.BackColor = AccentColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 8, 12, 8);
            grid.ColumnHeadersHeight = 44;

            grid.DefaultCellStyle.Font = DefaultFont;
            grid.DefaultCellStyle.BackColor = SurfaceColor;
            grid.DefaultCellStyle.ForeColor = TextPrimaryColor;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = TextPrimaryColor;
            grid.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(191, 219, 254);
            grid.RowTemplate.Height = 36;
        }

        private static void EnableDoubleBuffering(Control control)
        {
            try
            {
                var prop = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                prop?.SetValue(control, true, null);
            }
            catch
            {
                // ignorar
            }
        }
    }
}
