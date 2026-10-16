using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraStringValIsNot : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdPlayerAuraStringValIsNot()
        {
            Tag = "Player Aura";
            Text = "Player Aura's value is not (string)";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.isAuraWithStrValActive("Self", instance.Value(Value1), instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura's value is not (string): {Value1}, {Value2}";
        }

        #endregion
    }
}