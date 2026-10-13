package grimoire.game
{
	import grimoire.Root;
	
	public class Skill 
	{
		private static var game:* = Root.Game;
		private static var pref:* = Root.Game.userPreference;
		public static var skillRange:String = "";
		
		// This function returns a number for whether all of the player's skills are available or not.
        public static function allSkillsAvailable():int
        {
            return (Math.max(skillReady(game.world.actions.active[1]), skillReady(game.world.actions.active[2]), skillReady(game.world.actions.active[3]), skillReady(game.world.actions.active[4])));
        }

		// This function returns a number for whether the player's corresponding skill is available or not.
        public static function isSkillReady(index:String):int
        {
            return (skillReady(game.world.actions.active[parseInt(index)]));
        }
		
		// This function returns a boolean whether the player's corresponding skill is available or not.
		public static function isSkillAvailable(index:String):String
		{
			return ((skillAvailability(game.world.actions.active[parseInt(index)])) ? Root.trueString : Root.falseString);
		}

		// This function returns a boolean whether the corresponding skill is available or not.
		private static function skillAvailability(skill:*):Boolean
        {
            var _local_4:int;
            var _local_2:Number = new Date().getTime();
            var _local_3:Number = (1 - Math.min(Math.max(game.world.myAvatar.dataLeaf.sta.$tha, -1), 0.5));
            if (skill.auto)
            {
                if (game.world.autoActionTimer.running)
                {
                    return (false);
                };
                return (true);
            };
            if ((_local_2 - game.world.GCDTS) < game.world.GCD)
            {
                return (false);
            };
            if (skill.OldCD != null)
            {
                _local_4 = Math.round((skill.OldCD * _local_3));
            }
            else
            {
                _local_4 = Math.round((skill.cd * _local_3));
            };
            if ((_local_2 - skill.ts) >= _local_4)
            {
                delete skill.OldCD;
                return (true);
            };
            return (false);
        }
		
		// This function returns a number for whether the skill is ready or not.
        private static function skillReady(param1:*):int
        {
            var _loc4_:Number = NaN;
            var _loc2_:Number = new Date().getTime();
            var _loc3_:Number = (1 - Math.min(Math.max(game.world.myAvatar.dataLeaf.sta.$tha, -1), 0.5));
            if (param1.OldCD != null)
            {
                _loc4_ = Math.round((param1.OldCD * _loc3_));
                delete param1.OldCD;
            }
            else
            {
                _loc4_ = Math.round((param1.cd * _loc3_));
            };
            var _loc5_:Number = (game.world.GCD - (_loc2_ - game.world.GCDTS));
            if (_loc5_ < 0)
            {
                _loc5_ = 0;
            };
            var _loc6_:Number = (_loc4_ - (_loc2_ - param1.ts));
            if (_loc6_ < 0)
            {
                _loc6_ = 0;
            };
            return (Math.max(_loc5_, _loc6_));
        }

		// This function executes the corresponding skill.
		public static function skillAction(index:String):void
		{
			var skill:Object = game.world.actions.active[parseInt(index)];
			var skillLock:* = game.ui.mcInterface.actBar.getChildByName(("i" + (parseInt(index) + 1))).actObj.skillLock;
			var tgt:* = null;
            if (game.world.myAvatar.target == game.world.myAvatar)
            {
                game.world.myAvatar.target = null;
                return;
            };
            if ((skill.tgt == "s") || (skill.tgt == "f"))
            {
                tgt = game.world.myAvatar.target;
                game.world.myAvatar.target = game.world.myAvatar;
            };
            if ((game.world.myAvatar.target != null) && (game.world.myAvatar.target.dataLeaf.intHP > 0))
            {
                game.world.approachTarget();
                if (skillAvailability(skill) && (game.world.myAvatar.dataLeaf.intMP >= skill.mp) && skill.isOK && (skillLock == null || !skillLock)) 
				{
					game.world.testAction(skill);
                };
            };
            if ((skill.tgt == "s") || (skill.tgt == "f"))
            {
                game.world.myAvatar.target = tgt;
            };
		}		
		
		// This function fills the skillRange string with the skills' original range.
		public static function getRange():void
		{
			var i:int = 0;
			skillRange = "";
			while (i < 6)
			{
				skillRange = skillRange + game.world.actions.active[i].range + ";";
				i++;
			}
		}
		
		
	}
}