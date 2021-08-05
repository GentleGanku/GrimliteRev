using System;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdInt : IBotCommand
    {
        public enum Types
        {
            Set,
            Upper,
            Lower
        }

        public Types type
        {
            get;
            set;
        }

        public string Int
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

        public Task Execute(IBotEngine instance)
        {
            switch (type)
            {
                case Types.Set:
                    if (Configuration.Tempvalues.ContainsKey(Int))
                        Configuration.Tempvalues[Int] = Value;
                    else
                        Configuration.Tempvalues.Add(Int, Value);
                    break;
                case Types.Upper:
                    Configuration.Tempvalues[Int]++;
                    break;
                case Types.Lower:
                    Configuration.Tempvalues[Int]--;
                    break;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            switch (type)
            {
                case Types.Set:
                    return $"Set {Int}: {Value}";
                case Types.Upper:
                    return $"Increase {Int} by 1";
                default:
                    return $"Decrease {Int} by 1";
            }
        }
    }
}