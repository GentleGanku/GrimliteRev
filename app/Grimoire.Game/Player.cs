using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Map;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.Utils;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Game
{
    public static class Player
    {

        //public static Avatar Self => Data.Map.Players.Find(a => a.Username.Equals(Username, StringComparison.OrdinalIgnoreCase));

        //public static Avatar Avatar(int userID) => Data.Map.Players.Find(p => p.UserID == userID);

        //public static Avatar Avatar(string name) => Data.Map.Players.Find(p => p.Username.Equals(name, StringComparison.OrdinalIgnoreCase));

        public enum State
        {
            Dead,
            Idle,
            InCombat
        }

        public static int UserID => Flash.Call<int>("UserID", new object[0]);
        //public static int UserID => Self.Extended.UserID;

        public static int CharUserID
        {
            get;
            set;
        }

        public static Bank Bank
        {
            get;
        }

        public static Inventory Inventory
        {
            get;
        }

        public static TempInventory TempInventory
        {
            get;
        }

        public static House House
        {
            get;
        }

        /// <summary>
        /// Gets an array containing all the names of the factions that the player has some reputation in.
        /// </summary>
        public static List<Faction> Factions => JsonConvert.DeserializeObject<List<Faction>>(Flash.Call("GetFactions", new object[0]));
        //public static List<Faction> Factions => Self.Extended.Factions;

        public static Quests Quests
        {
            get;
        }

        /// <summary>
        /// Grabs Username string.
        /// </summary>
        //public static string Username => Flash.Call<string>("Username", new string[0]);
        public static string Username
        {
            get;
            set;
        }

        /// <summary>
        /// Grabs Password string.
        /// </summary>
        private static string Password => Flash.Call<string>("Password", new string[0]);

        /// <summary>
        /// Checks if Logged in.
        /// </summary>
        public static bool IsLoggedIn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets Cell.
        /// </summary>
        public static string Cell => Flash.Call<string>("Cell", new string[0]);
        //public static string Cell => Self.Cell;

        /// <summary>
        /// Gets Pad.
        /// </summary>
        public static string Pad => Flash.Call<string>("Pad", new string[0]);
        //public static string Pad => Self.Pad;

        /// <summary>
        /// Gets Current State (Dead, Idle or InCombat).
        /// </summary>
        public static State CurrentState => (State)Flash.Call<int>("State", new string[0]);
        //public static State CurrentState => (State)Self.CurrentState;

        /// <summary>
        /// Gets Current Health.
        /// </summary>
        public static int Health => Flash.Call<int>("Health", new string[0]);
        //public static int Health => Self.HP;

        /// <summary>
        /// Gets Health max.
        /// </summary>
        public static int HealthMax => Flash.Call<int>("HealthMax", new string[0]);
        //public static int HealthMax => Self.MaxHP;

        /// <summary>
        /// Checks if Health is above 0.
        /// </summary>
        public static bool IsAlive => Health > 0;

        /// <summary>
        /// Gets Current Mana.
        /// </summary>
        public static int Mana => Flash.Call<int>("Mana", new string[0]);
        //public static int Mana => Self.MP;

        /// <summary>
        /// Gets Mana max.
        /// </summary>
        public static int ManaMax => Flash.Call<int>("ManaMax", new string[0]);
        //public static int ManaMax => Self.MaxMP;

        /// <summary>
        /// Gets map string.
        /// </summary>
        //public static string Map => Flash.Call<string>("Map", new string[0]);
        public static string Map => Data.Map.Area.Split('-')[0];

        /// <summary>
        /// Gets level int.
        /// </summary>
        public static int Level => Flash.Call<int>("Level", new string[0]);
        //public static int Level => Self.Level;

        /// <summary>
        /// Gets gold int.
        /// </summary>
        public static int Gold => Flash.Call<int>("Gold", new string[0]);
        //public static int Gold => Self.Extended.Gold;

        /// <summary>
        /// Checks if the player has a target.
        /// </summary>
        public static bool HasTarget => Flash.Call<bool>("HasTarget", new string[0]);

        /// <summary>
        /// Checks if all skills are available or off cooldown.
        /// </summary>
        public static int AllSkillsAvailable => Flash.Call<int>("AllSkillsAvailable", new string[0]);

        /// <summary>
        /// Checks if the player is afk.
        /// </summary>
        public static bool IsAfk => Flash.Call<bool>("IsAfk", new string[0]);
        //public static bool IsAfk => Self.AFK;

        /// <summary>
        /// Finds player position (float).
        /// </summary>
        public static float[] Position => Flash.Call<float[]>("Position", new string[0]);
        //public static float[] Position => Self.Position;

        /// <summary>
        /// Checks if the player is a member (upgrade).
        /// </summary>
        public static bool IsMember => Flash.Call<bool>("IsMember", new string[0]);
        //public static bool IsMember => Self.Member;

        /// <summary>
        /// Checks if int skill is available (i think if its also off cooldown).
        /// </summary>
        /// <param name="index"></param>
        public static int SkillAvailable(string index) => Flash.Call<int>("SkillAvailable", new string[1] { index });

        /// <summary>
        /// Checks if int skill is available.
        /// </summary>
        /// <param name="skillIndex"></param>
        public static bool IsSkillAvailable(string skillIndex) => Flash.Call<bool>("CheckSkillAvailability", new string[1] { skillIndex });

        /// <summary>
        ///  Toggles mute.
        /// </summary>
        public static void ToggleMute() => Flash.Call("MuteToggle", new string[0]);

        /// <summary>
        /// Change between AccessLevels (Non Member, Member, Moderator).
        /// </summary>
        /// <param name="level"></param>
        public static void ChangeAccessLevel(string level) => Flash.Call("ChangeAccessLevel", level);

        public static void WalkToPoint(string x, string y) => Flash.Call("WalkToPoint", x, y);

        /// <summary>
        /// Cancels Auto Attack
        /// </summary>
        public static void CancelAutoAttack() => Flash.Call("CancelAutoAttack", new string[0]);

        /// <summary>
        /// Cancels Target
        /// </summary>
        public static void CancelTarget() => Flash.Call("CancelTarget", new string[0]);

        /// <summary>
        /// Cancels Target on Self
        /// </summary>
        public static void CancelTargetSelf() => Flash.Call("CancelTargetSelf", new string[0]);

        /// <summary>
        /// Attacks Monster
        /// </summary>
        /// <param name="name"></param>
        //public static void AttackMonster(string name) => Flash.Call("AttackMonster", name);
        public static void AttackMonster(string name)
        {
            if (!BotUtilities.ShouldUseSkill)
            {
                CancelAutoAttack();
                return;
            }
            Flash.Call("AttackMonster", name);
        }

        /// <summary>
        /// Sets Respawn Point to Current Cell Pad
        /// </summary>
        public static void SetSpawnPoint() => Flash.Call("SetSpawnPoint", new string[0]);

        public static async void MoveToCell(string cell = "Enter", string pad = "Spawn") => Flash.Call("Jump", (cell != "Wait" && cell != "Blank") ? cell : "Enter", pad);

        public static void Rest() => Flash.Call("Rest", new string[0]);

        public static void JoinMap(string map, string cell, string pad) => Flash.Call("Join", map, cell, pad);

        public static void Equip(string id) => Flash.Call("Equip", id);

        public static void Equip(int id) => Flash.Call("Equip", id.ToString());

        public static void EquipPotion(int id, string desc, string file, string name) => Flash.Call("EquipPotion", id.ToString(), desc, file, name);

        public static void GoToPlayer(string name)
        {
            Flash.Call("GoTo", name);
        }

        public static bool HasActiveBoost(string name) => Flash.Call<bool>("HasActiveBoost", new string[1] { name });
        //public static bool HasActiveBoost(string name)
        //{
        //    if (name.ToLower().IndexOf("gold") > -1)
        //        return Self.Extended.BoostGold;
        //    if (name.ToLower().IndexOf("xp") > -1)
        //        return Self.Extended.BoostXP;
        //    if (name.ToLower().IndexOf("rep") > -1)
        //        return Self.Extended.BoostRep;
        //    return Self.Extended.BoostCP;
        //}

        public static void UseBoost(string id) => Flash.Call("UseBoost", id);

        public static void UseBoost(int id) => Flash.Call("UseBoost", id.ToString());

        //public static void UseSkill(string index) => Flash.Call("UseSkill", index);
        public static void UseSkill(string index)
        {
            if (!BotUtilities.ShouldUseSkill)
            {
                CancelAutoAttack();
                return;
            }
            Flash.Call("UseSkill", index);
        }

        public static void GetMapItem(string id) => Flash.Call("GetMapItem", id);

        public static void GetMapItem(int id) => Flash.Call("GetMapItem", id.ToString());

        public static void Logout() => Flash.Call("Logout", new string[0]);

        public static void ShowServers() => Flash.Call("ShowServers", new string[0]);

        public static void LoadMap(string name) => Flash.Call("LoadMap", new string[1] { name });

        public static string Class => Flash.Call<string>("Class", new string[0]);
        //public static string Class => Self.Class;

        public static void AcceptQuest(int id) => Flash.Call("Accept", id.ToString());

        public static void CompleteQuest(int id, string itemid = null) => Flash.Call("Complete", id.ToString(), itemid);

        /// <summary>
        /// Checks if the player has their target set on the parameter.
        /// </summary>
        public static bool HasTargetByID(int monID) => Flash.Call<bool>("HasTargetByID", new int[1] { monID });

        public static void AFKPostpone() => Flash.Call("afkPostpone", new string[0]);

        public static void EnableChat() => Flash.Call("EnableChat", new string[0]);

        //public static bool HasDrop(string item) => Flash.Call<bool>("HasDrop", new string[1] { item });
        public static bool HasDrop(string item) => World.DropStack.FirstOrDefault(i => i.Name.Equals(item, StringComparison.OrdinalIgnoreCase)) != null;

        public static void AcceptDrop(int itemID) => Flash.Call("AcceptDrop", itemID);

        public static bool HasLoadedAvatar() => Flash.Call<bool>("HasLoadedAvatar", new string[0]);

        [ObjectBinding("world.myAvatar.objData.intActivationFlag")]
        public static int ActivationFlag;

        [ObjectBinding("userPreference.data.bDeathAd")]
        public static bool HasDeathAd;

        public static bool isAuraActive(string target, string aura) => Flash.Call<bool>("isAuraActive", target, aura);

        public static bool isAuraWithStrValActive(string target, string aura, string value) => Flash.Call<bool>("isAuraWithStrValActive", target, aura, value);

        public static bool auraComparison(string target, string comparison, string aura, int value) => Flash.Call<bool>("auraComparison", target, comparison, aura, value);

        public static void cancelAfk() => Flash.Call("cancelAfk", new string[0]);

        [ObjectBinding("world.myAvatar.target.isMyAvatar")]
        public static bool targetOnSelf;

        [ObjectBinding("world.WALKSPEED")]
        public static int walkSpeed;

        public static void Unequip(string itemID) => Flash.Call("Unequip", itemID);

        public static void UnequipPotion(string itemName) => Flash.Call("UnequipPotion", itemName);

        [ObjectBinding("intLevelCap")]
        public static int levelCap;

        static Player()
        {
            Bank = new Bank();
            Inventory = new Inventory();
            TempInventory = new TempInventory();
            House = new House();
            Quests = new Quests();
        }
    }
}