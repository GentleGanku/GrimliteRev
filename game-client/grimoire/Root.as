package grimoire
{
    import grimoire.*;
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
		private static var _handler:*;
		private static var objLogin:Object = new Object();
		private const sURL:String = "https://game.aq.com/game/";
		private const versionURL:String = (sURL + "api/data/gameversion");
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
		
		/// This function creates the instance and the loader.
        public function Root()
        {
			Root.instance = this;
            Root.GameStage = this.stage;

            InitSuperObjects();

            addEventListener(Event.ADDED_TO_STAGE, this.onAddedToStage);
        }
		
        /// This function initializes the super objects.
        private function InitSuperObjects():void
        {
            DebugMode = Capabilities.isDebugger;

            ShouldBeAnonymous = DebugMode ? falseString : String(ExternalInterface.call("getAnonymousMode"));

            BackgroundIndex = DebugMode ? 0 : Number(ExternalInterface.call("getBackgroundIndex"));
			Backgrounds = 
			[
				FableForest
            ];
			MainBackground = new Backgrounds[BackgroundIndex]("MainBackground");
			StandbyBackground = new Backgrounds[BackgroundIndex]("StandbyBackground");
			LoaderBackground = new Backgrounds[BackgroundIndex]("LoaderBackground");
        }

		///
		/// Functions below are the whole process of managing the loader.
		///
        private function onAddedToStage(e:Event):void
        {
            removeEventListener(Event.ADDED_TO_STAGE, this.onAddedToStage);
            Security.allowDomain("*");
            
            GameStage.addChild(MainBackground);

            this.urlLoader = new URLLoader();
            this.urlLoader.addEventListener(Event.COMPLETE, this.onDataComplete);
            this.urlLoader.load(new URLRequest(this.versionURL));
        }

        private function onDataComplete(e:Event):void
        {
            this.urlLoader.removeEventListener(Event.COMPLETE, this.onDataComplete);
			var loader:Object = JSON.parse(e.target.data);
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
            this.loader.load(new URLRequest(((this.sURL + "gamefiles/") + this.sFile)));
        }

        private function onProgress(e:ProgressEvent):void
        {
            if (!DebugMode)
			{
                ExternalInterface.call("progress", Math.round((Number((e.currentTarget.bytesLoaded / e.currentTarget.bytesTotal)) * 100)));
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
            };
			Game.params.vars = this.loaderVars;
            Game.params.sURL = this.sURL;
            Game.params.sTitle = this.loaderVars.sTitle;
            Game.params.sBG = "";
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
			Game.sfc.addEventListener(SFSEvent.onDebugMessage, this.onXtPacketResponse);
			Game.sfc.addEventListener(SFSEvent.onDebugMessage, this.onPacketResponse);
			gameDomain = LoaderInfo(e.target).applicationDomain;
			this.external = new Externalizer();
            this.external.init(this);

			GameStage.removeChild(MainBackground);
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
            };
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
					};
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
					e.target.mcTitle.removeChildAt(0);
                    e.target.mcTitle.addChild(MainBackground);
					return;
			}
			
			switch (getQualifiedClassName(e.target))
			{
				case "ConnDetailMC":
					Client.shiftBackgrounds(BackgroundIndex, BackgroundIndex, BackgroundIndex);
					return;
			}
		}

		private function onCharBtnClick(btn:String):void
		{
			if (btn == "Login")
			{
				var server:String = Game.mcCharSelect.mngr.displayAvts[Game.mcCharSelect.pos].server;
			};
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
				};
			};
		}
		
		private function filter(e:String):String
        {
            if (e.indexOf("[Sending - STR]: ") > -1)
            {
                e = e.replace("[Sending - STR]: ", "");
            };
			if (e.indexOf("[Sending - JSON]: ") > -1)
			{
				e = e.replace("[Sending - JSON]: ", "");
			};
            if (e.indexOf("[ RECEIVED ]: ") > -1)
            {
                e = e.replace("[ RECEIVED ]: ", "");
            };
			if (e.indexOf("[Sending]: ") > -1)
            {
                e = e.replace("[Sending]: ", "");
            };
			if (e.indexOf(", (len: ") > -1)
			{
				var index:int = e.indexOf(", (len: ");
				e = e.slice(0, index);
			};
            return (e);
        }
		
        public function onXtPacketResponse(e:*):void
        {
            if ( (e.params.message.indexOf("%xt%zm%") > -1) && (e.params.message.indexOf("%hi%") < 0))
            {
                if (DebugMode)
                {
                    trace("DEBUG: " + this.filter(e.params.message));
                }
                else
                {
                    ExternalInterface.call("xtPacket", this.filter(e.params.message));
                }
            };
        }
		
		public function onPacketResponse(e:*):void
        {
			if (e.params.message.indexOf("%xt%zm%hi%") > -1)
			{
				return;
			}
			if ((e.params.message.indexOf("%xt%hi%") > -1) || (e.params.message.indexOf("Please slow down. Last action was too soon!") > -1))
			{
				grimoire.game.Client.shouldPing = true;
				return;
			}
			else if (e.params.message.indexOf("%xt%loginResponse%") > -1)
            {
				grimoire.game.Bank.loaded = false;
			}
			else if (e.params.message.indexOf('"cmd":"loadInventoryBig"') > -1)
			{
				isLoggedIn = true;
				grimoire.game.Client.shouldPing = true;
			}
			else if (e.params.message.indexOf("<msg t='sys'><body action='logout' r='0'></body></msg>") > -1)
			{
				isLoggedIn = false;
				grimoire.game.Client.shouldPing = false;
			}

            if (DebugMode)
            {
                trace("DEBUG: " + this.filter(e.params.message));
            }
            else
            {
                ExternalInterface.call("packet", this.filter(e.params.message));
            }
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
		///
		/// Functions above are the whole process of managing the loader.
		///
		
		// This function calls a delay action of milliseconds, waiting for the corresponding function to return True boolean, and then executes the second corresponding function.
		public static function waitFor(bool:Function, func:Function, funcParams:Array, delay:int=300, boolString:String=Root.trueString):void
		{
			var f:Function;
			var timer:Timer = new Timer(delay);
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
            return (('"' + username) + '"');
        }

		// This function returns the password of the player.
        public static function Password():String
        {
            return (('"' + password) + '"');
        }
		
		// This function sets the game's FPS to a corresponding number.
        public static function setFPS(rate:int):void
        {
			Game.addUpdate("Frames Per Second (FPS) cap has been changed from " + GameStage.frameRate + " FPS to " + rate + " FPS.");
            GameStage.frameRate = rate;
        }

		// This function sets the login screen's title to a corresponding string.
        public static function setTitle(title:String):void
        {
            Game.mcLogin.mcLogo.txtTitle.htmlText = ('<font color="#CC1F41">Release:</font>: ' + title);
            Game.params.sTitle.htmlText = ('<font color="#CC1F41">Release:</font>: ' + title);
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
            };
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
            return (JSON.stringify(func.apply(null, args)));
        }		
		
		///
		/// Functions below are the whole process of managing the game objects.
		///
		public static function selectArrayObjects(path:String, selector:String):String
        {
            var obj:* = _getObjectS(Root.Game, path);
            if ((!(obj is Array)))
            {
                return ("");
            };
            var array:Array = (obj as Array);
            var narray:Array = new Array();
            var j:int;
            while (j < array.length)
            {
                narray.push(_getObjectS(array[j], selector));
                j++;
            };
            return (JSON.stringify(narray));
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
            };
            return (obj);
        }		
		
		public static function _getObjectS(root:*, path:String):*
        {
            return (_getObjectA(root, path.split(".")));
        }
		
		public static function getGameObject(path:String):String
        {
            var obj:* = _getObjectS(Root.Game, path);
            return (JSON.stringify(obj));
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
                return ((_getObjectS(Root.Game, path) == null).toString());
            }
            catch(ex:Error) {};
            return ("true");
        }		
		///
		/// Functions above are the whole process of managing the game objects.
		///			
		
    }
}//package grimoire

