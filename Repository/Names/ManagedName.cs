using System.Diagnostics.CodeAnalysis;

namespace Repository
{
    public record ManagedName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(ManagedName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
