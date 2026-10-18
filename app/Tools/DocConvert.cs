using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Tools
{
    public static class DocConvert
    {
        #region Methods

        /// <summary>
        /// Whether <paramref name="str"/> is base64: it must decode, and its length without spaces must be a multiple
        /// of 4. The padding check comes from d@anish's solution.
        /// </summary>
        public static bool IsBase64Encoded(string str)
        {
            try
            {
                byte[] data = Convert.FromBase64String(str);
                return (str.Replace(" ", "").Length % 4 == 0);
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// True for text that <see cref="Zip"/> produced: Base64 of gzip data, which starts with the gzip header 1F 8B.
        /// Plain words such as "Farm" are valid Base64 too, so the encoding alone can't tell them apart.
        /// </summary>
        public static bool IsCompressed(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length % 4 != 0)
            {
                return false;
            }
            try
            {
                byte[] data = Convert.FromBase64String(str);
                return data.Length > 2 && data[0] == 0x1F && data[1] == 0x8B;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static string Zip<T>(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, compressionLevel: CompressionLevel.Optimal))
                {
                    CopyTo(msi, gs);
                }

                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static string Unzip(string str)
        {
            byte[] data = Convert.FromBase64String(str);
            using (var msi = new MemoryStream(data))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static Task toRtf(string source, string target, out string output)
        {
            Microsoft.Office.Interop.Word.Application newApp = new Microsoft.Office.Interop.Word.Application();

            object Source = source;
            object Target = target;

            object Unknown = Type.Missing;

            newApp.Documents.Open(ref Source, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown, ref Unknown);

            object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF;

            newApp.ActiveDocument.SaveAs(ref Target, ref format,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown);

            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);
            output = $"{target}.rtf";
            return Task.FromResult(0);
        }

        #endregion
    }
}
