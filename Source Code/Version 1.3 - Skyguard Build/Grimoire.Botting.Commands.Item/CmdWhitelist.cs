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

        public string Item
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
            switch (State)
            {
                case state.On:
                    Configuration.Instance.EnablePickup = true;
                    instance.Configuration.EnablePickup = true;
                    BotManager.Instance.chkPickup.Checked = true;
                    break;
                case state.Off:
                    Configuration.Instance.EnablePickup = false;
                    instance.Configuration.EnablePickup = false;
                    BotManager.Instance.chkPickup.Checked = false;
                    break;
                case state.Clear:
                    for (int n = BotManager.Instance.lstDrops.Items.Count - 1; n >= 0; n--)
                    {
                        BotManager.Instance.lstDrops.Items.RemoveAt(n);
                        instance.Configuration.Drops.RemoveAt(n);
                    }
                    break;
                case state.Add:
                    BotManager.Instance.AddDrop(instance.IsVar(Item) ? Configuration.Tempvariable[instance.GetVar(Item)] : Item);
                    instance.Configuration.Drops.Add(instance.IsVar(Item) ? Configuration.Tempvariable[instance.GetVar(Item)] : Item);
                    break;
                case state.Remove:
                    BotManager.Instance.RemoveDrop(instance.IsVar(Item) ? Configuration.Tempvariable[instance.GetVar(Item)] : Item);
                    instance.Configuration.Drops.Remove(instance.IsVar(Item) ? Configuration.Tempvariable[instance.GetVar(Item)] : Item);
                    break;
            }
        }

        public override string ToString()
        {
            switch (State)
            {
                case state.On:
                    return "Whitelist: On";
                    break;
                case state.Off:
                    return "Whitelist: Off";
                    break;
                case state.Add:
                    return $"Add to whitelist: {Item}";
                    break;
                case state.Remove:
                    return $"Remove from whitelist: {Item}";
                    break;
                case state.Clear:
                    return "Clear whitelist";
            }
            return "Whitelist";
        }
    }
}