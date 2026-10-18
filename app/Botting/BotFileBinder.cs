using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Grimoire.Botting
{
    /// <summary>
    /// Restricts the <c>$type</c> names that bot files, pasted commands and saved sets may use.
    /// <see cref="TypeNameHandling.All"/> would otherwise let a shared file construct any type the CLR can load,
    /// which is enough to run code on the user's machine (CR-01).
    /// </summary>
    /// <remarks>
    /// Allowed: types from Grimoire.exe that aren't forms or controls, <see cref="IBotCommand"/> types from any
    /// assembly (plugins), simple value types, and arrays or common generic collections of allowed types.
    /// </remarks>
    public sealed class BotFileBinder : ISerializationBinder
    {
        #region Fields

        public static readonly BotFileBinder Instance = new BotFileBinder();

        private static readonly HashSet<Type> AllowedGenerics = new HashSet<Type>
        {
            typeof(List<>),
            typeof(Dictionary<,>),
            typeof(HashSet<>),
            typeof(KeyValuePair<,>),
            typeof(Nullable<>)
        };

        private readonly DefaultSerializationBinder _default = new DefaultSerializationBinder();

        #endregion

        #region Constructors

        private BotFileBinder()
        {
        }

        #endregion

        #region Methods

        public Type BindToType(string assemblyName, string typeName)
        {
            Type type = _default.BindToType(assemblyName, typeName);
            if (!IsAllowed(type))
            {
                throw new JsonSerializationException($"The type '{typeName}, {assemblyName}' isn't allowed in bot files.");
            }
            return type;
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            _default.BindToName(serializedType, out assemblyName, out typeName);
        }

        /// <summary>
        /// Whether a bot file may create an instance of <paramref name="type"/>. Forms and controls from this assembly
        /// are refused: they run UI code when constructed, and nothing in a bot file needs them.
        /// </summary>
        private static bool IsAllowed(Type type)
        {
            if (type.IsArray)
            {
                return IsAllowed(type.GetElementType());
            }
            if (type.IsGenericType)
            {
                return AllowedGenerics.Contains(type.GetGenericTypeDefinition()) && type.GetGenericArguments().All(IsAllowed);
            }
            if (typeof(IBotCommand).IsAssignableFrom(type))
            {
                return true;
            }
            if (type.Assembly == typeof(BotFileBinder).Assembly)
            {
                return !typeof(Component).IsAssignableFrom(type);
            }
            return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal)
                || type == typeof(DateTime) || type == typeof(TimeSpan) || type == typeof(Guid);
        }

        #endregion
    }
}
