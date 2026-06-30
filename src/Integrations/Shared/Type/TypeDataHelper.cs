using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

internal static class TypeDataHelper
{
    public static TypeData? GetTypeData(INamedTypeSymbol typeSymbol)
    {
        var properties = GetTypeProperties(typeSymbol);
        if (properties is null)
            return null;

        var upperNestedTypes = GetUpperNestedClasses(typeSymbol);
        if (upperNestedTypes is null)
            return null;

        return new TypeData(
            Filename: GetFileName(typeSymbol),
            Namespace: typeSymbol.ContainingNamespace.ToDisplayString(),
            IsGlobalNamespace: typeSymbol.ContainingNamespace.IsGlobalNamespace,
            Properties: properties,
            UpperNestedTypes: upperNestedTypes
        );
    }

    private static string GetFileName(INamedTypeSymbol typeSymbol)
    {
        var prefix = GetFileNamePrefix(typeSymbol);
        var suffix = typeSymbol.Arity == 0 ? string.Empty : $"_{typeSymbol.Arity}";

        return prefix + typeSymbol.Name + suffix;
    }

    private static string GetFileNamePrefix(ISymbol typeSymbol)
    {
        var sb = new StringBuilder();

        var containingType = typeSymbol.ContainingType;
        while (containingType is not null)
        {
            sb.Insert(0, $"{containingType.Name}.");
            containingType = containingType.ContainingType;
        }

        return sb.ToString();
    }

    private static TypeProperties? GetTypeProperties(ITypeSymbol typeSymbol)
    {
        var accessibility = GetDeclaredAccessibilityAsString(typeSymbol);
        if (accessibility is null)
            return null;

        var kind = GetKind(typeSymbol);
        if (kind is null)
            return null;

        return new TypeProperties(
            Name: typeSymbol.Name,
            NameGenericArguments: typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            Accessibility: accessibility,
            Kind: kind
        );
    }

    private static Stack<TypeProperties>? GetUpperNestedClasses(ISymbol symbol)
    {
        var stack = new Stack<TypeProperties>();
        var containingType = symbol.ContainingType;

        while (containingType is not null)
        {
            var typeProperties = GetTypeProperties(containingType);
            if (typeProperties is null)
                return null;

            stack.Push(typeProperties);
            containingType = containingType.ContainingType;
        }

        return stack;
    }

    private static string? GetKind(ITypeSymbol symbol)
    {
        return (symbol.TypeKind, symbol.IsRecord) switch
        {
            (TypeKind.Struct, _) => "struct",
            (TypeKind.Class, true) => "record",
            (TypeKind.Class, _) => "class",
            _ => null
        };
    }

    private static string? GetDeclaredAccessibilityAsString(ISymbol type)
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
}
