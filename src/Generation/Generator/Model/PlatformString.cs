namespace Generator.Model;

internal static partial class PlatformString
{
    private const string NullableTypeNamePrefix = "GLib.Internal.NullablePlatformString";
    private const string NonNullableTypeNamePrefix = "GLib.Internal.NonNullablePlatformString";

    public static string GetInternalNullableHandleName() => NullableTypeNamePrefix + "Handle";
    public static string GetInternalNullableOwnedHandleName() => NullableTypeNamePrefix + "OwnedHandle";
    public static string GetInternalNullableUnownedHandleName() => NullableTypeNamePrefix + "UnownedHandle";

    public static string GetInternalNonNullableHandleName() => NonNullableTypeNamePrefix + "Handle";
    public static string GetInternalNonNullableOwnedHandleName() => NonNullableTypeNamePrefix + "OwnedHandle";
    public static string GetInternalNonNullableUnownedHandleName() => NonNullableTypeNamePrefix + "UnownedHandle";
}
