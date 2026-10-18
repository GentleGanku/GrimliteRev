using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI
{
    /// <summary>
    /// Draws a small raised "NEW!" mark at the top-right of menu items that changed in the current version.
    /// </summary>
    public static class NewBadge
    {
        #region Fields

        private const string Text = "NEW!";
        private static readonly Font BadgeFont = new Font("Segoe UI", 6F, FontStyle.Bold);
        private static readonly Size BadgeSize = TextRenderer.MeasureText(Text, BadgeFont, Size.Empty, TextFormatFlags.NoPadding);

        #endregion

        #region Methods

        /// <summary>
        /// Reserves room for the NEW! badge on each item. Drop-down menus size themselves from text and shortcut widths
        /// only, so a blank shortcut text reserves the space; top-level items get extra right padding and left-aligned
        /// text, which keeps that space free for the badge.
        /// </summary>
        public static void Attach(params ToolStripMenuItem[] items)
        {
            foreach (ToolStripMenuItem item in items)
            {
                if (item.IsOnDropDown)
                {
                    item.ShortcutKeyDisplayString = new string(' ', 8);
                }
                else
                {
                    Padding padding = item.Padding;
                    item.Padding = new Padding(padding.Left, padding.Top, padding.Right + BadgeSize.Width - 9, padding.Bottom);
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

        #endregion
    }
}
