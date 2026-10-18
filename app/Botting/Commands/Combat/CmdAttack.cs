using Grimoire.Game;
using Grimoire.Utils;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdAttack : IBotCommand
    {
        #region Fields

        private string _monster;

        #endregion

        #region Properties

        public string Monster
        {
            get
            {
                return _monster;
            }
            set
            {
                _monster = value.UnifyQuotes();
            }
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            Player.AttackMonster(instance.Value(Monster));
        }

        public override string ToString()
        {
            return "Attack: " + Monster;
        }

        #endregion
    }
}