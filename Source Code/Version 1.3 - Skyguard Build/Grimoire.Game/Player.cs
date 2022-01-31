using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Botting.Commands.Map;
using Grimoire.Game.Data;
using Grimoire.Tools;
using Grimoire.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Game
{
    public static class Player
    {
        public enum State
        {
            Dead,
            Idle,
            InCombat
        }

        public static int UserID => Flash.Call<int>("UserID", new object[0]);

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

        public static Quests Quests
        {
            get;
        }

        public static CmdTravel CreateJoinCommand(string map, string cell, string pad)
        {
            return new CmdTravel
            {
                Map = map,
                Cell = cell,
                Pad = pad
            };
        }

        public static async void ExecuteTravel(List<IBotCommand> cmds)
        {
            foreach (IBotCommand cmd in cmds)
            {
                await cmd.Execute(null);
                await Task.Delay(1000);
            }
        }

        private static bool usernamecheck = true;
        /// <summary>
        /// Grabs Username string.
        /// </summary>
        public static string Username
        {
            get
            {
                if (!IsLoggedIn)
                    return "";
                string usr = Flash.Call<string>("Username", new string[0]);
                if (usr == "null" && usernamecheck)
                {
                    DarkMessageBox.Show(new Form { TopMost = true, StartPosition = FormStartPosition.CenterScreen },
                        $"Your username is not registered correctly (null). You may restart the client in order to use the packet command " +
                        $"(that utilizes your username) and the relogin feature properly.", "Null Username Detected!", MessageBoxIcon.Warning
                        );
                    usernamecheck = false;
                }
                return usr;
            }
        }

        /// <summary>
        /// Checks if Logged in.
        /// </summary>
        public static bool IsLoggedIn => Flash.Call<bool>("IsLoggedIn", new string[0]);

        /// <summary>
        /// Gets Cell.
        /// </summary>
        public static string Cell => Flash.Call<string>("Cell", new string[0]);

        /// <summary>
        /// Gets Pad.
        /// </summary>
        public static string Pad => Flash.Call<string>("Pad", new string[0]);

        /// <summary>
        /// Gets Current State (Dead, Idle or InCombat).
        /// </summary>
        public static State CurrentState => (State)Flash.Call<int>("State", new string[0]);

        /// <summary>
        /// Gets Current Health.
        /// </summary>
        public static int Health => Flash.Call<int>("Health", new string[0]);

        /// <summary>
        /// Gets Health max.
        /// </summary>
        public static int HealthMax => Flash.Call<int>("HealthMax", new string[0]);

        /// <summary>
        /// Checks if Health is above 0.
        /// </summary>
        public static bool IsAlive => Health > 0;

        /// <summary>
        /// Gets Current Mana.
        /// </summary>
        public static int Mana => Flash.Call<int>("Mana", new string[0]);

        /// <summary>
        /// Gets Mana max.
        /// </summary>
        public static int ManaMax => Flash.Call<int>("ManaMax", new string[0]);

        /// <summary>
        /// Gets map string.
        /// </summary>
        public static string Map => Flash.Call<string>("Map", new string[0]);

        /// <summary>
        /// Gets level int.
        /// </summary>
        public static int Level => Flash.Call<int>("Level", new string[0]);

        /// <summary>
        /// Gets gold int.
        /// </summary>
        public static int Gold => Flash.Call<int>("Gold", new string[0]);

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

        /// <summary>
        /// Finds player position (float).
        /// </summary>
        public static float[] Position => Flash.Call<float[]>("Position", new string[0]);

        /// <summary>
        /// Checks if the player is a member (upgrade).
        /// </summary>
        public static bool IsMember => Flash.Call<bool>("IsMember", new string[0]);

        /// <summary>
        /// Checks if int skill is available (i think if its also off cooldown).
        /// </summary>
        /// <param name="index"></param>
        public static int SkillAvailable(string index) => Flash.Call<int>("SkillAvailable", new string[1] { index });

        /// <summary>
        /// Checks if int skill is available.
        /// </summary>
        /// <param name="skillIndex"></param>
        public static bool CheckSkillAvailability(string skillIndex) => Flash.Call<bool>("CheckSkillAvailability", new string[1] { skillIndex });

        /// <summary>
        ///  Toggles mute.
        /// </summary>
        public static void ToggleMute(bool b) => Flash.Call("MuteToggle", b);

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
        public static void AttackMonster(string name) => Flash.Call("AttackMonster", name);

        /// <summary>
        /// Sets Respawn Point to Current Cell Pad
        /// </summary>
        public static void SetSpawnPoint() => Flash.Call("SetSpawnPoint", new string[0]);

        public static void MoveToCell(string cell, string pad) => Flash.Call("Jump", cell, pad);

        public static void Rest() => Flash.Call("Rest", new string[0]);

        public static void JoinMap(string map, string cell, string pad) => Flash.Call("Join", map, cell, pad);

        public static void Equip(string id) => Flash.Call("Equip", id);
        public static void Equip(int id) => Flash.Call("Equip", id.ToString());

        public static void EquipPotion(int id, string desc, string file, string name) => Flash.Call("EquipPotion", id.ToString(), desc, file, name);

        public static void GoToPlayer(string name) => Flash.Call("GoTo", name);

        public static bool HasActiveBoost(string name) => Flash.Call<bool>("HasActiveBoost", new string[1] { name });

        public static void UseBoost(string id) => Flash.Call("UseBoost", id);

        public static void UseBoost(int id) => Flash.Call("UseBoost", id.ToString());

        public static void UseSkill(string index) => Flash.Call("UseSkill", index);

        public static void GetMapItem(string id) => Flash.Call("GetMapItem", id);

        public static void GetMapItem(int id) => Flash.Call("GetMapItem", id.ToString());

        public static void Logout() => Flash.Call("Logout", new string[0]);

        public static void ShowServers() => Flash.Call("ShowServers", new string[0]);

        public static void LoadMap(string name) => Flash.Call("LoadMap", new string[1] { name });

        public static string Class => Flash.Call<string>("Class", new string[0]);

        public static void AcceptQuest(int id) => Flash.Call("Accept", id);

        public static void CompleteQuest(int id, string itemid = null) => Flash.Call("Complete", id, itemid);

        public static void AFKPostpone() => Flash.Call("afkPostpone", new string[0]);

        public static bool HasDrop(string item) => Flash.Call<bool>("HasDrop", new string[1] { item });

        public static void AcceptDrop(int itemID) => Flash.Call("AcceptDrop", itemID);

        public static bool HasLoadedAvatar() => Flash.Call<bool>("HasLoadedAvatar", new string[0]);

        public static bool isAuraActive(string target, string aura) => Flash.Call<bool>("isAuraActive", target, aura);

        public static bool isAuraWithStrValActive(string target, string aura, string value) => Flash.Call<bool>("isAuraWithStrValActive", target, aura, value);

        public static bool auraComparison(string target, string comparison, string aura, int value) => Flash.Call<bool>("auraComparison", target, comparison, aura, value);

        public static void cancelAfk() => Flash.Call("cancelAfk", new string[0]);

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