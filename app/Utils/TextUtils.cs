using System.Text;

namespace Grimoire.Utils
{
    /// <summary>
    /// Monster and item names copied from the Wiki or typed on some keyboards carry curly apostrophes and quotes, while
    /// the game mostly uses straight ones. Both sides of a name comparison go through <see cref="UnifyQuotes"/>.
    /// </summary>
    public static class TextUtils
    {
        #region Fields

        private static readonly char[] Apostrophes = { '‘', '’', '‚', '‛', 'ʼ', '′', '´' };
        private static readonly char[] DoubleQuotes = { '“', '”', '„', '‟', '″' };

        #endregion

        #region Methods

        /// <summary>
        /// Replaces curly and look-alike apostrophes with <c>'</c> and curly double quotes with <c>"</c>.
        /// </summary>
        public static string UnifyQuotes(this string text)
        {
            if (string.IsNullOrEmpty(text) || (text.IndexOfAny(Apostrophes) < 0 && text.IndexOfAny(DoubleQuotes) < 0))
            {
                return text;
            }
            StringBuilder builder = new StringBuilder(text);
            foreach (char apostrophe in Apostrophes)
            {
                builder.Replace(apostrophe, '\'');
            }
            foreach (char doubleQuote in DoubleQuotes)
            {
                builder.Replace(doubleQuote, '"');
            }
            return builder.ToString();
        }

        #endregion
    }
}
