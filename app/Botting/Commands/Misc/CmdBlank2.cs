using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdBlank2 : IBotCommand
    {
        #region Properties

        public string Text
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
        }

        public override string ToString()
        {
            return $"{Text}";
        }

        #endregion
    }
}
