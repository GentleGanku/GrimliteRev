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
            string packet;

            if (IsVar(Packet))
            {
                packet = Configuration.Tempvariable[GetVar(Packet)];
            }
            else
            {
                packet = Packet;
            }
            packet = packet.Replace("{ROOM_ID}", World.RoomId.ToString()).Replace("{ROOM_NUMBER}", World.RoomNumber.ToString()).Replace("PLAYERNAME", Player.Username);
            packet = packet.Replace("{GETMAP}", Player.Map);
            while (packet.Contains("--"))
            {
                packet = new Regex("-{1,}", RegexOptions.IgnoreCase).Replace(packet, (Match m) => "-");
            }
            packet = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(packet, (Match m) => new Random().Next(1001, 99999).ToString());
            if (Client)
            {
                await Proxy.Instance.SendToClient(packet);
            }
            else
            {
                await Proxy.Instance.SendToServer(packet);
            }
            if (packet.Contains("%xt%zm%gar%"))
            {
                await Task.Delay(700);
            }
            else
            {
                await Task.Delay(2000);
            }
        }

        public override string ToString()
        {
            return (Client ? "Send client packet: " : "Send server packet: ") + Packet;
        }
    }

    public class CmdPacket2 : RegularExpression, IBotCommand
    {
        public string Packet
        {
            get;
            set;
        }

        public int Delay
        {
            get;
            set;
        }

        public bool Client
        {
            get;
            set;
        }

        private string _packet;

        public async Task Execute(IBotEngine instance)
        {
            _packet = instance.Value(Packet).Replace("{ROOM_ID}", World.RoomId.ToString()).Replace("{ROOM_NUMBER}", World.RoomNumber.ToString()).Replace("PLAYERNAME", Player.Username).Replace("{GETMAP}", Player.Map);
            _packet = new Regex("(1e)[0-9]{1,}", RegexOptions.IgnoreCase).Replace(_packet, (Match m) => new Random().Next(1001, 99999).ToString());
            await (Client ? Proxy.Instance.SendToClient(_packet) : Proxy.Instance.SendToServer(_packet));
            await Task.Delay(Delay);
        }

        public override string ToString()
        {
            return (Client ? "Send packet to client: " : "Send packet to server: ") + Packet;
        }
    }

}