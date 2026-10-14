using DarkUI.Controls;
using DarkUI.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class ShortcutsForm : DarkForm
    {
        // Keep in sync with the handlers in BotManager (lstBoxs_KeyPress, btnSearchCmd_KeyDown, lstCommands_DoubleClick, AddCommand).
        private static readonly string[][] Sections =
        {
            new[] { "Command lists (on the selected items)" },
            new[] { "Ctrl + Up / Down", "Move up or down" },
            new[] { "Ctrl + D", "Duplicate" },
            new[] { "Ctrl + C", "Copy" },
            new[] { "Ctrl + X", "Cut" },
            new[] { "Ctrl + V", "Paste below the selection" },
            new[] { "Ctrl + Delete, Ctrl + R", "Remove" },
            new[] { "Ctrl + S", "Save bot" },
            new[] { "Search" },
            new[] { "Ctrl + F", "Go to the search box" },
            new[] { "Enter", "Find the next match" },
            new[] { "Mouse" },
            new[] { "Double-click a command", "Edit it" },
            new[] { "Double-click an empty list", "Load a bot" },
            new[] { "Ctrl + click an add button", "Insert above the selected command" },
            new[] { "Drop a .gbot file on the list", "Load it" },
            new[] { "Right-click", "Bot Manager menu" }
        };

        public ShortcutsForm()
        {
            InitializeComponent();
            tblShortcuts.SuspendLayout();
            foreach (string[] row in Sections)
            {
                if (row.Length == 1)
                {
                    AddHeading(row[0]);
                }
                else
                {
                    AddRow(row[0], row[1]);
                }
            }
            tblShortcuts.ResumeLayout();
        }

        private void AddHeading(string text)
        {
            DarkLabel heading = new DarkLabel
            {
                AutoSize = true,
                Font = new Font(Font, FontStyle.Bold),
                ForeColor = Color.FromArgb(246, 64, 97),
                Margin = new Padding(0, tblShortcuts.RowCount == 0 ? 4 : 12, 0, 4),
                Text = text
            };
            tblShortcuts.RowCount++;
            tblShortcuts.Controls.Add(heading, 0, tblShortcuts.RowCount - 1);
            tblShortcuts.SetColumnSpan(heading, 2);
        }

        private void AddRow(string keys, string action)
        {
            tblShortcuts.RowCount++;
            tblShortcuts.Controls.Add(new DarkLabel { AutoSize = true, Margin = new Padding(0, 2, 16, 2), Text = keys }, 0, tblShortcuts.RowCount - 1);
            tblShortcuts.Controls.Add(new DarkLabel { AutoSize = true, Margin = new Padding(0, 2, 0, 2), Text = action }, 1, tblShortcuts.RowCount - 1);
        }

        private void ShortcutsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.MdiFormClosing || e.CloseReason == CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
