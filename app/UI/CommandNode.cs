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
}
