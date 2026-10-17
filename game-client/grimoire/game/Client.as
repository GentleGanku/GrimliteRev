package grimoire.game
{
    import flash.ui.Keyboard;
    import flash.display.MovieClip;
    import flash.display.DisplayObject;
    import flash.events.MouseEvent;
    import flash.events.KeyboardEvent;
    import flash.events.TimerEvent;
    import flash.utils.Timer;
    import flash.external.ExternalInterface;
    import flash.utils.getQualifiedClassName;
    import grimoire.Root;

    public class Client
    {
        // #region Fields

        public static var cTravelTriggers:String = "";
        public static var timer:Timer = new Timer(10000);
        public static var shouldPing:Boolean;
        public static var funcYes:Function;
        public static var funcNo:Function;
        public static var latency:Number;
        // The connection screen text that each isClientLoading type looks for.
        private static const loadingTexts:Object = {
            Account: "Authenticating Account Info...",
            Client: "Initializing Client...",
            Lobby: "Joining Lobby..",
            Server: "Connecting to game server...",
            Character: "Loading Character Data...",
            Communication: "Communication with server has been lost. Please check your internet connection and try again.",
            MapLoading: "Loading Map Files...",
            MapLoadingStuck: "Loading Map... 100%",
            MapLoadingError: "Loading Map Files... Failed!"
        };
        private static var game:* = Root.Game;
        private static var charPage:*;
        private static var hookedInterface:*;
        private static var officialStandby:DisplayObject;
        private static var officialLoader:DisplayObject;

        // #endregion

        // #region Methods

        // This function returns a boolean for whether the client is loading on the corresponding type. MapLoadingStuck also
        // needs the back button, which the game shows 10 seconds after the last progress update (ConnDetailMC), so a map
        // that stops at 100% counts as stuck only after that.
        public static function isClientLoading(type:String):String
        {
            if (!loadingTexts.hasOwnProperty(type))
            {
                return Root.falseString;
            }
            var detail:* = game.mcConnDetail;
            var loading:Boolean = ((detail.txtDetail.text == loadingTexts[type])
                && ((type != "MapLoadingStuck") || detail.btnBack.visible)
                && (detail.stage != null));
            return loading ? Root.trueString : Root.falseString;
        }

        // This function returns a boolean for whether the client label is on the Login label or not.
        public static function loginLabel():String
        {
            return (game.currentLabel == "Login" || game.currentLabel == "Select") ? Root.trueString : Root.falseString;
        }

        // This function returns a boolean for whether the client label is on the Server label or not.
        public static function serverLabel():String
        {
            return (game.mcLogin.currentLabel == "Servers") ? Root.trueString : Root.falseString;
        }

        // This function returns a boolean for whether the client label is on the Game label or not.
        public static function gameLabel():String
        {
            return (game.currentLabel == "Game") ? Root.trueString : Root.falseString;
        }

        // This function returns a boolean for whether the game has loaded or not.
        public static function inGame():String
        {
            if (game.intChatMode)
            {
                return game.ui.mcInterface.ncText.visible ? Root.trueString : Root.falseString;
            }
            else
            {
                return game.ui.mcInterface.te.visible ? Root.trueString : Root.falseString;
            }
        }

        // This function returns a boolean for whether the connection screen is visible or not.
        public static function inConnStage():String
        {
            return (game.mcConnDetail.stage != null) ? Root.trueString : Root.falseString;
        }

        // This function puts the bridge's chat listeners on the game's chat input once per interface the game builds. They
        // run at a higher priority than the game's own listeners on the same fields, so they see each line first and stop
        // only the ones the bridge handles. Root calls this every frame, which costs a few property reads.
        public static function hookChat():void
        {
            var mci:* = (game.ui == null) ? null : game.ui.mcInterface;
            if ((mci == null) || (mci == hookedInterface))
            {
                return;
            }
            hookedInterface = mci;
            if (("ncText" in mci) && (mci.ncText != null))
            {
                mci.ncText.addEventListener(KeyboardEvent.KEY_DOWN, onChatKey, false, 1);
            }
            if (("te" in mci) && (mci.te != null))
            {
                mci.te.addEventListener(KeyboardEvent.KEY_DOWN, onChatKey, false, 1);
            }
            if (("ncSendText" in mci) && (mci.ncSendText != null))
            {
                mci.ncSendText.addEventListener(MouseEvent.CLICK, onChatSend, false, 1);
            }
        }

        // This function returns a chat trigger list for the Custom Travel feature.
        public static function loadTravelTriggers(triggers:String):void
        {
            cTravelTriggers = triggers;
        }

        // This function executes by starting a timer for the Latency packet.
        public static function startLatencyTimer():void
        {
            if (!timer.running)
            {
                latency = new Date().getTime();
                game.sfc.sendXtMessage("zm", "hi", [], "str", 1);
                shouldPing = false;
                var f:Function;
                timer.addEventListener(TimerEvent.TIMER, f = function():void
                {
                    if (Root.isLoggedIn && shouldPing && !game.world.myAvatar.dataLeaf.afk)
                    {
                        latency = new Date().getTime();
                        game.sfc.sendXtMessage("zm", "hi", [], "str", 1);
                        shouldPing = false;
                    }
                });
                timer.start();
            }
        }

        // This function executes by joining on a custom travel.
        public static function gotoCustomTravel(text:String):void
        {
            if ((cTravelTriggers != "") && (cTravelTriggers != null))
            {
                var triggerList:Array = cTravelTriggers.split("|");
                var travelIndex:int = text.split("t")[1];
                if (travelIndex <= (triggerList.length - 1))
                {
                    var target:String = triggerList[travelIndex];
                    var travel:Array = target.split("`");
                    game.world.gotoTown(travel[1], travel[2], travel[3]);
                }
            }
        }

        // This function executes by displaying a character page for the corresponding name.
        public static function displayCharPage(name:String):void
        {
            var cls:Class = Class(Root.gameDomain.getDefinition("liteAssets.draw.charPage"));
            charPage = new cls(game, name);
            game.ui.addChild(charPage);
        }

        // This function executes by Gullible.
        public static function gullible():void
        {
            game.world.strAreaName = "gullible-1";
            game.updateAreaName();
            game.world.loadMap("town-limbo.swf");
            game.chatF.pushMsg("server", 'You joined "gullible-1"', "SERVER", "", 0);
        }

        // This function executes by sending a message box from the game client.
        public static function msgBox(text:String, stayOnScreen:Boolean=true, yesNo:Boolean=false, fYes:Function=null, fNo:Function=null):void
        {
            if (yesNo)
            {
                game.showConfirmtaionBox(text, null);
            }
            else
            {
                game.showMessageBox(text, null);
            }
            funcYes = fYes;
            funcNo = fNo;
            var child:MovieClip;
            var i:int = 0;
            while (i < game.ui.ModalStack.numChildren)
            {
                child = (game.ui.ModalStack.getChildAt(i) as MovieClip);
                if (child.cnt.strBody.text == text)
                {
                    child.greedy = stayOnScreen;
                    if (yesNo)
                    {
                        child.cnt.btns.dual.ybtn.removeEventListener(MouseEvent.CLICK, child.yClick);
                        child.cnt.btns.dual.nbtn.removeEventListener(MouseEvent.CLICK, child.nClick);
                        child.cnt.btns.dual.ybtn.addEventListener(MouseEvent.CLICK, yClick, false, 0, true);
                        child.cnt.btns.dual.nbtn.addEventListener(MouseEvent.CLICK, nClick, false, 0, true);
                    }
                    break;
                }
                i++;
            }
        }

        // This function shifts the backgrounds to the corresponding target in the Backgrounds array. The game's own art is
        // kept so setBackgroundIndex can put it back.
        public static function shiftBackgrounds(main:int = 0, standby:int = 0, loader:int = 0):void
        {
            if (Root.BackgroundIndex < 0)
            {
                Root.BackgroundIndex = Root.DebugMode ? 0 : Number(ExternalInterface.call("getBackgroundIndex"));
            }

            if (Root.BackgroundIndex != main)
            {
                Root.BackgroundIndex = main;

                Root.MainBackground = new Root.Backgrounds[main]("MainBackground");
                Root.StandbyBackground = new Root.Backgrounds[standby]("StandbyBackground");
                Root.LoaderBackground = new Root.Backgrounds[loader]("LoaderBackground");
            }

            if (game.mcLogin)
            {
                game.mcLogin.mcTitle.removeChildAt(0);
                game.mcLogin.mcTitle.addChild(Root.MainBackground);
                game.mcLogin.mcTitle.visible = true;
            }

            if (!isCustomBackground(game.mcConnDetail.getChildAt(0)))
            {
                officialStandby = game.mcConnDetail.getChildAt(0);
            }
            game.mcConnDetail.removeChildAt(0);
            game.mcConnDetail.addChildAt(Root.StandbyBackground, 0);

            if (!isCustomBackground(game.ldrMC.getChildAt(0)))
            {
                officialLoader = game.ldrMC.getChildAt(0);
            }
            game.ldrMC.removeChildAt(0);
            game.ldrMC.addChildAt(Root.LoaderBackground, 0);
        }

        // This function switches between a custom background (index 0 and up) and the game's own backgrounds (below 0) without a restart.
        public static function setBackgroundIndex(index:Number):void
        {
            if (index >= 0)
            {
                game.params.sBG = "";
                Root.BackgroundIndex = index;
                Root.MainBackground = new Root.Backgrounds[index]("MainBackground");
                Root.StandbyBackground = new Root.Backgrounds[index]("StandbyBackground");
                Root.LoaderBackground = new Root.Backgrounds[index]("LoaderBackground");
                shiftBackgrounds(index, index, index);
                return;
            }

            Root.BackgroundIndex = index;
            game.params.sBG = game.params.vars.sBG;
            if (officialStandby && isCustomBackground(game.mcConnDetail.getChildAt(0)))
            {
                game.mcConnDetail.removeChildAt(0);
                game.mcConnDetail.addChildAt(officialStandby, 0);
            }
            if (officialLoader && isCustomBackground(game.ldrMC.getChildAt(0)))
            {
                game.ldrMC.removeChildAt(0);
                game.ldrMC.addChildAt(officialLoader, 0);
            }
            if (game.mcLogin)
            {
                game.loadTitle();
            }
        }

        // This function veils the player's profile and modifies the corresponding UI elements to reflect the anonymous status.
        public static function setAnonymousMode(enable:Boolean = true):void
        {
            Root.ShouldBeAnonymous = enable ? Root.trueString : Root.falseString;

            var myAvt:* = game.world.myAvatar;
            var mcP:* = game.ui.mcPortrait;
            var mcPT:* = game.ui.mcPortraitTarget;
            var uoTree:* = game.world.uoTree;
            var oHData:* = game.world.objHouseData;
            var wMap:* = game.world.map;
            var oFr:* = game.ui.mcOFrame;

            switch (enable)
            {
                case true:
                    if (myAvt)
                    {
                        if (!myAvt.dataLeaf.anonymous && myAvt.objData)
                        {
                            myAvt.dataLeaf.ogName = myAvt.objData.strUsername;
                            myAvt.dataLeaf.anonymous = true;

                            myAvt.pMC.mcChar.visible = false;
                            myAvt.pMC.pname.ti.text = myAvt.pMC.pname.ti.text.indexOf("<AFK>") > -1 ? ("<AFK> " + "Anonymous") : "Anonymous";

                            mcP.mcHead.visible = false;
                        }

                        if (mcP.strName.text != "ANONYMOUS")
                        {
                            mcP.strName.text = "ANONYMOUS";
                        }

                        if (uoTree[myAvt.pnm])
                        {
                            if (uoTree[myAvt.pnm].strUsername != "Anonymous")
                            {
                                uoTree[myAvt.pnm].strUsername = "Anonymous";
                            }
                        }

                        if (myAvt.objData)
                        {
                            if (myAvt.objData.strUsername != "Anonymous")
                            {
                                myAvt.objData.strUsername = "Anonymous";
                            }

                            if (myAvt.pMC.pname.tg.text != "< Anonymous >")
                            {
                                myAvt.pMC.pname.tg.text = "< Anonymous >";
                            }

                            if (mcPT.strName.text == myAvt.dataLeaf.ogName.toUpperCase())
                            {
                                mcPT.mcHead.visible = false;
                                mcPT.strName.text = "ANONYMOUS";
                            }
                            else
                            {
                                mcPT.mcHead.visible = true;
                            }
                        }

                        if (oHData)
                        {
                            if (oHData.unm == myAvt.dataLeaf.ogName.toLowerCase())
                            {
                                oHData.unm = "anonymous";

                                if (wMap && wMap.txtHouse)
                                {
                                    wMap.txtHouse.text = "Anonymous's House";
                                }
                            }
                        }
                    }

                    var arTxt:* = game.ui.mcInterface.areaList.title.t1;
                    if (arTxt.htmlText.indexOf("-?") == -1)
                    {
                        arTxt.htmlText = arTxt.htmlText.replace(arTxt.text.split(game.world.strMapName)[1], "-?");
                    }

                    if (oFr && oFr.fData && oFr.fData.ul)
                    {
                        if (oFr.fData.ul[game.sfc.myUserId.toString()])
                        {
                            oFr.fData.ul[game.sfc.myUserId.toString()].sName = "Anonymous";
                        }
                    }
                    break;

                case false:
                    if (myAvt)
                    {
                        myAvt.dataLeaf.anonymous = false;

                        myAvt.pMC.mcChar.visible = true;
                        myAvt.pMC.pname.ti.text = myAvt.pMC.pname.ti.text.indexOf("<AFK>") > -1 ? ("<AFK> " + myAvt.dataLeaf.ogName) : myAvt.dataLeaf.ogName;

                        mcP.mcHead.visible = true;
                        mcP.strName.text = myAvt.dataLeaf.ogName.toUpperCase();

                        if (uoTree[myAvt.pnm])
                        {
                            uoTree[myAvt.pnm].strUsername = myAvt.dataLeaf.ogName;
                        }

                        if (myAvt.objData)
                        {
                            myAvt.objData.strUsername = myAvt.dataLeaf.ogName;
                            myAvt.pMC.pname.tg.text = "< " + (myAvt.objData.guild ? myAvt.objData.guild.Name : "") + " >";

                            if (mcPT.strName.text == "ANONYMOUS")
                            {
                                mcPT.mcHead.visible = true;
                                mcPT.strName.text = myAvt.dataLeaf.ogName.toUpperCase();
                            }
                        }

                        if (oHData)
                        {
                            if (oHData.unm == "anonymous")
                            {
                                oHData.unm = myAvt.dataLeaf.ogName.toLowerCase();

                                if (wMap && wMap.txtHouse)
                                {
                                    wMap.txtHouse.text = game.strToProperCase(oHData.unm) + "'s House";
                                }
                            }
                        }

                        game.updateAreaName();

                        if (oFr && oFr.fData && oFr.fData.ul)
                        {
                            if (oFr.fData.ul[game.sfc.myUserId.toString()])
                            {
                                oFr.fData.ul[game.sfc.myUserId.toString()].sName = myAvt.dataLeaf.ogName;
                            }
                        }
                    }
                    break;
            }
        }

        // This function runs before the game's Enter handler and stops the key when the bridge has handled the line.
        private static function onChatKey(e:KeyboardEvent):void
        {
            if ((e.keyCode == Keyboard.ENTER) && handleChatLine(e.currentTarget))
            {
                e.stopImmediatePropagation();
            }
        }

        // This function runs before the game's Send button handler and stops the click when the bridge has handled the line.
        private static function onChatSend(e:MouseEvent):void
        {
            if (handleChatLine(game.ui.mcInterface.ncText))
            {
                e.stopImmediatePropagation();
            }
        }

        // This function handles a chat line before the game sends it, and returns true when the game must not see it:
        // .ct travel shortcuts, the gullible easter egg, and the bot's own / commands, which C# runs and confirms through
        // the chatCommand return value. The game's own commands such as /join still reach it, and "/join map-1e99" gets a
        // random room from 1001 to 99999 in place of 1e99 first.
        private static function handleChatLine(field:*):Boolean
        {
            var text:String = field.text;
            if (text.indexOf(".ct") == 0)
            {
                gotoCustomTravel(text);
                clearChat(field);
                return true;
            }
            if (text.indexOf("/join gullible") == 0)
            {
                gullible();
                return true;
            }
            if ((text.indexOf("/join ") == 0) && (text.indexOf("-") > -1) && (text.split("-")[1].indexOf("1e99") == 0))
            {
                text = text.split("-")[0] + "-" + (Math.floor(Math.random() * 98999) + 1001);
                field.text = text;
            }
            if ((text.indexOf("/") == 0) && (ExternalInterface.call("chatCommand", text.substr(1)) == true))
            {
                clearChat(field);
                return true;
            }
            return false;
        }

        // This function empties the chat input after the bridge has handled its line.
        private static function clearChat(field:*):void
        {
            field.text = "";
            game.stage.focus = null;
        }

        private static function yClick(e:MouseEvent):void
        {
            funcYes();
        }

        private static function nClick(e:MouseEvent):void
        {
            funcNo();
        }

        private static function isCustomBackground(child:DisplayObject):Boolean
        {
            return getQualifiedClassName(child).indexOf("grimoire.backgrounds::") == 0;
        }

        // #endregion
    }
}
