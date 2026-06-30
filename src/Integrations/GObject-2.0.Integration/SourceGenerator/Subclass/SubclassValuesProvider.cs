using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GObject.Integration.SourceGenerator;

internal static class SubclassValuesProvider
{
    public static IncrementalValuesProvider<SubclassData> GetSubclassValuesProvider(this IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 },
                transform: GetSubclassData)
            .Where(data => data is not null)!;
    }

    private static SubclassData? GetSubclassData(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        if (context.Node is not ClassDeclarationSyntax classSyntax)
            return null;

        if (context.SemanticModel.GetDeclaredSymbol(classSyntax, cancellationToken) is not INamedTypeSymbol subclass)
            return null;

        var subclassAttribute = subclass.GetAttributes().FirstOrDefault(a => a.IsSubclassAttribute());
        if (subclassAttribute is null)
            return null;

        var parentType = subclassAttribute.AttributeClass?.TypeArguments.First();
        if (parentType is null)
            return null;

        var parentHandle = GetParentHandle(parentType);
        if (parentHandle is null)
            return null;

        var typeData = TypeDataHelper.GetTypeData(subclass);
        if (typeData is null)
            return null;

        return new SubclassData(
            TypeData: typeData,
            QualifiedName: GetQualifiedName(subclassAttribute),
            Parent: parentType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            ParentHandle: parentHandle,
            IsInitiallyUnowned: IsInitiallyUnowned(parentType),
            IsSealed: subclass.IsSealed,
            IsAbstractSubclass: subclass.IsAbstract
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
            .FirstOrDefault(x => x.IsSubclassAttribute());

        if (subclassAttribute is null)
            return null;

        return subclassAttribute.AttributeClass?.TypeArguments.First();
    }

    private static bool IsInitiallyUnowned(ITypeSymbol? type)
    {
        while (true)
        {
            if (type is null)
                return false;

            if (type.Name == "InitiallyUnowned" && type.ContainingNamespace?.ToDisplayString() == "GObject")
                return true;

            type = type.BaseType;
        }
    }

    private static string? GetQualifiedName(AttributeData subclassAttribute)
    {
        if (subclassAttribute.ConstructorArguments.IsDefaultOrEmpty)
            return null;

        string? qualifiedName = null;
        var constant = subclassAttribute.ConstructorArguments.First(); //QualifiedName constructor argument
        if (constant.Value is string value)
        {
            qualifiedName = value;
        }

        return qualifiedName;
    }
}
