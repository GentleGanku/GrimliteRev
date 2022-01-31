using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraIsNotActive : StatementCommand, IBotCommand
    {
        public CmdPlayerAuraIsNotActive()
        {
            Tag = "Player Aura";
            Text = "Player Aura is not active";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.isAuraActive("Self", instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura is not active: {Value1}";
        }
    }
}