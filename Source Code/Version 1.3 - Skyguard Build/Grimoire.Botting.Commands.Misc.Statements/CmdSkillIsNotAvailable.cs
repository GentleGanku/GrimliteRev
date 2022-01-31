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
            var skillIndex = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            if (Player.CheckSkillAvailability(skillIndex))
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