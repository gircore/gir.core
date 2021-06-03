using System.Diagnostics.CodeAnalysis;

namespace GirLoader.Output.Model
{
    public class CType
    {
        public string Value { get; }
        public bool IsPointer { get; }
        public bool IsConst { get; }
        public bool IsVolatile { get; }

        public CType(string value)
        {
            value = value.Replace(" ", "");
            IsPointer = TryRemove(ref value, "*");
            IsConst = TryRemove(ref value, "const");
            IsVolatile = TryRemove(ref value, "volatile");

            Value = value;
        }

        private bool TryRemove(ref string value, string remove)
        {
            var originalLength = value.Length;
            value = value.Replace(remove, "");

            return originalLength != value.Length;
        }

        [return: NotNullIfNotNull("name")]
        public static implicit operator string?(CType? name)
            => name?.Value;

        public override string ToString()
            => Value;
    }
}
