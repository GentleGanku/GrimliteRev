using DarkUI.Config;
using DarkUI.Controls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI
{
    /// <summary>
    /// A <see cref="DarkListBox"/> that keeps each item on one line. DarkListBox wraps text that is wider than the list,
    /// and the wrapped second line shows as slivers of letters inside the item's row; here a long item is cut with
    /// "…" instead. The colors are DarkListBox's own.
    /// </summary>
    public class SingleLineListBox : DarkListBox
    {
        #region Fields

        private static readonly StringFormat SingleLine = new StringFormat(StringFormat.GenericDefault)
        {
            FormatFlags = StringFormatFlags.NoWrap,
            Trimming = StringTrimming.EllipsisCharacter
        };

        #endregion

        #region Constructors

        public SingleLineListBox()
        {
        }

        public SingleLineListBox(IContainer container) : base(container)
        {
        }

        #endregion

        #region Methods

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count)
            {
                return;
            }
            Color background = (e.Index % 2 != 0) ? Colors.GreyBackground : Colors.HeaderBackground;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                background = Focused ? Colors.BlueSelection : Colors.GreySelection;
            }
            using (SolidBrush brush = new SolidBrush(background))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, Brushes.White, e.Bounds, SingleLine);
        }

        #endregion
    }
}
