using System;

namespace Grimoire.Utils
{
    [Serializable]
    public class DefaultTypedValueProvider : TypedValueProvider
    {
        #region Methods

        public object Provide(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch
            {
                return type.GetDefaultValue();
            }
        }

        #endregion
    }
}
