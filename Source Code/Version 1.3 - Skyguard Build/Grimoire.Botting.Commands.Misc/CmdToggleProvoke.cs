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

        public async Task Execute(IBotEngine instance)
        {
            switch (Type)
            {
                case 0:
                    OptionsManager.ProvokeMonsters = false;
                    Root.Instance.provokeToolStripMenuItem1.Checked = false;
                    BotManager.Instance.chkProvoke.Checked = false;
                    break;
                case 1:
                    OptionsManager.ProvokeMonsters = true;
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
                    return "Provoke in cell: On";
                default:
                    return "Provoke in cell: Toggle";
            }
        }
    }
}