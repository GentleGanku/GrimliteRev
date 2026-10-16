using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdTargetAuraIsActive : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdTargetAuraIsActive()
        {
            Tag = "Target Aura";
            Text = "Target Aura is active";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Player.isAuraActive("Target", instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Target Aura is active: {Value1}";
        }

        #endregion
    }
}