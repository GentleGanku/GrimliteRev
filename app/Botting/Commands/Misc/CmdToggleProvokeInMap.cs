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