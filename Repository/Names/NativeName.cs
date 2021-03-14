using System.Diagnostics.CodeAnalysis;

namespace Repository
{
    public record NativeName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(NativeName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
