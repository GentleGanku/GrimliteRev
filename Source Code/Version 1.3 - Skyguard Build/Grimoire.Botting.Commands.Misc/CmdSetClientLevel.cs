using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdSetClientLevel : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            Task.Run(() => OptionsManager.SetClientLevel());
        }

        public override string ToString()
        {
            return "Set client level to max";
        }
    }
}