using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdSkillIsNotAvailable : StatementCommand, IBotCommand
    {
        public CmdSkillIsNotAvailable()
        {
            Tag = "This player";
            Text = "Skill is not available";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Player.IsSkillAvailable(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Skill is not available:" + Value1;
        }
    }
}