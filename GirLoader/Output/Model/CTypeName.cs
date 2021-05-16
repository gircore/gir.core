using System.Diagnostics.CodeAnalysis;

namespace Gir.Output.Model
{
    public record CTypeName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(CTypeName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
