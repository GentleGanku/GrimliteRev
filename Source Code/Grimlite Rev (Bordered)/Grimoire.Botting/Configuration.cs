using Grimoire.Game.Data;
using System.Collections.Generic;

namespace Grimoire.Botting
{
    public class Configuration
    {
        private static Configuration _instance;
        public static Configuration Instance => _instance ?? (_instance = new Configuration());

        public List<IBotCommand> Commands
        {
            get;
            set;
        }

        public List<Skill> Skills
        {
            get;
            set;
        }

        public List<Quest> Quests
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public List<InventoryItem> Boosts
        {
            get;
            set;
        }

        public List<string> Drops
        {
            get;
            set;
        }

        public Server Server
        {
            get;
            set;
        }

        public int SkillDelay
        {
            get;
            set;
        }

        public bool ExitCombatBeforeRest
        {
            get;
            set;
        }

        public bool ExitCombatBeforeQuest
        {
            get;
            set;
        }

        public bool EnablePickup
        {
            get;
            set;
        }

        public bool EnableRejection
        {
            get;
            set;
        }

        public bool EnablePickupAll
        {
            get;
            set;
        }

        public bool EnablePickupAcTagged
        {
            get;
            set;
        }

        public bool EnableRejectAll
        {
            get;
            set;
        }

        public bool AutoRelogin
        {
            get;
            set;
        }

        public int RelogDelay
        {
            get;
            set;
        }

        public bool RelogRetryUponFailure
        {
            get;
            set;
        }

        public int BotDelay
        {
            get;
            set;
        }

        public bool WaitForAllSkills
        {
            get;
            set;
        }

        public bool WaitForSkill
        {
            get;
            set;
        }

        public bool SkipDelayIndexIf
        {
            get;
            set;
        }

        public bool InfiniteAttackRange
        {
            get;
            set;
        }

        public bool ProvokeMonsters
        {
            get;
            set;
        }

        public bool Untarget
        {
            get;
            set;
        }

        public bool EnemyMagnet
        {
            get;
            set;
        }

        public bool LagKiller
        {
            get;
            set;
        }

        public bool HidePlayers
        {
            get;
            set;
        }

        public bool SkipCutscenes
        {
            get;
            set;
        }

        public bool DisableAnimations
        {
            get;
            set;
        }

        public int WalkSpeed
        {
            get;
            set;
        }

        public List<string> NotifyUponDrop
        {
            get;
            set;
        }

        public bool RestIfMp
        {
            get;
            set;
        }

        public bool RestIfHp
        {
            get;
            set;
        }

        public int RestMp
        {
            get;
            set;
        }

        public int RestHp
        {
            get;
            set;
        }

        public bool RestartUponDeath
        {
            get;
            set;
        }

        public List<string> Items
        {
            get;
            set;
        }

        public bool Packet
        {
            get;
            set;
        }

        public bool BankOnStop
        {
            get;
            set;
        }

        public bool EnsureComplete
        {
            get;
            set;
        }

        public int EnsureTries
        {
            get;
            set;
        }

        public static List<string> BlockedPlayers
        {
            get;
        }
        
        public bool AFK
        {
            get;
            set;
        }

        public int DropDelay
        { 
            get; 
            set; 
        }

        public static Dictionary<string, int> Tempvalues = new Dictionary<string, int>();
        public static Dictionary<string, string> Tempvariable = new Dictionary<string, string>();

        public Configuration()
        {
            Commands = new List<IBotCommand>();
            Skills = new List<Skill>();
            Quests = new List<Quest>();
            Boosts = new List<InventoryItem>();
            Drops = new List<string>();
            Items = new List<string>();
            NotifyUponDrop = new List<string>();
        }

        static Configuration()
        {
            BlockedPlayers = new List<string>
            {
                
            };
        }
    }
}