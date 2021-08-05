using System.Linq;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Botting;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdDelay : IBotCommand
    {
        public int Delay
        {
            get;
            set;
        }

        private readonly int[] badnumbers = new int[]
        {
            420,
            69,
            666,
            6969,
            1337,
            1111,
            2222,
            3333,
            4444,
            5555,
            6666,
            7777,
            8888,
            9999,
            10000,
        };

        public async Task Execute(IBotEngine instance)
        {
            await Task.Delay(Delay);
        }

        public override string ToString()
        {
            return "Delay: " + (badnumbers.Contains(Delay) ? Delay + 1 : Delay);
        }
    }
}