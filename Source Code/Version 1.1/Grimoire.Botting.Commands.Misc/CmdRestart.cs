using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdRestart : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            instance.Index = -1;
        }

        public override string ToString()
        {
            return "Restart bot";
        }
    }
}