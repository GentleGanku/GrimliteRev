//grimoire.game.Shops
package grimoire.game
{
	import flash.events.MouseEvent;
    import flash.display.MovieClip;
	import flash.display.DisplayObject;
    import grimoire.Root;
	import grimoire.game.Client;

    public class Shop
    {
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;
        private static var LoadedShops:Array = [];

		// This function executes by adding the shop's datas to the corresponding array.
        public static function onShopLoaded(shop:Object):void
        {
            var target:Object = new Object();
            target.Location = shop.Location;
            target.sName = shop.sName;
            target.ShopID = shop.ShopID;
            target.items = [];
            var i:int;
            while (i < shop.items.length)
            {
                target.items.push(shop.items[i]);
                i++;
            };
            LoadedShops.push(target);
        }

		// This function executes by resetting the player's contained shop datas.
        public static function resetShopInfo():void
        {
            game.world.shopinfo = null;
        }

		// This function returns a boolean for whether the shop has loaded or not.
        public static function isShopLoaded():String
        {
			var shopInfo:* = game.world.shopinfo;
            return ((((shopInfo != null) && (shopInfo.items != null)) && (shopInfo.items.length > 0)) ? Root.trueString : Root.falseString);
        }

		// This function executes by buying the corresponding item from the shop.
        public static function buyItem(itemName:String, byEvent:Boolean=false):void
        {
			if (!byEvent)
			{
				var item:Object = getShopItem(itemName.toLowerCase());
				if (item != null)
				{
					game.world.sendBuyItemRequest(item);
				};
			}
			else
			{
				//openShop();
				var frames:Array = new Array();
				var child:MovieClip = (game.ui.mcPopup.getChildByName("mcShop") as MovieClip);
				var isMergeShop:Boolean = (game.ui.mcPopup.currentLabel == "MergeShop");
				var childFrame:* = isMergeShop ? child.getChildAt(0) : child.getChildAt(2);
				var childList:MovieClip = isMergeShop ? childFrame.frames[8].mc.iList : childFrame.frames[5].mc.iList;
				var childItem:Object;
				var childItemName:String;
				var index:int;
				var i:int = 0;
				while (i < childList.numChildren)
				{
					var childListFrame:* = childList.getChildAt(i);
					childItem = childListFrame.fData;
					childItemName = childItem.sName;
					if (childItemName.indexOf(itemName) > -1)
                    {
						if (childItem.iStk > 1)
						{
							index = (childItemName.indexOf(" x" + childItem.iQty)) + 100;
							childItemName = childItemName.slice(0, index);
						}
						else if ((childItem.sES == "ar") && (childItem.EnhID > 0))
						{
							index = (childItemName.indexOf(", Rank " + childItem.iQty)) + 100;
							childItemName = childItemName.slice(0, index);
						}
						if ((childItemName.toLowerCase() == itemName.toLowerCase()))
						{
							childList.getChildAt(i).dispatchEvent(new MouseEvent(MouseEvent.CLICK));
							if (!isMergeShop)
							{
								child.previewPanel.frames[6].mc.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
							}
							else
							{
								childFrame.frames[12].mc.btn2.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
							}
							break;
						}
					}
					i++;
				}
			}
        }

		// This function executes by buying the corresponding item from the shop (by using IDs).
		public static function buyItemByID(itemID:int, shopID:int, shopItemID:int, byEvent:Boolean=false):void
		{
			if (!byEvent)
			{
				game.sfc.sendXtMessage("zm", "buyItem", [itemID, shopID, shopItemID], "str", Root.Game.world.curRoom);
			}
			else
			{
				//openShop();
				var frames:Array = new Array();
				var child:MovieClip = (game.ui.mcPopup.getChildByName("mcShop") as MovieClip);
				var isMergeShop:Boolean = (game.ui.mcPopup.currentLabel == "MergeShop");
				var childFrame:* = isMergeShop ? child.getChildAt(0) : child.getChildAt(2);
				var childList:MovieClip = isMergeShop ? childFrame.frames[8].mc.iList : childFrame.frames[5].mc.iList;
				var childItem:Object;
				var i:int = 0;
				while (i < childList.numChildren)
				{
					var childListFrame:* = childList.getChildAt(i);
					childItem = childListFrame.fData;
					if ((childItem.ItemID == itemID) && (childItem.ShopItemID == shopItemID))
					{
						childList.getChildAt(i).dispatchEvent(new MouseEvent(MouseEvent.CLICK));
						if (!isMergeShop)
						{
							child.previewPanel.frames[6].mc.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
						}
						else
						{
							childFrame.frames[12].mc.btn2.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
						}
						break;
					}
					i++;
				}
			}
		}
		
		// This function returns the item object of the corresponding item name.
        public static function getShopItem(itemName:String):Object
        {
            var item:Object;
            var i:int;
            while (i < game.world.shopinfo.items.length)
            {
                item = game.world.shopinfo.items[i];
                if (item.sName.toLowerCase() == itemName)
                {
                    return (item);
                };
                i++;
            };
            return (null);
        }
		
		// This function returns the loaded shop datas.
        public static function getShops():String
        {
            return (JSON.stringify(LoadedShops));
        }

		// This function executes by loading the corresponding shop.
        public static function load(shopID:String):void
        {
            game.world.sendLoadShopRequest(parseInt(shopID));
        }

		// This function executes by loading the corresponding hair shop.
        public static function loadHairShop(shopID:String):void
        {
            game.world.sendLoadHairShopRequest(parseInt(shopID));
        }

		// This function executes by loading the Armor Customizer window.
        public static function loadArmorCustomizer():void
        {
            game.openArmorCustomize();
        }

		// This function executes by selling the corresponding item.
        public static function sellItem(itemName:String):void
        {
            var item:Object = Inventory.getItem(itemName);
            game.world.sendSellItemRequest(item);
        }

		// This function executes by opening the loaded shop.
		public static function openShop():void
		{
			if (game.world.shopinfo.bHouse == 1)
			{
				game.ui.mcPopup.fOpen("HouseShop");
			}
			else
			{
				if (game.isMergeShop(game.world.shopinfo))
				{
					game.ui.mcPopup.fOpen("MergeShop");
				}
				else
				{
					game.ui.mcPopup.fOpen("Shop");
				};
			};
		}
		
		
    }
}//package grimoire.game

