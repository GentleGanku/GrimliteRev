using System;

namespace Grimoire.Utils
{
    public interface TypedValueProvider
    {
        #region Methods

        object Provide(Type type);

        #endregion
    }
}