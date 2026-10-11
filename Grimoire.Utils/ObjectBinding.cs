using Grimoire.Tools;
using Newtonsoft.Json;
using PostSharp.Aspects;
using System;

namespace Grimoire.Utils
{
    [Serializable]
    public class ObjectBindingAttribute : LocationInterceptionAspect
    {
        public string[] Names { get; set; }
        public bool Get { get; set; } = true;
        public bool Set { get; set; } = true;
        public int GetIndex { get; set; } = 0;
        public Type ConvertType { get; set; } = null;
        public object DefaultValue { get; set; } = null;
        public string Select { get; set; } = null;
        public string RequireNotNull { get; set; } = null;
        public bool Static { get; set; } = false;
        public Type DefaultProvider { get; set; }

        private TypedValueProvider _defaultProvider = new DefaultTypedValueProvider();
        private bool _defaultProviderSet;

        public ObjectBindingAttribute(params string[] names)
        {
            Names = names;
        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            if (DefaultProvider != null && !_defaultProviderSet)
            {
                _defaultProvider = (TypedValueProvider)Activator.CreateInstance(DefaultProvider);
                _defaultProviderSet = true;
            }

            if (Get)
            {
                if (RequireNotNull != null && IsNull(RequireNotNull))
                    args.Value = DefaultValue ?? _defaultProvider.Provide(ConvertType);
                else
                {
                    try
                    {
                        if (ConvertType == null)
                            ConvertType = args.Location.LocationType;
                        if (Select != null)
                            args.Value = JsonConvert.DeserializeObject(Flash.CallString("selectArrayObjects", Names[GetIndex], Select), ConvertType);
                        else
                            args.Value = JsonConvert.DeserializeObject(Flash.CallString("getGameObject" + (Static ? "S" : ""), Names[GetIndex]), ConvertType);
                    }
                    catch
                    {
                        args.Value = DefaultValue ?? _defaultProvider.Provide(ConvertType);
                    }
                }
            }
            else
                base.OnGetValue(args);
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            base.OnSetValue(args);
            if (Set)
            {
                foreach (string name in Names)
                    Flash.Call("setGameObject", name, args.Value);
            }
        }

        public bool IsNull(string path)
        {
            return Flash.Call<bool>("isNull", path);
        }

    }
}
