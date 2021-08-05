using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdSkillSet : IBotCommand
    {
        public string Name
        {
            get;
            set;
        }

        public Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Combat;
            BotData.BotSkill = Name;
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Skill Set: " + Name;
        }
    }
}