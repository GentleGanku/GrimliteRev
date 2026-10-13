package grimoire.game
{
	import flash.events.Event;
    import grimoire.Root;

    public class Bank 
    {
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;
		public static var loaded:Boolean = false;

		// This function returns all of the items in the bank.
        public static function bankItems():String
        {
            return (JSON.stringify(game.world.bankinfo.items));
        }

		// This function returns the assigned value of the available bank slots.
        public static function bankSlots():int
        {
            return (game.world.myAvatar.objData.iBankSlots);
        }

		// This function returns the assigned value of the used bank slots.
        public static function usedBankSlots():int
        {
            return (game.world.myAvatar.iBankCount);
        }

		// This function executes by transfering an item to the bank/inventory.
        public static function transfer(target:String, itemName:String):void
        {
            var item:Object = (target == "Bank") ? Inventory.getItem(itemName) : getItem(itemName);
            if (item != null)
            {
				if (target == "Bank")
				{
					game.world.sendBankFromInvRequest(item);
				}
				else
				{
					game.world.sendBankToInvRequest(item);
				}
            };
        }

		// This function returns a boolean for whether the game client allows the transfer of an item to the bank/inventory or not.
        public static function canTransfer(target:String, itemName:String):String
        {
			var invCat:* = Root.gameDomain.getDefinition("InvCat");
			var avatar:* = game.world.myAvatar;
			var label:String = game.ui.mcPopup.currentLabel;
            var item:Object = (target == "Bank") ? Inventory.getItem(itemName) : getItem(itemName);
			var allowed:Boolean;
            if (item == null)
            {
				return (Root.falseString);
            };
			if (target == "Bank")
			{
				allowed = !(invCat.isClass(item) || item.bEquip || ((item.bCoins == 0) && (avatar.iBankCount >= avatar.objData.iBankSlots)));
			}
			else if (invCat.isHouse(item))
			{
				allowed = (avatar.houseitems.length < avatar.objData.iHouseSlots);
			}
			else
			{
				allowed = !(((label == "Bank") && invCat.isFullFor(avatar.items, item, avatar.objData.iBagSlots)) || ((label == "HouseBank") && (avatar.houseitems.length >= avatar.objData.iHouseSlots)));
			};
            return (allowed ? Root.trueString : Root.falseString);
        }

		// This function executes by swapping an item with another between the bank and the inventory.
        public static function bankSwap(itemName:String, bItemName:String):void
        {
            var inventoryItem:Object = Inventory.getItem(itemName);
			var bankItem:Object = getItem(bItemName);
            if ((inventoryItem != null) && (bankItem != null))
            {
				game.world.sendBankSwapInvRequest(bankItem, inventoryItem);
            };
        }

		// This function returns a boolean for whether the game client allows the swap of an item with another between the bank and the inventory or not.
        public static function canBankSwap(itemName:String, bItemName:String):String
        {
			var invCat:* = Root.gameDomain.getDefinition("InvCat");
			var avatar:* = game.world.myAvatar;
            var inventoryItem:Object = Inventory.getItem(itemName);
			var bankItem:Object = getItem(bItemName);
            if ((inventoryItem == null) || (bankItem == null))
            {
				return (Root.falseString);
            };
			if (invCat.isClass(inventoryItem) || inventoryItem.bEquip
				|| ((inventoryItem.bCoins == 0) && (bankItem.bCoins == 1) && (avatar.iBankCount >= avatar.objData.iBankSlots))
				|| (invCat.isMisc(bankItem) && !invCat.isMisc(inventoryItem) && invCat.isFullFor(avatar.items, bankItem, avatar.objData.iBagSlots))
				|| (invCat.isBag(bankItem) && !invCat.isBag(inventoryItem) && (invCat.countBag(avatar.items) >= avatar.objData.iBagSlots)))
			{
				return (Root.falseString);
			};
            return (Root.trueString);
        }

		// This function returns the item object of the corresponding item name.
        public static function getItem(itemName:String):Object
        {
            var item:Object;
			var bankItems:* = game.world.bankinfo.items;
            if ((bankItems != null) && (bankItems.length > 0))
            {
                for each (item in bankItems)
                {
                    if (item.sName.toLowerCase() == itemName.toLowerCase())
                    {
                        return (item);
                    };
                };
            };
            return (null);
        }

		// This function executes by toggling the bank's visibility. It turns on when not visible, or turns off when visible.
        public static function toggleBank():void
        {
            game.world.toggleBank();
        }

		/// This function executes by loading the character's bank information. (below)
		public static function loadBank():void
		{
			game.requestAPI("bank", {"layout":{"cat":"all"}}, onBankComplete, game.onBankError, true);
		}
		
		public static function onBankComplete(e:Event):void
        {
            game.onBankComplete(e);
			loaded = true;
        }
		/// This function executes by loading the character's bank information. (above)
		
		// This function executes by loading the bank's datas in full.
        public static function loadBankItems():void
        {
            game.sfc.sendXtMessage("zm", "loadBank", ["Sword", "Axe", "Dagger", "Gun", "Bow", "Mace", "Polearm", "Staff", "Wand", "Class", "Armor", "Helm", "Cape", "Pet", "Amulet", "Necklace", "Note", "Resource", "Item", "Quest Item", "ServerUse", "House", "Wall Item", "Floor Item", "Enhancement"], "str", game.world.curRoom);
        }
		
		// This function returns a boolean for whether the bank is open or not.
        public static function isBankOpen():String
        {
            return ((game.ui.mcPopup.currentLabel != "Bank") ? Root.falseString : Root.trueString);
        }		
		
		// This function returns a boolean for whether the bank items are loaded or not.
		public static function isBankLoaded():String
		{
			return (loaded ? Root.trueString : Root.falseString);
		}
		
    }
}//package grimoire.game

