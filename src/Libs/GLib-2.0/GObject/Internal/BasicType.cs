namespace GObject.Internal;

public enum BasicType
{
    Invalid = 0 << 2,
    None = 1 << 2,
    Interface = 2 << 2,
    Char = 3 << 2,
    UChar = 4 << 2,
    Boolean = 5 << 2,
    Int = 6 << 2,
    UInt = 7 << 2,
    Long = 8 << 2,
    ULong = 9 << 2,
    Int64 = 10 << 2,
    UInt64 = 11 << 2,
    Enum = 12 << 2,
    Flags = 13 << 2,
    Float = 14 << 2,
    Double = 15 << 2,
    String = 16 << 2,
    Pointer = 17 << 2,
    Boxed = 18 << 2,
    Param = 19 << 2,
    Object = 20 << 2,
    Variant = 21 << 2
}
