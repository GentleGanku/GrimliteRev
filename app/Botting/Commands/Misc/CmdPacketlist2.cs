using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdPacketlist2 : IBotCommand
    {
        #region Properties

        public int Type
        {
            get;
            set;
        }

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

        #endregion

        #region Methods

        public async Task Execute(IBotEngine instance)
        {
            switch (Type)
            {
                case 0:
                    OptionsManager.PacketSpam = false;
                    break;
                case 1:
                    OptionsManager.PacketSpam = true;
                    break;
                default:
                    OptionsManager.PacketSpam = !OptionsManager.PacketSpam;
                    break;
            }
            OptionsManager.SpamPacket = Packet;
            OptionsManager.PacketDelay = Delay;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case 0:
                    return $"Spam packet: Off";
                case 1:
                    return $"Spam packet: On, {Packet}";
                default:
                    return "Spam packet";
            }
        }

        #endregion
    }
}