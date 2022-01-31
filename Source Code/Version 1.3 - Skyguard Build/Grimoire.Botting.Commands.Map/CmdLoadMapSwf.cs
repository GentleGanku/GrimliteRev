using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdLoadMapSwf : IBotCommand
    {
        public string Name
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            if (Name == "" || Name == "Map filename (.swf)")
            {
                World.GameMessage("Are you retarded?");
                return;
            }
            await instance.WaitUntil(() => World.IsActionAvailable(LockActions.Transfer));
            Player.LoadMap(instance.IsVar(Name) ? Configuration.Tempvariable[instance.GetVar(Name)] : Name);
            await instance.WaitUntil(() => !World.IsMapLoading, null, 40);
        }

        public override string ToString()
        {
            return $"Load Map SWF: {Name}";
        }
    }
}