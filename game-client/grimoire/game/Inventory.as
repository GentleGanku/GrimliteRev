package grimoire.game
{
    import grimoire.Root;

    public class Inventory
    {
        // #region Fields

        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function returns the datas from all of the inventory items.
        public static function inventoryItems():String
        {
            return JSON.stringify(game.world.myAvatar.items);
        }

        // This function returns the item object of the corresponding item name.
        public static function getItem(itemName:String):Object
        {
            var item:Object;
            for each (item in game.world.myAvatar.items)
            {
                if (item.sName.toLowerCase() == itemName.toLowerCase())
                {
                    return item;
                }
            }
            return null;
        }

        // This function returns the item object of the corresponding item ID.
        public static function getItemByID(itemID:int):Object
        {
            var item:Object;
            for each (item in game.world.myAvatar.items)
            {
                if (item.ItemID == itemID)
                {
                    return item;
                }
            }
            return null;
        }

        // This function returns the assigned value of the available inventory slots.
        public static function inventorySlots():int
        {
            return game.world.myAvatar.objData.iBagSlots;
        }

        // This function returns the assigned value of the used inventory slots (bag items only).
        public static function usedInventorySlots():int
        {
            return Root.gameDomain.getDefinition("InvCat").countBag(game.world.myAvatar.items);
        }

        // This function returns the assigned value of the used misc inventory slots.
        public static function usedMiscSlots():int
        {
            return Root.gameDomain.getDefinition("InvCat").countMisc(game.world.myAvatar.items);
        }

        // #endregion
    }
}
