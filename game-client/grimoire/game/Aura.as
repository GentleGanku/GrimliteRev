package grimoire.game
{
    import grimoire.Root;

    public class Aura
    {
        // #region Fields

        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function checks whether the aura element on the player is active or not.
        public static function isAuraActive(target:String, auraName:String):String
        {
            var aLeaf:Object = getAuras(target);
            var aura:Object;
            var bool:String = Root.falseString;
            for each (aura in aLeaf)
            {
                if (aura.nam.toLowerCase() == auraName.toLowerCase())
                {
                    bool = Root.trueString;
                    break;
                }
            }
            return bool;
        }

        // This function checks whether the aura element with the corresponding, string-type val on the player is active or not.
        public static function isAuraWithStrValActive(target:String, auraName:String, auraValue:String):String
        {
            var aLeaf:Object = getAuras(target);
            var aura:Object;
            var bool:String = Root.falseString;
            for each (aura in aLeaf)
            {
                if ((aura.nam.toLowerCase() == auraName.toLowerCase()) && ((aura.val == null) || (aura.val == "undefined") || (aura.val == "")))
                {
                    bool = Root.falseString;
                    break;
                }
                if ((aura.nam.toLowerCase() == auraName.toLowerCase()) && (aura.val.toLowerCase() == auraValue.toLowerCase()))
                {
                    bool = Root.trueString;
                    break;
                }
            }
            return bool;
        }

        // This function checks whether the assigned value of the aura element is over/under/equal that of the corresponding number.
        public static function auraComparison(target:String, comp:String, auraName:String, auraValue:int):String
        {
            var aLeaf:Object = getAuras(target);
            var aura:Object;
            var bool:String = Root.falseString;
            for each (aura in aLeaf)
            {
                if (aura.nam.toLowerCase() == auraName.toLowerCase())
                {
                    if ((aura.val == null) || (aura.val == "undefined") || (aura.val == ""))
                    {
                        bool = Root.falseString;
                        break;
                    }
                    if ((comp == "Greater") && aura.val.toFixed(0) > auraValue.toFixed(0))
                    {
                        bool = Root.trueString;
                        break;
                    }
                    else if ((comp == "Less") && aura.val.toFixed(0) < auraValue.toFixed(0))
                    {
                        bool = Root.trueString;
                        break;
                    }
                    else if ((comp == "Equal") && aura.val.toFixed(0) == auraValue.toFixed(0))
                    {
                        bool = Root.trueString;
                        break;
                    }
                }
            }
            return bool;
        }

        // This function returns the auras on the player ("Self") or on the player's current target (any other value).
        // It returns null while there is no avatar or no target, which the aura checks read as "no aura".
        private static function getAuras(target:String):Object
        {
            var avatar:* = game.world.myAvatar;
            var owner:* = (avatar == null) ? null : ((target == "Self") ? avatar : avatar.target);
            return (owner == null) ? null : owner.dataLeaf.auras;
        }

        // #endregion
    }
}
