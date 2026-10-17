package grimoire.game
{
    import flash.events.MouseEvent;
    import grimoire.Root;

    public class DropStack
    {
        // #region Fields

        private static var game:* = Root.Game;
        private static var pref:* = Root.Game.userPreference;

        // #endregion

        // #region Methods

        // This function returns a boolean for whether the player has the corresponding drop or not.
        public static function hasDrop(itemName:String):String
        {
            var source:*;
            var i:int;
            var child:*;
            var children:int;
            var item:String;
            var dItemName:String;
            var bool:String = Root.falseString;
            if (game.litePreference.data.bCustomDrops)
            {
                source = ((game.cDropsUI.mcDraggable) ? game.cDropsUI.mcDraggable.menu : game.cDropsUI);
                i = 0;
                while (i < source.numChildren)
                {
                    child = source.getChildAt(i);
                    if (child.itemObj)
                    {
                        dItemName = child.itemObj.sName.toLowerCase();
                        if (dItemName == itemName.toLowerCase())
                        {
                            bool = Root.trueString;
                            break;
                        };
                    };
                    i++;
                };
            }
            else
            {
                children = game.ui.dropStack.numChildren;
                while (i < children)
                {
                    child = game.ui.dropStack.getChildAt(i);
                    if (child.cnt.strName.text.toLowerCase() == itemName.toLowerCase())
                    {
                        bool = Root.trueString;
                        break;
                    };
                    i++;
                };
            };
            return (bool);
        }

        // This function executes by accepting the corresponding drop.
        public static function acceptDrop(itemID:int):void
        {
            game.sfc.sendXtMessage("zm", "getDrop", [itemID], "str", game.world.curRoom)
        }

        // This function returns a boolean for whether the game client allows the acceptance of the corresponding drop or not.
        public static function canAcceptDrop(itemID:int):String
        {
            var invCat:* = Root.gameDomain.getDefinition("InvCat");
            var avatar:* = game.world.myAvatar;
            var item:Object = game.world.invTree[itemID];
            var allowed:Boolean;
            if (item == null)
            {
                return (Root.trueString);
            };
            if (game.isHouseItem(item))
            {
                allowed = (avatar.houseitems.length < avatar.objData.iHouseSlots);
            }
            else
            {
                allowed = !invCat.isFullFor(avatar.items, item, avatar.objData.iBagSlots);
            };
            return (allowed ? Root.trueString : Root.falseString);
        }

        // This function executes by rejecting a drop based on the corresponding list.
        public static function rejectDrop(list:String):void
        {
            var source:*;
            var i:int;
            var child:*;
            var children:int;
            var item:String;
            var itemName:String;
            var pickup:Array = list.split(",");
            if (game.litePreference.data.bCustomDrops)
            {
                source = ((game.cDropsUI.mcDraggable) ? game.cDropsUI.mcDraggable.menu : game.cDropsUI);
                i = 0;
                while (i < source.numChildren)
                {
                    child = source.getChildAt(i);
                    if (child.itemObj)
                    {
                        itemName = child.itemObj.sName.toLowerCase();
                        if (pickup.indexOf(itemName) == -1)
                        {
                            child.btNo.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
                        };
                    };
                    i++;
                };
            }
            else
            {
                children = game.ui.dropStack.numChildren;
                for each (item in pickup)
                {
                    while (i < children)
                    {
                        child = game.ui.dropStack.getChildAt(i);
                        if (child.cnt.strName.text.toLowerCase().indexOf(item.toLowerCase()) == -1)
                        {
                            child.cnt.nbtn.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
                        };
                        i++;
                    };
                };
            };
        }

        // #endregion
    }
}
