namespace Grimoire.UI
{
    public class Item : ISetInterface
    {
        #region Properties

        public string Type
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"[{Type}] : {Name}";
        }

        #endregion
    }
}
