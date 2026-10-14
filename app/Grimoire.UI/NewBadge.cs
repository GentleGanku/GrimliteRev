using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI
{
    /// <summary>
    /// Draws a small raised "NEW!" mark at the top-right of menu items that changed in the current version.
    /// </summary>
    public static class NewBadge
    {
        private const string Text = "NEW!";

        private static readonly Font BadgeFont = new Font("Segoe UI", 6F, FontStyle.Bold);

        private static readonly Size BadgeSize = TextRenderer.MeasureText(Text, BadgeFont, Size.Empty, TextFormatFlags.NoPadding);

        public static void Attach(params ToolStripMenuItem[] items)
        {
            foreach (ToolStripMenuItem item in items)
            {
                if (item.IsOnDropDown)
                {
                    // Drop-down menus size themselves from text and shortcut widths only, so blank shortcut text reserves the space.
                    item.ShortcutKeyDisplayString = new string(' ', 8);
                }
                else
                {
                    // Left-aligned text keeps the reserved space on the right free for the badge.
                    Padding padding = item.Padding;
                    item.Padding = new Padding(padding.Left, padding.Top, padding.Right + BadgeSize.Width - 6, padding.Bottom);
                    item.TextAlign = ContentAlignment.MiddleLeft;
                }
                item.Paint += OnItemPaint;
            }
        }

        private static void OnItemPaint(object sender, PaintEventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            Point location = item.IsOnDropDown ? new Point(item.Width - BadgeSize.Width - 12, 2) : new Point(item.Width - BadgeSize.Width - 1, 0);
            TextRenderer.DrawText(e.Graphics, Text, BadgeFont, location, Color.LimeGreen, TextFormatFlags.NoPadding);
        }
    }
}
