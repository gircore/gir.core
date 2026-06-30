using Microsoft.CodeAnalysis;

namespace GObject.Integration;

internal static class HandleAttribute
{
    private const string FullyQualifiedDisplayName = "global::GObject.HandleAttribute<T>";

    public static bool IsHandleAttribute(this AttributeData data)
    {
        var displayString = data.AttributeClass?.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return displayString == FullyQualifiedDisplayName;
    }
}
