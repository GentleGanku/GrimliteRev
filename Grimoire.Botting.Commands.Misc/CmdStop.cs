using Grimoire.UI;
using System.Threading.Tasks;
namespace Grimoire.Botting.Commands.Misc
{
    public class CmdStop : IBotCommand
    {
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
            return;
        }

        public override string ToString()
        {
            return "Stop bot";
        }
    }
}