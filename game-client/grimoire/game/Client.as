package grimoire.game
{
	import flash.ui.Keyboard;
	import flash.sampler.Sample;
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
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;
		private static var charPage:*;
		public static var cTravelTriggers:String = "";
		public static var timer:Timer = new Timer(10000);
		public static var shouldPing:Boolean;
		public static var funcYes:Function;
		public static var funcNo:Function;
		public static var latency:Number;
		
		// This function returns a boolean for whether the client is loading on the corresponding type.
		public static function isClientLoading(type:String):String
        {
			switch (type)
			{
				case "Account":
					return (((game.mcConnDetail.txtDetail.text == "Authenticating Account Info...") && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
				case "Client":
					return (((game.mcConnDetail.txtDetail.text == "Initializing Client...") && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
				case "Lobby":
					return (((game.mcConnDetail.txtDetail.text == "Joining Lobby..") && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
				case "Server":
					return (((game.mcConnDetail.txtDetail.text == "Connecting to game server...") && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
				case "Character":
					return (((game.mcConnDetail.txtDetail.text == "Loading Character Data...") && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
				case "Communication":
					return (((game.mcConnDetail.txtDetail.text == "Communication with server has been lost. Please check your internet connection and try again.") && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
				case "MapLoading":
					return (((game.mcConnDetail.txtDetail.text == "Loading Map Files...") && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
				case "MapLoadingStuck":
					return (((game.mcConnDetail.txtDetail.text == "Loading Map... 100%") && (game.mcConnDetail.btnBack.visible == Root.trueString) && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
				case "MapLoadingError":
					return (((game.mcConnDetail.txtDetail.text == "Loading Map Files... Failed!") && (game.mcConnDetail.stage != null)) ? Root.trueString : Root.falseString);
			}
			return Root.falseString;
		}
		
		// This function returns a boolean for whether the client label is on the Login label or not.
		public static function loginLabel():String
		{
			return ((game.currentLabel == "Login" || game.currentLabel == "Select") ? Root.trueString : Root.falseString);
		}
		
		// This function returns a boolean for whether the client label is on the Server label or not.
		public static function serverLabel():String
		{
			return ((game.mcLogin.currentLabel == "Servers") ? Root.trueString : Root.falseString);
		}
		
		// This function returns a boolean for whether the client label is on the Game label or not.
		public static function gameLabel():String
		{
			return ((game.currentLabel == "Game") ? Root.trueString : Root.falseString);
		}		
		
		// This function returns a boolean for whether the game has loaded or not.
		public static function inGame():String
		{
			if (game.intChatMode)
			{
				return (game.ui.mcInterface.ncText.visible ? Root.trueString : Root.falseString);
			}
			else
			{
				return (game.ui.mcInterface.te.visible ? Root.trueString : Root.falseString);
			}
		}
		
		// This function returns a boolean for whether the connection screen is visible or not.
		public static function inConnStage():String
		{
			return ((game.mcConnDetail.stage != null) ? Root.trueString : Root.falseString);
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
			var num:int = 0;
			while (num < game.ui.ModalStack.numChildren)
			{
				child = (game.ui.ModalStack.getChildAt(num) as MovieClip);
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
				num++;
			};
		}
		
		///
		/// Function events.
		///
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
				};
			}
            if (e.keyCode == Keyboard.ESCAPE)
            {
                game.chatF.closeMsgEntry();
            };
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
		///
		/// Function events.
		///
		
	}
}