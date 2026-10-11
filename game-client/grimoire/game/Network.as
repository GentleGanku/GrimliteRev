//grimoire.game.Network
package grimoire.game
{
	import flash.events.MouseEvent;
    import grimoire.Root;

    public class Network 
    {
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;

		// This function checks whether the player is temporarily kicked or not.
        public static function isTemporarilyKicked():String
        {
			var mcLogin:* = game.mcLogin;
            return ((((mcLogin != null) && (mcLogin.btnLogin != null)) && (!mcLogin.btnLogin.visible)) ? Root.trueString : Root.falseString);
        }

		// This function executes the game's login function.
        public static function login():void
        {
			game.removeAllChildren();
			game.gotoAndPlay("Login");
			game.login(Root.username, Root.password);
        }
		
		// This function executes the game's Logout function.
        public static function logout():void
        {
            game.logout();
        }
		
		// This function executes the game's showServerList function.
		public static function showServers():void
		{
			game.showServerList();
		}
		
		// This function executes by resetting the server datas. It returns a Root.trueString boolean if succeeds, or a Root.falseString one if otherwise.
        public static function resetServers():String
        {
            try
            {
                game.serialCmd.servers = [];
                game.world.strMapName = "";
                return (Root.trueString);
            }
            catch(e) { return (Root.falseString); };
			return (Root.falseString);
        }

		// This function checks whether the servers are loaded or not.
        public static function areServersLoaded():String
        {
			var serialCmd:* = game.serialCmd;
			return (((serialCmd != null) && (serialCmd.servers) && (serialCmd.servers.length > 0)) ? Root.trueString : Root.falseString);
        }

		// This function executes the game's connect function.
        public static function connect(serverName:String):void
        {
            var server:Object;
            for each (server in game.serialCmd.servers)
            {
                if (server.sName == serverName)
                {
                    game.objServerInfo = server;
                    game.chatF.iChat = server.iChat;
                    break;
                };
            };
            game.connectTo(game.objServerInfo.sIP, game.objServerInfo.iPort);
        }

		// This function executes the game's login function with more measure.
		public static function onLoginExecute():void
        {
			Root.username = (game.mcCharSelect != null) ? game.mcCharSelect.mngr.displayAvts[game.mcCharSelect.pos].loginInfo.strUsername : game.mcLogin.ni.text;
			Root.password = (game.mcCharSelect != null) ? game.mcCharSelect.mngr.displayAvts[game.mcCharSelect.pos].loginInfo.strPassword : game.mcLogin.pi.text;
			game.removeAllChildren();
			game.gotoAndPlay("Login");
			game.login(Root.username, Root.password);
        }
		
		// This function returns the real address (in string) of the current server.
        public static function realAddress():String
        {
            return (('"' + game.objServerInfo.RealAddress) + '"');
        }

		// This function returns the real port (in string) of the current server.
        public static function realPort():String
        {
            return (('"' + game.objServerInfo.RealPort) + '"');
        }

		// This function returns the current server time.
		public static function serverTime():String
		{
			return (('"' + game.date_server.toLocaleTimeString()) + '"');
		}
		
		// This function returns the name of the current server.
		public static function serverName():String
		{
			return (('"' + game.objServerInfo.sName) + '"');
		}
		
		// This function returns the IP address of the current server.
		public static function serverIP():String
		{
			return (('"' + game.objServerInfo.sIP) + '"');
		}		
		
    }
}//package grimoire.game

