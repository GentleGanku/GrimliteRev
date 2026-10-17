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
        private static var officialStandby:DisplayObject;
        private static var officialLoader:DisplayObject;

        // #endregion

        // #region Methods

        // This function returns a boolean for whether the client is loading on the corresponding type.
        public static function isClientLoading(type:String):String
        {
            if (!loadingTexts.hasOwnProperty(type))
            {
                return Root.falseString;
            }
            var detail:* = game.mcConnDetail;
            // btnBack.visible is a Boolean and never equals the '"True"' string, so MapLoadingStuck is never reported
            // (CR-43, fix phase). Kept as it was.
            var loading:Boolean = ((detail.txtDetail.text == loadingTexts[type])
                && ((type != "MapLoadingStuck") || (detail.btnBack.visible == Root.trueString))
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

        // This function executes by modifying the Send to Chat button's events (on click and on Enter key) to integrate them with the Chat Trigger handle.
        public static function modifyBtnSend():void
        {
            game.ui.mcInterface.ncSendText.removeEventListener(MouseEvent.CLICK, game.chatF.onBtnSendText);
            game.ui.mcInterface.ncSendText.addEventListener(MouseEvent.CLICK, onBtnSendText);
            game.ui.mcInterface.ncText.removeEventListener(KeyboardEvent.KEY_DOWN, game.key_ChatEntry);
            game.ui.mcInterface.ncText.addEventListener(KeyboardEvent.KEY_DOWN, key_ChatEntry);
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

        // This function shifts the backgrounds to the corresponding target in the Backgrounds array.
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

            // The game's own art is kept so setBackgroundIndex can put it back.
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

        private static function onBtnSendText(e:MouseEvent):void
        {
            if (game.ui.mcInterface.ncText.text.indexOf(".") == 0 && game.ui.mcInterface.ncText.text.indexOf("c") == 1 && game.ui.mcInterface.ncText.text.indexOf("t") == 2)
            {
                gotoCustomTravel(game.ui.mcInterface.ncText.text);
                closeChat();
            }
            else
            {
                var text:String = game.ui.mcInterface.ncText.text;
                if (text.indexOf("/join gullible") == 0)
                {
                    gullible();
                    return;
                }
                if ((text.indexOf("/join ") == 0) && (text.indexOf("-") > -1) && (text.split("-")[1].indexOf("1e99") == 0))
                {
                    var room:int = Math.floor( Math.random() * 98999 | 1001);
                    text = text.split("-")[0] + "-" + (text.split("-")[1] = room.toString());
                }
                if (text.indexOf("/") == 0)
                {
                    ExternalInterface.call("chatCommand", text.replace("/", ""));
                    if ((text.indexOf("/combat") == 0) || (text.indexOf("/fps") == 0))
                    {
                        closeChat();
                        return;
                    }
                }
                game.chatF.submitMsg(text, game.chatF.chn.cur.typ, game.chatF.pmNm);
                game.stage.focus = null;
            }
        }

        private static function key_ChatEntry(e:KeyboardEvent):void
        {
            if (e.keyCode == Keyboard.ENTER)
            {
                var chatText:String = game.intChatMode ? game.ui.mcInterface.ncText.text : game.ui.mcInterface.te.text;
                if (chatText.indexOf(".") == 0 && chatText.indexOf("c") == 1 && chatText.indexOf("t") == 2)
                {
                    gotoCustomTravel(chatText);
                    closeChat();
                }
                else
                {
                    var text:String = chatText;
                    if (text.indexOf("/join gullible") == 0)
                    {
                        gullible();
                        return;
                    }
                    if ((text.indexOf("/join ") == 0) && (text.indexOf("-") > -1) && (text.split("-")[1].indexOf("1e99") == 0))
                    {
                        var room:int = Math.floor( Math.random() * 98999 | 1001);
                        text = text.split("-")[0] + "-" + (text.split("-")[1] = room.toString());
                    }
                    if (text.indexOf("/") == 0)
                    {
                        ExternalInterface.call("chatCommand", text.replace("/", ""));
                        if ((text.indexOf("/combat") == 0) || (text.indexOf("/fps") == 0))
                        {
                            closeChat();
                        }
                        return;
                    }
                    game.chatF.submitMsg(text, game.chatF.chn.cur.typ, game.chatF.pmNm);
                }
            }
            if (e.keyCode == Keyboard.ESCAPE)
            {
                game.chatF.closeMsgEntry();
            }
        }

        private static function closeChat():void
        {
            if (game.intChatMode)
            {
                game.ui.mcInterface.ncText.text = "";
            }
            else
            {
                game.ui.mcInterface.te.text = "";
            }
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
