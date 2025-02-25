using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace GObject.Integration.SourceGenerator;

internal static class SubclassValuesProvider
{

    public static IncrementalValuesProvider<SubclassData> GetSubclassValuesProvider(this IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider
            .ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: SubclassAttribute.MetadataName,
                predicate: static (_, _) => true,
                transform: GetSubclassData)
            .Where(data => data is not null)!;
    }

    private static SubclassData? GetSubclassData(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol subclass)
            return null;

        var subclassAttribute = context.Attributes.First().AttributeClass;
        if (subclassAttribute is null)
            return null;

        var parentType = subclassAttribute.TypeArguments.First();

        var parentHandle = GetParentHandle(parentType);
        if (parentHandle is null)
            return null;

        var accessibility = GetAccessibility(context.TargetSymbol);
        if (accessibility is null)
            return null;

        var upperNestedClasses = GetUpperNestedClasses(context.TargetSymbol);
        if (upperNestedClasses is null)
            return null;

        return new SubclassData(
            Name: subclass.Name,
            NameGenericArguments: subclass.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            Parent: parentType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            ParentHandle: parentHandle,
            Namespace: context.TargetSymbol.ContainingNamespace.ToDisplayString(),
            IsGlobalNamespace: context.TargetSymbol.ContainingNamespace.IsGlobalNamespace,
            Accessibility: accessibility,
            FileName: GetFileName(subclass),
            UpperNestedClasses: upperNestedClasses
        );
    }

    private static Stack<TypeData>? GetUpperNestedClasses(ISymbol symbol)
    {
        var stack = new Stack<TypeData>();
        var containingType = symbol.ContainingType;

        while (containingType is not null)
        {
            var accessibility = GetAccessibility(containingType);
            if (accessibility is null)
                return null;

            var kind = GetKind(containingType);
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

    private static string? GetKind(INamedTypeSymbol symbol)
    {
        return (symbol.TypeKind, symbol.IsRecord) switch
        {
            (TypeKind.Struct, _) => "struct",
            (TypeKind.Class, true) => "record",
            (TypeKind.Class, _) => "class",
            _ => null
        };
    }

    private static string GetFileName(INamedTypeSymbol typeSymbol)
    {
        var prefix = GetFileNamePrefix(typeSymbol);
        var suffix = typeSymbol.Arity == 0 ? string.Empty : $"_{typeSymbol.Arity}";

        return prefix + typeSymbol.Name + suffix;
    }

    private static string GetFileNamePrefix(INamedTypeSymbol typeSymbol)
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

    private static string? GetAccessibility(ISymbol type)
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

    private static string? GetParentHandle(ITypeSymbol type)
    {
        ITypeSymbol? currentType = type;
        INamedTypeSymbol? parentHandleAttribute;
        do
        {
            parentHandleAttribute = GetHandleAttribute(currentType);

            if (parentHandleAttribute is null)
                currentType = GetTypeInSubclassAttribute(currentType);

        } while (parentHandleAttribute is null && currentType is not null);

        return parentHandleAttribute?.TypeArguments.First().ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    private static INamedTypeSymbol? GetHandleAttribute(ITypeSymbol type)
    {
        var attributeData = type
            .GetAttributes()
            .FirstOrDefault(x => x.IsHandleAttribute());

        return attributeData?.AttributeClass;
    }

    private static ITypeSymbol? GetTypeInSubclassAttribute(ITypeSymbol type)
    {
        var subclassAttribute = type
            .GetAttributes()
            .FirstOrDefault(x => x.AttributeClass?.ConstructedFrom.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == SubclassAttribute.FullyQualifiedDisplayName);

        if (subclassAttribute is null)
            return null;

        return subclassAttribute.AttributeClass?.TypeArguments.First();
    }
}
