using Grimoire.UI;
using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsNotInCell : StatementCommand, IBotCommand
    {
        public CmdPlayerIsNotInCell()
        {
            Tag = "Player";
            Text = "Player is not in cell";
        }

        private string _cell
        {
            get
            {
                return BotManager.Instance.ActiveBotEngine.Value(Value2);
            }
        }

        public Task Execute(IBotEngine instance)
        {
            try
            {
                if (World.AvatarsInCell(_cell).FirstOrDefault((Avatar p) => p.Name.Equals(instance.Value(Value1), StringComparison.OrdinalIgnoreCase)) != null)
                {
                    instance.Index++;
                }
            }
            catch
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player is not in {Value2}: " + Value1;
        }
    }
}