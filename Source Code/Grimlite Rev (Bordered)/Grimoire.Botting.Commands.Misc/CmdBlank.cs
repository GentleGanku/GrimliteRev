using System.Drawing;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdBlank : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
        }

        public override string ToString()
        {
            return "";
        }
    }

    public class CmdBlank2 : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
        }

        public string Text
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"{Text}";
        }
    }

    public class CmdBlank3 : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
        }

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

        public Color Argb()
        {
            return Color.FromArgb(Alpha, R, G, B);
        }

        public override string ToString()
        {
            return $"{Text}";
        }
    }
}