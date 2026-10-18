using Grimoire.Tools;
using Grimoire.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class TempInventory
    {
        #region Properties

        public List<TempItem> Items
        {
            get
            {
                return Flash.Call<List<TempItem>>("GetTempItems", new string[0]);
            }
        }

        #endregion

        #region Methods

        public bool ContainsItem(string name, string qty)
        {
            TempItem tempItem = Items.FirstOrDefault((TempItem i) => i.Name.Equals(name.UnifyQuotes(), StringComparison.OrdinalIgnoreCase));
            if (tempItem == null)
            {
                return false;
            }
            if (!(qty == "*"))
            {
                return tempItem.Quantity >= int.Parse(qty);
            }
            return true;
        }

        #endregion
    }
}