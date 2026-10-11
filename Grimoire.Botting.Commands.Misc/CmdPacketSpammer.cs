using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdPacketSpammer : RegularExpression, IBotCommand
    {
        public string Packet
        {
            get;
            set;
        }
        
        public PacketSpammer Spammer
        {
            get;
            set;
        }

        public enum CommandType
        {
            Add,
            Remove,
            Start,
            Stop,
            Clear,
            Restart,
            SetDelay,
            GoPressTheGodDamnCorrectButton
        }

        public CommandType type
        {
            get;
            set;
        }

        public int Delay
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            switch (type)
            {
                case CommandType.Add:
                    Spammer.lstPackets.Items.Add(Packet);
                    break;

                case CommandType.Remove:
                    Spammer.lstPackets.Items.Remove(Packet);
                    break;

                case CommandType.Clear:
                    Spammer.btnClear.PerformClick();
                    break;

                case CommandType.Start:
                    Spammer.btnStart.PerformClick();
                    break;

                case CommandType.Stop:
                    Spammer.btnStop.PerformClick();
                    break;

                case CommandType.Restart:
                    Spammer.btnStop.PerformClick();
                    Spammer.btnStart.PerformClick();
                    break;

                case CommandType.SetDelay:
                    Spammer.numDelay.Value = Delay;
                    break;
            }
        }

        public override string ToString()
        {
            switch (type)
            {
                case CommandType.Add:
                    return "Add to P.S: " + Packet;

                case CommandType.Remove:
                    return "Remove from P.S: " + Packet;
                    
                case CommandType.SetDelay:
                    return "Set P.S. Delay to: " + Delay;
            }
            return type.ToString() + " P.S.";
        }
    }
}