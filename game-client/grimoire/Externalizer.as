package grimoire
{
    import flash.external.ExternalInterface;
    import grimoire.game.Aura;
    import grimoire.game.Bank;
    import grimoire.game.Client;
    import grimoire.game.DropStack;
    import grimoire.game.House;
    import grimoire.game.Inventory;
    import grimoire.game.Network;
    import grimoire.game.Player;
    import grimoire.game.Quest;
    import grimoire.game.Setting;
    import grimoire.game.Shop;
    import grimoire.game.Skill;
    import grimoire.game.TempInventory;
    import grimoire.game.World;

    public class Externalizer
    {
        // #region Methods

        // This function executes by initiating the callbacks for the External Interface.
        public function init(root:Root):void
        {
            // Root
            this.addCallback("selectArrayObjects", Root.selectArrayObjects);
            this.addCallback("callGameFunction", Root.callGameFunction);
            this.addCallback("callGameFunction0", Root.callGameFunction);
            this.addCallback("sendClientPacket", Root.sendClientPacket);
            this.addCallback("setGameObject", Root.setGameObject);
            this.addCallback("getGameObject", Root.getGameObject);
            this.addCallback("getGameObjectS", Root.getGameObjectS);
            this.addCallback("GameMessage", Root.gameMessage);
            this.addCallback("GamePopup", Root.gamePopup);
            this.addCallback("setTitle", Root.setTitle);
            this.addCallback("Username", Root.Username);
            this.addCallback("Password", Root.Password);
            this.addCallback("SetFPS", Root.setFPS);
            this.addCallback("isNull", Root.isNull);

            // Network
            this.addCallback("IsTemporarilyKicked", Network.isTemporarilyKicked);
            this.addCallback("AreServersLoaded", Network.areServersLoaded);
            this.addCallback("OnLoginExecute", Network.onLoginExecute);
            this.addCallback("ResetServers", Network.resetServers);
            this.addCallback("GetServerName", Network.serverName);
            this.addCallback("GetServerTime", Network.serverTime);
            this.addCallback("ShowServers", Network.showServers);
            this.addCallback("RealAddress", Network.realAddress);
            this.addCallback("RealPort", Network.realPort);
            this.addCallback("ServerIP", Network.serverIP);
            this.addCallback("Connect", Network.connect);
            this.addCallback("Logout", Network.logout);
            this.addCallback("Login", Network.login);

            // Setting
            this.addCallback("SetInfiniteRange", Setting.toggleInfiniteRange);
            this.addCallback("advancedOpt", Setting.toggleAdvancedOpt);
            this.addCallback("DisableDeathAd", Setting.disableDeathAd);
            this.addCallback("SetSkipCutscenes", Setting.skipCutscenes);
            this.addCallback("SetEnemyMagnet", Setting.setEnemyMagnet);
            this.addCallback("SetLagKiller", Setting.toggleLagKiller);
            this.addCallback("dropUIOpt", Setting.toggleDropUIOpt);
            this.addCallback("IsDropMenuOpen", Setting.isDropMenuOpen);
            this.addCallback("SetWalkSpeed", Setting.setWalkSpeed);
            this.addCallback("EnableChat", Setting.enableChat);

            // World
            this.addCallback("GetVisibleMonstersInCell", World.getVisibleMonstersInCell);
            this.addCallback("IsMonsterAvailable", World.isMonsterAvailable);
            this.addCallback("CheckPlayerInCombat", World.isPlayerInCombat);
            this.addCallback("GetMonstersInCell", World.getMonstersInCell);
            this.addCallback("IsActionAvailable", World.isActionAvailable);
            this.addCallback("CheckPlayerClass", World.isPlayerWithClass);
            this.addCallback("reloadCurrentMap", World.reloadCurrentMap);
            this.addCallback("MapLoadComplete", World.mapLoadComplete);
            this.addCallback("ChangeAreaName", World.changeAreaName);
            this.addCallback("MonsterHealth", World.monsterHealth);
            this.addCallback("SetSpawnPoint", World.setSpawnPoint);
            this.addCallback("MonstersInMap", World.monstersInMap);
            this.addCallback("GetSkillName", World.getSkillName);
            this.addCallback("PlayersInMap", World.playersInMap);
            this.addCallback("AvatarsInMap", World.avatarsInMap);
            this.addCallback("GetItemTree", World.getItemTree);
            this.addCallback("GetMapCells", World.getMapCells);
            this.addCallback("GetMapItems", World.getMapItems);
            this.addCallback("GetCellInfo", World.getCellInfo);
            this.addCallback("RoomNumber", World.roomNumber);
            this.addCallback("GetAllPads", World.scanMap);
            this.addCallback("GetCells", World.getCells);
            this.addCallback("GetPads", World.getPads);
            this.addCallback("LoadMap", World.loadMap);
            this.addCallback("Players", World.players);
            this.addCallback("RoomId", World.roomID);
            this.addCallback("IsBusy", World.isBusy);

            // Player
            this.addCallback("ChangeAccessLevel", Player.changeAccessLevel);
            this.addCallback("CancelAutoAttack", Player.cancelAutoAttack);
            this.addCallback("UnequipPotion", Player.unequipUseableItem);
            this.addCallback("HasLoadedAvatar", Player.isAvatarLoaded);
            this.addCallback("HasActiveBoost", Player.hasActiveBoost);
            this.addCallback("EquipPotion", Player.equipUseableItem);
            this.addCallback("AttackMonster", Player.attackMonster);
            this.addCallback("HasTargetByID", Player.hasTargetByID);
            this.addCallback("ChangeClassName", Player.changeClass);
            this.addCallback("CancelTarget", Player.cancelTarget);
            this.addCallback("WalkToPoint", Player.walkToPoint);
            this.addCallback("ChangeGuild", Player.changeGuild);
            this.addCallback("afkPostpone", Player.afkPostpone);
            this.addCallback("MuteToggle", Player.muteToggle);
            this.addCallback("GetMapItem", Player.getMapItem);
            this.addCallback("PlayerData", Player.playerData);
            this.addCallback("IsLoggedIn", Player.isLoggedIn);
            this.addCallback("ChangeName", Player.changeName);
            this.addCallback("GetFactions", Player.factions);
            this.addCallback("HealthMax", Player.maxHealth);
            this.addCallback("HasTarget", Player.hasTarget);
            this.addCallback("cancelAfk", Player.cancelAfk);
            this.addCallback("Unequip", Player.unequipItem);
            this.addCallback("IsMember", Player.isMember);
            this.addCallback("GetEquip", Player.getEquip);
            this.addCallback("Position", Player.position);
            this.addCallback("SetEquip", Player.setEquip);
            this.addCallback("UseBoost", Player.useBoost);
            this.addCallback("ManaMax", Player.maxMana);
            this.addCallback("Class", Player.className);
            this.addCallback("Equip", Player.equipItem);
            this.addCallback("Health", Player.health);
            this.addCallback("UserID", Player.userID);
            this.addCallback("Gender", Player.gender);
            this.addCallback("CharID", Player.charID);
            this.addCallback("IsAfk", Player.isAFK);
            this.addCallback("State", Player.state);
            this.addCallback("Level", Player.level);
            this.addCallback("Gold", Player.gold);
            this.addCallback("GoTo", Player.goTo);
            this.addCallback("Rest", Player.rest);
            this.addCallback("Mana", Player.mana);
            this.addCallback("Jump", Player.jump);
            this.addCallback("Join", Player.join);
            this.addCallback("Cell", Player.cell);
            this.addCallback("Map", Player.map);
            this.addCallback("Pad", Player.pad);

            // Quest
            this.addCallback("LoadQuests", Quest.loadMultipleQuests);
            this.addCallback("IsInProgress", Quest.isInProgress);
            this.addCallback("GetQuestTree", Quest.getQuestTree);
            this.addCallback("IsAvailable", Quest.isAvailable);
            this.addCallback("CanComplete", Quest.canComplete);
            this.addCallback("Complete", Quest.completeQuest);
            this.addCallback("GetQuests", Quest.getQuests);
            this.addCallback("LoadQuest", Quest.loadQuest);
            this.addCallback("Accept", Quest.acceptQuest);

            // Shop
            this.addCallback("LoadArmorCustomizer", Shop.loadArmorCustomizer);
            this.addCallback("ResetShopInfo", Shop.resetShopInfo);
            this.addCallback("IsShopLoaded", Shop.isShopLoaded);
            this.addCallback("LoadHairShop", Shop.loadHairShop);
            this.addCallback("BuyItemById", Shop.buyItemByID);
            this.addCallback("SellItem", Shop.sellItem);
            this.addCallback("GetShops", Shop.getShops);
            this.addCallback("BuyItem", Shop.buyItem);
            this.addCallback("CanBuyItem", Shop.canBuyItem);
            this.addCallback("CanBuyItemById", Shop.canBuyItemByID);
            this.addCallback("LoadShop", Shop.load);

            // DropStack
            this.addCallback("AcceptDrop", DropStack.acceptDrop);
            this.addCallback("CanAcceptDrop", DropStack.canAcceptDrop);
            this.addCallback("RejectDrop", DropStack.rejectDrop);
            this.addCallback("RejectDropByName", DropStack.rejectDropByName);
            this.addCallback("HasDrop", DropStack.hasDrop);

            // Skill
            this.addCallback("CheckSkillAvailability", Skill.isSkillAvailable);
            this.addCallback("AllSkillsAvailable", Skill.allSkillsAvailable);
            this.addCallback("SkillAvailable", Skill.isSkillReady);
            this.addCallback("UseSkill", Skill.skillAction);
            this.addCallback("getRange", Skill.getRange);

            // Aura
            this.addCallback("isAuraWithStrValActive", Aura.isAuraWithStrValActive);
            this.addCallback("auraComparison", Aura.auraComparison);
            this.addCallback("isAuraActive", Aura.isAuraActive);

            // Client
            this.addCallback("LoadTravelTriggers", Client.loadTravelTriggers);
            this.addCallback("StartLatencyTimer", Client.startLatencyTimer);
            this.addCallback("IsClientLoading", Client.isClientLoading);
            this.addCallback("DisplayCharPage", Client.displayCharPage);
            this.addCallback("InConnStage", Client.inConnStage);
            this.addCallback("ServerLabel", Client.serverLabel);
            this.addCallback("LoginLabel", Client.loginLabel);
            this.addCallback("GameLabel", Client.gameLabel);
            this.addCallback("InGame", Client.inGame);
            this.addCallback("shiftBackgrounds", Client.shiftBackgrounds);
            this.addCallback("setBackgroundIndex", Client.setBackgroundIndex);
            this.addCallback("setAnonymousMode", Client.setAnonymousMode);

            // Bank
            this.addCallback("UsedBankSlots", Bank.usedBankSlots);
            this.addCallback("LoadBankItems", Bank.loadBankItems);
            this.addCallback("IsBankLoaded", Bank.isBankLoaded);
            this.addCallback("HasBankOpen", Bank.isBankOpen);
            this.addCallback("GetBankItems", Bank.bankItems);
            this.addCallback("BankSlots", Bank.bankSlots);
            this.addCallback("ShowBank", Bank.toggleBank);
            this.addCallback("BankSwap", Bank.bankSwap);
            this.addCallback("LoadBank", Bank.loadBank);
            this.addCallback("Transfer", Bank.transfer);
            this.addCallback("CanTransfer", Bank.canTransfer);
            this.addCallback("CanBankSwap", Bank.canBankSwap);

            // Inventory
            this.addCallback("UsedInventorySlots", Inventory.usedInventorySlots);
            this.addCallback("GetInventoryItems", Inventory.inventoryItems);
            this.addCallback("InventorySlots", Inventory.inventorySlots);
            this.addCallback("UsedMiscSlots", Inventory.usedMiscSlots);

            // TempInventory
            this.addCallback("ItemIsInTemp", TempInventory.isItemInTemp);
            this.addCallback("GetTempItems", TempInventory.tempItems);

            // House
            this.addCallback("GetHouseItems", House.houseItems);
            this.addCallback("HouseSlots", House.houseSlots);

            // Test
            this.addCallback("GetCellPlayers", World.GetCellPlayers);
        }

        // This function executes by adding the corresponding callback to the External Interface.
        public function addCallback(name:String, func:Function):void
        {
            ExternalInterface.addCallback(name, func);
        }

        // #endregion
    }
}
