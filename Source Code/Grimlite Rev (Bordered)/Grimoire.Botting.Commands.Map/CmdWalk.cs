using Grimoire.Game;
using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Map
{
    public class CmdWalk : IBotCommand
    {
        public string X
        {
            get;
            set;
        }

        public string Y
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            if (Type == "Random")
            {
                Player.WalkToPoint(y: new Random().Next(320, 450).ToString(), x: new Random().Next(150, 700).ToString());
                await Task.Delay(1000);
            }
            else
            {
                Player.WalkToPoint(X, Y);
                await instance.WaitUntil(delegate
                {
                    float[] position = Player.Position;
                    return position[0].ToString() == X && position[1].ToString() == Y;
                });
            }
        }

        public override string ToString()
        {
            if (Type == "Random")
            {
                return "Walk Randomly";
            }
            return "Walk to: " + X + ", " + Y;
        }
    }
}