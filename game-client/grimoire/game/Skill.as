package grimoire.game
{
    import grimoire.Root;

    public class Skill
    {
        // #region Fields

        public static var skillRange:String = "";
        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function returns a number for whether all of the player's skills are available or not.
        public static function allSkillsAvailable():int
        {
            return Math.max(skillReady(game.world.actions.active[1]), skillReady(game.world.actions.active[2]), skillReady(game.world.actions.active[3]), skillReady(game.world.actions.active[4]));
        }

        // This function returns a number for whether the player's corresponding skill is available or not.
        public static function isSkillReady(index:String):int
        {
            return skillReady(game.world.actions.active[parseInt(index)]);
        }

        // This function returns a boolean whether the player's corresponding skill is available or not.
        public static function isSkillAvailable(index:String):String
        {
            return (skillAvailability(game.world.actions.active[parseInt(index)])) ? Root.trueString : Root.falseString;
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
            }
            if ((skill.tgt == "s") || (skill.tgt == "f"))
            {
                tgt = game.world.myAvatar.target;
                game.world.myAvatar.target = game.world.myAvatar;
            }
            if ((game.world.myAvatar.target != null) && (game.world.myAvatar.target.dataLeaf.intHP > 0))
            {
                game.world.approachTarget();
                if (skillAvailability(skill) && (game.world.myAvatar.dataLeaf.intMP >= skill.mp) && skill.isOK && (skillLock == null || !skillLock))
                {
                    game.world.testAction(skill);
                }
            }
            if ((skill.tgt == "s") || (skill.tgt == "f"))
            {
                game.world.myAvatar.target = tgt;
            }
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

        // This function returns a boolean whether the corresponding skill is available or not.
        private static function skillAvailability(skill:*):Boolean
        {
            var cooldown:int;
            var now:Number = new Date().getTime();
            var hasteFactor:Number = (1 - Math.min(Math.max(game.world.myAvatar.dataLeaf.sta.$tha, -1), 0.5));
            if (skill.auto)
            {
                if (game.world.autoActionTimer.running)
                {
                    return false;
                }
                return true;
            }
            if ((now - game.world.GCDTS) < game.world.GCD)
            {
                return false;
            }
            if (skill.OldCD != null)
            {
                cooldown = Math.round((skill.OldCD * hasteFactor));
            }
            else
            {
                cooldown = Math.round((skill.cd * hasteFactor));
            }
            if ((now - skill.ts) >= cooldown)
            {
                delete skill.OldCD;
                return true;
            }
            return false;
        }

        // This function returns a number for whether the skill is ready or not.
        private static function skillReady(skill:*):int
        {
            var cooldown:Number = NaN;
            var now:Number = new Date().getTime();
            var hasteFactor:Number = (1 - Math.min(Math.max(game.world.myAvatar.dataLeaf.sta.$tha, -1), 0.5));
            if (skill.OldCD != null)
            {
                cooldown = Math.round((skill.OldCD * hasteFactor));
                delete skill.OldCD;
            }
            else
            {
                cooldown = Math.round((skill.cd * hasteFactor));
            }
            var globalLeft:Number = (game.world.GCD - (now - game.world.GCDTS));
            if (globalLeft < 0)
            {
                globalLeft = 0;
            }
            var skillLeft:Number = (cooldown - (now - skill.ts));
            if (skillLeft < 0)
            {
                skillLeft = 0;
            }
            return Math.max(globalLeft, skillLeft);
        }

        // #endregion
    }
}
