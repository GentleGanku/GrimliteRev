using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMonsterHealthGreaterThan : StatementCommand, IBotCommand
    {
        public CmdMonsterHealthGreaterThan()
        {
            Tag = "Monster";
            Text = "Monster's health is greater than";
        }

        public async Task Execute(IBotEngine instance)
        {
            var MonsterName = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            var MonsterHealth = instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2;
            if (World.MonsterHealth(MonsterName) < int.Parse(MonsterHealth))
            {
                instance.Index++;
            }
        }

        public override string ToString()
        {
            return $"Monster's health is greater than: {Value1}, {Value2}";
        }
    }
}
