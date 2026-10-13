package grimoire.game
{
    import flash.utils.ByteArray;
    import grimoire.Root;

    public class Quest
    {
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;

		// This function returns a boolean for whether the corresponding quest is in progress or not.
        public static function isInProgress(questID:String):String
        {
            return (!game.world.isQuestInProgress(parseInt(questID)) ? Root.falseString : Root.trueString);
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
			};
            return (boolean);
        }

		// This function returns a boolean whether the corresponding quest can be completed or not.
        public static function canComplete(questID:String):String
        {
            return ((game.world.canTurnInQuest(parseInt(questID)) && (getQuestValidation(parseInt(questID)) == "")) ? Root.trueString : Root.falseString);
        }
			
		// This function clones an object from the corresponding target.
        private static function cloneObject(obj:Object):Object
        {
            var _loc2_:ByteArray = new ByteArray();
            _loc2_.writeObject(obj);
            _loc2_.position = 0;
            return (_loc2_.readObject());
        }

		// This function returns the quest tree of every quest available.
        public static function getQuestTree():String
        {
            var _loc2_:Object;
            var _loc3_:Object;
            var _loc4_:Array;
            var _loc5_:Array;
            var _loc6_:Object;
            var _loc7_:Object;
            var _loc8_:* = undefined;
            var _loc9_:Object;
            var _loc10_:* = undefined;
            var _loc11_:Object;
            var _loc12_:Object;
            var _loc1_:Array = [];
            for each (_loc2_ in Root.Game.world.questTree)
            {
                _loc3_ = cloneObject(_loc2_);
                _loc4_ = [];
                _loc5_ = [];
                if (((!(_loc2_.turnin == null)) && (!(_loc2_.oItems == null))))
                {
                    for each (_loc6_ in _loc2_.turnin)
                    {
                        _loc7_ = new Object();
                        _loc8_ = _loc2_.oItems[_loc6_.ItemID];
                        _loc7_.sName = _loc8_.sName;
                        _loc7_.ItemID = _loc8_.ItemID;
                        _loc7_.iQty = _loc6_.iQty;
                        _loc7_.bTemp = _loc8_.bTemp;
                        _loc4_.push(_loc7_);
                    };
                };
                _loc3_.RequiredItems = _loc4_;
                if (((!(_loc2_.reward == null)) && (!(_loc2_.oRewards == null))))
                {
                    for each (_loc9_ in _loc2_.reward)
                    {
                        for each (_loc10_ in _loc2_.oRewards)
                        {
                            for each (_loc11_ in _loc10_)
                            {
                                if (((!(_loc11_.ItemID == null)) && (_loc11_.ItemID == _loc9_.ItemID)))
                                {
                                    _loc12_ = new Object();
                                    _loc12_.sName = _loc11_.sName;
                                    _loc12_.ItemID = _loc9_.ItemID;
                                    _loc12_.iQty = _loc9_.iQty;
                                    _loc12_.DropChance = (String(_loc9_.iRate) + "%");
                                    _loc5_.push(_loc12_);
                                };
                            };
                        };
                    };
                };
                _loc3_.Rewards = _loc5_;
                _loc1_.push(_loc3_);
            };
            return (JSON.stringify(_loc1_));
        }

		// This function returns a boolean whether the player has the required items or not (for the quest).
        public static function hasRequiredItems(param1:Object):String
        {
            var _loc2_:Object;
            var _loc3_:int;
            var _loc4_:int;
            var _loc5_:Object;
            if (((!(param1.reqd == null)) && (param1.reqd.length > 0)))
            {
                for each (_loc2_ in param1.reqd)
                {
                    _loc3_ = _loc2_.ItemID;
                    _loc4_ = int(_loc2_.iQty);
                    _loc5_ = Root.Game.world.invTree[_loc3_];
                    if (((_loc5_ == null) || (_loc5_.iQty < _loc4_)))
                    {
                        return (Root.falseString);
                    };
                };
            };
            return (Root.trueString);
        }

		// This function returns a string from the quest validation.
        public static function getQuestValidation(param1:int):String
        {
            var _loc3_:int;
            var _loc4_:int;
            var _loc5_:int;
            var _loc6_:* = null;
            var _loc7_:Object;
            var _loc8_:int;
            var _loc9_:int;
            var _loc10_:Object;
            var _loc2_:Object = Root.Game.world.questTree[param1];
            if (((!(_loc2_.sField == null)) && (!(Root.Game.world.getAchievement(_loc2_.sField, _loc2_.iIndex) == 0))))
            {
                if (_loc2_.sField == "im0")
                {
                    return ("Monthly Quests are only available once per month.");
                };
                return ("Daily Quests are only available once per day.");
            };
            if (((_loc2_.bUpg == 1) && (!(Root.Game.world.myAvatar.isUpgraded()))))
            {
                return ("Upgrade is required for this quest!");
            };
            if (((_loc2_.iSlot >= 0) && (Root.Game.world.getQuestValue(_loc2_.iSlot) < (_loc2_.iValue - 1))))
            {
                return ("Quest has not been unlocked!");
            };
            if (_loc2_.iLvl > Root.Game.world.myAvatar.objData.intLevel)
            {
                return (("Unlocks at Level " + _loc2_.iLvl) + ".");
            };
            if (((_loc2_.iClass > 0) && (Root.Game.world.myAvatar.getCPByID(_loc2_.iClass) < _loc2_.iReqCP)))
            {
                _loc3_ = Root.Game.getRankFromPoints(_loc2_.iReqCP);
                _loc4_ = (_loc2_.iReqCP - Root.Game.arrRanks[(_loc3_ - 1)]);
                if (_loc4_ > 0)
                {
                    return (((((("Requires " + _loc4_) + " Class Points on ") + _loc2_.sClass) + ", Rank ") + _loc3_) + ".");
                };
                return (((("Requires " + _loc2_.sClass) + ", Rank ") + _loc3_) + ".");
            };
            if (((_loc2_.FactionID > 1) && (Root.Game.world.myAvatar.getRep(_loc2_.FactionID) < _loc2_.iReqRep)))
            {
                _loc3_ = Root.Game.getRankFromPoints(_loc2_.iReqRep);
                _loc5_ = (_loc2_.iReqRep - Root.Game.arrRanks[(_loc3_ - 1)]);
                if (_loc5_ > 0)
                {
                    return (((((("Requires " + _loc5_) + " Reputation for ") + _loc2_.sFaction) + ", Rank ") + _loc3_) + ".");
                };
                return (((("Requires " + _loc2_.sFaction) + ", Rank ") + _loc3_) + ".");
            };
            if (((!(_loc2_.reqd == null)) && (!(hasRequiredItems(_loc2_)))))
            {
                _loc6_ = "Required Item(s): ";
                for each (_loc7_ in _loc2_.reqd)
                {
                    _loc8_ = _loc7_.ItemID;
                    _loc9_ = int(_loc7_.iQty);
                    _loc10_ = Root.Game.world.invTree[_loc8_];
                    if (_loc10_.sES == "ar")
                    {
                        _loc3_ = Root.Game.getRankFromPoints(_loc9_);
                        _loc4_ = (_loc9_ - Root.Game.arrRanks[(_loc3_ - 1)]);
                        if (_loc4_ > 0)
                        {
                            _loc6_ = ((_loc6_ + _loc4_) + " Class Points on ");
                        };
                        _loc6_ = (((_loc6_ + _loc10_.sName) + ", Rank ") + _loc3_);
                    }
                    else
                    {
                        _loc6_ = (_loc6_ + _loc10_.sName);
                        if (_loc9_ > 1)
                        {
                            _loc6_ = ((_loc6_ + "x") + _loc9_);
                        };
                    };
                    _loc6_ = (_loc6_ + ", ");
                };
                _loc6_ = (_loc6_.substr(0, (_loc6_.length - 2)) + ".");
                return (_loc6_);
            };
            return ("");
        }


    }
}//package grimoire.game

