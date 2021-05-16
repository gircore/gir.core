namespace Gir.Output.Model
{
    public abstract class PrimitiveValueType : Type
    {
        protected PrimitiveValueType(string nativeName, string managedName)
            : base(new CTypeName(nativeName), new TypeName(nativeName), new SymbolName(managedName))
        {
        }
    }

    public class Boolean : PrimitiveValueType
    {
        public Boolean(string nativeName) : base(nativeName, "bool") { }
    }

    public class Float : PrimitiveValueType
    {
        public Float(string nativeName) : base(nativeName, "float") { }
    }

    public class UnsignedShort : PrimitiveValueType
    {
        public UnsignedShort(string nativeName) : base(nativeName, "ushort") { }
    }

    public class Short : PrimitiveValueType
    {
        public Short(string nativeName) : base(nativeName, "short") { }
    }

    public class Double : PrimitiveValueType
    {
        public Double(string nativeName) : base(nativeName, "double") { }
    }

    public class Integer : PrimitiveValueType
    {
        public Integer(string nativeName) : base(nativeName, "int") { }
    }

    public class UnsignedInteger : PrimitiveValueType
    {
        public UnsignedInteger(string nativeName) : base(nativeName, "uint") { }
    }

    public class Byte : PrimitiveValueType
    {
        public Byte(string nativeName) : base(nativeName, "byte") { }
    }

    public class SignedByte : PrimitiveValueType
    {
        public SignedByte(string nativeName) : base(nativeName, "sbyte") { }
    }

    public class Long : PrimitiveValueType
    {
        public Long(string nativeName) : base(nativeName, "long") { }
    }

    public class UnsignedLong : PrimitiveValueType
    {
        public UnsignedLong(string nativeName) : base(nativeName, "ulong") { }
    }

    public class NativeUnsignedInteger : PrimitiveValueType
    {
        public NativeUnsignedInteger(string nativeName) : base(nativeName, "nuint") { }
    }
}
