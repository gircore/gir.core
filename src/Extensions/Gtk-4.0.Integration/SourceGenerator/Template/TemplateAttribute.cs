using Microsoft.CodeAnalysis;

namespace Gtk.Integration;

internal static class TemplateAttribute
{
    public const string MetadataName = "Gtk.TemplateAttribute`1";
    public const string FullyQualifiedDisplayName = "global::Gtk.TemplateAttribute<TLoader>";

    public static bool IsTemplateAttribute(this AttributeData data)
    {
        var displayString = data.AttributeClass?.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return displayString == FullyQualifiedDisplayName;
    }
}
