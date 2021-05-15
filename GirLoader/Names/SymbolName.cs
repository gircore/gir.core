using System.Diagnostics.CodeAnalysis;

namespace Gir
{
    public record SymbolName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(SymbolName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
