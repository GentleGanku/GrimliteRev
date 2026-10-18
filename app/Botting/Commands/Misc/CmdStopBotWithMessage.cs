using DarkUI.Forms;
using Grimoire.UI;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdStopBotWithMessage : IBotCommand
    {
        #region Properties

        public string Message
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotManager.Instance.btnBotStart.Enabled = false;
            Root.Instance.stopToolStripMenuItem.Enabled = false;
            BotManager.Instance.ActiveBotEngine.Stop();
            BotManager.Instance.CustomCommandToggle(true);
            BotManager.Instance.SelectionModeToggle(false);
            BotManager.Instance.BotStateChanged(IsRunning: false);
            await Task.Delay(2000);
            Root.Instance.BotStateChanged(IsRunning: false);
            BotManager.Instance.UpdateStartEnabled();
            DarkMessageBox.Show
            (
                new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                $"{(string.IsNullOrWhiteSpace(Message) ? "Bot has stopped!" : Message)}", "Bot has stopped!", MessageBoxIcon.Information
            );
        }

        public override string ToString()
        {
            return "Stop bot with message";
        }

        #endregion
    }
}