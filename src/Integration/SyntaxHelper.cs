using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gir.Integration.CSharp
{
    internal static class SyntaxHelper
    {
        #region Methods

        public static AttributeSyntax GetAttributeSyntax(MemberDeclarationSyntax member, string attributeName)
        {
            if (!TryGetAttributeSyntax(member, attributeName, out var attributeSyntax))
                throw new Exception($"Attribute {attributeName} not found.");

            return attributeSyntax;
        }

        public static bool TryGetAttributeSyntax(MemberDeclarationSyntax member, string attributeName, [NotNullWhen(true)] out AttributeSyntax? attributeSyntax)
        {
            var attributes = member.AttributeLists.SelectMany(x => x.Attributes);
            attributeSyntax = attributes.FirstOrDefault(x => x.Name.ToString() == attributeName);

            return attributeSyntax is not null;
        }

        public static string GetFirstArgument(AttributeSyntax attribute)
        {
            if (attribute.ArgumentList is null)
                throw new Exception($"Attribute does not contain an argument");

            return attribute.ArgumentList.Arguments.First().ToString();
        }

        public static IEnumerable<FieldDeclarationSyntax> GetFieldSyntaxes(ClassDeclarationSyntax classDeclarationSyntax)
        {
            return classDeclarationSyntax.Members.Where(x => x is FieldDeclarationSyntax).Cast<FieldDeclarationSyntax>();
        }

        public static bool HasAttribute(MemberDeclarationSyntax memberSyntax, string attribute)
        {
            return TryGetAttributeSyntax(memberSyntax, attribute, out _);
        }

        public static string GetFirstFieldName(FieldDeclarationSyntax field)
        {
            return field.Declaration.Variables.First().Identifier.ValueText;
        }

        #endregion
    }
}
