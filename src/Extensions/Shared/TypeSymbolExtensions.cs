using Microsoft.CodeAnalysis;

internal static class TypeSymbolExtensions
{

    public static string? GetKind(this ITypeSymbol symbol)
    {
        return (symbol.TypeKind, symbol.IsRecord) switch
        {
            (TypeKind.Struct, _) => "struct",
            (TypeKind.Class, true) => "record",
            (TypeKind.Class, _) => "class",
            _ => null
        };
    }
}
