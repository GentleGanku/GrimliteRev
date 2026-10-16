using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Grimoire.Botting
{
    public static class Extensions
    {
        #region Methods

        public static string Correct(this string str)
        {
            return !str.Contains("<false/>") && !str.Contains("<true/>") ? str : str.Replace("<false/>", bool.FalseString).Replace("<true/>", bool.TrueString);
        }

        public static string ReplaceLink(this string str)
        {
            string replace = str;
            return replace.Replace(".swf", "").Replace("_skin", "");
        }

        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static string FromBase64(this string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        public static string generatePastelHex(Random random, int mixR, int mixG, int mixB)
        {
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);

            red = (red + mixR) / 2;
            green = (green + mixG) / 2;
            blue = (blue + mixB) / 2;
            return string.Format("FF{0:X2}{1:X2}{2:X2}", red, green, blue);
        }

        public static string SanitizeXml(this string str)
        {
            return str.Replace("&apos;", "'").Replace("&amp;", "&");
        }

        public static string NullIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }

        public static string NullIfWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        public static string MakeRelativePath(String fromPath, String toPath)
        {
            if (string.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (string.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }
            return relativePath;
        }

        public static string MakeRelativePathFrom(String fromPath, String toPath)
        {
            if (string.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (string.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }
            string[] path = relativePath.Split(new string[1] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            return relativePath.Replace(path[0] + @"\", "");
        }

        public static string[] JtoArray(string result)
        {
            string[] resultArray = JsonConvert.DeserializeObject<string[]>(result);
            return resultArray;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
        /// </summary>
        /// <param name="control">the control for which the update is required</param>
        /// <param name="action">action to be performed on the control</param>
        public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        public static float ToSingle(double value)
        {
            return (float)value;
        }

        #endregion

        #region Classes

        public class Line : IComparable<Line>
        {
            #region Fields

            readonly string _line;
            int _number;
            string _afterNumber;

            #endregion

            #region Constructors

            public Line(string line)
            {
                int firstSpace = line.IndexOf(' ');
                string integer = line.Substring(0, firstSpace);
                this._number = int.Parse(integer);

                this._afterNumber = line.Substring(firstSpace);
                this._line = line;
            }

            #endregion

            #region Methods

            public int CompareTo(Line other)
            {
                int result1 = _number.CompareTo(other._number);
                if (result1 != 0)
                {
                    return result1;
                }
                return _afterNumber.CompareTo(other._afterNumber);
            }

            public override string ToString()
            {
                return this._line;
            }

            #endregion
        }

        #endregion
    }
}