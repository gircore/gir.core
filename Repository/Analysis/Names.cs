using System.Diagnostics.CodeAnalysis;

namespace Repository.Analysis
{
    public record TypeName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(TypeName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
    
    public record CTypeName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(CTypeName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
    
    public record NamespaceName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(NamespaceName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
