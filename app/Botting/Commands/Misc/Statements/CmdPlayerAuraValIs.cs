using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraValIs : StatementCommand, IBotCommand
    {
        public CmdPlayerAuraValIs()
        {
            Tag = "Player Aura";
            Text = "Player Aura's value is";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.auraComparison("Self", "Equal", instance.Value(Value1), int.Parse(instance.Value(Value2))))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura's value is: {Value1}, {Value2}";
        }
    }
}