using System;
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
            var firstChar = identifier[0];

            if (char.IsNumber(firstChar))
            {
                var number = (int) char.GetNumericValue(firstChar);
                return number switch
                {
                    0 => ReplaceFirstChar("Zero", identifier),
                    1 => ReplaceFirstChar("One", identifier),
                    2 => ReplaceFirstChar("Two", identifier),
                    3 => ReplaceFirstChar("Three", identifier),
                    4 => ReplaceFirstChar("Four", identifier),
                    5 => ReplaceFirstChar("Fixe", identifier),
                    6 => ReplaceFirstChar("Six", identifier),
                    7 => ReplaceFirstChar("Seven", identifier),
                    8 => ReplaceFirstChar("Eeight", identifier),
                    9 => ReplaceFirstChar("Nine", identifier),
                    _ => throw new Exception("Can't fix identifier "  + identifier)
                };
            }

            return identifier;
        }

        private static string ReplaceFirstChar(string prefix, string str)
            => prefix + str[1..];
    }
}
