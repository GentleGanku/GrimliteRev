using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Utils;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdListInInv : StatementCommand, IBotCommand
    {
        public CmdListInInv()
        {
            Tag = "Item";
            Text = "All in List is in inventory";
        }

        public Task Execute(IBotEngine instance)
        {
            if (Configuration.Instance.Items.TrueForAll(x => Player.Inventory.ContainsItem(x, "1")))
            {

            }
            else
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"All in List is in inventory";
        }
    }
}