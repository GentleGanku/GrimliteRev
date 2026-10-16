using Grimoire.Game;
using Grimoire.Networking;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdPacket2 : RegularExpression, IBotCommand
    {
        #region Fields

        private string _packet;

        #endregion

        #region Properties

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

        #endregion

        #region Methods

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

        #endregion
    }
}
