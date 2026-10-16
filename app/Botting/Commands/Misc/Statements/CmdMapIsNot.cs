using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMapIsNot : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdMapIsNot()
        {
            Tag = "Map";
            Text = "Map is not";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.Map.Equals(instance.Value(Value1).Contains("-") ? instance.Value(Value1).Split('-')[0] : instance.Value(Value1), StringComparison.OrdinalIgnoreCase))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Map is not: " + Value1;
        }

        #endregion
    }
}