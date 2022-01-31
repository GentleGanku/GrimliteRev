using Grimoire.Game;
using Grimoire.Networking;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdPacketDelay : RegularExpression, IBotCommand
    {
        public string Packet
        {
            get;
            set;
        }

        public bool Client
        {
            get;
            set;
        } = false;

        public int Delay
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            string text;

            if (IsVar(Packet))
            {
                text = Configuration.Tempvariable[GetVar(Packet)];
            }
            else
            {
                text = Packet;
            }

            text = text.Replace("{ROOM_ID}", World.RoomId.ToString()).Replace("{ROOM_NUMBER}", World.RoomNumber.ToString()).Replace("PLAYERNAME", Player.Username);
            text = text.Replace("{GETMAP}", Player.Map);
            while (text.Contains("--"))
            {
                text = new Regex("-{1,}", RegexOptions.IgnoreCase).Replace(text, (Match m) => "-");
            }
            text = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(text, (Match m) => new Random().Next(1001, 99999).ToString());
            if (Client)
            {
                await Task.Delay(Delay);
                await Proxy.Instance.SendToClient(text);
            }
            else
            {
                await Task.Delay(Delay);
                await Proxy.Instance.SendToServer(text);
            } 
            // commented because useless safemode delay 
            // if (text.Contains("%xt%zm%gar%"))
            //    await Task.Delay(700);

        }

        public override string ToString()
        {
            return (Client ? "Send delayed client packet: " : "Send delayed packet: ") + Packet;
        }
    }
}
