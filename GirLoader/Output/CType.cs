using System.Diagnostics.CodeAnalysis;

namespace GirLoader.Output
{
    public record CType(string Value)
    {
        [return: NotNullIfNotNull("ctype")]
        public static implicit operator string?(CType? ctype)
            => ctype?.Value;
    }
}
