using DarkUI.Forms;
using System;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class RawCommandEditor : DarkForm
    {
        #region Fields

        public static RawCommandEditor Instance = new RawCommandEditor();

        #endregion

        #region Constructors

        public RawCommandEditor()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public string Input
        {
            get
            {
                return txtCmd.Text;
            }
        }

        public string Content
        {
            set
            {
                txtCmd.Text = value;
            }
        }

        #endregion

        #region Methods

        public static string Show(string content)
        {
            using (RawCommandEditor rawCommandEditor = new RawCommandEditor
            {
                Content = content
            })
            {
                DialogResult result = rawCommandEditor.ShowDialog();
                if (result == DialogResult.OK)
                {
                    BotManager.Instance.LastIndexedSearch = 0;
                    return rawCommandEditor.Input;
                }
                else
                    return null;
            }
        }

        private void RawCommandEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void RawCommandEditor_Load(object sender, EventArgs e)
        {
            txtCmd.Select();
        }

        private void txtCmd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    btnOK.PerformClick();
                    break;

                case Keys.Escape:
                    btnCancel.PerformClick();
                    break;
            }
        }

        #endregion
    }
}