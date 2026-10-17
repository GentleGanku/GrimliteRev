package grimoire.game
{
    import flash.events.MouseEvent;
    import grimoire.Root;

    public class DropStack
    {
        // #region Fields

        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function returns a boolean for whether the player has the corresponding drop or not.
        public static function hasDrop(itemName:String):String
        {
            var source:*;
            var i:int;
            var child:*;
            var children:int;
            var dItemName:String;
            var bool:String = Root.falseString;
            if (game.litePreference.data.bCustomDrops)
            {
                source = customDropList();
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
                        }
                    }
                    i++;
                }
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
                    }
                    i++;
                }
            }
            return bool;
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
                return Root.trueString;
            }
            if (game.isHouseItem(item))
            {
                allowed = (avatar.houseitems.length < avatar.objData.iHouseSlots);
            }
            else
            {
                allowed = !invCat.isFullFor(avatar.items, item, avatar.objData.iBagSlots);
            }
            return allowed ? Root.trueString : Root.falseString;
        }

        // This function executes by rejecting every drop whose name isn't in the corresponding comma-separated whitelist; an
        // empty list rejects them all. Custom Drops UI names must match a whitelist name exactly, while classic popup names
        // only have to contain one, since their text can hold more than the item name.
        public static function rejectDrop(list:String):void
        {
            var keep:Array = [];
            var name:String;
            for each (name in list.toLowerCase().split(","))
            {
                if (name != "")
                {
                    keep.push(name);
                }
            }
            rejectWhere(function(itemName:String, exact:Boolean):Boolean
            {
                var kept:String;
                for each (kept in keep)
                {
                    if (exact ? (itemName == kept) : (itemName.indexOf(kept) > -1))
                    {
                        return false;
                    }
                }
                return true;
            });
        }

        // This function executes by rejecting the drops with the corresponding name, matched the same way as in rejectDrop.
        public static function rejectDropByName(itemName:String):void
        {
            var wanted:String = itemName.toLowerCase();
            rejectWhere(function(name:String, exact:Boolean):Boolean
            {
                return exact ? (name == wanted) : (name.indexOf(wanted) > -1);
            });
        }

        // This function returns the display list that holds the custom drops UI's items: the draggable window's menu when
        // that window exists, otherwise the drops UI itself.
        private static function customDropList():*
        {
            return (game.cDropsUI.mcDraggable) ? game.cDropsUI.mcDraggable.menu : game.cDropsUI;
        }

        // This function clicks No on every drop that reject(name, exact) picks, where name is lowercased and exact tells
        // whether it is a Custom Drops UI name. The buttons are collected first, because a click removes its drop from the
        // list being walked, which used to skip the next drop or read past the end.
        private static function rejectWhere(reject:Function):void
        {
            var source:*;
            var child:*;
            var button:*;
            var i:int;
            var buttons:Array = [];
            if (game.litePreference.data.bCustomDrops)
            {
                source = customDropList();
                for (i = 0; i < source.numChildren; i++)
                {
                    child = source.getChildAt(i);
                    if (child.itemObj && reject(child.itemObj.sName.toLowerCase(), true))
                    {
                        buttons.push(child.btNo);
                    }
                }
            }
            else
            {
                source = game.ui.dropStack;
                for (i = 0; i < source.numChildren; i++)
                {
                    child = source.getChildAt(i);
                    if (reject(child.cnt.strName.text.toLowerCase(), false))
                    {
                        buttons.push(child.cnt.nbtn);
                    }
                }
            }
            for each (button in buttons)
            {
                button.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
            }
        }

        // #endregion
    }
}
