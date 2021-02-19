using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Sf = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Sk = Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using St = Microsoft.CodeAnalysis.SyntaxToken;

namespace Repository
{
    internal class IdentifierConverter
    {
        public string Convert(string identifier)
        {
            identifier = FixFirstCharIfNumber(identifier);
            identifier = FixIfIdentifierIsKeyword(identifier);

            return identifier;
        }

        private static string FixIfIdentifierIsKeyword(string identifier)
        {
            St token = Sf.ParseToken(identifier);
            if (token.Kind() != Sk.IdentifierToken)
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

            if (char.IsNumber(firstChar))
            {
                var number = (int) char.GetNumericValue(firstChar);
                var sb = new StringBuilder();

                if (data.TryGetValue(number, out var str))
                {
                    sb.Append(str);
                    sb.Append(identifier, 1, identifier.Length - 1);

                    identifier = sb.ToString();
                }
            }

            return identifier;
        }
    }
}
