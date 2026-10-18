package grimoire.game
{
    import flash.events.MouseEvent;
    import flash.net.SharedObject;
    import grimoire.Root;

    public class Setting
    {
        // #region Fields

        public static var _visibility:Boolean = true;
        public static var oneTime:Object = {lagKiller:false, setFPS:false};
        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function sets the player's skill ranges to infinite when executed with a True boolean, or the original ranges when executed with a False boolean.
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
            }
        }

        // This function forcibly pulls the enemy target to the player's position. (CLIENT-SIDE)
        public static function setEnemyMagnet():void
        {
            if (game.world.myAvatar.target != null)
            {
                game.world.myAvatar.target.pMC.x = game.world.myAvatar.pMC.x;
                game.world.myAvatar.target.pMC.y = game.world.myAvatar.pMC.y;
            }
        }

        // This function sets the game world's visibility to Off when executed with a True boolean.
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

        // This function hides all player's avatar (excluding self) in the room.
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
                    }
                }
            }
        }

        // This function skips all cutscene in the room.
        public static function skipCutscenes(cell:String, pad:String):void
        {
            while (game.mcExtSWF.numChildren > 0)
            {
                game.mcExtSWF.removeChildAt(0);
            }
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

        // This function sets the player's walk speed to a corresponding value.
        public static function setWalkSpeed(speed:int):void
        {
            game.world.WALKSPEED = speed;
        }

        // This function turns off the game's Untarget Dead, Reaccept Quest and Decline Warning options while a bot runs
        // (running true), and puts back the values it turned off when the bot stops. The game saves its preferences when
        // the client closes, so the values are also kept in the bridge's own SharedObject: after a crash mid-bot, the next
        // start keeps those instead of saving the ones left switched off. A value saved by an older version, such as
        // Untarget Self, is still put back.
        public static function toggleAdvancedOpt(running:Boolean = false):void
        {
            var data:* = game.litePreference.data;
            var store:SharedObject = SharedObject.getLocal("grimliteAdvancedOpt");
            var saved:Object = store.data.saved;
            var key:String;
            if (running)
            {
                if (saved == null)
                {
                    saved = {"bUntargetDead": data.bUntargetDead, "bReaccept": data.bReaccept};
                    if (data.bCustomDrops)
                    {
                        saved.warnDecline = data.dOptions["warnDecline"];
                    }
                    store.data.saved = saved;
                    store.flush();
                }
                for (key in saved)
                {
                    if (key == "warnDecline")
                    {
                        data.dOptions["warnDecline"] = false;
                    }
                    else
                    {
                        data[key] = false;
                    }
                }
            }
            else if (saved != null)
            {
                for (key in saved)
                {
                    if (key == "warnDecline")
                    {
                        data.dOptions["warnDecline"] = saved[key];
                    }
                    else
                    {
                        data[key] = saved[key];
                    }
                }
                delete store.data.saved;
                store.flush();
            }
        }

        // This function returns a boolean for whether the custom drops menu is open or not (False while Custom Drops UI is off).
        public static function isDropMenuOpen():String
        {
            return (game.litePreference.data.bCustomDrops && game.cDropsUI && game.cDropsUI.isMenuOpen()) ? Root.trueString : Root.falseString;
        }

        // This function executes by toggling the drop UI options.
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
            game.userPreference.data.bDeathAd = false;
        }

        // This function executes by enabling the player's ability to chat.
        public static function enableChat():void
        {
            if (game.world.myAvatar.objData.intActivationFlag != 5)
            {
                game.world.myAvatar.objData.intActivationFlag = 5;
            }
        }

        // #endregion
    }
}
