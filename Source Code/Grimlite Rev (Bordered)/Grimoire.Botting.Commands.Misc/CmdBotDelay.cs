using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdBotDelay : IBotCommand
    {
        public int Delay
        {
            get;
            set;
        }

        public Task Execute(IBotEngine instance)
        {
            instance.Configuration.BotDelay = Delay;
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Set bot delay: {Delay}";
        }
    }
}