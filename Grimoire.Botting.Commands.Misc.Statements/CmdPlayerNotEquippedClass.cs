using Grimoire.Tools;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerNotEquippedClass : StatementCommand, IBotCommand
    {
        public CmdPlayerNotEquippedClass()
        {
            Tag = "Player";
            Text = "Player's equipped class is not";
        }

        public Task Execute(IBotEngine instance)
        {
            string player = instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1;
            string className = instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2;
            string boolString = Flash.Call<string>("CheckPlayerClass", new string[] { player, className });
            bool isExists = bool.Parse(boolString);
            if (isExists)
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player's equipped class is not: {Value1}, {Value2}";
        }
    }
}
