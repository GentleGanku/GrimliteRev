using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetAuraIsNotActive : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdTargetAuraIsNotActive()
        {
            Tag = "Target Aura";
            Text = "Target Aura is not active";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (Player.isAuraActive("Target", instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target Aura is not active: {Value1}";
        }

        #endregion
    }
}