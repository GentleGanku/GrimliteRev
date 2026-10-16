using DarkUI.Forms;
using Grimoire.Botting.Commands;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Game;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        private void btnKill_Click(object sender, EventArgs e)
        {
            string monster = string.IsNullOrEmpty(txtMonster.Text) ? "*" : txtMonster.Text;
            AddCommand(new CmdKill
            {
                Monster = (txtMonster.Text == "Monster (* = any)") ? "*" : monster
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnKillF_Click(object sender, EventArgs e)
        {
            if (txtKillFItem.Text.Length > 0 && txtKillFQ.Text.Length > 0)
            {
                string monster = string.IsNullOrEmpty(txtKillFMon.Text) ? "*" : txtKillFMon.Text;
                AddCommand(new CmdKillFor
                {
                    ItemType = (!rbItems.Checked) ? ItemType.TempItems : ItemType.Items,
                    Monster = (txtKillFMon.Text == "Monster (* = any)") ? "*" : monster,
                    ItemName = txtKillFItem.Text,
                    Quantity = txtKillFQ.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnAddSkill_Click(object sender, EventArgs e)
        {
            if (lstSkills.Items.Cast<Skill>().ToList().FirstOrDefault((Skill s) => s.Type == Skill.SkillType.Label) == null)
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Please add a Skill Set label on top of the skills in the list.", "Add Skill", MessageBoxIcon.Error);
                return;
            }
            string skillIndex = numSkill.Text;
            AddSkill(new Skill
            {
                Text = skillIndex + ": " + Skill.GetSkillName(skillIndex),
                Index = skillIndex,
                Type = Skill.SkillType.Normal
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnAddSafe2_Click(object sender, EventArgs e)
        {
            if (lstSkills.Items.Cast<Skill>().ToList().FirstOrDefault((Skill s) => s.Type == Skill.SkillType.Label) == null)
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "Please add a Skill Set label on top of the skills in the list.", "Add Safe Skill", MessageBoxIcon.Error);
                return;
            }
            string skillIndex = numSkill.Text;
            AddSkill(new Skill
            {
                Text = "[Safe] " + skillIndex + ": " + Skill.GetSkillName(skillIndex),
                Index = skillIndex,
                Type = Skill.SkillType.Safe,
                ComparisonType = chkSafeGreaterThan.Checked,
                SafePercentage = (int)numSafe.Value,
                SafeHp = chkSafeHP.Checked,
                SafeMp = chkSafeMP.Checked
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdRest(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnRestF_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdRest
            {
                Full = true
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void AddSkill(Skill skill, bool Insert)
        {
            if (Insert)
            {
                lstSkills.Items.Insert((lstSkills.SelectedIndex > -1) ? lstSkills.SelectedIndex : lstSkills.Items.Count, skill);
            }
            else
            {
                lstSkills.Items.Add(skill);
            }
        }

        private void btnAddSkillSet_Click(object sender, EventArgs e)
        {
            if (txtSkillSet.TextLength > 0)
            {
                txtSkillSet.Text = ((Player.IsLoggedIn && txtSkillSet.Text.Equals("Skill Set Name", StringComparison.OrdinalIgnoreCase)) ? Player.Class : txtSkillSet.Text);
                AddSkill(new Skill
                {
                    Text = "[" + txtSkillSet.Text.ToUpper() + "]",
                    Type = Skill.SkillType.Label
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnUseSkillSet_Click(object sender, EventArgs e)
        {
            if (txtSkillSet.TextLength > 0)
            {
                txtSkillSet.Text = ((Player.IsLoggedIn && txtSkillSet.Text.Equals("Skill Set Name", StringComparison.OrdinalIgnoreCase)) ? Player.Class : txtSkillSet.Text);
                AddCommand(new CmdSkillSet
                {
                    Name = txtSkillSet.Text.ToUpper()
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnAddSkillCmd_Click(object sender, EventArgs e)
        {
            string monster = string.IsNullOrEmpty(txtMonsterSkillCmd.Text) ? "*" : txtMonsterSkillCmd.Text;
            if (txtMonsterSkillCmd.Text == "Monster (* = any)")
            {
                monster = "*";
            }
            string skillIndex = numSkillCmd.Text;
            AddCommand(new CmdUseSkill2
            {
                Monster = chkSkillCmdTargetSelf.Checked ? "Self-targeted" : monster,
                Index = skillIndex,
                SafeHp = (int)numSkillCmdHPMP.Value,
                SafeMp = (int)numSkillCmdHPMP.Value,
                Wait = chkSkillCmdWait.Checked,
                Skill = skillIndex + ": " + Skill.GetSkillName(skillIndex)
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            string monster = string.IsNullOrEmpty(txtMonster.Text) ? "*" : txtMonster.Text;
            AddCommand(new CmdAttack
            {
                Monster = (txtMonster.Text == "Monster (* = any)") ? "*" : txtMonster.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnCancelTargetCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdCancelTarget(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnCancelAutoAttackCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdCancelAutoAttack(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkSafeHP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSafeMP.Checked)
                chkSafeMP.Checked = !chkSafeHP.Checked;
        }

        private void chkSafeMP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSafeHP.Checked)
                chkSafeHP.Checked = !chkSafeMP.Checked;
        }

        private void chkSafeGreaterThan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSafeLessThan.Checked)
                chkSafeLessThan.Checked = !chkSafeGreaterThan.Checked;
        }

        private void chkSafeLessThan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSafeGreaterThan.Checked)
                chkSafeGreaterThan.Checked = !chkSafeLessThan.Checked;
        }

        #endregion
    }
}
