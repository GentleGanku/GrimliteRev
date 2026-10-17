package grimoire.game
{
    import grimoire.Root;

    public class World
    {
        // #region Fields

        public static var mapCells:Array = [];
        public static var busy:Boolean = false;
        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function returns a boolean for whether the map has completely loaded or not.
        public static function mapLoadComplete():String
        {
            if (!game.world.mapLoadInProgress)
            {
                try
                {
                    return (game.getChildAt((game.numChildren - 1)) != game.mcConnDetail) ? Root.trueString : Root.falseString;
                }
                catch(e)
                {
                    return Root.falseString;
                }
            }
            return Root.falseString;
        }

        // This function returns a boolean for whether the corresponding action is available or not.
        public static function isActionAvailable(_arg_1:String):String
        {
            var _local_2:*;
            var _local_3:*;
            var _local_4:*;
            var _local_5:*;
            _local_2 = game.world.lock[_arg_1];
            _local_3 = new Date();
            _local_4 = _local_3.getTime();
            _local_5 = (_local_4 - _local_2.ts);
            return (_local_5 < _local_2.cd) ? Root.falseString : Root.trueString;
        }

        // This function returns the players' datas in the current map.
        public static function playersInMap():String
        {
            return JSON.stringify(game.world.areaUsers);
        }

        // This function returns the monsters' datas in the current cell.
        public static function getMonstersInCell():String
        {
            var _loc3_:* = null;
            var _loc4_:Object;
            var _loc5_:Object;
            var _loc1_:Array = game.world.getMonstersByCell(Root.Game.world.strFrame);
            var _loc2_:Array = [];
            for (_loc3_ in _loc1_)
            {
                _loc4_ = _loc1_[_loc3_];
                _loc5_ = new Object();
                _loc5_.sRace = _loc4_.objData.sRace;
                _loc5_.strMonName = _loc4_.objData.strMonName;
                _loc5_.MonID = _loc4_.dataLeaf.MonID;
                _loc5_.iLvl = _loc4_.dataLeaf.iLvl;
                _loc5_.intState = _loc4_.dataLeaf.intState;
                _loc5_.intHP = _loc4_.dataLeaf.intHP;
                _loc5_.intHPMax = _loc4_.dataLeaf.intHPMax;
                _loc2_.push(_loc5_);
            }
            return JSON.stringify(_loc2_);
        }

        // This function returns the visible monsters' datas in the current cell.
        public static function getVisibleMonstersInCell():String
        {
            var _loc3_:* = null;
            var _loc4_:Object;
            var _loc5_:Object;
            var _loc1_:Array = Root.Game.world.getMonstersByCell(Root.Game.world.strFrame);
            var _loc2_:Array = [];
            for (_loc3_ in _loc1_)
            {
                _loc4_ = _loc1_[_loc3_];
                if (!(((_loc4_.pMC == null) || (!(_loc4_.pMC.visible))) || (_loc4_.dataLeaf.intState <= 0)))
                {
                    _loc5_ = new Object();
                    _loc5_.sRace = _loc4_.objData.sRace;
                    _loc5_.strMonName = _loc4_.objData.strMonName;
                    _loc5_.MonID = _loc4_.dataLeaf.MonID;
                    _loc5_.iLvl = _loc4_.dataLeaf.iLvl;
                    _loc5_.intState = _loc4_.dataLeaf.intState;
                    _loc5_.intHP = _loc4_.dataLeaf.intHP;
                    _loc5_.intHPMax = _loc4_.dataLeaf.intHPMax;
                    _loc2_.push(_loc5_);
                }
            }
            return JSON.stringify(_loc2_);
        }

        // This function executes by setting the player's spawn point on the corresponding cell and pad.
        public static function setSpawnPoint():void
        {
            game.world.setSpawnPoint(game.world.strFrame, game.world.strPad);
        }

        // This function returns a boolean for whether the corresponding monster is available or not.
        public static function isMonsterAvailable(monName:String):String
        {
            return (getMonsterByName(monName) != null) ? Root.trueString : Root.falseString;
        }

        // This function returns the corresponding skill's name.
        public static function getSkillName(index:String):String
        {
            return ('"' + Root.Game.world.actions.active[parseInt(index)].nam) + '"';
        }

        // This function returns the monster object of the corresponding monster name.
        public static function getMonster(name:String):Object
        {
            var mon:Object;
            var monName:String;
            var monster:Object = game.world.getMonstersByCell(game.world.strFrame);
            for each (mon in monster)
            {
                if (mon.pMC)
                {
                    if (name)
                    {
                        monName = mon.pMC.pname.ti.text.toLowerCase();
                        if ((monName.indexOf(name.toLowerCase()) > -1) || (name == "*") && (mon.dataLeaf.intState > 0))
                        {
                            return mon;
                        }
                    }
                    else
                    {
                        if (mon.dataLeaf.intState > 0)
                        {
                            return mon;
                        }
                    }
                }
            }
            return null;
        }

        // This function returns the monster object of the corresponding monster name.
        public static function getMonsterByName(param1:String):Object
        {
            var _loc2_:Object;
            var _loc3_:String;
            for each (_loc2_ in game.world.getMonstersByCell(game.world.strFrame))
            {
                _loc3_ = _loc2_.pMC.pname.ti.text.toLowerCase();
                if ((((_loc3_.indexOf(param1.toLowerCase()) > -1) || (param1 == "*")) && (_loc2_.dataLeaf.intState > 0)))
                {
                    return _loc2_;
                }
            }
            return null;
        }

        // This function returns the list of cells in the current map.
        public static function getCells():String
        {
            var _loc2_:Object;
            var _loc1_:Array = [];
            for each (_loc2_ in game.world.map.currentScene.labels)
            {
                _loc1_.push(_loc2_.name);
            }
            return JSON.stringify(_loc1_);
        }

        // This function returns the list of pads in the current cell.
        public static function getPads(json:Boolean=true):*
        {
            var pads:Array = [];
            var i:int = 0;
            while (i < game.world.map.numChildren)
            {
                if (game.world.map.getChildAt(i).toString().split(" ")[1].toLowerCase().indexOf("pad") > -1)
                {
                    pads.push(game.world.map.getChildAt(i).name);
                }
                i++;
            }
            if (!json)
            {
                return pads;
            }
            return JSON.stringify(pads);
        }

        // This function returns the list of on-screen map items in the current cell.
        public static function getMapItems(json:Boolean=true):*
        {
            var mapItem:Object;
            var mapItems:Array = [];
            var ID:int = 0;
            var i:int = 0;
            while (i < game.world.map.numChildren)
            {
                try
                {
                    game.world.map.getChildAt(i)["componentInspectorSetting"] = true;
                }
                catch(err:Error){}
                if ("mapItem" in game.world.map.getChildAt(i) && ID != game.world.map.getChildAt(i).mapItem)
                {
                    mapItem = new Object();
                    mapItem.ID = game.world.map.getChildAt(i).mapItem;
                    mapItem.name = game.world.map.getChildAt(i).strName.replace(/(^[a-z]|\s[a-z])/g, function():String{ return arguments[1].toUpperCase(); });
                    mapItem.qID = game.world.map.getChildAt(i).intQuest;
                    mapItem.itemName = game.world.map.getChildAt(i).strItemName;
                    mapItem.collectMsg = game.world.map.getChildAt(i).strCollectMsg;
                    mapItems.push(mapItem);
                    ID = game.world.map.getChildAt(i).mapItem;
                }
                try
                {
                    game.world.map.getChildAt(i)["componentInspectorSetting"] = false;
                }
                catch(err:Error){}
                i++;
            }
            if (!json)
            {
                return mapItems;
            }
            return JSON.stringify(mapItems);
        }

        // This function executes by scanning the map in frames.
        public static function scanMap(getInfo:Boolean=false):*
        {
            var cell:Object;
            var fps:int = game.stage.frameRate;
            busy = true;
            mapCells = [];
            game.stage.frameRate = 60;
            for each (cell in game.world.map.currentScene.labels)
            {
                if (cell.name != "Wait" && cell.name != "Blank")
                {
                    mapCells.push(getCellInfo(cell.name, false));
                }
            }
            game.stage.frameRate = fps;
            busy = false;
            if (getInfo)
            {
                return JSON.stringify(mapCells);
            }
        }

        // This function returns the map cells.
        public static function getMapCells():String
        {
            return JSON.stringify(mapCells);
        }

        // This function returns the boolean whether the game client is busy or not.
        public static function isBusy():String
        {
            return busy ? Root.trueString : Root.falseString;
        }

        // This function returns the information of the corresponding cell.
        public static function getCellInfo(name:String, json:Boolean=true):*
        {
            game.world.visible = false;
            if (game.world.strFrame != name)
            {
                game.world.map.gotoAndPlay(name);
            }
            var cell:Object = new Object();
            cell.name = game.world.map.currentLabel;
            try
            {
                cell.pads = getPads(false);
            }
            catch(e:Error)
            {
                cell.pads = ["Left"];
            }
            try
            {
                cell.mapItems = getMapItems(false);
            }
            catch(e:Error)
            {
                cell.mapItems = [];
            }
            if (game.world.strFrame != name)
            {
                game.world.exitCell();
                game.world.map.gotoAndPlay("Blank");
            }
            game.world.visible = grimoire.game.Setting._visibility;
            if (!json)
            {
                return cell;
            }
            return JSON.stringify(cell);
        }

        // This function returns the item tree of the corresponding item.
        public static function getItemTree():String
        {
            var _loc2_:* = null;
            var _loc1_:Array = [];
            for (_loc2_ in game.world.invTree)
            {
                _loc1_.push(game.world.invTree[_loc2_]);
            }
            return JSON.stringify(_loc1_);
        }

        // This function returns the room ID of the current map.
        public static function roomID():String
        {
            return game.world.curRoom.toString();
        }

        // This function returns the room number of the current map.
        public static function roomNumber():String
        {
            return game.world.strAreaName.split("-")[1];
        }

        // This function returns the players' datas in the current map.
        public static function players():String
        {
            return JSON.stringify(game.world.uoTree);
        }

        // This function returns the corresponding monster's health.
        public static function monsterHealth(monName:String):int
        {
            var monster:Object = grimoire.game.World.getMonsterByName(monName);
            if (monster != null)
            {
                return monster.dataLeaf.intHP;
            }
            return 0;
        }

        // This function returns a boolean for whether the corresponding player is in combat or not.
        public static function isPlayerInCombat(name:String):String
        {
            var player:Object;
            var p:Object;
            var list:Object = game.world.uoTree;
            var bool:String = Root.falseString;
            for (p in list)
            {
                player = list[p];
                if ((player.strUsername.toLowerCase() == name.toLowerCase()) && (player.intState == 2))
                {
                    bool = Root.trueString;
                }
            }
            return bool;
        }

        // This function returns a boolean for whether the corresponding player has the corresponding class equipped or not.
        public static function isPlayerWithClass(name:String, cName:String):String
        {
            var avatar:Object = grimoire.game.World.getAvatar(name);
            var bool:String = Root.falseString;
            if ((avatar != null) && (avatar.objData.strClassName.toLowerCase() == cName.toLowerCase()))
            {
                bool = Root.trueString;
            }
            return bool;
        }

        // This function returns the avatar object of the corresponding player's name.
        public static function getAvatar(name:String):Object
        {
            var u:*;
            var uName:String;
            var a:* = game.world.avatars;
            for each (u in a)
            {
                if (u.pMC)
                {
                    if (name)
                    {
                        uName = u.pnm.toLowerCase();
                        if ((uName.indexOf(name.toLowerCase()) > -1) || (name == "*"))
                        {
                            return u;
                        }
                    }
                    else
                    {
                        return u;
                    }
                }
            }
            return null;
        }

        // This function executes by reloading the current map where the player is in.
        public static function reloadCurrentMap():void
        {
            game.world.reloadCurrentMap();
        }

        // This function executes by changing the area's name of the current map.
        public static function changeAreaName(name:String):void
        {
            game.world.strAreaName = name;
            game.updateAreaName();
        }

        // This function executes the game's Load Map function with the corresponding file path.
        public static function loadMap(filePath:String):void
        {
            game.world.loadMap(filePath);
        }

        // This function returns the list of avatars in the map.
        public static function avatarsInMap():String
        {
            var target:*;
            var avatar:Object;
            var avatarList:Array = [];
            for each (target in game.world.avatars)
            {
                avatar = new Object();
                avatar.uoName = target.dataLeaf.uoName;
                avatar.strUsername = target.dataLeaf.strUsername;
                avatar.intHP = target.dataLeaf.intHP;
                avatar.intHPMax = target.dataLeaf.intHPMax;
                avatar.intMP = target.dataLeaf.intMP;
                avatar.afk = target.dataLeaf.afk;
                avatar.entID = target.dataLeaf.entID;
                avatar.intLevel = target.dataLeaf.intLevel;
                avatar.strFrame = target.dataLeaf.strFrame;
                avatar.strPad = target.dataLeaf.strPad;
                avatar.intState = target.dataLeaf.intState;
                avatarList.push(avatar);
            }
            return JSON.stringify(avatarList);
        }

        // This function returns the list of monsters in the map.
        public static function monstersInMap():String
        {
            var target:*;
            var monster:Object;
            var monsterList:Array = [];
            for each (target in game.world.monsters)
            {
                monster = new Object();
                monster.sRace = target.objData.sRace;
                monster.strMonName = target.objData.strMonName;
                monster.MonID = target.dataLeaf.MonID;
                monster.iLvl = target.dataLeaf.iLvl;
                monster.intState = target.dataLeaf.intState;
                monster.intHP = target.dataLeaf.intHP;
                monster.intHPMax = target.dataLeaf.intHPMax;
                monsterList.push(monster);
            }
            return JSON.stringify(monsterList);
        }

        // Test function.
        public static function GetCellPlayers(_arg_1:*):*
        {
            var _local_2:*;
            var _local_3:*;
            var _local_4:* = Root.Game.world.uoTree;
            var _local_5:String = Root.falseString;
            for (_local_3 in _local_4)
            {
                _local_2 = _local_4[_local_3];
                if (_local_2.strUsername.toLowerCase() == _arg_1.toLowerCase())
                {
                    if (_local_2.strFrame.toLowerCase() == Root.Game.world.strFrame.toLowerCase())
                    {
                        _local_5 = Root.trueString;
                    }
                }
            }
            return _local_5;
        }

        // #endregion
    }
}
