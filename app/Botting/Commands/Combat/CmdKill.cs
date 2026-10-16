using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.UI;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Combat
{
    public class CmdKill : IBotCommand
    {
        public string Monster { get; set; }

        public bool Packet { get; set; }

        private CancellationTokenSource _cts;

        private int Index;

        private int Count;

        public async Task Execute(IBotEngine instance)
        {
            _cts = new CancellationTokenSource();
            BotData.BotState = BotData.State.Combat;
            BotUtilities.MoveToBotCell();
            await instance.WaitUntil(() => World.IsMonsterAvailable(instance.Value(Monster)), () => !_cts.IsCancellationRequested, 16, 500);
            if (instance.Configuration.WaitForAllSkills)
            {
                await Task.Delay(Player.AllSkillsAvailable);
            }
            Player.AttackMonster(instance.Value(Monster));
            Task.Run(() => UseSkills(instance));
            await instance.WaitUntil(() => !Player.HasTarget, () => !_cts.IsCancellationRequested, -1, 500);
            _cts.Cancel();
            Player.CancelTarget();
        }

        private async Task UseSkills(IBotEngine instance)
        {
            var monster = instance.Value(Monster);
            int ClassIndex = (BotData.SkillSet != null && BotData.SkillSet.ContainsKey("[" + BotData.BotSkill + "]")) ? (BotData.SkillSet["[" + BotData.BotSkill + "]"]) + 1 : -1;
            Count = instance.Configuration.Skills.Count - 1;
            Index = ClassIndex;
            while (!_cts.IsCancellationRequested)
            {
                if (!BotUtilities.ShouldUseSkill)
                {
                    Player.CancelAutoAttack();
                    await instance.WaitUntil(() => BotUtilities.ShouldUseSkill, () => !_cts.IsCancellationRequested, 30, 500);
                }
                if (!Player.IsLoggedIn)
                {
                    return;
                }
                if (!Player.IsAlive)
                {
                    Player.CancelTarget();
                    return;
                }
                if (Bot.ProtectedMonsters.ContainsKey(monster) && World.IsMonsterAvailable(Bot.ProtectedMonsters[monster]))
                {
                    Player.AttackMonster(Bot.ProtectedMonsters[monster]);
                }
                if (ClassIndex != -1)
                {
                    Skill skill = instance.Configuration.Skills[Index];
                    if (skill.Type == Skill.SkillType.Label)
                    {
                        Index = ClassIndex;
                        continue;
                    }
                    if (instance.Configuration.WaitForSkill)
                    {
                        BotManager.Instance.OnSkillIndexChanged(Index);
                        await Task.Delay(Player.SkillAvailable(skill.Index));
                    }
                    if (skill.Type == Skill.SkillType.Safe)
                    {
                        skill.UseSafeSkill();
                    }
                    else
                    {
                        Player.UseSkill(skill.Index);
                    }
                    Index = (Index >= Count) ? ClassIndex : (++Index);
                    await Task.Delay(instance.Configuration.SkillDelay);
                }
                else
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (i == 5 && Player.Inventory.Items.FirstOrDefault((InventoryItem item) => item.IsEquipped && item.Category == "Item") == null)
                        {
                            break;
                        }
                        if (Player.IsSkillAvailable(i.ToString()))
                        {
                            Player.UseSkill(i.ToString());
                        }
                        await Task.Delay(instance.Configuration.SkillDelay);
                    }
                }
            }
        }

        public override string ToString()
        {
            return "Kill: " + Monster;
        }
    }
}