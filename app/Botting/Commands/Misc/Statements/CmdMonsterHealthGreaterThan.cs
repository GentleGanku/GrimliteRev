using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMonsterHealthGreaterThan : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdMonsterHealthGreaterThan()
        {
            Tag = "Monster";
            Text = "Monster's health is greater than";
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            if (World.MonsterHealth(instance.Value(Value1)) <= int.Parse(instance.Value(Value2)))
            {
                instance.Index++;
            }
        }

        public override string ToString()
        {
            return $"Monster's health is greater than: {Value1}, {Value2}";
        }

        #endregion
    }
}
