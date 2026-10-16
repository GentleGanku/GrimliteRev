using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdRest : IBotCommand
    {
        #region Properties

        public bool Full
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Rest;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Rest), () => instance.IsRunning && Player.IsLoggedIn, 10, 500);
            if (instance.Configuration.ExitCombatBeforeRest)
            {
                await BotUtilities.MoveOutOfCombat();
            }
            Player.Rest();
            if (Full)
            {
                await instance.WaitUntil(() => Player.Mana >= Player.ManaMax && Player.Health >= Player.HealthMax, null, 30, 500);
            }
        }

        public override string ToString()
        {
            return Full ? "Rest fully" : "Rest";
        }

        #endregion
    }
}