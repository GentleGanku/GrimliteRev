using Grimoire.Tools;
using Newtonsoft.Json;

namespace Grimoire.Utils
{
    public static class GameObject
    {
        #region Fields

        private static readonly TypedValueProvider _defaultProvider = new DefaultTypedValueProvider();

        #endregion

        #region Methods

        public static T Get<T>(string path)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(Flash.CallString("getGameObject", path));
            }
            catch
            {
                return (T)_defaultProvider.Provide(typeof(T));
            }
        }

        public static T Select<T>(string path, string select)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(Flash.CallString("selectArrayObjects", path, select));
            }
            catch
            {
                return (T)_defaultProvider.Provide(typeof(T));
            }
        }

        public static void Set(string path, object value) => Flash.Call("setGameObject", path, value);

        #endregion
    }
}
