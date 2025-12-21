using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace Gtk.Integration.SourceGenerator;

internal static class TemplateValuesProvider
{
    public static IncrementalValuesProvider<TemplateData> GetTemplateValuesProvider(this IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider
            .ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: TemplateAttribute.MetadataName,
                predicate: static (_, _) => true,
                transform: GetData)
            .Where(data => data is not null)!;
    }

    private static TemplateData? GetData(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol template)
            return null;

        var accessibility = GetAccessibility(context.TargetSymbol);
        if (accessibility is null)
            return null;

        var upperNestedClasses = GetUpperNestedClasses(context.TargetSymbol);
        if (upperNestedClasses is null)
            return null;

        var bla = GetBla(context);
        if (bla is null)
            return null;

        return new TemplateData(
            NameGenericArguments: template.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            Namespace: context.TargetSymbol.ContainingNamespace.ToDisplayString(),
            IsGlobalNamespace: context.TargetSymbol.ContainingNamespace.IsGlobalNamespace,
            Accessibility: accessibility,
            FileName: GetFileName(template),
            RessourceName: bla,
            UpperNestedClasses: upperNestedClasses
        );
    }

    private static string? GetBla(GeneratorAttributeSyntaxContext context)
    {
        var attribute = context
            .Attributes
            .FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == TemplateAttribute.MetadataName);

        if (attribute is null)
            return null;

        if (attribute.NamedArguments.IsDefaultOrEmpty)
            return null;
        
        string? ressourceName = null;
        var argument = attribute.NamedArguments.FirstOrDefault(kvp => kvp.Key == "RessourceName");
        if (argument.Value.Value is string value)
        {
            ressourceName = value;
        }
        
        return ressourceName;
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
}
