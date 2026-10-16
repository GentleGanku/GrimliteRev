using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdGotoLabel : IBotCommand
    {
        public string Label { get; set; }

        public Task Execute(IBotEngine instance)
        {
            int labelIndex = instance.Configuration.Commands.FindIndex((IBotCommand c) => c is CmdLabel && ((CmdLabel)c).Name.Equals(instance.Value(Label), StringComparison.OrdinalIgnoreCase));
            if (labelIndex > -1)
            {
                instance.Index = labelIndex;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Goto label: " + Label;
        }
    }
}