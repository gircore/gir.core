using System;
using System.Diagnostics.CodeAnalysis;

namespace GirLoader.Output.Model
{
    public class CType : IEquatable<CType>
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

        public bool Equals(CType? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != typeof(CType))
                return false;

            return Equals((CType) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(CType? left, CType? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CType? left, CType? right)
        {
            return !Equals(left, right);
        }
    }
}
