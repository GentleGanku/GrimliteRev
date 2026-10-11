using Grimoire.Game;
using Grimoire.UI;
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

        private string _x => (Type == "Random") ? new Random().Next(150, 700).ToString() : BotManager.Instance.ActiveBotEngine.Value(X);

        private string _y => (Type == "Random") ? new Random().Next(320, 450).ToString() : BotManager.Instance.ActiveBotEngine.Value(Y);

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Others;
            Player.WalkToPoint(_x, _y);
            await instance.WaitUntil(delegate
            {
                float[] position = Player.Position;
                return position[0].ToString() == X && position[1].ToString() == Y;
            }, null, 6, 500);
        }

        public override string ToString()
        {
            return (Type == "Random") ? "Walk randomly" : "Walk to: " + X + ", " + Y;
        }
    }
}