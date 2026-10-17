package grimoire.backgrounds
{
    import flash.display.MovieClip;
    import flash.events.*

    public class FableForest extends MovieClip
    {
        // #region Constructors

        public function FableForest(name:String)
        {
            this.name = name;
            this.x = -435;
            this.y = -218;

            // this.addEventListener(MouseEvent.MOUSE_DOWN, this.onDragStart);
            // this.addEventListener(MouseEvent.MOUSE_UP, this.onDragEnd);
        }

        // #endregion

        // #region Methods

        // This function drags the background's position to a corresponding x and y coordinate.
        private function onDragStart(e:MouseEvent):void
        {
            this.startDrag();
        }

        // This function stops the background's position from being dragged, and logs the final x and y coordinates.
        private function onDragEnd(e:MouseEvent):void
        {
            this.stopDrag();
            trace("Background position has been changed to x: " + this.x + ", y: " + this.y + ".");
        }

        // #endregion
    }
}
