using System;
using System.Windows.Forms;
using System.Drawing;
using DarkUI.Forms;
using DarkUI.Controls;
using Grimoire.UI;

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
            //This function creates the custom input dialog box by individually creating the different window elements and adding them to the dialog box

            //Specify the size of the window using the parameters passed
            Size size = new Size(width, height);
            //Create a new form using a System.Windows Form
            DarkForm inputBox = new DarkForm();

            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.TopMost = true;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            //Set the window title using the parameter passed
            inputBox.Text = title;

            //Create a new label to hold the prompt
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

            //Create a textbox to accept the user's input
            DarkTextBox textBox = new DarkTextBox();
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Size = new Size(size.Width - 10, 23);
            textBox.Location = new Point(5, label.Location.Y + extraHeight);
            textBox.Text = "";
            textBox.TabIndex = 1;
            inputBox.Controls.Add(textBox);

            //Create an OK Button 
            DarkButton okButton = new DarkButton();
            okButton.DialogResult = DialogResult.OK;
            okButton.BackColor = Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "OK";
            okButton.Location = new Point(size.Width - 80 - 80, size.Height - 30);
            inputBox.Controls.Add(okButton);

            //Create a Cancel Button
            DarkButton cancelButton = new DarkButton();
            cancelButton.BackColor = Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "Cancel";
            cancelButton.Location = new Point(size.Width - 80, size.Height - 30);
            inputBox.Controls.Add(cancelButton);

            //Set the input box's buttons to the created OK and Cancel Buttons respectively so the window appropriately behaves with the button clicks
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            //Show the window dialog box 
            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;

            //After input has been submitted, return the input value
            return result;
        }
    }
}