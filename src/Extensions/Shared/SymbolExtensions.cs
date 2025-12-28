using System.Collections.Generic;
using GObject.Integration.SourceGenerator;
using Microsoft.CodeAnalysis;

internal static class SymbolExtensions
{
    public static string? GetDeclaredAccessibilityAsString(this ISymbol type)
    {
        var accessibility = type.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            Accessibility.Private => "private",
            Accessibility.NotApplicable => "internal",
            _ => null
        };

        return accessibility;
    }

    public static Stack<TypeData>? GetUpperNestedClasses(this ISymbol symbol)
    {
        var stack = new Stack<TypeData>();
        var containingType = symbol.ContainingType;

        while (containingType is not null)
        {
            var accessibility = containingType.GetDeclaredAccessibilityAsString();
            if (accessibility is null)
                return null;

            var kind = containingType.GetKind();
            if (kind is null)
                return null;

            var typeData = new TypeData(
                NameGenericArguments: containingType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                Accessibility: accessibility,
                Kind: kind
            );
            stack.Push(typeData);
            containingType = containingType.ContainingType;
        }

        return stack;
    }
}
