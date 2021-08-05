using Grimoire.Game;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grimoire.Tools;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerIsNotInMyCell : StatementCommand, IBotCommand
    {
        public CmdPlayerIsNotInMyCell()
        {
            Tag = "Player";
            Text = "Player is not in my cell";
        }

        public Task Execute(IBotEngine instance)
        {
            string reqs;
            if (instance.IsVar(Value1))
            {
                reqs = Flash.Call<string>("GetCellPlayers", new string[] { Configuration.Tempvariable[instance.GetVar(Value1)] });
            }
            else
            {
                reqs = Flash.Call<string>("GetCellPlayers", new string[] { Value1 });
            }

            bool isExists = bool.Parse(reqs);

            if (isExists)
            {
                instance.Index++;
            }

            Console.WriteLine(isExists);
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return "Player is not in my cell: " + Value1;
        }
    }
}