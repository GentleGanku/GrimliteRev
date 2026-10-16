using Grimoire.Tools;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerEquippedClass : StatementCommand, IBotCommand
    {
        public CmdPlayerEquippedClass()
        {
            Tag = "Player";
            Text = "Player's equipped class is";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Flash.Call<bool>("CheckPlayerClass", instance.Value(Value1), instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player's equipped class is: {Value1}, {Value2}";
        }
    }
}
