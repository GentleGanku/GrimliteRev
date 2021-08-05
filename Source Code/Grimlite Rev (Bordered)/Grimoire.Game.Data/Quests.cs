using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class Quests
    {
        public List<Quest> QuestTree => Flash.Call<List<Quest>>("GetQuestTree", new string[0]);

        public List<Quest> AcceptedQuests => QuestTree.Where((Quest q) => q.IsInProgress).ToList();

        public List<Quest> UnacceptedQuests => QuestTree.Where((Quest q) => !q.IsInProgress).ToList();

        public List<Quest> CompletedQuests => QuestTree.Where((Quest q) => q.CanComplete).ToList();

        public event Action<List<Quest>> QuestsLoaded;

        public event Action<CompletedQuest> QuestCompleted;

        public void OnQuestsLoaded(List<Quest> quests) => this.QuestsLoaded?.Invoke(quests);

        public void OnQuestCompleted(CompletedQuest quest) => this.QuestCompleted?.Invoke(quest);

        public void Accept(int questId) => Flash.Call("Accept", questId.ToString());

        public void Accept(string questId) => Flash.Call("Accept", questId);

        public void Complete(int questId) => Flash.Call("Complete", questId.ToString());

        public void Complete(string questId) => Flash.Call("Complete", questId);

        public void Complete(string questId, string itemId) => Flash.Call("Complete", itemId, bool.TrueString);

        public void Load(int id) => Flash.Call("LoadQuest", id.ToString());

        public void Load(List<int> ids) => Flash.Call("LoadQuests", string.Join(",", ids));
        
        public void Get(List<int> ids) => Flash.Call("GetQuests", string.Join(",", ids.Select(delegate (int i) { return i.ToString(); })));

        public bool IsInProgress(int id) => Flash.Call<bool>("IsInProgress", id.ToString());

        public bool IsInProgress(string id) => Flash.Call<bool>("IsInProgress", id);

        public bool CanComplete(int id) => Flash.Call<bool>("CanComplete", id.ToString());

        public bool CanComplete(string id) => Flash.Call<bool>("CanComplete", id);

        public bool IsAvailable(int id) => Flash.Call<bool>("IsAvailable", id.ToString());
    }
}