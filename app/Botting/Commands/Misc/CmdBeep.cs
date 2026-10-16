using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdBeep : IBotCommand
    {
        #region Properties

        public int Times
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            for (int i = 0; i < Times; i++)
            {
                Console.Beep();
            }
        }

        public override string ToString()
        {
            return string.Format("Beep {0} Times ", Times.ToString());
        }

        #endregion
    }
}