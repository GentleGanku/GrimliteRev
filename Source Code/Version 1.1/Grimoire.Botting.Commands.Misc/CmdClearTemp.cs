using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdClearTemp : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            Configuration.Tempvalues.Clear();
            Configuration.Tempvariable.Clear();
        }

        public override string ToString()
        {
            return "Clear TempVar";
        }
    }
}