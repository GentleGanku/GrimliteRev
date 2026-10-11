using Grimoire.Game;
using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdToggleProvokeInMap : IBotCommand
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
                    OptionsManager.ProvokeAllMonster = false;
                    Root.Instance.provokeAllMonsterInMapToolStripMenuItem.Checked = false;
                    BotManager.Instance.chkProvokeAllMon.Checked = false;
                    await BotUtilities.MoveOutOfCombat();
                    break;
                case 1:
                    OptionsManager.ProvokeAllMonster = true;
                    Root.Instance.provokeAllMonsterInMapToolStripMenuItem.Checked = true;
                    BotManager.Instance.chkProvokeAllMon.Checked = true;
                    break;
                default:
                    OptionsManager.ProvokeAllMonster = !OptionsManager.ProvokeAllMonster;
                    break;
            }
            OptionsManager.ProvokePacket = ProvokePacket;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case 0:
                    return "Provoke in map: Off";
                case 1:
                    return "Provoke in map: On";
                default:
                    return "Provoke in map: Toggle";
            }
        }
    }
}