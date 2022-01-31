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
            var MonsterName = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            var MonsterHealth = instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2;
            if (World.MonsterHealth(MonsterName) > int.Parse(MonsterHealth))
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
