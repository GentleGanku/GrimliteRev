﻿using Grimoire.Game;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc.Statements
{
    public class CmdPlayerAuraValIsLessThan : StatementCommand, IBotCommand
    {
        public CmdPlayerAuraValIsLessThan()
        {
            Tag = "Player Aura";
            Text = "Player Aura's value is less than";
        }

        public Task Execute(IBotEngine instance)
        {
            if (!Player.auraComparison("Self", "Less", (instance.IsVar(Value1) ? Configuration.Tempvariable[instance.GetVar(Value1)] : Value1), int.Parse(instance.IsVar(Value2) ? Configuration.Tempvariable[instance.GetVar(Value2)] : Value2)))
            {
                instance.Index++;
            }
            return Task.FromResult<object>(null);
        }

        public override string ToString()
        {
            return $"Player Aura's value is less than: {Value1}, {Value2}";
        }
    }
}