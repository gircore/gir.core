using System.Diagnostics.CodeAnalysis;

namespace Gir.Output.Model
{
    public record ElementName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(ElementName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
