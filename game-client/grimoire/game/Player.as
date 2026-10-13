package grimoire.game
{
	import flash.events.MouseEvent;
    import flash.filters.GlowFilter;
	import flash.utils.getQualifiedClassName;
    import grimoire.Root;
	import grimoire.game.Inventory;

    public class Player 
    {
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;		
		
		// This function returns a boolean whether the player is logged into the game or not.
        public static function isLoggedIn():String
        {
            return (((game != null) && (game.sfc != null) && game.sfc.isConnected) ? Root.trueString : Root.falseString);
        }
		
		// This function returns the player's cell.
        public static function cell():String
        {
            return (('"' + game.world.strFrame) + '"');
        }

		// This function returns the player's faction datas.
        public static function factions():String
        {
            return (JSON.stringify(game.world.myAvatar.factions));
        }

		// This function returns the player's pad.
        public static function pad():String
        {
            return (('"' + game.world.strPad) + '"');
        }

		// This function returns the player's state.
        public static function state():int
        {
            return (game.world.myAvatar.dataLeaf.intState);
        }

		// This function returns the player's current health.
        public static function health():int
        {
            return (game.world.myAvatar.dataLeaf.intHP);
        }

		// This function returns the player's maximum health.
        public static function maxHealth():int
        {
            return (game.world.myAvatar.dataLeaf.intHPMax);
        }

		// This function returns the player's current mana.
        public static function mana():int
        {
            return (game.world.myAvatar.dataLeaf.intMP);
        }

		// This function returns the player's maximum mana.
        public static function maxMana():int
        {
            return (game.world.myAvatar.dataLeaf.intMPMax);
        }

		// This function returns the name of the map where the player is currently in.
        public static function map():String
        {
            return (('"' + game.world.strMapName) + '"');
        }

		// This function returns the player's current level.
        public static function level():int
        {
            return (game.world.myAvatar.dataLeaf.intLevel);
        }

		// This function returns a boolean whether the player is currently member or not.
        public static function isMember():String
        {
			return ((game.world.myAvatar.objData.iUpgDays > -1) ? Root.trueString : Root.falseString);
        }

		// This function returns the player's current gold.
        public static function gold():int
        {
            return (game.world.myAvatar.objData.intGold);
        }

		// This function returns a boolean whether the player has a target or not.
        public static function hasTarget():String
        {
            return (((game.world.myAvatar.target != null) && (game.world.myAvatar.target.dataLeaf.intHP > 0)) ? Root.trueString : Root.falseString);
        }

		// This function returns a boolean whether the player is AFK or not.
        public static function isAFK():String
        {
            return ((!game.world.myAvatar.dataLeaf.afk) ? Root.falseString : Root.trueString);
        }

		// This function returns the player's current coordinates.
        public static function position():String
        {
            return (JSON.stringify([game.world.myAvatar.pMC.x, game.world.myAvatar.pMC.y]));
        }

        public static function walkToPoint(tx:String, ty:String):void
        {
            game.world.myAvatar.pMC.walkTo(parseInt(tx), parseInt(ty), game.world.WALKSPEED);
            game.world.moveRequest({
                "mc":game.world.myAvatar.pMC,
                "tx":parseInt(tx),
                "ty":parseInt(ty),
                "sp":game.world.WALKSPEED
            });
        }

		// This function executes by canceling the player's auto attack play.
		public static function cancelAutoAttack():void
		{
			game.world.cancelAutoAttack();
		}
		
		// This function executes by removing the player's target.
        public static function cancelTarget():void
        {
			game.world.cancelTarget();
            if (game.world.myAvatar.target != null)
            {
				game.world.setTarget(null);
            };
        }

		// This function executes by removing the player's target if it's on them.
        public static function cancelTargetSelf():void
        {
            if (game.world.myAvatar.target == game.world.myAvatar)
            {
                game.world.cancelTarget();
            };
        }

		// This function executes by toggling the player's mute ability.
        public static function muteToggle():void
        {
            if (game.chatF.amIMute())
            {
                Root.Game.chatF.unmuteMe();
            }
            else
            {
                Root.Game.chatF.muteMe(300000);
            };
        }

		// This function executes by attacking the corresponding monster.
        public static function attackMonster(monName:String):void
        {
            var monster:Object = grimoire.game.World.getMonster(monName);
            if (monster != null)
            {
				game.world.setTarget(monster);
				game.world.approachTarget();
            };
        }

		// This function executes by jumping in to the corresponding cell and pad.
        public static function jump(cell:String="Enter", pad:String="Spawn"):void
        {
            game.world.moveToCell(cell, pad);
        }

		// This function executes the game's Rest function.
        public static function rest():void
        {
            game.world.rest();
        }

		// This function executes by joining the corresponding map with the cell and pad.
        public static function join(mapName:String, cell:String="Enter", pad:String="Spawn"):void
        {
            game.world.gotoTown(mapName, cell, pad);
        }

		// This function executes by equipping the corresponding item.
        public static function equipItem(itemID:String):void
        {
/*			rootClass.sfc.sendXtMessage("zm", "equipItem", [parseInt(itemID)], "str", curRoom);*/
            game.world.sendEquipItemRequest({
				"ItemID":parseInt(itemID)
			});
        }

		// This function executes by equipping the corresponding potion.
        public static function equipUseableItem(itemID:int, sDesc:String, sFile:String, sName:String):void
        {
            game.world.equipUseableItem({
                "ItemID":itemID,
                "sDesc":sDesc,
                "sFile":sFile,
                "sName":sName
            });
        }

		// This function executes by unequipping the corresponding item.
		public static function unequipItem(itemID:String):void
		{
			game.world.sendUnequipItemRequest({
				"ItemID":parseInt(itemID)
			});
		}
		
		// This function executes by unequipping the corresponding item.
		public static function unequipUseableItem(itemName:String):void
		{
			game.world.unequipUseableItem(grimoire.game.Inventory.getItem(itemName));
		}
		
		// This function executes the game's GoTo function.
        public static function goTo(playerName:String):void
        {
            game.world["goto"](playerName);
        }

		// This function executes by using the corresponding item boost.
        public static function useBoost(itemID:String):void
        {
            var item:Object = Inventory.getItemByID(parseInt(itemID));
            if (item != null)
            {
                game.world.sendUseItemRequest(item);
            };
        }

		// This function executes by getting the corresponding map item.
        public static function getMapItem(itemID:String):void
        {
            game.world.getMapItem(parseInt(itemID));
        }

		// This function returns a boolean for whether the player has an active boost or not.
        public static function hasActiveBoost(boostType:String):String
        {
            if (boostType.toLowerCase().indexOf("gold") > -1)
            {
                return ((game.world.myAvatar.objData.iBoostG > 0) ? Root.trueString : Root.falseString);
            }
            else if (boostType.toLowerCase().indexOf("xp") > -1)
            {
                return ((game.world.myAvatar.objData.iBoostXP > 0) ? Root.trueString : Root.falseString);
            }
            else if (boostType.toLowerCase().indexOf("rep") > -1)
            {
                return ((game.world.myAvatar.objData.iBoostRep > 0) ? Root.trueString : Root.falseString);
            }
            else if (boostType.toLowerCase().indexOf("class") > -1)
            {
                return ((game.world.myAvatar.objData.iBoostCP > 0) ? Root.trueString : Root.falseString);
            }
            return (Root.falseString);
        }

		// This function returns the player's class name.
        public static function className():String
        {
            return (('"' + game.world.myAvatar.objData.strClassName.toUpperCase()) + '"');
        }

		// This function returns the player's User ID.
        public static function userID():int
        {
            return (game.world.myAvatar.uid);
        }

		// This function returns the player's character ID.
        public static function charID():int
        {
            return (game.world.myAvatar.objData.CharID);
        }

		// This function returns the player's gender.
        public static function gender():String
        {
            return (('"' + game.world.myAvatar.objData.strGender.toUpperCase()) + '"');
        }

		// This function returns the player's datas.
        public static function playerData():Object
        {
            return (game.world.myAvatar.objData);
        }

		// This function executes by equipping the corresponding item on client-side.
        public static function setEquip(slot:*, equip:*):void
        {
            if (game.world.myAvatar.pMC.pAV.objData.eqp.Weapon == null)
            {
                return;
            };
            if (slot == "Off")
            {
                game.world.myAvatar.pMC.pAV.objData.eqp.Weapon.sLink = equip.sLink;
                game.world.myAvatar.pMC.loadWeaponOff(equip.sFile, equip.sLink);
                game.world.myAvatar.pMC.pAV.getItemByEquipSlot("Weapon").sType = "Dagger";
            }
            else
            {
                game.world.myAvatar.objData.eqp[slot] = equip;
                game.world.myAvatar.loadMovieAtES(slot, equip.sFile, equip.sLink);
            };
        }

		// This function returns the player's equipment datas.
        public static function getEquip(itemID:int):String
        {
            return (JSON.stringify(game.world.avatars[itemID].objData.eqp));
        }

		// This function executes by changing the player's name on client-side.
        public static function changeName(name:String):void
        {
            game.world.myAvatar.pMC.pAV.objData.strUsername = name.toUpperCase();
            game.world.myAvatar.objData.strUsername = name.toUpperCase();
            game.world.myAvatar.pMC.pname.ti.text = name.toUpperCase();
            game.ui.mcPortrait.strName.text = name.toUpperCase();
        }

		// This function executes by changing the player's guild name on client-side.
        public static function changeGuild(name:String):void
        {
			game.world.myAvatar.pMC.pname.tg.text = name.toUpperCase();
			game.world.myAvatar.objData.guild.Name = name.toUpperCase();
			game.world.myAvatar.pMC.pAV.objData.guild.Name = name.toUpperCase();
        }

		// This function executes by changing the player's access level on client-side.
        public static function changeAccessLevel(type:String):void
        {
            if (type == "Non Member")
            {
                game.world.myAvatar.pMC.pname.ti.textColor = 0xFFFFFF;
                game.world.myAvatar.pMC.pname.filters = [new GlowFilter(0, 1, 3, 3, 64, 1)];
                game.world.myAvatar.objData.iUpgDays = -1;
                game.world.myAvatar.objData.iUpg = 0;
            }
            else if (type == "Member")
			{
				game.world.myAvatar.pMC.pname.ti.textColor = 9229823;
				game.world.myAvatar.pMC.pname.filters = [new GlowFilter(0, 1, 3, 3, 64, 1)];
				game.world.myAvatar.objData.iUpgDays = 30;
				game.world.myAvatar.objData.iUpg = 1;
			}
			else if (type == "Moderator")
			{
				game.world.myAvatar.pMC.pname.ti.textColor = 16698168;
				game.world.myAvatar.pMC.pname.filters = [new GlowFilter(0, 1, 3, 3, 64, 1)];
				game.world.myAvatar.objData.intAccessLevel = 60;
			};
        }
		
		// This function executes by changing the player's class name on client-side.
		public static function changeClass(name:String):void
		{
			game.ui.mcPortrait.strClass.text = name;
			game.world.myAvatar.objData.strClassName = name;
		}
		
		// This function returns a boolean for whether the player has a target or not (by using the corresponding monster ID).
        public static function hasTargetByID(monID:int):String
        {
			var monster:Object = game.world.getMonster(monID);
            return (((game.world.myAvatar.target == monster) && (game.world.myAvatar.target.dataLeaf.intHP > 0)) ? Root.trueString : Root.falseString);
        }
		
		// This function executes by restarting the player's AFK timer.
		public static function afkPostpone():void
        {
            var ts:* = new Date().getTime();
            var userTree:* = game.world.uoTree[game.sfc.myUserName];
			userTree.afkts = ts;
        }
		
		// This function executes by canceling the player's AFK state.
		public static function cancelAfk():void
		{
			game.world.afkPostpone();
		}
		
		// This function returns a boolean for whether the player's avatar has loaded or not.
		public static function isAvatarLoaded():String
		{
			return (((game.world.myAvatar.invLoaded) || (game.world.myAvatar.pMC.artLoaded())) ? Root.trueString : Root.falseString);
		}
		
    }
}//package grimoire.game


