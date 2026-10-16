using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdSkillSet : IBotCommand
    {
        public string Name { get; set; }

        public Task Execute(IBotEngine instance)
        {
            BotData.BotSkill = instance.Value(Name).ToUpper();
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Use skill set: " + Name;
        }
    }
}