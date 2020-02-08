using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Sf = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Sk = Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace CWrapper
{
    public class CSharpIdentifierFixer : IdentifierFixer
    {
        private static Dictionary<int, string> data = new Dictionary<int, string>()
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

        public string Fix(string identifier)
        {
            identifier = FixFirstCharIfNumber(identifier);
            identifier = FixIfIdentifierIsKeyword(identifier);

            return identifier;
        }

        private string FixIfIdentifierIsKeyword(string identifier)
        {
            var token = Sf.ParseToken(identifier);
            if(token.Kind() != Sk.IdentifierToken)
                identifier = "@" + identifier;

            return identifier;
        }

        private string FixFirstCharIfNumber(string identifier)
        {

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