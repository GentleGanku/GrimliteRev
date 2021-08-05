using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
    public class CmdYulgar : IBotCommand
    {
        public async Task Execute(IBotEngine instance)
        {
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
            if (Player.CurrentState == Player.State.InCombat)
            {
                Player.MoveToCell(Player.Cell, Player.Pad);
                await Task.Delay(1250);
            }
            if (!Player.Map.Equals("yulgar", StringComparison.OrdinalIgnoreCase))
            {
                Player.JoinMap("yulgar", "Enter", "Spawn");
                await instance.WaitUntil(() => Player.Map.Equals("yulgar", StringComparison.OrdinalIgnoreCase));
                await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
            }
            Player.WalkToPoint(y: new Random().Next(320, 450).ToString(), x: new Random().Next(150, 700).ToString());
            instance.Stop();
        }

        public override string ToString()
        {
            return string.Concat("Join yulgar");
        }
    }
}