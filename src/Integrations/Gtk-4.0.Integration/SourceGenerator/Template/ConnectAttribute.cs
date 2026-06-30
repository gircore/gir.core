using Microsoft.CodeAnalysis;

namespace Gtk.Integration;

internal static class ConnectAttribute
{
    public const string FullyQualifiedDisplayName = "global::Gtk.ConnectAttribute";

    public static bool IsConnectAttribute(this AttributeData data)
    {
        var displayString = data.AttributeClass?.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return displayString == FullyQualifiedDisplayName;
    }
}
