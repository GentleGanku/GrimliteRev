using System;
using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class CommandNode : ResizableUserControl
    {
        #region Fields

        public Point previousPosition;

        #endregion

        #region Constructors

        public CommandNode()
        {
            InitializeComponent();
            previousPosition = this.Location;
        }

        #endregion

        #region Properties

        public Control activeControl
        {
            get;
            set;
        }

        #endregion

        #region Methods

        private void CommandNode_MouseDown(object sender, MouseEventArgs e)
        {
            activeControl = sender as Control;
            previousPosition = e.Location;
            Cursor = Cursors.Hand;
        }

        private void CommandNode_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.activeControl.Location.X == Math.Min(Math.Max(activeControl.Right + (e.X - previousPosition.X), 0), activeControl.Parent.Width - activeControl.Width) && this.activeControl.Location.Y == Math.Min(Math.Max(activeControl.Bottom + (e.Y - previousPosition.Y), 0), activeControl.Parent.Height - activeControl.Height))
                {
                    int RightX = Math.Min(Math.Max(activeControl.Right + (e.X - previousPosition.X), 0), activeControl.Parent.Width - activeControl.Width);
                    int BottomY = Math.Min(Math.Max(activeControl.Bottom + (e.Y - previousPosition.Y), 0), activeControl.Parent.Height - activeControl.Height);

                    activeControl.Location = new Point(RightX, BottomY);

                    ///!LocationRightPanel.Equals(this.activeControl) && this.activeControl.Bounds.IntersectsWith(LocationRightPanel.Bounds)
                }

                else
                {
                    int LeftX = Math.Min(Math.Max(activeControl.Left + (e.X - previousPosition.X), 0), activeControl.Parent.Width - activeControl.Width);
                    int TopY = Math.Min(Math.Max(activeControl.Top + (e.Y - previousPosition.Y), 0), activeControl.Parent.Height - activeControl.Height);

                    activeControl.Location = new Point(LeftX, TopY);

                }

            }
        }

        private void CommandNode_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    this.Dispose();
                    break;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion
    }

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

        public ResizableUserControl()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts
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

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
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
