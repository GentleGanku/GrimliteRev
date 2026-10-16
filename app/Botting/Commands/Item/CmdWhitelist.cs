using Grimoire.UI;
using System.Threading.Tasks;


namespace Grimoire.Botting.Commands.Item
{
    public class CmdWhitelist : IBotCommand
    {
        public enum state
        {
            On,
            Off,
            Clear,
            Add,
            Remove
        }

        public string Item { get; set; }

        public state State { get; set; }

        public async Task Execute(IBotEngine instance)
        {
            switch (State)
            {
                case state.On:
                    instance.Configuration.EnablePickup = true;
                    BotManager.Instance.chkPickup.Checked = true;
                    break;
                case state.Off:
                    instance.Configuration.EnablePickup = false;
                    BotManager.Instance.chkPickup.Checked = false;
                    break;
                case state.Clear:
                    BotManager.Instance.lstDrops.Items.Clear();
                    instance.Configuration.Drops.Clear();
                    break;
                case state.Add:
                    BotManager.Instance.AddDrop(instance.Value(Item), true);
                    break;
                case state.Remove:
                    BotManager.Instance.RemoveDrop(instance.Value(Item), true);
                    break;
            }
        }

        public override string ToString()
        {
            switch (State)
            {
                case state.On:
                    return "Drop list: On";
                    break;
                case state.Off:
                    return "Drop list: Off";
                    break;
                case state.Add:
                    return $"Add to Drop list: {Item}";
                    break;
                case state.Remove:
                    return $"Remove from Drop list: {Item}";
                    break;
                case state.Clear:
                    return "Clear Drop list";
            }
            return "Drop list";
        }
    }
}