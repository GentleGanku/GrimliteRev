using System.Windows.Forms;

namespace Grimoire.Tools
{
    public class Hotkey
    {
        #region Properties

        public string Text
        {
            get;
            set;
        }

        public Keys Key
        {
            get;
            set;
        }

        public int ActionIndex
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void Install()
        {
            KeyboardHook.Instance.TargetedKeys.Add(Key);
        }

        public void Uninstall()
        {
            KeyboardHook.Instance.TargetedKeys.Remove(Key);
        }

        #endregion
    }
}