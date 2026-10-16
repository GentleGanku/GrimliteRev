using System.Drawing;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdBlank3 : IBotCommand
    {
        #region Properties

        public string Text
        {
            get;
            set;
        }

        public int Alpha
        {
            get;
            set;
        }

        public int R
        {
            get;
            set;
        }

        public int G
        {
            get;
            set;
        }

        public int B
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
        }

        public Color Argb()
        {
            return Color.FromArgb(Alpha, R, G, B);
        }

        public override string ToString()
        {
            return $"{Text}";
        }

        #endregion
    }
}
