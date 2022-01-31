using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraIsActive : StatementCommand, IBotCommand
    {
        public CmdPlayerAuraIsActive()
        {
            Tag = "Player Aura";
            Text = "Player Aura is active";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.isAuraActive("Self", instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura is active: {Value1}";
        }
    }
}