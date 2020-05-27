using System.Text;

namespace Generator
{
    public static class StringExtension
    {
        public static string CommentLineByLine(this string str)
        {
            var sArray = str.Split("\n");
            var sb = new StringBuilder();

            foreach(var s in sArray)
                sb.AppendLine("///" + s);

            return sb.ToString();
        }

        public static string MakeSingleLine(this string str)
            => str.Replace("\n", "");
    
        public static string EscapeQuotes(this string str)
            => str.Replace("\"", @"\""");
    }
}