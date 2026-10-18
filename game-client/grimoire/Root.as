package grimoire
{
    import grimoire.backgrounds.*;
    import grimoire.game.*;
    import grimoire.tools.*;

    import flash.display.*;
    import flash.events.*;
    import flash.external.*;
    import flash.net.*;
    import flash.system.*;
    import flash.utils.*;
    import flash.ui.*;

    public class Root extends MovieClip
    {
        // #region Fields

        public static const trueString:String = '"True"';
        public static const falseString:String = '"False"';
        public static var username:String;
        public static var password:String;
        public static var Game:MovieClip;
        public static var instance:Root;
        public static var GameStage:Stage;
        public static var isLoggedIn:Boolean = false;
        public static var gameDomain:ApplicationDomain;
        public static var DebugMode:Boolean;
        public static var ShouldBeAnonymous:String;
        public static var Backgrounds:Array;
        public static var BackgroundIndex:Number;
        public static var MainBackground:MovieClip;
        public static var StandbyBackground:MovieClip;
        public static var LoaderBackground:MovieClip;
        private static const RECEIVED_PREFIX:String = "[ RECEIVED ]: ";
        // Most traffic is received, so that prefix is tried first.
        private static const DEBUG_PREFIXES:Array = [RECEIVED_PREFIX, "[Sending - STR]: ", "[Sending - JSON]: ", "[Sending]: "];
        private const sURL:String = "https://game.aq.com/game/";
        private const versionURL:String = (sURL + "api/data/gameversion");
        private static var _handler:*;
        private static var objLogin:Object = new Object();
        private var loginURL:* = (sURL + "api/login/now");
        private var external:Externalizer;
        private var loader:Loader;
        private var urlLoader:URLLoader;
        private var doSignup:Boolean;
        private var isEU:Boolean;
        private var isWeb:Boolean;
        private var loaderVars:Object;
        private var sBG:String;
        private var sFile:*;

        // #endregion

        // #region Constructors

        // This function creates the instance and the loader.
        public function Root()
        {
            Root.instance = this;
            Root.GameStage = this.stage;

            InitSuperObjects();

            addEventListener(Event.ADDED_TO_STAGE, this.onAddedToStage);
        }

        // #endregion

        // #region Methods

        // This function calls a delay action of milliseconds, waiting for the corresponding function to return True boolean, and then executes the second corresponding function.
        // It gives up after timeout milliseconds, so a condition that never comes true (servers that never load) doesn't
        // keep the timer ticking for the rest of the session.
        public static function waitFor(bool:Function, func:Function, funcParams:Array, delay:int=300, boolString:String=Root.trueString, timeout:int=60000):void
        {
            var f:Function;
            var timer:Timer = new Timer(delay, Math.max(1, Math.ceil(timeout / delay)));
            timer.addEventListener(TimerEvent.TIMER, f = function():void
            {
                if (bool() == boolString)
                {
                    timer.removeEventListener(TimerEvent.TIMER, f);
                    timer.stop();
                    func.apply(null, funcParams);
                }
            });
            timer.start();
        }

        // This function returns the username of the player.
        public static function Username():String
        {
            return quote(username);
        }

        // This function returns the password of the player.
        public static function Password():String
        {
            return quote(password);
        }

        // This function returns a value as a JSON string for the callbacks whose C# side decodes the result as JSON.
        // JSON.stringify escapes quotes, backslashes and control characters, so a password or a name with one of them no
        // longer breaks that decoding (CR-44). undefined and null still arrive as "undefined" and "null".
        public static function quote(value:*):String
        {
            return JSON.stringify(String(value));
        }

        // This function returns a monster or item name in the form names are compared in: lowercased, with curly and
        // look-alike apostrophes and quotes made straight. Names copied from the Wiki carry the curly ones, while the game
        // mostly uses straight ones.
        public static function nameKey(name:String):String
        {
            return (name == null) ? null : name.toLowerCase().replace(/[‘’‚‛ʼ′´]/g, "'").replace(/[“”„‟″]/g, "\"");
        }

        // This function sets the game's FPS to a corresponding number.
        public static function setFPS(rate:int):void
        {
            Game.addUpdate("Frames Per Second (FPS) cap has been changed from " + GameStage.frameRate + " FPS to " + rate + " FPS.");
            GameStage.frameRate = rate;
        }

        // This function sets the login screen's title to a corresponding string. params.sTitle keeps the plain title,
        // which the game builds the label from when it shows the login screen again.
        public static function setTitle(title:String):void
        {
            Game.params.sTitle = title;
            if (Game.mcLogin)
            {
                Game.mcLogin.mcLogo.txtTitle.htmlText = '<font color="#CC1F41">Release:</font> ' + title;
            }
        }

        // This function executes the game's Notify function.
        public static function gameMessage(text:String):void
        {
            Game.MsgBox.notify(text);
        }

        // This function executes the game's AddUpdate function.
        public static function gamePopup(text:String, red:Boolean=false):void
        {
            Game.addUpdate(text, red);
        }

        // This function sends a packet to the game client.
        public static function sendClientPacket(packet:String, type:String):void
        {
            var cls:Class;
            if (_handler == null)
            {
                cls = Class(gameDomain.getDefinition("it.gotoandplay.smartfoxserver.handlers.ExtHandler"));
                _handler = new cls(Game.sfc);
            }
            if (type == "xml")
            {
                xmlReceived(packet);
            }
            else if (type == "json")
            {
                jsonReceived(packet);
            }
            else if (type == "str")
            {
                strReceived(packet);
            }
        }

        // This function sends a packet in XML-type.
        public static function xmlReceived(packet:String):void
        {
            _handler.handleMessage(new XML(packet), "xml");
        }

        // This function sends a packet in JSON-type.
        public static function jsonReceived(packet:String):void
        {
            _handler.handleMessage(JSON.parse(packet)["b"], "json");
        }

        // This function sends a packet in XT-type.
        public static function strReceived(packet:String):void
        {
            var array:Array = packet.substr(1, (packet.length - 2)).split("%");
            _handler.handleMessage(array.splice(1, (array.length - 1)), "str");
        }

        // This function calls a game function.
        public static function callGameFunction(path:String, ... args):String
        {
            var parts:Array = path.split(".");
            var funcName:String = parts.pop();
            var obj:* = _getObjectA(Root.Game, parts);
            var func:Function = (obj[funcName] as Function);
            return JSON.stringify(func.apply(null, args));
        }

        public static function selectArrayObjects(path:String, selector:String):String
        {
            var obj:* = _getObjectS(Root.Game, path);
            if ((!(obj is Array)))
            {
                return "";
            }
            var array:Array = (obj as Array);
            var narray:Array = new Array();
            var j:int;
            while (j < array.length)
            {
                narray.push(_getObjectS(array[j], selector));
                j++;
            }
            return JSON.stringify(narray);
        }

        public static function _getObjectA(root:*, parts:Array):*
        {
            var i:int;
            var obj:* = root;
            i = 0;
            while (i < parts.length)
            {
                obj = obj[parts[i]];
                i++;
            }
            return obj;
        }

        public static function _getObjectS(root:*, path:String):*
        {
            return _getObjectA(root, path.split("."));
        }

        public static function getGameObject(path:String):String
        {
            var obj:* = _getObjectS(Root.Game, path);
            return JSON.stringify(obj);
        }

        // This function returns a static member of the game's document class, such as "loginInfo.strUsername".
        public static function getGameObjectS(path:String):String
        {
            return JSON.stringify(_getObjectS(gameDomain.getDefinition("Game"), path));
        }

        public static function setGameObject(path:String, value:*):void
        {
            var parts:Array = path.split(".");
            var varName:String = parts.pop();
            var obj:* = _getObjectA(Root.Game, parts);
            obj[varName] = value;
        }

        public static function isNull(path:String):String
        {
            try
            {
                return (_getObjectS(Root.Game, path) == null).toString();
            }
            catch (ex:Error) {}
            return "true";
        }

        // This function forwards each SmartFox debug message to C# once: extension messages (%xt%zm%, except the %hi% pings)
        // as "xtPacket" first, then every message as "packet", after keeping the bridge's ping and login state. It replaces
        // two listeners that each searched and filtered the same message.
        public function onDebugMessage(e:*):void
        {
            var message:String = e.params.message;
            var packet:String = this.filter(message);
            if ((message.indexOf("%xt%zm%") > -1) && (message.indexOf("%hi%") < 0))
            {
                this.forward("xtPacket", packet);
            }
            if (message.indexOf("%xt%zm%hi%") > -1)
            {
                return;
            }
            if ((message.indexOf("%xt%hi%") > -1) || (message.indexOf("Please slow down. Last action was too soon!") > -1))
            {
                grimoire.game.Client.shouldPing = true;
                return;
            }
            else if (message.indexOf("%xt%loginResponse%") > -1)
            {
                grimoire.game.Bank.loaded = false;
            }
            else if (message.indexOf('"cmd":"loadInventoryBig"') > -1)
            {
                isLoggedIn = true;
                grimoire.game.Client.shouldPing = true;
            }
            else if (message.indexOf("<msg t='sys'><body action='logout' r='0'></body></msg>") > -1)
            {
                isLoggedIn = false;
                grimoire.game.Client.shouldPing = false;
            }
            this.forward("packet", packet);
        }

        public function onExtensionResponse(packet:*):void
        {
            if (packet.params.type == "str")
            {
                if (packet.params.dataObj[0] == "hi")
                {
                    Client.latency = Client.latency - new Date().getTime();
                    Client.latency = Math.abs(Client.latency);

                    if (DebugMode)
                    {
                        trace("DEBUG: " + Client.latency + " ms");
                    }
                    else
                    {
                        ExternalInterface.call("ping", Client.latency);
                    }

                    if (Game.ui.mcPopup.getChildByName("mcO") != null)
                    {
                        Game.ui.mcPopup.getChildByName("mcO").latency = Client.latency;
                        Game.ui.mcPopup.getChildByName("mcO").updateLatency();
                    }
                }
            }

            if (DebugMode)
            {
                trace("DEBUG: " + JSON.stringify(packet));
            }
            else
            {
                ExternalInterface.call("pext", JSON.stringify(packet));
            }
        }

        // This function initializes the super objects.
        private function InitSuperObjects():void
        {
            DebugMode = Capabilities.isDebugger;

            ShouldBeAnonymous = DebugMode ? falseString : String(ExternalInterface.call("getAnonymousMode"));

            BackgroundIndex = DebugMode ? 0 : Number(ExternalInterface.call("getBackgroundIndex"));
            Backgrounds =
            [
                FableForest
            ];

            if (BackgroundIndex >= 0)
            {
                MainBackground = new Backgrounds[BackgroundIndex]("MainBackground");
                StandbyBackground = new Backgrounds[BackgroundIndex]("StandbyBackground");
                LoaderBackground = new Backgrounds[BackgroundIndex]("LoaderBackground");
            }
        }

        // This function starts loading the game once the bridge is on the stage. Only aq.com hosts may script this SWF,
        // which owns the stage and ExternalInterface; the game uses the same "*.aq.com" rule for itself.
        private function onAddedToStage(e:Event):void
        {
            removeEventListener(Event.ADDED_TO_STAGE, this.onAddedToStage);
            Security.allowDomain("*.aq.com");

            if (BackgroundIndex >= 0)
            {
                GameStage.addChild(MainBackground);
            }

            this.urlLoader = new URLLoader();
            this.urlLoader.addEventListener(Event.COMPLETE, this.onDataComplete);
            this.urlLoader.addEventListener(IOErrorEvent.IO_ERROR, this.onLoadError);
            this.urlLoader.addEventListener(SecurityErrorEvent.SECURITY_ERROR, this.onLoadError);
            this.urlLoader.load(new URLRequest(this.versionURL));
        }

        // This function reads the game version response and starts loading the game SWF it names.
        private function onDataComplete(e:Event):void
        {
            this.urlLoader.removeEventListener(Event.COMPLETE, this.onDataComplete);
            var loader:Object;
            try
            {
                loader = JSON.parse(e.target.data);
            }
            catch (err:Error)
            {
                loadFailed("The game version response could not be read: " + err.message);
                return;
            }
            this.sFile = ((loader.sFile + "?ver=") + loader.sVersion);
            this.sBG = loader.sBG;
            this.loaderVars = loader;
            this.loadGame();
        }

        private function loadGame():void
        {
            this.loader = new Loader();
            this.loader.contentLoaderInfo.addEventListener(ProgressEvent.PROGRESS, this.onProgress);
            this.loader.contentLoaderInfo.addEventListener(Event.COMPLETE, this.onComplete);
            this.loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, this.onLoadError);
            this.loader.contentLoaderInfo.addEventListener(SecurityErrorEvent.SECURITY_ERROR, this.onLoadError);
            this.loader.load(new URLRequest(((this.sURL + "gamefiles/") + this.sFile)));
        }

        // This function reports the game SWF download progress. bytesTotal is 0 until the size is known, which would
        // send NaN.
        private function onProgress(e:ProgressEvent):void
        {
            if (!DebugMode && (e.currentTarget.bytesTotal > 0))
            {
                ExternalInterface.call("progress", Math.round((Number((e.currentTarget.bytesLoaded / e.currentTarget.bytesTotal)) * 100)));
            }
        }

        // This function reports a failed download of the game version or the game SWF. Without it the client stays on
        // the loading screen with no sign of what went wrong.
        private function onLoadError(e:ErrorEvent):void
        {
            loadFailed(e.text);
        }

        // This function sends a load failure to C#, which shows it to the user, or traces it in debug mode.
        private function loadFailed(message:String):void
        {
            if (DebugMode)
            {
                trace("DEBUG: " + message);
            }
            else
            {
                ExternalInterface.call("loadError", message);
            }
        }

        private function onComplete(e:Event):void
        {
            var info:*;
            this.loader.contentLoaderInfo.removeEventListener(ProgressEvent.PROGRESS, this.onProgress);
            this.loader.contentLoaderInfo.removeEventListener(Event.COMPLETE, this.onComplete);

            Game = MovieClip(e.currentTarget.content);
            for (info in root.loaderInfo.parameters)
            {
                Game.params[info] = root.loaderInfo.parameters[info];
            }
            Game.params.vars = this.loaderVars;
            Game.params.sURL = this.sURL;
            Game.params.sTitle = this.loaderVars.sTitle;
            Game.params.sBG = BackgroundIndex < 0 ? this.sBG : "";
            Game.params.isWeb = this.isWeb;
            Game.params.isEU = this.isEU;
            Game.params.doSignup = this.doSignup;
            Game.params.loginURL = this.loginURL;
            Game.addEventListener(MouseEvent.CLICK, this.onGameClick);
            Game.addEventListener(KeyboardEvent.KEY_DOWN, this.onGameKeyboard);
            Game.addEventListener(Event.ENTER_FRAME, this.onGameEnter);
            Game.addEventListener(Event.ADDED, this.onGameAdded);
            Game.loginLoader.addEventListener(Event.COMPLETE, this.onLoginComplete);
            Game.sfc.debug = true;
            Game.sfc.addEventListener(SFSEvent.onConnectionLost, this.onDisconnect);
            Game.sfc.addEventListener(SFSEvent.onExtensionResponse, this.onExtensionResponse);
            Game.sfc.addEventListener(SFSEvent.onDebugMessage, this.onDebugMessage);
            gameDomain = LoaderInfo(e.target).applicationDomain;
            this.external = new Externalizer();
            this.external.init(this);

            if (BackgroundIndex >= 0)
            {
                GameStage.removeChild(MainBackground);
            }
            GameStage.addChildAt(Game, 0);
        }

        private function onDisconnect(e:*):void
        {
            ExternalInterface.call("disconnect");
        }

        private function onLoginComplete(e:Event):void
        {
            objLogin = JSON.parse(e.target.data);
            e.target.data = DebugMode ? e.target.data : String(ExternalInterface.call("modifyServers", e.target.data));
            if (Game.mcCharSelect)
            {
                Game.mcCharSelect.Game.objLogin = objLogin;
            }
        }

        private function onGameClick(e:MouseEvent):void
        {
            switch (e.target.name)
            {
                case "btnLogin":
                    if (!Game.mcCharSelect)
                    {
                        username = Game.mcLogin.ni.text;
                        password = Game.mcLogin.pi.text;
                    }
                    else
                    {
                        Game.mcCharSelect.btnLogin.removeEventListener(MouseEvent.CLICK, Game.mcCharSelect.onBtnLogin);
                        onCharBtnClick("Login");
                    }
                    return;
                case "btnServer":
                    if (Game.mcCharSelect)
                    {
                        Game.mcCharSelect.btnServer.removeEventListener(MouseEvent.CLICK, Game.mcCharSelect.onBtnServer);
                        onCharBtnClick("Server");
                    }
                    return;
                case "charPage":
                    ExternalInterface.call("openWebsite", ("https://www.aq.com/aw-character.asp?id=" + gameDomain.getDefinition("Game").loginInfo.strUsername));
                    return;
                case "txtClassName":
                case "txtWeapon":
                case "txtArmor":
                case "txtHelm":
                case "txtCape":
                case "txtPet":
                case "txtMisc":
                    if (e.target.text != "None")
                    {
                        ExternalInterface.call("openWebsite", ("http://aqwwiki.wikidot.com/" + e.target.text));
                    }
                    return;
                case "btCharPage":
                    ExternalInterface.call("openWebsite", ("https://account.aq.com/CharPage?id=" + e.target.parent.txtUserName.text));
                    return;
                case "hit":
                    if (getQualifiedClassName(e.target.parent) != "LPFElementScrollBar" && e.target.parent.ti && e.target.parent.ti.text == "Wiki Monster")
                    {
                        ExternalInterface.call("openWebsite", ("http://aqwwiki.wikidot.com/" + Game.ui.mcPortraitTarget.strName.text));
                    }
                    return;
                case "btnWiki":
                    var className:String = getQualifiedClassName(e.target.parent);
                    if (className.indexOf("LPFFrameItemPreview") > -1)
                    {
                        ExternalInterface.call("openWebsite", ("http://aqwwiki.wikidot.com/" + e.target.parent.tInfo.getLineText(0)));
                    }
                    else if (className.indexOf("LPFFrameHousePreview") > -1)
                    {
                        ExternalInterface.call("openWebsite", ("http://aqwwiki.wikidot.com/" + Game.ui.mcPopup.getChildByName("mcInventory").previewPanel.frames[3].mc.tInfo.getLineText(0)));
                    }
                    else if (className.indexOf("mcQFrame") > -1)
                    {
                        ExternalInterface.call("openWebsite", ("http://aqwwiki.wikidot.com/search:site/a/p/q/" + Game.getInstanceFromModalStack("QFrameMC").qData.sName));
                    }
                    return;
            }
        }

        private function onGameKeyboard(e:KeyboardEvent):void
        {
            switch (e.target.name)
            {
                case "ni":
                case "pi":
                    if (e.keyCode == Keyboard.ENTER)
                    {
                        username = Game.mcLogin.ni.text;
                        password = Game.mcLogin.pi.text;
                    }
                    break;
            }
        }

        private function onGameEnter(e:Event):void
        {
            Client.hookChat();
            if (e.target.world && ShouldBeAnonymous == Root.trueString)
            {
                Client.setAnonymousMode(true);
            }
        }

        private function onGameAdded(e:Event):void
        {
            switch (e.target.name)
            {
                case "mcLogin":
                    if (BackgroundIndex < 0)
                    {
                        return;
                    }

                    e.target.mcTitle.removeChildAt(0);
                    e.target.mcTitle.addChild(MainBackground);
                    return;
            }

            switch (getQualifiedClassName(e.target))
            {
                case "ConnDetailMC":
                    if (BackgroundIndex < 0)
                    {
                        return;
                    }

                    Client.shiftBackgrounds(BackgroundIndex, BackgroundIndex, BackgroundIndex);
                    return;
            }
        }

        private function onCharBtnClick(btn:String):void
        {
            if (btn == "Login")
            {
                var server:String = Game.mcCharSelect.mngr.displayAvts[Game.mcCharSelect.pos].server;
            }
            username = Game.mcCharSelect.mngr.displayAvts[Game.mcCharSelect.pos].loginInfo.strUsername;
            password = Game.mcCharSelect.mngr.displayAvts[Game.mcCharSelect.pos].loginInfo.strPassword;
            var loginData:* = Game.mcCharSelect.mngr.displayAvts[Game.mcCharSelect.pos].loginInfo;
            if (loginData.bAsk)
            {
                Game.mcCharSelect.utl.close(1);
                Game.mcCharSelect.passwordui.pos = Game.mcCharSelect.pos;
                Game.mcCharSelect.passwordui.bCharOpts = false;
                Game.mcCharSelect.passwordui.visible = true;
            }
            else
            {
                Game.removeAllChildren();
                Game.gotoAndPlay("Login");
                Game.login(username, password);
                if (btn == "Login")
                {
                    waitFor(Network.areServersLoaded, Network.connect, [server])
                }
            }
        }

        // This function strips the prefix that SmartFoxClient.debugMessage puts on a message ("[ RECEIVED ]: ",
        // "[Sending - STR]: ", "[Sending - JSON]: " or "[Sending]: ") and the ", (len: n)" it adds after a received one.
        // Only the start and the end are touched, so a packet whose text contains one of these markers, such as a chat
        // line, arrives whole. lastIndexOf(prefix, 0) looks at position 0 only, where indexOf would scan the whole packet.
        private function filter(message:String):String
        {
            var prefix:String;
            var end:int;
            for each (prefix in DEBUG_PREFIXES)
            {
                if (message.lastIndexOf(prefix, 0) == 0)
                {
                    message = message.substr(prefix.length);
                    if (prefix == RECEIVED_PREFIX)
                    {
                        end = message.lastIndexOf(", (len: ");
                        if (end > -1)
                        {
                            message = message.slice(0, end);
                        }
                    }
                    return message;
                }
            }
            return message;
        }

        // This function sends a packet to C# under the corresponding call name, or traces it in debug mode.
        private function forward(name:String, packet:String):void
        {
            if (DebugMode)
            {
                trace("DEBUG: " + packet);
            }
            else
            {
                ExternalInterface.call(name, packet);
            }
        }

        // #endregion
    }
}
