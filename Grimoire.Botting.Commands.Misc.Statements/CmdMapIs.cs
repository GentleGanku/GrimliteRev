using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMapIs : StatementCommand, IBotCommand
    {
        public CmdMapIs()
        {
            Tag = "Map";
            Text = "Map is";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!((instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1).Contains("-") ? (instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1).Split('-')[0] : (instance.IsVar(Value1)  ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1)).Equals(Player.Map, StringComparison.OrdinalIgnoreCase))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Map is: " + Value1;
        }
    }
}