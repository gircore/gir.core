using System;
using System.Text;

namespace CWrapper
{
    public static class StringExtension
    {
        public static string? CommentLineByLine(this string? str)
        {
            if(str is null)
                return null;

            var sArray = str.Split("\n");

            var sb = new StringBuilder();

            foreach(var s in sArray)
                sb.AppendLine("///" + s);

            return sb.ToString();
        }
    }
}