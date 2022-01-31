using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdKill : IBotCommand
    {
        private CancellationTokenSource _cts;

        private int Index;

        public string Monster
        {
            get;
            set;
        }

        public bool Packet
        {
            get;
            set;
        }

        public async Task Execute(IBotEngine instance)
        {
            BotData.BotState = BotData.State.Combat;
            if (BotData.BotCell != null && !Player.Cell.Equals(BotData.BotCell, StringComparison.OrdinalIgnoreCase))
            {
                Player.MoveToCell(BotData.BotCell, BotData.BotPad);
                await Task.Delay(1000);
            }
            await instance.WaitUntil(() => World.IsMonsterAvailable(Monster), null, 3);
            if (instance.Configuration.WaitForAllSkills)
            {
                await Task.Delay(Player.AllSkillsAvailable);
            }
            if (instance.IsRunning && Player.IsAlive && Player.IsLoggedIn)
            {
                Player.AttackMonster(Monster);
                await Task.Delay(500);
                Task.Run(() => UseSkills(instance));
                await instance.WaitUntil(() => !Player.HasTarget, null, 360);
                _cts?.Cancel(throwOnFirstException: false);
            }
        }

        private async Task UseSkills(IBotEngine instance)
        {
            _cts = new CancellationTokenSource();
            int ClassIndex = -1;
            if (BotData.SkillSet != null && BotData.SkillSet.ContainsKey("[" + BotData.BotSkill + "]"))
            {
                ClassIndex = BotData.SkillSet["[" + BotData.BotSkill + "]"] + 1;
            }
            int Count = instance.Configuration.Skills.Count - 1;
            Index = ClassIndex;
            while (!_cts.IsCancellationRequested)
            {
                if (!Player.IsLoggedIn || !Player.IsAlive)
                {
                    while (Player.HasTarget)
                    {
                        Player.CancelTarget();
                        await Task.Delay(500);
                    }
                    return;
                }
                if (Monster.ToLower() == "escherion" && World.IsMonsterAvailable("Staff of Inversion"))
                {
                    Player.AttackMonster("Staff of Inversion");
                }
                else if (Monster.ToLower() == "vath" && World.IsMonsterAvailable("Stalagbite"))
                {
                    Player.AttackMonster("Stalagbite");
                }
                if (ClassIndex != -1)
                {
                    Skill s = instance.Configuration.Skills[Index];
                    if (s.Type == Skill.SkillType.Label)
                    {
                        Index = ClassIndex;
                        continue;
                    }
                    if (instance.Configuration.WaitForSkill)
                    {
                        BotManager.Instance.OnSkillIndexChanged(Index);
                        await Task.Delay(Player.SkillAvailable(s.Index));
                    }
                    if (s.Type == Skill.SkillType.Safe)
                    {
                        if (s.SafeMp)
                        {
                            if (Player.Mana / (double)Player.ManaMax * 100.0 <= s.SafeHealth)
                            {
                                Player.UseSkill(s.Index);
                            }
                        }
                        else if (Player.Health / (double)Player.HealthMax * 100.0 <= s.SafeHealth)
                        {
                            Player.UseSkill(s.Index);
                        }
                    }
                    else
                    {
                        Player.UseSkill(s.Index);
                    }
                    int num = Index = (Index >= Count) ? ClassIndex : (++Index);
                }
                else
                {
                    int[] array = new int[4]
                    {
                        Player.SkillAvailable("1"),
                        Player.SkillAvailable("2"),
                        Player.SkillAvailable("3"),
                        Player.SkillAvailable("4")
                    };
                    int num2 = array[0];
                    int MinIndex = 1;
                    for (int i = 1; i < 4; i++)
                    {
                        if (array[i] < num2)
                        {
                            num2 = array[i];
                            MinIndex = i + 1;
                        }
                    }
                    await Task.Delay(num2);
                    Player.UseSkill(MinIndex.ToString());
                }
                await Task.Delay(instance.Configuration.SkillDelay);
            }
            while (Player.HasTarget)
            {
                Player.CancelTarget();
                await Task.Delay(500);
            }
        }

        public override string ToString()
        {
            return "Kill: " + Monster;
        }
    }
}