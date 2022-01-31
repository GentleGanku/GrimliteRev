using Grimoire.Game;
using Grimoire.Networking;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdPacket : RegularExpression, IBotCommand
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

        public async Task Execute(IBotEngine instance)
        {
            string text;

            if ( IsVar(Packet) )
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
            text = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(text, (Match m) => new Random().Next(1001,99999).ToString());
            if (Client)
                await Proxy.Instance.SendToClient(text);
            else
                await Proxy.Instance.SendToServer(text);
            if (text.Contains("%xt%zm%gar%"))
                await Task.Delay(700);
            else
                await Task.Delay(2000);
        }

        public override string ToString()
        {
            return (Client ? "Send client packet: " : "Send packet: ") + Packet;
        }
    }
}