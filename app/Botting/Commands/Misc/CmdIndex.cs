using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdIndex : IBotCommand
    {
        public enum IndexCommand
        {
            Up,
            Down,
            Goto
        }

        public IndexCommand Type { get; set; }

        public int Index { get; set; }

        public Task Execute(IBotEngine instance)
        {
            switch (Type)
            {
                case IndexCommand.Down:
                    {
                        int offset = Index - 1;
                        if (offset > 0)
                        {
                            int newIndex = instance.Index += offset;
                            if (newIndex < instance.Configuration.Commands.Count)
                            {
                                instance.Index = newIndex;
                            }
                        }
                        break;
                    }
                case IndexCommand.Up:
                    {
                        int offset = Index + 1;
                        if (offset > 0)
                        {
                            int newIndex = instance.Index -= offset;
                            if (newIndex > -1)
                            {
                                instance.Index = newIndex;
                            }
                        }
                        break;
                    }
                case IndexCommand.Goto:
                    {
                        int target = Index - 1;
                        if (target > 0)
                            instance.Index = target;
                        break;
                    }
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            switch (Type)
            {
                case IndexCommand.Down:
                    return $"Index down: {Index}";

                case IndexCommand.Up:
                    return $"Index up: {Index}";

                default:
                    return $"Goto index: {Index}";
            }
        }
    }
}