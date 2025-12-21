using Microsoft.CodeAnalysis;

namespace Gtk.Integration;

internal static class TemplateAttribute
{
    public const string MetadataName = "Gtk.TemplateAttribute";
    public const string FullyQualifiedDisplayName = "global::Gtk.TemplateAttribute";

    public static bool IsTemplateAttribute(this AttributeData data)
    {
        var displayString = data.AttributeClass?.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return displayString == FullyQualifiedDisplayName;
    }
}
