using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdLabel : IBotCommand
    {
        #region Properties

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "[" + Name.ToUpper() + "]";
        }

        #endregion
    }
}