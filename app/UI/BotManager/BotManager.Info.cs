using DarkUI.Forms;
using Grimoire.Tools;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rtbInfo.Rtf = richTextBox2.Text;
                return;

                var currDir = System.AppDomain.CurrentDomain.BaseDirectory;
                var tempDoc = $@"{currDir}/tempdoc";
                var target = $@"{currDir}/temprtf";
                if (File.Exists(tempDoc))
                {
                    File.Delete(tempDoc);
                }
                using (FileStream newFile = File.Create(tempDoc))
                {
                    byte[] toWrite = new UTF8Encoding(true).GetBytes(richTextBox2.Text);
                    newFile.Write(toWrite, 0, toWrite.Length);
                }
                var tempRtf = "";
                Task.WaitAll(Task.Run(() => DocConvert.toRtf(tempDoc, target, out tempRtf)));
                rtbInfo.Rtf = File.ReadAllText(tempRtf);
            }
            catch { };
        }

        private void rtbInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void lblGuideDescription_Click(object sender, EventArgs e)
        {
            DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
            "Do the steps in order:\r\n\r\n" +
            "1) Create and edit an RTF file with the WordPad application or any RTF editor.\r\n" +
            "2) Type anything you wish for the information panel. You can also insert links, images, and shapes.\r\n" +
            "3) Save the file as an RTF file (.rtf).\r\n" +
            "4) Open the file with the Notepad/Notepad++ application. You will then see the raw content in the file.\r\n" +
            "5) Copy the entire content inside the file.\r\n" +
            "6) Paste the content copied from the file into the Description (RTF) box in the bot manager.\r\n" +
            "7) Save the bot file.\r\n" +
            "8) Load the bot file and the information panel will be loaded with the saved content. You will see it on the Info tab in the bot manager.", "Guide on Setting Up The Description", MessageBoxIcon.Information);
        }

        #endregion
    }
}
