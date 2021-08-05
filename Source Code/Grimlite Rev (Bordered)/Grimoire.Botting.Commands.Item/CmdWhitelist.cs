using Grimoire.Game;
using Grimoire.UI;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Item
{
    public class CmdWhitelist : IBotCommand
    {
        public enum State
        {
            On,
            Off,
            Toggle,
            Add,
            Remove
        }

        public string ItemName
        {
            get;
            set;
        }

        public State state
        {
            get;
            set;
        }

        private bool ItemNameOrNot()
        {
            if (state == State.On || state == State.Off || state == State.Toggle) return false;
            else return true;
        }

        public async Task Execute(IBotEngine instance)
        {
            var Value1 = ItemName;
            switch (state)
            {
                case State.On:
                    instance.Configuration.EnablePickup = true;
                    break;
                case State.Off:
                    instance.Configuration.EnablePickup = true;
                    break;
                case State.Toggle:
                    instance.Configuration.EnablePickup = !instance.Configuration.EnablePickup;
                    break;
                case State.Add:
                    instance.Configuration.Drops.Add(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1); 
                    break;
                case State.Remove:
                    instance.Configuration.Drops.Remove(instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1);
                    break;
            }
        }

        public override string ToString()
        {
            return $"Whitelist {(ItemNameOrNot() ? $"{state.ToString()}: {ItemName}" : state.ToString())}";
        }
    }
}