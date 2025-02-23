namespace Generator.Model;

internal static partial class Utf8String
{
    private const string NullableTypeNamePrefix = "GLib.Internal.NullableUtf8String";
    private const string NonNullableTypeNamePrefix = "GLib.Internal.NonNullableUtf8String";
    private const string ConstantStringName = "GLib.ConstantString";

    public static string GetInternalNullableHandleName() => NullableTypeNamePrefix + "Handle";
    public static string GetInternalNullableOwnedHandleName() => NullableTypeNamePrefix + "OwnedHandle";
    public static string GetInternalNullableUnownedHandleName() => NullableTypeNamePrefix + "UnownedHandle";

    public static string GetInternalNonNullableHandleName() => NonNullableTypeNamePrefix + "Handle";
    public static string GetInternalNonNullableOwnedHandleName() => NonNullableTypeNamePrefix + "OwnedHandle";
    public static string GetInternalNonNullableUnownedHandleName() => NonNullableTypeNamePrefix + "UnownedHandle";
    public static string GetPublicConstantStringName() => ConstantStringName;
}
