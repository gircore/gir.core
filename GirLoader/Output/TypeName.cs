using System.Diagnostics.CodeAnalysis;

namespace GirLoader.Output
{
    public record TypeName(string Value)
    {
        public TypeName(Helper.String helper) : this(helper.Get())
        {
        }

        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(TypeName? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
