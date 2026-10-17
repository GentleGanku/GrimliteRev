package grimoire.game
{
    import flash.display.MovieClip;
    import grimoire.Root;

    public class Shop
    {
        // #region Fields

        private static var game:* = Root.Game;
        private static var LoadedShops:Array = [];

        // #endregion

        // #region Methods

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
            }
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
            return (((shopInfo != null) && (shopInfo.items != null)) && (shopInfo.items.length > 0)) ? Root.trueString : Root.falseString;
        }

        // This function executes by buying the corresponding item from the shop.
        public static function buyItem(itemName:String, byEvent:Boolean=false):void
        {
            var item:Object = getShopItem(itemName.toLowerCase());
            if (item != null)
            {
                if (!byEvent)
                {
                    game.world.sendBuyItemRequest(item);
                }
                else
                {
                    buyByEvent(item);
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
                var item:Object = getShopItemByID(itemID, shopItemID);
                if (item != null)
                {
                    buyByEvent(item);
                }
            }
        }

        // This function returns a boolean for whether the corresponding item passes the game client's purchase checks or not.
        public static function canBuyItem(itemName:String):String
        {
            return isBuyable(getShopItem(itemName.toLowerCase()));
        }

        // This function returns a boolean for whether the corresponding item passes the game client's purchase checks or not (by using IDs).
        public static function canBuyItemByID(itemID:int, shopItemID:int):String
        {
            return isBuyable(getShopItemByID(itemID, shopItemID));
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
                    return item;
                }
                i++;
            }
            return null;
        }

        // This function returns the item object of the corresponding item ID and shop item ID.
        public static function getShopItemByID(itemID:int, shopItemID:int):Object
        {
            var item:Object;
            for each (item in game.world.shopinfo.items)
            {
                if ((item.ItemID == itemID) && (item.ShopItemID == shopItemID))
                {
                    return item;
                }
            }
            return null;
        }

        // This function returns the loaded shop datas.
        public static function getShops():String
        {
            return JSON.stringify(LoadedShops);
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
            if (game.isMergeShop(game.world.shopinfo))
            {
                game.ui.mcPopup.fOpen("MergeShop");
            }
            else
            {
                game.ui.mcPopup.fOpen("Shop");
            }
        }

        // This function executes by selecting the corresponding item in the shop and buying it without the confirmation prompt.
        private static function buyByEvent(item:Object):void
        {
            var layout:MovieClip = (game.ui.mcPopup.getChildByName("mcShop") as MovieClip);
            if (layout != null)
            {
                layout.update({"eventType":"listItemASel", "fData":item});
                game.world.sendBuyItemRequestWithQuantity({"accept":true, "iSel":item, "iQty":1});
            }
        }

        // This function returns a boolean for whether the item passes the quest lock and token checks of v4000 or not.
        private static function isBuyable(item:Object):String
        {
            var invCat:* = Root.gameDomain.getDefinition("InvCat");
            if ((item == null) || invCat.isQuestLocked(item, game.world)
                || (invCat.isToken(item) && (invCat.tokenHave(item, game.world) < invCat.tokenNeed(item, ((Number(item.iQty)) || (1))))))
            {
                return Root.falseString;
            }
            return Root.trueString;
        }

        // #endregion
    }
}
