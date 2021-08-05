using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdRest : IBotCommand
    {
        public bool Full
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Rest;
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Rest), () => instance.IsRunning && Player.IsLoggedIn);
            if (instance.Configuration.ExitCombatBeforeRest)
            {
                while (Player.CurrentState == Player.State.InCombat)
                {
                    Player.MoveToCell(Player.Cell, Player.Pad);
                    await Task.Delay(1250);
                }
            }
            Player.Rest();
            if (Full)
            {
                await instance.WaitUntil(() => Player.Mana >= Player.ManaMax && Player.Health >= Player.HealthMax);
            }
        }

        public override string ToString()
        {
            if (!Full)
            {
                return "Rest";
            }
            return "Rest fully";
        }
    }
}