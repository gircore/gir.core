using Microsoft.CodeAnalysis;

namespace GObject.Integration;

internal static class SubclassAttribute
{
    public const string MetadataName = "GObject.SubclassAttribute`1";
    public const string FullyQualifiedDisplayName = "global::GObject.SubclassAttribute<T>";

    public static bool IsSubclassAttribute(this AttributeData data)
    {
        var displayString = data.AttributeClass?.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return displayString == FullyQualifiedDisplayName;
    }
}
