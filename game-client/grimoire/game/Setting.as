package grimoire.game
{
	import flash.events.MouseEvent;
    import grimoire.Root;
	import grimoire.game.Client;

    public class Setting
    {
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;
		private static var _untargetDeadTgts:Boolean = false;
		private static var _untargetSelf:Boolean = false;
		private static var _reaccQuest:Boolean = false;
		private static var _warnDecline:Boolean = false;
		private static var _presetDropUI:Boolean = false;
		public static var _visibility:Boolean = true;
		public static var oneTime:Object = {lagKiller:false, setFPS:false};

		//This function sets the player's skill ranges to infinite when executed with a True boolean, or the original ranges when executed with a False boolean.
        public static function toggleInfiniteRange(toggle:Boolean):void
        {
            var i:int = 0;
			var originalRange:int;
            while (i < 6)
            {
				if (toggle)
				{
					game.world.actions.active[i].range = 20000;
				}
				else
				{
					originalRange = parseInt(grimoire.game.Skill.skillRange.split(";")[i]);
					game.world.actions.active[i].range = originalRange;
				}
                i++;
            };
        }

		//This function executes the game's aggroMon function.
        public static function provokeMonsters():void
        {
            game.world.aggroAllMon();
        }

		//This function forcibly pulls the enemy target to the player's position. (CLIENT-SIDE)
        public static function setEnemyMagnet():void
        {
            if (game.world.myAvatar.target != null)
            {
                game.world.myAvatar.target.pMC.x = game.world.myAvatar.pMC.x;
                game.world.myAvatar.target.pMC.y = game.world.myAvatar.pMC.y;
            };
        }

		//This function sets the game world's visibility to Off when executed with a True boolean.
        public static function toggleLagKiller(visibility:Boolean):void
        {
			if (!visibility && !oneTime.lagKiller)
			{
				Client.msgBox("The game screen is on Lag Killer mode. This mode improves the client's performance. You can turn it off by going to the Options tab on the main menu and untick the option.");
				oneTime.lagKiller = true;
			}
			_visibility = visibility;
            game.world.visible = visibility;
        }

		//This function hides all player's avatar (excluding self) in the room.
        public static function hidePlayers(hide:Boolean):void
        {
			var avatar:*;
            for each (avatar in game.world.avatars)
            {
                if ((((avatar != null) && (avatar.pnm != null)) && (!avatar.isMyAvatar)))
                {
                    if (hide)
                    {
                        avatar.hideMC();
                    }
                    else
					{
						avatar.showMC();
					};
                };
            };
        }

		
		//This function skips all cutscene in the room.
        public static function skipCutscenes(cell:String, pad:String):void
        {
            while (game.mcExtSWF.numChildren > 0)
            {
                game.mcExtSWF.removeChildAt(0);
            };
			if (game.world.strFrame != cell)
			{
				game.world.moveToCell(cell, pad);
			}
			if (_visibility)
			{
				game.world.visible = true;
			}
			game.showInterface();
        }

		//This function sets the player's walk speed to a corresponding value.
        public static function setWalkSpeed(speed:int):void
        {
            game.world.WALKSPEED = speed;
        }
		
		//This function disables all animation in the room.
		public static function disableAnimations(disable:Boolean):void
        {
			game.world.showAnimations = !disable;
        }

		//This function sets Untarget Dead, Untarget Self, Reaccept Quest, and Decline Warning options to a corresponding value.
		public static function toggleAdvancedOpt(force:Boolean):void
		{
			var data:* = game.litePreference.data;
			if (data.bUntargetDead || _untargetDeadTgts)
			{
				_untargetDeadTgts = !_untargetDeadTgts;
				data.bUntargetDead = !data.bUntargetDead;
			}
			if ((data.bUntargetSelf || _untargetSelf) && force)
			{
				_untargetSelf = !_untargetSelf;
				data.bUntargetSelf = !data.bUntargetSelf;
			}
			if (data.bReaccept || _reaccQuest)
			{
				_reaccQuest = !_reaccQuest;
				data.bReaccept = !data.bReaccept;
			}
			if (data.bCustomDrops && (data.dOptions["warnDecline"] || _warnDecline))
			{
				_warnDecline = !_warnDecline;
				data.dOptions["warnDecline"] = !data.dOptions["warnDecline"];
			}
		}
		
		//This function executes by toggling the drop UI options.
		public static function toggleDropUIOpt(force:Boolean):void
		{
			var data:* = game.litePreference.data;
			if (data.bCustomDrops)
            {
				if (force || (data.dOptions["dragMode"] && !game.cDropsUI.isMenuOpen()))
				{
					game.cDropsUI.mcDraggable.menuBar.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
				}
				else if (force || (!data.dOptions["dragMode"] && !game.cDropsUI.isMenuOpen()))
				{
					game.cDropsUI.inner_menu.dispatchEvent(new MouseEvent(MouseEvent.CLICK));
				}
            }
		}
		
		// This function disables the Death Ad in-game.
		public static function disableDeathAd():void
		{
			var bDeathAd:* = game.userPreference.data.bDeathAd;
			if (bDeathAd || (bDeathAd == null)) 
			{
				bDeathAd = false;
			}
		};		
		
		// This function executes by enabling the player's ability to chat.
		public static function enableChat():void
		{
			if (game.world.myAvatar.objData.intActivationFlag != 5)
			{
				game.world.myAvatar.objData.intActivationFlag = 5;
			}
		}		
		
    }
}//package grimoire.game

