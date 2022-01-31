using Grimoire.Game;
using Grimoire.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdPacketlist : IBotCommand
    {
        public enum state
        {
            On,
            Off,
            Add,
            Remove,
            Clear,
            SetDelay
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

        public state State
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            var packetName = instance.IsVar(Packet) ? Configuration.Tempvariable[instance.GetVar(Packet)] : Packet;
            bool on = Bot.SpamPackets;
            switch (State)
            {
                case state.On:
                    on = true;
                    Bot.SpamPackets = on;
                    break;

                case state.Off:
                    on = false;
                    Bot.SpamPackets = on;
                    break;

                case state.Add:
                    BotManager.Instance.AddPacket(packetName);
                    instance.Configuration.Packets.Add(packetName);
                    await instance.WaitUntil(() => instance.Configuration.Packets.Contains(packetName));
                    Configuration.Instance.Packets = new List<string>();
                    break;

                case state.Remove:
                    BotManager.Instance.RemovePacket(packetName);
                    instance.Configuration.Packets.Remove(packetName);
                    await instance.WaitUntil(() => !instance.Configuration.Packets.Contains(packetName));
                    Configuration.Instance.Packets = new List<string>();
                    break;

                case state.Clear:
                    BotManager.Instance.lstPackets.Items.Clear();
                    instance.Configuration.Packets.Clear();
                    await instance.WaitUntil(() => instance.Configuration.Packets.Contains(null));
                    Configuration.Instance.Packets = new List<string>();
                    break;

                case state.SetDelay:
                    Delay = instance.Configuration.PacketDelay;
                    break;
            }
        }

        public override string ToString()
        {
            switch (State)
            {
                case state.On:
                    return "Packetlist On";

                case state.Off:
                    return "Packetlist Off";

                case state.Add:
                    return "Add to Packetlist: " + Packet;

                case state.Remove:
                    return "Remove from Packetlist: " + Packet;

                case state.Clear:
                    return "Clear Packetlist";

                case state.SetDelay:
                    return "Set Packetlist's Delay to: " + Delay;
            }
            return "Packetlist";
        }
    }

    public class CmdPacketlist2 : IBotCommand
    {
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

        public async Task Execute(IBotEngine instance)
        {
            switch (Type)
            {
                case 0:
                    OptionsManager.PacketSpam = false;
                    Player.MoveToCell(Player.Cell, Player.Pad);
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
    }
}