using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;

namespace Grimoire.Tools
{
    public static class DocConvert
    {
        public static bool IsBase64Encoded(string str)
        {
            try
            {
                // If no exception is caught, then it is possibly a base64 encoded string
                byte[] data = Convert.FromBase64String(str);
                // The part that checks if the string was properly padded to the
                // correct length was borrowed from d@anish's solution
                return (str.Replace(" ", "").Length % 4 == 0);
            }
            catch
            {
                // If exception is caught, then it is not a base64 encoded string
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
                    //msi.CopyTo(gs);
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
                    //msi.CopyTo(gs);
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
                    //gs.CopyTo(mso);
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
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static Task toRtf(string source, string target, out string output)
        {
            //Creating the instance of Word Application
            Microsoft.Office.Interop.Word.Application newApp = new Microsoft.Office.Interop.Word.Application();

            // specifying the Source & Target file names
            object Source = source;
            object Target = target;

            // Use for the parameter whose type are not known or  
            // say Missing
            object Unknown = Type.Missing;

            // Source document open here
            // Additional Parameters are not known so that are  
            // set as a missing type
            newApp.Documents.Open(ref Source, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown, ref Unknown);

            // Specifying the format in which you want the output file 
            object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatRTF;

            //Changing the format of the document
            newApp.ActiveDocument.SaveAs(ref Target, ref format,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown);

            // for closing the application
            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);
            output = $"{target}.rtf";
            return Task.FromResult(0);
        }

    }
}
