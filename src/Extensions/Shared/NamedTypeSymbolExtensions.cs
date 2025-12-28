using System.Text;
using Microsoft.CodeAnalysis;

internal static class NamedTypeSymbolExtensions
{
    public static string GetFileName(this INamedTypeSymbol typeSymbol)
    {
        var prefix = GetFileNamePrefix(typeSymbol);
        var suffix = typeSymbol.Arity == 0 ? string.Empty : $"_{typeSymbol.Arity}";

        return prefix + typeSymbol.Name + suffix;
    }

    private static string GetFileNamePrefix(ISymbol typeSymbol)
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
}
