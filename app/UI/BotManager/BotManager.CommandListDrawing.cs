using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Fields

        private static readonly string[] LabelLess = { "Label", "GotoLabel", "Blank", "Blank2", "StatementCommand", "Index", "SkillSet" };

        private static readonly ColorBlend RainbowBlend = new ColorBlend
        {
            Colors = new[]
            {
                Color.FromArgb(255, 0, 0), Color.FromArgb(255, 0, 0), Color.FromArgb(255, 128, 0), Color.FromArgb(255, 255, 0),
                Color.FromArgb(0, 255, 0), Color.FromArgb(0, 255, 128), Color.FromArgb(0, 255, 255), Color.FromArgb(0, 128, 255),
                Color.FromArgb(0, 0, 255), Color.FromArgb(0, 0, 255)
            },
            Positions = Enumerable.Range(0, 10).Select(i => i / 9f).ToArray()
        };

        // The list repaints at every bot step. Brushes and fonts are GDI handles, so each color or font is created once
        // and reused instead of being created (and never disposed) on every paint.
        private readonly Dictionary<Color, SolidBrush> _brushCache = new Dictionary<Color, SolidBrush>();
        private readonly Dictionary<string, Font> _fontCache = new Dictionary<string, Font>();

        #endregion

        #region Methods

        public Color GetCurrentColor(string cmd)
        {
            if (!CurrentColors.ContainsKey(cmd))
            {
                CurrentColors[cmd] = GetColor(cmd);
            }
            return CurrentColors[cmd];
        }

        public Color GetColor(string name)
        {
            return Color.FromArgb(int.Parse(Config.Instance.GetValue<string>(name + "Color") ?? "FFDCDCDC", System.Globalization.NumberStyles.HexNumber));
        }

        public async void CustomCommandToggle(bool Type)
        {
            try
            {
                if (BotClientConfig.Instance.GetValue<bool>("customCommandToggle"))
                {
                    CustomCommandsEnabled = Type;
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running BotManager.CustomCommandToggle", ex);
            }
        }

        private void changeFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fdlg = new FontDialog();
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                this.Font = new Font(fdlg.Font.FontFamily, fdlg.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
                foreach (Control control in this.Controls)
                {
                    control.Font = new Font(fdlg.Font.FontFamily, fdlg.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                var selectedOption = DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Would you like to save style and have it load on the next startup?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (selectedOption == DialogResult.Yes)
                {
                    Config.Instance.SetValue("font", fdlg.Font.FontFamily.Name.ToString());
                    Config.Instance.SetValue("fontSize", fdlg.Font.Size.ToString());
                }
            }
        }

        private bool GetCurrentBoolCentered(string cmd)
        {
            if (!CurrentCentered.ContainsKey(cmd))
            {
                CurrentCentered[cmd] = GetBoolCentered(cmd);
            }
            return CurrentCentered[cmd];
        }

        private bool GetBoolCentered(string name)
        {
            return bool.Parse(Config.Instance.GetValue<string>(name + "Centered") ?? "false");
        }

        private void lstCommands_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!(e.Index > -1))
                return;
            e.DrawBackground();
            if (!CustomCommandsEnabled)
            {
                e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(), e.Font, Brushes.Gainsboro, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
                return;
            }

            IBotCommand cmd = (IBotCommand)lstCommands.Items[e.Index];
            string scmd = cmd.GetType().Name.Replace("Cmd", "");
            Brush color = BrushFor(GetCurrentColor(scmd));
            Brush varColor = BrushFor(GetCurrentColor("Variable"));
            Brush eVarColor = BrushFor(GetCurrentColor("ExtendedVariable"));
            Brush indexcolor = BrushFor(GetCurrentColor("Index"));
            RectangleF region = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height);
            Font font = CachedFont(e.Font.FontFamily.Name, e.Font.Size, FontStyle.Regular, GraphicsUnit.Point);
            using (StringFormat centered = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                if (cmd is CmdBlank || cmd is CmdBlank2 || cmd is CmdBlank3)
                {
                    DrawBlank(e, cmd, centered);
                    return;
                }

                if (!LabelLess.Contains(scmd))
                {
                    using (Region first = DrawString(e.Graphics, $"[{e.Index}] ", font, indexcolor, region, StringFormat.GenericDefault))
                    {
                        region = new RectangleF(region.X + first.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
                    }
                }

                if (GetBoolCentered(scmd))
                {
                    e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(), font, color, e.Bounds, centered);
                    return;
                }

                string cmdText = lstCommands.Items[e.Index].ToString();
                StringFormat Format() => GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault;
                if (cmdText.Contains(':'))
                {
                    string[] toDraw = cmdText.Split(':');
                    using (Region second = DrawString(e.Graphics, toDraw[0], font, color, region, Format()))
                    {
                        region = new RectangleF(region.X + second.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
                    }
                    if (toDraw[1].Contains(","))
                    {
                        toDraw = toDraw[1].Split(',');
                        Region third = DrawString(e.Graphics, toDraw[0], font, varColor, region, Format());
                        for (int i = 1; i < toDraw.Length; i++)
                        {
                            region = new RectangleF(region.X + third.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
                            third.Dispose();
                            third = DrawString(e.Graphics, toDraw[i], font, eVarColor, region, Format());
                        }
                        third.Dispose();
                    }
                    else
                    {
                        DrawString(e.Graphics, toDraw[1], font, varColor, region, Format()).Dispose();
                    }
                }
                else
                {
                    DrawString(e.Graphics, cmdText, font, color, region, Format()).Dispose();
                }
            }
        }

        private void DrawBlank(DrawItemEventArgs e, IBotCommand cmd, StringFormat centered)
        {
            string txt = lstCommands.Items[e.Index].ToString();
            Font cmdFont = CachedFont("Arial", e.Font.Size + 6.5f, FontStyle.Bold, GraphicsUnit.Pixel);
            if (cmd is CmdBlank2 && txt.Contains("[RGB]"))
            {
                Font the_font = CachedFont("Times New Roman", e.Font.Size + 6.5f, FontStyle.Bold, GraphicsUnit.Pixel);
                var font_info = new FontInfo(e.Graphics, the_font);
                SizeF text_size = e.Graphics.MeasureString(txt, the_font);
                int x0 = (int)((this.ClientSize.Width - text_size.Width) / 2);
                int y0 = (int)((this.ClientSize.Height - text_size.Height) / 2);
                int brush_y0 = (int)(y0 + font_info.InternalLeadingPixels) + (int)font_info.InternalLeadingPixels;
                int brush_y1 = (int)(y0 + font_info.AscentPixels) + 5;
                using (LinearGradientBrush rainbowbrush = new LinearGradientBrush(new Point(x0, brush_y0), new Point(x0, brush_y1), Color.Red, Color.Violet))
                {
                    rainbowbrush.InterpolationColors = RainbowBlend;
                    e.Graphics.DrawString(txt.Replace("[RGB]", ""), the_font, rainbowbrush, e.Bounds, centered);
                }
            }
            else if (cmd is CmdBlank2 && txt.StartsWith("["))
            {
                try
                {
                    string[] rgbarray = txt.Replace("[", "").Split(']')[0].Split(',');
                    Brush b2b = Brushes.Black;
                    if (rgbarray.Length == 3)
                        b2b = BrushFor(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2])));
                    else if (rgbarray.Length == 4)
                        b2b = BrushFor(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2]), int.Parse(rgbarray[3])));
                    txt = Regex.Replace(txt, @"\[.*?\]", "");
                    if (txt.Contains("(TROLL)"))
                        e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, b2b, e.Bounds, StringFormat.GenericDefault);
                    else
                        e.Graphics.DrawString(txt, cmdFont, b2b, e.Bounds, centered);
                }
                catch
                {
                    // Painting must not throw; an item with a bad custom color is drawn without it.
                }
            }
            else if (cmd is CmdBlank3 blank3)
            {
                try
                {
                    e.Graphics.DrawString(txt, cmdFont, BrushFor(blank3.Argb()), e.Bounds, centered);
                }
                catch
                {
                    // Painting must not throw; an item with a bad custom color is drawn without it.
                }
            }
            else if (txt.Contains("(TROLL)"))
                e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);
        }

        private SolidBrush BrushFor(Color color)
        {
            if (!_brushCache.TryGetValue(color, out SolidBrush brush))
            {
                brush = new SolidBrush(color);
                _brushCache[color] = brush;
            }
            return brush;
        }

        private Font CachedFont(string family, float size, FontStyle style, GraphicsUnit unit)
        {
            string key = $"{family}|{size}|{style}|{unit}";
            if (!_fontCache.TryGetValue(key, out Font font))
            {
                font = new Font(family, size, style, unit);
                _fontCache[key] = font;
            }
            return font;
        }

        private Region DrawString(Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            format.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, s.Length) });
            format.Alignment = StringAlignment.Near;
            g.DrawString(s, font, brush, layoutRectangle, format);
            return g.MeasureCharacterRanges(s, font, layoutRectangle, format)[0];
        }

        private Region DrawRTLString(Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            var format = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };
            format.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, s.Length) });
            Region length = g.MeasureCharacterRanges(s, font, layoutRectangle, format)[0];
            layoutRectangle = new RectangleF(layoutRectangle.Width, layoutRectangle.Y, length.GetBounds(g).Width, layoutRectangle.Height);
            DrawString(g, s, font, brush, layoutRectangle, format);
            return length;
        }

        private void commandColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Root.Instance.ShowForm(CommandColorForm.Instance);
        }

        private void lnkCustomCommands_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomCommandsEnabled = !CustomCommandsEnabled;
        }

        #endregion
    }
}
