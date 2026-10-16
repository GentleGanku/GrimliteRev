using DarkUI.Forms;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public partial class BotManager
    {
        #region Methods

        public void AddQuest(int QuestID, string ItemID = null, bool toConfig = false)
        {
            string questText = (ItemID != null) ? $"{QuestID}:{ItemID}" : QuestID.ToString();
            if (lstQuests.Items.Cast<Quest>().ToList().Find(q => q.Text == questText) == null)
            {
                Quest quest = new Quest
                {
                    Id = QuestID,
                    ItemId = ItemID,
                    Text = questText
                };
                lstQuests.Items.Add(quest);
                if (toConfig)
                {
                    BotManager.Instance.ActiveBotEngine.Configuration.Quests.Add(quest);
                }
            }
        }

        public void RemoveQuest(int QuestID, string ItemID = null, bool toConfig = false)
        {
            Quest quest = lstQuests.Items.Cast<Quest>().ToList().Find(q => q.Text == ((ItemID != null) ? $"{QuestID}:{ItemID}" : QuestID.ToString()));
            if (quest != null)
            {
                lstQuests.Items.Remove(quest);
                if (toConfig)
                {
                    BotManager.Instance.ActiveBotEngine.Configuration.Quests.Remove(quest);
                }
            }
        }

        private void btnQuestAdd_Click(object sender, EventArgs e)
        {
            if (numQuestID.Text.StartsWith("[") || numQuestItem.Text.StartsWith("["))
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "You cannot add a Variable value to the Quest List.", $"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(numQuestID.Text, out int questId))
            {
                DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen }, "The quest ID must be a number.", $"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AddQuest(questId, chkQuestItem.Checked ? numQuestItem.Text : null);
        }

        private void btnQuestComplete_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdCompleteQuest2
            {
                QuestID = numQuestID.Text,
                ItemID = chkQuestItem.Checked ? numQuestItem.Text : null,
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnQuestAccept_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdAcceptQuest2
            {
                QuestID = numQuestID.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkQuestItem_CheckedChanged(object sender, EventArgs e)
        {
            numQuestItem.Enabled = chkQuestItem.Checked;
        }

        private void btnQuestlistAddCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdQuestlist
            {
                QuestID = txtQuestID.Text,
                ItemID = chkQuestlistItemID.Checked ? txtQuestItemID.Text : null,
                state = CmdQuestlist.State.Add
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnQuestlistRemoveCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdQuestlist
            {
                QuestID = txtQuestID.Text,
                ItemID = chkQuestlistItemID.Checked ? txtQuestItemID.Text : null,
                state = CmdQuestlist.State.Remove
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void txtQuestItemID_CheckedChanged(object sender, EventArgs e)
        {
            txtQuestItemID.Enabled = chkQuestlistItemID.Checked;
        }

        private void btnQuestlistClearCmd_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdQuestlist
            {
                QuestID = "",
                ItemID = "",
                state = CmdQuestlist.State.Clear
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        #endregion
    }
}
