using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdToggleProvoke : IBotCommand
    {
        #region Properties

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

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            switch (Type)
            {
                case 0:
                    OptionsManager.ProvokePacket = null;
                    BotManager.Instance.chkProvoke.Checked = false;
                    break;
                case 1:
                    OptionsManager.ProvokePacket = ProvokePacket;
                    BotManager.Instance.chkProvoke.Checked = true;
                    break;
                default:
                    BotManager.Instance.chkProvoke.Checked = !BotManager.Instance.chkProvoke.Checked;
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

        #endregion
    }
}