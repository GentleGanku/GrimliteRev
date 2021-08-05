using System;
using System.IO;
using System.Reflection;

namespace Grimoire.Utils
{
    public static class ReflectUtils
    {
        public static object GetDefaultValue(this Type type)
        {
            return type != typeof(void) && type != null && type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static bool IsCompatible(this ProcessorArchitecture arch)
        {
            return arch == ProcessorArchitecture.MSIL || Assembly.GetExecutingAssembly().GetName().ProcessorArchitecture == arch;
        }

        public static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
        {
            string filePath = assembly.Location;
            byte[] buffer = new byte[2048];
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);
            int offset = BitConverter.ToInt32(buffer, 60);
            int secondsSince1970 = BitConverter.ToInt32(buffer, offset + 8);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime linkTimeUtc = epoch.AddSeconds(secondsSince1970);
            TimeZoneInfo tz = target ?? TimeZoneInfo.Local;
            return TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);
        }
    }
}