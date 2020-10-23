using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Sf = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Sk = Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace Generator
{
    public static class StringExtension
    {
        public static string CommentLineByLine(this string str, string linePrefix = "")
        {
            var sArray = str.Split("\n");
            var sb = new StringBuilder();

            foreach(var s in sArray)
                sb.AppendLine(linePrefix + "/// " + s);

            return sb.ToString();
        }

        public static string MakePascalCase(this string str)
        {
            string ToPascalCase(string s)
            {
                var words = str.Replace("_", "-").Split("-");
                var builder = new StringBuilder();
                foreach (var word in words)
                {
                    builder.Append(char.ToUpper(word[0]) + word.Substring(1));
                }

                return builder.ToString();
            }
            
            return str switch
            {
                {Length: 0} => "",
                {Length: 1} => str.ToUpper(),
                _ => ToPascalCase(str)
            };
        }

        public static string MakeSingleLine(this string str)
            => str.Replace("\n", "");
    
        public static string EscapeQuotes(this string str)
            => str.Replace("\"", @"\""");

        public static string FixIdentifier(this string identifier)
        {
            identifier = FixFirstCharIfNumber(identifier);
            identifier = FixIfIdentifierIsKeyword(identifier);

            return identifier;
        }

        private static string FixIfIdentifierIsKeyword(string identifier)
        {
            var token = Sf.ParseToken(identifier);
            if(token.Kind() != Sk.IdentifierToken)
                identifier = "@" + identifier;

            return identifier;
        }

        private static string FixFirstCharIfNumber(string identifier)
        {
            var data = new Dictionary<int, string>()
            {
                {0, "Zero"},
                {1, "One"},
                {2, "Two"},
                {3, "Three"},
                {4, "Four"},
                {5, "Five"},
                {6, "Six"},
                {7, "Seven"},
                {8, "Eight"},
                {9, "Nine"}
            };
            var firstChar = identifier[0];
            if(char.IsNumber(firstChar))
            {
                var number = (int)char.GetNumericValue(firstChar);
                var sb = new StringBuilder();

                if(data.TryGetValue(number, out string str))
                {
                    sb.Append(str);
                    sb.Append(identifier.Substring(1));

                    identifier = sb.ToString();
                }
            }

            return identifier;
        }
    }
}