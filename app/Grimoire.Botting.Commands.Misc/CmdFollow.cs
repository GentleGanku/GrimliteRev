using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdFollow : RegularExpression, IBotCommand
    {
        public string Name
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            string player = instance.IsVar(Name) ? Configuration.Tempvariable[instance.GetVar(Name)] : Name;
        }

        public override string ToString()
        {
            return "Follow player: " + Name;
        }
    }
}