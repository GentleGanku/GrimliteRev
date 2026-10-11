using Newtonsoft.Json;

namespace Grimoire.Game.Data
{
    public class Faction
    {
        /// <summary>
        /// The ID of the faction.
        /// </summary>
        [JsonProperty("FactionID")]
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// The in-game name of the faction.
        /// </summary>
        [JsonProperty("sName")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The rank that the player has achieved in this faction.
        /// </summary>
        //[JsonProperty("iRank")]
        public int Rank
        {
            get
            {
                return CalculatePoints(TotalRep);
            }
        }

        /// <summary>
        /// The total amount of rep the player has for this faction.
        /// </summary>
        [JsonProperty("iRep")]
        public int TotalRep
        {
            get;
            set;
        }

        /// <summary>
        /// The amount of rep the player has for their current rank.
        /// </summary>
        //[JsonProperty("iSpillRep")]
        public int Rep
        {
            get
            {
                return (Rank != 10) ? (TotalRep - CalculateRank(Rank, true)) : 0;
            }
        }

        /// <summary>
        /// The total required rep for the player to rank up.
        /// </summary>
        //[JsonProperty("iRepToRank")]
        public int RequiredRep
        {
            get
            {
                return CalculateRank(Rank, false);
            }
        }

        /// <summary>
        /// The remaining amount of rep required for the player to rank up.
        /// </summary>
        public int RemainingRep
        {
            get
            {
                return RequiredRep - Rep;
            }
        }

        public int CalculatePoints(int value)
        {
            if (value >= 302500)
                return 10;
            if (value >= 202500)
                return 9;
            if (value >= 129600)
                return 8;
            if (value >= 78400)
                return 7;
            if (value >= 44100)
                return 6;
            if (value >= 22500)
                return 5;
            if (value >= 10000)
                return 4;
            if (value >= 3600)
                return 3;
            if (value >= 900)
                return 2;
            if (value < 900)
                return 1;
            return 0;
        }

        public int CalculateRank(int value, bool add)
        {
            if (value == 10)
                return 0;
            if (value == 9)
                return add ? 302500 : 100000;
            if (value == 8)
                return add ? 202500 : 72900;
            if (value == 7)
                return add ? 129600 : 51200;
            if (value == 6)
                return add ? 78400 : 34300;
            if (value == 5)
                return add ? 44100 : 21600;
            if (value == 4)
                return add ? 22500 : 12500;
            if (value == 3)
                return add ? 10000 : 6400;
            if (value == 2)
                return add ? 3600 : 2700;
            if (value == 1)
                return 900;
            return 0;
        }

    }
}