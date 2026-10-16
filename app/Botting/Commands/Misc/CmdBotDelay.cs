using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdBotDelay : IBotCommand
    {
        #region Properties

        public int Delay
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            instance.Configuration.BotDelay = Delay;
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Set bot delay: {Delay}";
        }

        #endregion
    }

    public class CmdBotDelay2 : IBotCommand
    {
        #region Properties

        public string Delay
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            instance.Configuration.BotDelay = int.Parse(instance.Value(Delay));
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Set bot delay: {Delay}";
        }

        #endregion
    }
}