using System.Diagnostics.CodeAnalysis;

namespace Repository
{
    public record NamespaceName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(NamespaceName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
