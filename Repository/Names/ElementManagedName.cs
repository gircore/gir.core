using System.Diagnostics.CodeAnalysis;

namespace Repository
{
    public record ElementManagedName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(ElementManagedName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
