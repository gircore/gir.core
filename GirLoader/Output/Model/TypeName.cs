using System.Diagnostics.CodeAnalysis;

namespace GirLoader.Output.Model
{
    public record TypeName(string Value)
    {
        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(TypeName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
