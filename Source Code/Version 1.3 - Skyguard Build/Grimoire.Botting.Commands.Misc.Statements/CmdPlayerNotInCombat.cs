using Grimoire.Tools;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerNotInCombat : StatementCommand, IBotCommand
    {
        public CmdPlayerNotInCombat()
        {
            Tag = "Player";
            Text = "Player is not in combat";
        }

        public Task Execute(IBotEngine instance)
        {
            string Val1 = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            string reqs = Flash.Call<string>("CheckPlayerInCombat", new string[] { Val1 });
            bool isExists = bool.Parse(reqs);
            if (isExists)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player is not in combat: {Value1}";
        }
    }
}