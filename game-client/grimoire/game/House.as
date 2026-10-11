//grimoire.game.House
package grimoire.game
{
    import grimoire.Root;

    public class House 
    {
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;

		// This function returns the datas from all of the house items.
        public static function houseItems():String
        {
            return (JSON.stringify(game.world.myAvatar.houseitems));
        }

		// This function returns the assigned value of the available house slots.
        public static function houseSlots():int
        {
            return (game.world.myAvatar.objData.iHouseSlots);
        }

		// This function returns the item object of the corresponding item name.
        public static function getItem(itemName:String):Object
        {
            var item:Object;
			var houseItems:* = game.world.myAvatar.houseitems;
            if ((houseItems != null) && (houseItems.length > 0))
            {
                for each (item in houseItems)
                {
                    if (item.sName.toLowerCase() == itemName.toLowerCase())
                    {
                        return (item);
                    };
                };
            };
            return (null);
        }


    }
}//package grimoire.game

