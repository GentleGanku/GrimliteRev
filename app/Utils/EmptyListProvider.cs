using System;
using System.Collections.Generic;

namespace Grimoire.Utils
{
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
