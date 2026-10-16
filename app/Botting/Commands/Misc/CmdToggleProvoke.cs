using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdToggleProvoke : IBotCommand
    {
        public int Type
        {
            get;
            set;
        }

        public string ProvokePacket
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            switch (Type)
            {
                case 0:
                    OptionsManager.ProvokeMonsters = false;
                    OptionsManager.ProvokePacket = null;
                    Root.Instance.provokeToolStripMenuItem1.Checked = false;
                    BotManager.Instance.chkProvoke.Checked = false;
                    break;
                case 1:
                    OptionsManager.ProvokeMonsters = true;
                    OptionsManager.ProvokePacket = ProvokePacket;
                    Root.Instance.provokeToolStripMenuItem1.Checked = true;
                    BotManager.Instance.chkProvoke.Checked = true;
                    break;
                default:
                    OptionsManager.ProvokeMonsters = !OptionsManager.ProvokeMonsters;
                    break;
            }
        }

        public override string ToString()
        {
            switch (Type)
            {
                case 0:
                    return "Provoke in cell: Off";
                case 1:
                    return string.IsNullOrEmpty(ProvokePacket) ? "Provoke in cell: On" : "Provoke in cell: On (custom)";
                default:
                    return "Provoke in cell: Toggle";
            }
        }
    }
}