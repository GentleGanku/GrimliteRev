﻿using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraValIs : StatementCommand, IBotCommand
    {
        public CmdPlayerAuraValIs()
        {
            Tag = "Player Aura";
            Text = "Player Aura's value is";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.auraComparison("Self", "Equal", (instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), int.Parse(instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura's value is: {Value1}, {Value2}";
        }
    }
}