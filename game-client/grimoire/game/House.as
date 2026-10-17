package grimoire.game
{
    import grimoire.Root;

    public class House
    {
        // #region Fields

        private static var game:* = Root.Game;

        // #endregion

        // #region Methods

        // This function returns the datas from all of the house items.
        public static function houseItems():String
        {
            return JSON.stringify(game.world.myAvatar.houseitems);
        }

        // This function returns the assigned value of the available house slots.
        public static function houseSlots():int
        {
            return game.world.myAvatar.objData.iHouseSlots;
        }

        // #endregion
    }
}
