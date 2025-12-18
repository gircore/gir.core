using System.Linq;
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
        if (context.TargetSymbol is not INamedTypeSymbol subclass)
            return null;

        var templateAttribute = context
            .Attributes
            .FirstOrDefault(a => a.IsTemplateAttribute());

        if (templateAttribute is null)
            return null;

        var loader = templateAttribute.AttributeClass?.TypeArguments.First();
        if (loader is null)
            return null;

        var resourceName = GetResourceName(templateAttribute);
        if (resourceName is null)
            return null;

        var typeData = TypeDataHelper.GetTypeData(subclass);
        if (typeData is null)
            return null;

        return new TemplateData(
            TypeData: typeData,
            RessourceName: resourceName,
            Loader: loader.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
        );
    }

    private static string? GetResourceName(AttributeData templateAttribute)
    {
        if (templateAttribute.ConstructorArguments.IsDefaultOrEmpty)
            return null;

        string? resourceName = null;
        var constant = templateAttribute.ConstructorArguments.First(); //ResourceName constructor argument
        if (constant.Value is string value)
        {
            resourceName = value;
        }

        return resourceName;
    }
}
