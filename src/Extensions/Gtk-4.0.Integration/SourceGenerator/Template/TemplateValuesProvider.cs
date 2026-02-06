using System.Collections.Generic;
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
            ResourceName: resourceName,
            Loader: loader.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            Connections: GetConnections(subclass)
        );
    }

    private static HashSet<TemplateData.Connect> GetConnections(INamedTypeSymbol subclass)
    {
        var connections = new HashSet<TemplateData.Connect>();

        foreach (var member in subclass.GetMembers())
        {
            var attribute = member.GetAttributes().FirstOrDefault(x => x.IsConnectAttribute());
            if (attribute is null)
                continue;

            var objectId = GetObjectId(attribute) ?? member.Name;
            var type = GetMemberType(member);

            if (type is null)
                continue;

            connections.Add(new TemplateData.Connect(
                ObjectId: objectId,
                Type: type,
                MemberName: member.Name
            ));
        }

        return connections;
    }

    private static string? GetObjectId(AttributeData attributeData)
    {
        if (attributeData.ConstructorArguments.Length > 0 && attributeData.ConstructorArguments[0].Value is string value)
            return value;

        return null;
    }

    private static string? GetMemberType(ISymbol symbol)
    {
        return symbol switch
        {
            IFieldSymbol fieldSymbol => fieldSymbol.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            IPropertySymbol propertySymbol => propertySymbol.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            _ => null
        };
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
