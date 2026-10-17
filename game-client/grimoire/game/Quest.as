package grimoire.game
{
    import flash.utils.ByteArray;
    import grimoire.Root;

    public class Quest
    {
        // #region Fields

        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function returns a boolean for whether the corresponding quest is in progress or not.
        public static function isInProgress(questID:String):String
        {
            return !game.world.isQuestInProgress(parseInt(questID)) ? Root.falseString : Root.trueString;
        }

        // This function executes by trying to complete the corresponding quest.
        public static function completeQuest(questID:String, itemID:String="-1", bool:Boolean=false):void
        {
            if (game.world.coolDown("tryQuestComplete"))
            {
                game.world.tryQuestComplete(parseInt(questID), parseInt(itemID), bool);
            }
        }

        // This function executes by trying to accept the corresponding quest.
        public static function acceptQuest(questID:String):void
        {
            if (game.world.coolDown("acceptQuest"))
            {
                game.world.acceptQuest(parseInt(questID));
            }
        }

        // This function executes by loading the corresponding quest.
        public static function loadQuest(quest:String):void
        {
            game.world.showQuests([quest], "q");
        }

        // This function executes by loading the corresponding list of quests.
        public static function loadMultipleQuests(quests:String):void
        {
            game.world.showQuests(quests.split(","), "q");
        }

        // This function executes by getting datas from the corresponding list of quests.
        public static function getQuests(quests:String):void
        {
            game.world.getQuests(quests.split(","));
        }

        // This function returns a boolean whether the corresponding quest is completely available or not.
        public static function isAvailable(questID:String):String
        {
            var quest:Object = game.world.questTree[parseInt(questID)];
            var boolean:String = Root.trueString;
            if (((((quest.sField != null && game.world.getAchievement(quest.sField, quest.iIndex) != 0) || quest.iLvl > game.world.myAvatar.objData.intLevel)
                || (quest.bUpg == 1 && !game.world.myAvatar.isUpgraded())
                || (quest.iSlot >= 0 && game.world.getQuestValue(quest.iSlot) < (Math.abs(quest.iValue) - 1)))
                || (quest.iClass > 0 && game.world.myAvatar.getCPByID(quest.iClass) < quest.iReqCP))
                || (quest.FactionID > 1 && game.world.myAvatar.getRep(quest.FactionID) < quest.iReqRep))
            {
                boolean = Root.falseString;
            }
            return boolean;
        }

        // This function returns a boolean whether the corresponding quest can be completed or not.
        public static function canComplete(questID:String):String
        {
            return (game.world.canTurnInQuest(parseInt(questID)) && (getQuestValidation(parseInt(questID)) == "")) ? Root.trueString : Root.falseString;
        }

        // This function returns the quest tree of every quest available.
        public static function getQuestTree():String
        {
            var quest:Object;
            var copy:Object;
            var requiredItems:Array;
            var rewards:Array;
            var turnin:Object;
            var required:Object;
            var item:* = undefined;
            var reward:Object;
            var rewardGroup:* = undefined;
            var rewardItem:Object;
            var rewardInfo:Object;
            var tree:Array = [];
            for each (quest in Root.Game.world.questTree)
            {
                copy = cloneObject(quest);
                requiredItems = [];
                rewards = [];
                if (((!(quest.turnin == null)) && (!(quest.oItems == null))))
                {
                    for each (turnin in quest.turnin)
                    {
                        required = new Object();
                        item = quest.oItems[turnin.ItemID];
                        required.sName = item.sName;
                        required.ItemID = item.ItemID;
                        required.iQty = turnin.iQty;
                        required.bTemp = item.bTemp;
                        requiredItems.push(required);
                    }
                }
                copy.RequiredItems = requiredItems;
                if (((!(quest.reward == null)) && (!(quest.oRewards == null))))
                {
                    for each (reward in quest.reward)
                    {
                        for each (rewardGroup in quest.oRewards)
                        {
                            for each (rewardItem in rewardGroup)
                            {
                                if (((!(rewardItem.ItemID == null)) && (rewardItem.ItemID == reward.ItemID)))
                                {
                                    rewardInfo = new Object();
                                    rewardInfo.sName = rewardItem.sName;
                                    rewardInfo.ItemID = reward.ItemID;
                                    rewardInfo.iQty = reward.iQty;
                                    rewardInfo.DropChance = (String(reward.iRate) + "%");
                                    rewards.push(rewardInfo);
                                }
                            }
                        }
                    }
                }
                copy.Rewards = rewards;
                tree.push(copy);
            }
            return JSON.stringify(tree);
        }

        // This function returns a boolean whether the player has the required items or not (for the quest).
        public static function hasRequiredItems(quest:Object):String
        {
            var required:Object;
            var itemID:int;
            var qty:int;
            var item:Object;
            if (((!(quest.reqd == null)) && (quest.reqd.length > 0)))
            {
                for each (required in quest.reqd)
                {
                    itemID = required.ItemID;
                    qty = int(required.iQty);
                    item = Root.Game.world.invTree[itemID];
                    if (((item == null) || (item.iQty < qty)))
                    {
                        return Root.falseString;
                    }
                }
            }
            return Root.trueString;
        }

        // This function returns a string from the quest validation.
        public static function getQuestValidation(questID:int):String
        {
            var rank:int;
            var extraPoints:int;
            var extraRep:int;
            var message:* = null;
            var required:Object;
            var itemID:int;
            var qty:int;
            var item:Object;
            var quest:Object = Root.Game.world.questTree[questID];
            if (((!(quest.sField == null)) && (!(Root.Game.world.getAchievement(quest.sField, quest.iIndex) == 0))))
            {
                if (quest.sField == "im0")
                {
                    return "Monthly Quests are only available once per month.";
                }
                return "Daily Quests are only available once per day.";
            }
            if (((quest.bUpg == 1) && (!(Root.Game.world.myAvatar.isUpgraded()))))
            {
                return "Upgrade is required for this quest!";
            }
            if (((quest.iSlot >= 0) && (Root.Game.world.getQuestValue(quest.iSlot) < (quest.iValue - 1))))
            {
                return "Quest has not been unlocked!";
            }
            if (quest.iLvl > Root.Game.world.myAvatar.objData.intLevel)
            {
                return ("Unlocks at Level " + quest.iLvl) + ".";
            }
            if (((quest.iClass > 0) && (Root.Game.world.myAvatar.getCPByID(quest.iClass) < quest.iReqCP)))
            {
                rank = Root.Game.getRankFromPoints(quest.iReqCP);
                extraPoints = (quest.iReqCP - Root.Game.arrRanks[(rank - 1)]);
                if (extraPoints > 0)
                {
                    return ((((("Requires " + extraPoints) + " Class Points on ") + quest.sClass) + ", Rank ") + rank) + ".";
                }
                return ((("Requires " + quest.sClass) + ", Rank ") + rank) + ".";
            }
            if (((quest.FactionID > 1) && (Root.Game.world.myAvatar.getRep(quest.FactionID) < quest.iReqRep)))
            {
                rank = Root.Game.getRankFromPoints(quest.iReqRep);
                extraRep = (quest.iReqRep - Root.Game.arrRanks[(rank - 1)]);
                if (extraRep > 0)
                {
                    return ((((("Requires " + extraRep) + " Reputation for ") + quest.sFaction) + ", Rank ") + rank) + ".";
                }
                return ((("Requires " + quest.sFaction) + ", Rank ") + rank) + ".";
            }
            if (((!(quest.reqd == null)) && (!(hasRequiredItems(quest)))))
            {
                message = "Required Item(s): ";
                for each (required in quest.reqd)
                {
                    itemID = required.ItemID;
                    qty = int(required.iQty);
                    item = Root.Game.world.invTree[itemID];
                    if (item.sES == "ar")
                    {
                        rank = Root.Game.getRankFromPoints(qty);
                        extraPoints = (qty - Root.Game.arrRanks[(rank - 1)]);
                        if (extraPoints > 0)
                        {
                            message = ((message + extraPoints) + " Class Points on ");
                        }
                        message = (((message + item.sName) + ", Rank ") + rank);
                    }
                    else
                    {
                        message = (message + item.sName);
                        if (qty > 1)
                        {
                            message = ((message + "x") + qty);
                        }
                    }
                    message = (message + ", ");
                }
                message = (message.substr(0, (message.length - 2)) + ".");
                return message;
            }
            return "";
        }

        // This function clones an object from the corresponding target.
        private static function cloneObject(obj:Object):Object
        {
            var bytes:ByteArray = new ByteArray();
            bytes.writeObject(obj);
            bytes.position = 0;
            return bytes.readObject();
        }

        // #endregion
    }
}
