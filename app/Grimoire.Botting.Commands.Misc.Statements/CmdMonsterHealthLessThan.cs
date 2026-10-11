using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMonsterHealthLessThan : StatementCommand, IBotCommand
    {
        public CmdMonsterHealthLessThan()
        {
            Tag = "Monster";
            Text = "Monster's health is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (World.MonsterHealth(instance.Value(Value1)) >= int.Parse(instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Monster's health is less than: {Value1}, {Value2}";
        }
    }
}
