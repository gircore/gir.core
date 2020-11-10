using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GLib
{
    public static class Markup
    {
        ///<summary>
        /// Escapes text so that the markup parser will parse it verbatim.
        /// Less than, greater than, ampersand, etc. are replaced with the
        /// corresponding entities. This function would typically be used
        /// when writing out a file to be parsed with the markup parser.
        /// 
        /// Note that this function doesn't protect whitespace and line endings
        /// from being processed according to the XML rules for normalization
        /// of line endings and attribute values.
        /// 
        /// Note also that this function will produce character references in
        /// the range of &#x1; ... &#x1f; for all control sequences
        /// except for tabstop, newline and carriage return.  The character
        /// references in this range are not valid XML 1.0, but they are
        /// valid XML 1.1 and will be accepted by the GMarkup parser.
        ///</summary>
        public static string EscapeText(string text)
        {
            var numBytes = Encoding.UTF8.GetByteCount(text);
            IntPtr ret = Global.markup_escape_text(text, numBytes);

            return Marshal.PtrToStringAuto(ret);
        }
    }
}
