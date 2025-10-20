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

        private const int HeaderHeight = 60;
        private const int WrapperPadding = 18;
        private const int CardPadding = 14;

        public static readonly Color BackgroundColor = Color.FromArgb(244, 247, 252);
        public static readonly Color SurfaceColor = Color.White;
        public static readonly Color AccentColor = Color.FromArgb(37, 99, 235);
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
            {
                return;
            }

            if (form.Tag is string tag && tag == StyledTag)
            {
                return;
            }

            form.SuspendLayout();

            EnableDoubleBuffering(form);
            form.BackColor = BackgroundColor;
            form.ForeColor = TextPrimaryColor;
            form.Font = DefaultFont;

            var originalSize = form.ClientSize;

            var headerPanel = CreateHeaderPanel(form);
            var contentWrapper = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(WrapperPadding),
                BackColor = Color.Transparent
            };

            var cardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SurfaceColor,
                Padding = new Padding(CardPadding),
                AutoScroll = true
            };

            EnableDoubleBuffering(cardPanel);
            ApplyRoundedCorners(cardPanel, 20);

            AttachShadow(contentWrapper, cardPanel);

            var controls = form.Controls.Cast<Control>().ToList();

            form.Controls.Clear();
            form.Controls.Add(contentWrapper);
            form.Controls.Add(headerPanel);
            contentWrapper.Controls.Add(cardPanel);

            foreach (var control in controls)
            {
                if (control is null)
                {
                    continue;
                }

                cardPanel.Controls.Add(control);
            }

            StyleControl(cardPanel);

            var newWidth = originalSize.Width + 2 * (WrapperPadding + CardPadding);
            var newHeight = originalSize.Height + HeaderHeight + 2 * WrapperPadding + 2 * CardPadding;
            form.ClientSize = new Size(newWidth, newHeight);
            form.MinimumSize = form.Size;

            form.Tag = StyledTag;
            form.ResumeLayout();
        }

        private static Panel CreateHeaderPanel(Form form)
        {
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = HeaderHeight,
                BackColor = AccentColor,
                Padding = new Padding(24, 16, 24, 16),
                Tag = StyledTag
            };

            var titleLabel = new Label
            {
                AutoSize = true,
                Text = form.Text,
                ForeColor = Color.White,
                Font = TitleFont,
                Location = new Point(0, 6),
                Tag = StyledTag
            };

            headerPanel.Controls.Add(titleLabel);

            var subtitle = new Label
            {
                AutoSize = true,
                Text = "Control de Pedidos",
                ForeColor = Color.FromArgb(226, 232, 240),
                Font = SubtitleFont,
                Location = new Point(0, 36),
                Tag = StyledTag
            };

            headerPanel.Controls.Add(subtitle);

            return headerPanel;
        }

        private static void StyleControl(Control control)
        {
            if (control is null)
            {
                return;
            }

            if (control.Tag is string tag && tag == StyledTag)
            {
                return;
            }

            switch (control)
            {
                case Panel panel:
                    if (panel.Parent is Panel)
                    {
                        panel.BackColor = SurfaceColor;
                    }
                    EnableDoubleBuffering(panel);
                    break;
                case GroupBox groupBox:
                    groupBox.BackColor = SurfaceColor;
                    groupBox.ForeColor = TextPrimaryColor;
                    groupBox.Font = SemiBoldFont;
                    groupBox.Padding = new Padding(groupBox.Padding.Left, groupBox.Padding.Top + 8, groupBox.Padding.Right, groupBox.Padding.Bottom + 8);
                    break;
                case Label label:
                    label.ForeColor = TextSecondaryColor;
                    if (Math.Abs(label.Font.Size - DefaultFont.Size) > 0.1f || label.Font.FontFamily.Name == DefaultFont.FontFamily.Name)
                    {
                        label.Font = new Font("Segoe UI Semibold", DefaultFont.Size - 0.5f, FontStyle.Regular, GraphicsUnit.Point);
                    }
                    break;
                case TextBox textBox:
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = TextPrimaryColor;
                    textBox.Font = DefaultFont;
                    textBox.Margin = new Padding(0, 0, 0, 12);
                    break;
                case ComboBox comboBox:
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.BackColor = Color.White;
                    comboBox.ForeColor = TextPrimaryColor;
                    comboBox.Font = DefaultFont;
                    comboBox.Margin = new Padding(0, 0, 0, 12);
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
                ApplyButtonStateColors(button, DangerColor, DangerHoverColor, DangerActiveColor, Color.White, 0);
            }
            else if (text.Contains("limpiar") || text.Contains("cancelar"))
            {
                button.BackColor = NeutralButtonColor;
                button.ForeColor = AccentColor;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = AccentColor;
                ApplyButtonStateColors(button, NeutralButtonColor, Color.FromArgb(226, 232, 240), Color.FromArgb(209, 213, 219), AccentColor, 1);
            }
            else
            {
                ApplyButtonStateColors(button, AccentColor, AccentHoverColor, AccentActiveColor, Color.White, 0);
            }
        }

        private static void ApplyButtonStateColors(Button button, Color normal, Color hover, Color active, Color textColor, int borderSize)
        {
            button.BackColor = normal;
            button.ForeColor = textColor;
            button.FlatAppearance.BorderSize = borderSize;
            button.FlatAppearance.BorderColor = borderSize > 0 ? AccentColor : Color.Transparent;

            button.MouseEnter += (_, __) =>
            {
                if (button.Enabled)
                {
                    button.BackColor = hover;
                }
            };

            button.MouseLeave += (_, __) =>
            {
                if (button.Enabled)
                {
                    button.BackColor = normal;
                }
            };

            button.MouseDown += (_, __) =>
            {
                if (button.Enabled)
                {
                    button.BackColor = active;
                }
            };

            button.MouseUp += (_, __) =>
            {
                if (button.Enabled)
                {
                    button.BackColor = hover;
                }
            };
        }

        private static void StyleDataGridView(DataGridView grid)
        {
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

        private static void ApplyRoundedCorners(Control control, int radius)
        {
            void UpdateRegion()
            {
                if (control.Width <= 0 || control.Height <= 0)
                {
                    return;
                }

                using (var path = CreateRoundedRectangle(control.ClientRectangle, radius))
                {
                    control.Region = new Region(path);
                }
            }

            control.HandleCreated += (_, __) => UpdateRegion();
            control.Resize += (_, __) => UpdateRegion();

            if (control.IsHandleCreated)
            {
                UpdateRegion();
            }
        }

        private static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            var diameter = radius * 2;
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static void AttachShadow(Panel wrapper, Panel card)
        {
            wrapper.Paint += (sender, args) =>
            {
                var graphics = args.Graphics;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var rect = card.Bounds;
                rect.Offset(4, 6);
                rect.Inflate(4, 4);

                using (var path = CreateRoundedRectangle(rect, 22))
                using (var brush = new SolidBrush(Color.FromArgb(60, 15, 23, 42)))
                {
                    graphics.FillPath(brush, path);
                }
            };

            card.SizeChanged += (_, __) => wrapper.Invalidate();
            card.LocationChanged += (_, __) => wrapper.Invalidate();
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
                // ignored
            }
        }
    }
}
