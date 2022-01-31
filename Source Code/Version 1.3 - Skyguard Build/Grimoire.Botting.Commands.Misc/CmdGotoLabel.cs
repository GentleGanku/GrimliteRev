using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdGotoLabel : IBotCommand
    {
        public string Label
        {
            get;
            set;
        }

        public Task Execute(IBotEngine instance)
        {
            int num = instance.Configuration.Commands.FindIndex((IBotCommand c) => c is CmdLabel && ((CmdLabel)c).Name.Equals((instance.IsVar(Label) ? Configuration.Tempvariable[instance.GetVar(Label)] : Label), StringComparison.OrdinalIgnoreCase));
            if (num > -1)
            {
                instance.Index = num;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Goto label: " + Label;
        }
    }
}