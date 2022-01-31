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
            bool on = OptionsManager.ProvokeMonsters;
            switch (Type)
            {
                case 0:
                    on = false;
                    break;
                case 1:
                    on = true;
                    break;
                default:
                    on = !on;
                    break;
            }
            OptionsManager.ProvokeMonsters = on;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case 0:
                    return "Provoke Off";
                case 1:
                    return "Provoke On";
                default:
                    return "Provoke Toggle";
            }
        }
    }
}