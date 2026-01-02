using System.Linq;
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

        var subclassAttribute = context.Attributes.First(a => a.IsSubclassAttribute()).AttributeClass;
        if (subclassAttribute is null)
            return null;

        var parentType = subclassAttribute.TypeArguments.First();

        var parentHandle = GetParentHandle(parentType);
        if (parentHandle is null)
            return null;

        var typeData = TypeDataHelper.GetTypeData(subclass);
        if (typeData is null)
            return null;

        return new SubclassData(
            TypeData: typeData,
            Parent: parentType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            ParentHandle: parentHandle
        );
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
