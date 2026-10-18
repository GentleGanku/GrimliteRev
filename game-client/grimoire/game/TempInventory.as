package grimoire.game
{
    import grimoire.Root;

    public class TempInventory
    {
        // #region Fields

        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function returns the temporary item datas.
        public static function tempItems():String
        {
            return JSON.stringify(game.world.myAvatar.tempitems);
        }

        // This function returns the temporary item object of the corresponding item name.
        public static function getTempItem(itemName:String):Object
        {
            var item:Object;
            for each (item in game.world.myAvatar.tempitems)
            {
                if (Root.nameKey(item.sName) == Root.nameKey(itemName))
                {
                    return item;
                }
            }
            return null;
        }

        // This function returns a boolean for whether the temporary item is in inventory or not.
        public static function isItemInTemp(itemName:String, itemQty:String):String
        {
            var item:Object = getTempItem(itemName);
            if (item == null)
            {
                return Root.falseString;
            }
            return (itemQty == "*") ? Root.trueString : ((item.iQty >= parseInt(itemQty)) ? Root.trueString : Root.falseString);
        }

        // #endregion
    }
}
