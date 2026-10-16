using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Misc;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
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
            if (BotClientConfig.Instance.GetValue<bool>("customCommandToggle"))
            {
                CustomCommandsEnabled = Type;
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
                Brush myBrush = Brushes.Gainsboro;
                e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(),
                    e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
                return;
            }

            #region Settings
            IBotCommand cmd = (IBotCommand)lstCommands.Items[e.Index];
            string[] count = cmd.GetType().ToString().Split('.');
            string scmd = count[count.Count() - 1].Replace("Cmd", "");
            SolidBrush color = new SolidBrush(GetCurrentColor(scmd));
            SolidBrush varColor = new SolidBrush(GetCurrentColor("Variable"));
            SolidBrush eVarColor = new SolidBrush(GetCurrentColor("ExtendedVariable"));
            SolidBrush indexcolor = new SolidBrush(GetCurrentColor("Index"));
            RectangleF region = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height);
            Font font = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Regular);
            StringFormat centered = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            #endregion

            string[] LabelLess = new string[]
            {
                "Label",
                "GotoLabel",
                "Blank",
                "Blank2",
                "StatementCommand",
                "Index",
                "SkillSet"
            };

            if (cmd is CmdBlank || cmd is CmdBlank2 || cmd is CmdBlank3)
            {
                string txt = lstCommands.Items[e.Index].ToString();
                Font cmdFont = new Font("Arial", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel);
                if (cmd is CmdBlank2 && txt.Contains("[RGB]"))
                    using (Font the_font = new Font("Times New Roman", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        var font_info = new FontInfo(e.Graphics, the_font);
                        SizeF text_size = e.Graphics.MeasureString(txt, the_font);
                        int x0 = (int)((this.ClientSize.Width - text_size.Width) / 2);
                        int y0 = (int)((this.ClientSize.Height - text_size.Height) / 2);
                        int brush_y0 = (int)(y0 + font_info.InternalLeadingPixels) + (int)font_info.InternalLeadingPixels;
                        int brush_y1 = (int)(y0 + font_info.AscentPixels) + 5;
                        using (LinearGradientBrush rainbowbrush = new LinearGradientBrush(new Point(x0, brush_y0), new Point(x0, brush_y1), Color.Red, Color.Violet))
                        {
                            Color[] colors = new Color[]
                            {
                                Color.FromArgb(255, 0, 0),
                                Color.FromArgb(255, 0, 0),
                                Color.FromArgb(255, 128, 0),
                                Color.FromArgb(255, 255, 0),
                                Color.FromArgb(0, 255, 0),
                                Color.FromArgb(0, 255, 128),
                                Color.FromArgb(0, 255, 255),
                                Color.FromArgb(0, 128, 255),
                                Color.FromArgb(0, 0, 255),
                                Color.FromArgb(0, 0, 255),
                            };
                            int num_colors = colors.Length;
                            float[] blend_positions = new float[num_colors];
                            for (int i = 0; i < num_colors; i++)
                                blend_positions[i] = i / (num_colors - 1f);
                            ColorBlend color_blend = new ColorBlend
                            {
                                Colors = colors,
                                Positions = blend_positions
                            };
                            rainbowbrush.InterpolationColors = color_blend;

                            txt = txt.Replace("[RGB]", "");
                            e.Graphics.DrawString(txt, the_font, rainbowbrush, e.Bounds, centered);
                        }
                    }
                else if (cmd is CmdBlank2 && txt.StartsWith("["))
                {
                    try
                    {
                        string[] rgbarray = txt.Replace("[", "").Split(']')[0].Split(',');
                        SolidBrush b2b = new SolidBrush(Color.Black);
                        if (rgbarray.Length == 3)
                            b2b = new SolidBrush(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2])));
                        else if (rgbarray.Length == 4)
                            b2b = new SolidBrush(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2]), int.Parse(rgbarray[3])));
                        txt = Regex.Replace(txt, @"\[.*?\]", "");
                        if (txt.Contains("(TROLL)"))
                            e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, b2b, e.Bounds, StringFormat.GenericDefault);
                        else
                            e.Graphics.DrawString(txt, cmdFont, b2b, e.Bounds, centered);
                    }
                    catch { }
                }
                else if (cmd is CmdBlank3)
                {
                    var jsonObj = JsonConvert.DeserializeObject<CmdBlank3>(JsonConvert.SerializeObject(cmd));
                    try
                    {
                        SolidBrush colorBrush = new SolidBrush(jsonObj.Argb());
                        e.Graphics.DrawString(txt, cmdFont, colorBrush, e.Bounds, centered);
                    }
                    catch { }
                }
                else if (txt.Contains("(TROLL)"))
                    e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
                return;
            }

            if (!LabelLess.Contains(scmd))
            {
                Region first = DrawString(e.Graphics, $"[{e.Index.ToString()}] ", font, indexcolor, region, StringFormat.GenericDefault);
                region = new RectangleF(region.X + first.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
            }

            if (GetBoolCentered(scmd))
            {
                e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(), font, color, e.Bounds, centered);
                return;
            }

            string cmdText = lstCommands.Items[e.Index].ToString();
            string[] toDraw;
            if (cmdText.Contains(':'))
            {
                toDraw = lstCommands.Items[e.Index].ToString().Split(':');
                Region second = DrawString(e.Graphics, toDraw[0], font, color, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
                region = new RectangleF(region.X + second.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
                if (toDraw[1].Contains(","))
                {
                    toDraw = toDraw[1].Split(',');
                    Region third = DrawString(e.Graphics, toDraw[0], font, varColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
                    for (int i = 1; i < toDraw.Length; i++)
                    {
                        region = new RectangleF(region.X + third.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
                        third = DrawString(e.Graphics, toDraw[i], font, eVarColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
                    }
                }
                else
                    DrawString(e.Graphics, toDraw[1], font, varColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
            }
            else
                DrawString(e.Graphics, cmdText, font, color, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
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
