using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdMonsterNotInRoom : StatementCommand, IBotCommand
    {
        #region Constructors

        public CmdMonsterNotInRoom()
        {
            Tag = "Monster";
            Text = "Is not in room";
        }

        #endregion

        #region Methods

        public Task Execute(IBotEngine instance)
        {
            if (World.IsMonsterAvailable(instance.Value(Value1)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Monster is not in room: " + Value1;
        }

        #endregion
    }
}