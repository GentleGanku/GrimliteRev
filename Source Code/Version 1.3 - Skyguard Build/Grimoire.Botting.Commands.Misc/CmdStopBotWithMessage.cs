using DarkUI.Forms;
using Grimoire.UI;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdStopBotWithMessage : IBotCommand
    {
        public string Message
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotManager.Instance.btnBotStart.Enabled = false;
            Root.Instance.stopToolStripMenuItem.Enabled = false;
            BotManager.Instance.ActiveBotEngine.Stop();
            BotManager.Instance.OnBankItemExecute();
            BotManager.Instance.CustomCommandToggle(true);
            BotManager.Instance.SelectionModeToggle(false);
            BotManager.Instance.BotStateChanged(IsRunning: false);
            await Task.Delay(2000);
            Root.Instance.BotStateChanged(IsRunning: false);
            BotManager.Instance.btnBotStart.Enabled = true;
            DarkMessageBox.Show
            (
                new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                $"{Message}", "Bot has stopped!", MessageBoxIcon.Information
            );
            return;
        }

        public override string ToString()
        {
            return "Stop bot with message";
        }
    }
}