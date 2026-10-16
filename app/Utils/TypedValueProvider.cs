using System;
using System.Collections.Generic;

namespace Grimoire.Utils
{
    public interface TypedValueProvider
    {
        #region Methods

        object Provide(Type type);

        #endregion
    }

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

    [Serializable]
    public class EmptyListProvider<T> : TypedValueProvider
    {
        #region Methods

        public object Provide(Type type)
        {
            return new List<T>();
        }

        #endregion
    }
}