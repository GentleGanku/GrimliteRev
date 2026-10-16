using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Grimoire.Game.Data
{
    public class Map
    {

        /// <summary>
        /// The ID of the map area.
        /// </summary>
        [JsonProperty("areaId")]
        public static int ID
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the map.
        /// </summary>
        [JsonProperty("strMapName")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the map area.
        /// </summary>
        [JsonProperty("areaName")]
        public static string Area
        {
            get;
            set;
        }

        /// <summary>
        /// The instance of the map area.
        /// </summary>
        public static string Instance
        {
            get
            {
                return Area.Split('-')[1];
            }
        }

        /// <summary>
        /// The file path of the map.
        /// </summary>
        [JsonProperty("strMapFileName")]
        public string FilePath
        {
            get;
            set;
        }

        /// <summary>
        /// The entire file path for the map.
        /// </summary>
        public string File
        {
            get
            {
                return "https://game.aq.com/game/gamefiles/maps/" + FilePath;
            }
        }

        /// <summary>
        /// The type of the map.
        /// </summary>
        [JsonProperty("intType")]
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// The list of monsters in the map.
        /// </summary>
        public List<Monster> Monsters
        {
            get;
            set;
        }

        /// <summary>
        /// The list of cells in the map.
        /// </summary>
        public List<Cell> Cells
        {
            get;
            set;
        } = new List<Cell>();

        /// <summary>
        /// The list of empty cells in the map, excluding the Wait and Blank cells.
        /// </summary>
        public List<string> EmptyCells
        {
            get
            {
                List<string> emptyCells = new List<string>();
                foreach (string cell in World.Cells)
                {
                    if (cell != "Wait" && cell != "Blank" && World.Monsters.FirstOrDefault(m => m.Cell.Equals(cell, StringComparison.OrdinalIgnoreCase)) == null)
                    {
                        emptyCells.Add(cell);
                    }
                }
                if (emptyCells.Count == 0)
                {
                    emptyCells.Add("Enter");
                }
                return emptyCells;
            }
        }

        /// <summary>
        /// The list of map items in the map.
        /// </summary>
        public List<MapItem> MapItems
        {
            get
            {
                List<MapItem> mapItems = new List<MapItem>();
                foreach (Cell cell in Cells)
                {
                    mapItems.AddRange(cell.MapItems);
                }
                return mapItems;
            }
        }

    }

    public class Cell
    {

        /// <summary>
        /// The name of the cell.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The list of pads in the cell.
        /// </summary>
        [JsonProperty("pads")]
        public List<string> Pads
        {
            get;
            set;
        }

        /// <summary>
        /// The list of map items in the cell.
        /// </summary>
        [JsonProperty("mapItems")]
        public List<MapItem> MapItems
        {
            get;
            set;
        }

    }

    public class MapItem
    {
        /// <summary>
        /// The ID of the map item.
        /// </summary>
        [JsonProperty("ID")]
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the map item.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The quest ID for the map item.
        /// </summary>
        [JsonProperty("qID")]
        public int QuestID
        {
            get;
            set;
        }

        /// <summary>
        /// The process message for the map item.
        /// </summary>
        [JsonProperty("itemName")]
        public string ProcessMsg
        {
            get;
            set;
        }

        /// <summary>
        /// The collect message for the map item.
        /// </summary>
        [JsonProperty("collectMsg")]
        public string CollectMsg
        {
            get;
            set;
        }

    }
}