using System.Windows.Forms;
using System.Drawing;
using DarkUI.Forms;
using DarkUI.Controls;

namespace Grimoire.Utils
{
    public class InputBox
    {
        private static int extraHeight = 20;

        private static int labelHeight = 20;

        public static DialogResult InputDialog(ref string input, string prompt, string title = "Title", int width = 250, int height = 85, int eHeight = 20, int lHeight = 20)
        {
            extraHeight = eHeight;
            labelHeight = lHeight;
            return ShowInputDialogBox(ref input, prompt, title, width, height);
        }

        public static DialogResult ShowInputDialogBox(ref string input, string prompt, string title = "Title", int width = 250, int height = 85)
        {
            Size size = new Size(width, height);
            DarkForm inputBox = new DarkForm();

            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.TopMost = true;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;

            RichTextBox label = new RichTextBox();
            label.ForeColor = Color.White;
            label.Text = prompt;
            label.Location = new Point(5, 5);
            label.Multiline = true;
            label.BorderStyle = BorderStyle.None;
            label.Size = new Size(size.Width - 10, labelHeight);
            label.ReadOnly = true;
            label.ForeColor = Color.White;
            label.BackColor = Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            label.TabIndex = 2;
            inputBox.Controls.Add(label);

            DarkTextBox textBox = new DarkTextBox();
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Size = new Size(size.Width - 10, 23);
            textBox.Location = new Point(5, label.Location.Y + extraHeight);
            textBox.Text = "";
            textBox.TabIndex = 1;
            inputBox.Controls.Add(textBox);

            DarkButton okButton = new DarkButton();
            okButton.DialogResult = DialogResult.OK;
            okButton.BackColor = Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "OK";
            okButton.Location = new Point(size.Width - 80 - 80, size.Height - 30);
            inputBox.Controls.Add(okButton);

            DarkButton cancelButton = new DarkButton();
            cancelButton.BackColor = Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(size.Width - 80, size.Height - 30);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;

            return result;
        }
    }
}