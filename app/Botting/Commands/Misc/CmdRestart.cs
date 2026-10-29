using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdRestart : IBotCommand
    {
        #region Methods

        /// <summary>
        /// Goes back to the first command. The bot adds 1 to the index after every command, so -1 makes the next one
        /// command 0, as Load bot does.
        /// </summary>
        public async Task Execute(IBotEngine instance)
        {
            instance.Index = -1;
            return;
        }

        public override string ToString()
        {
            return "Restart bot";
        }

        #endregion
    }
}