using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdSkillIsAvailable : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdSkillIsAvailable()
        {
            Tag = "This player";
            Text = "Skill is available";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (!Player.IsSkillAvailable(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Skill is available:" + Value1;
        }

        #endregion
    }
}