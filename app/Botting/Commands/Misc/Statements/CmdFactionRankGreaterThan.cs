using Grimoire.Game;
using Grimoire.Game.Data;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdFactionRankGreaterThan : StatementCommand, IBotCommand
    {
        public CmdFactionRankGreaterThan()
        {
            Tag = "This player";
            Text = "Faction Rank is greater than";
        }

        public Task Execute(IBotEngine instance)
        {
            if ((Player.Factions.Find((Faction m) => m.Name == (instance.Value(Value1))) ?? new Faction()).Rank <= int.Parse(instance.Value(Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Faction Rank is greater than: {Value1}, {Value2}";
        }
    }
}