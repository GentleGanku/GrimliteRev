using System;
using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class ResizableUserControl : UserControl
    {
        #region Fields

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 6;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates the control. ResizeRedraw avoids visual artifacts while it is resized.
        /// </summary>
        public ResizableUserControl()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #endregion

        #region Properties

        Rectangle Top
        {
            get
            {
                return new Rectangle(0, 0, this.ClientSize.Width, _);
            }
        }

        Rectangle Left
        {
            get
            {
                return new Rectangle(0, 0, _, this.ClientSize.Height);
            }
        }

        Rectangle Bottom
        {
            get
            {
                return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _);
            }
        }

        Rectangle Right
        {
            get
            {
                return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height);
            }
        }

        Rectangle TopLeft
        {
            get
            {
                return new Rectangle(0, 0, _, _);
            }
        }

        Rectangle TopRight
        {
            get
            {
                return new Rectangle(this.ClientSize.Width - _, 0, _, _);
            }
        }

        Rectangle BottomLeft
        {
            get
            {
                return new Rectangle(0, this.ClientSize.Height - _, _, _);
            }
        }

        Rectangle BottomRight
        {
            get
            {
                return new Rectangle(this.ClientSize.Width - _, this.ClientSize.Height - _, _, _);
            }
        }

        #endregion

        #region Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Transparent, Top);
            e.Graphics.FillRectangle(Brushes.Transparent, Left);
            e.Graphics.FillRectangle(Brushes.Transparent, Right);
            e.Graphics.FillRectangle(Brushes.Transparent, Bottom);
        }

        /// <summary>
        /// Answers WM_NCHITTEST (0x84) with the border or corner under the cursor, so the control can be resized from
        /// its edges.
        /// </summary>
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84)
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }

        }

        #endregion
    }
}
