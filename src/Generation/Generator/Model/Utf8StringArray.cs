namespace Generator.Model;

internal static class Utf8StringArray
{
    internal static class NullTerminated
    {
        public static string GetInternalOwnedHandleName() => "GLib.Internal.Utf8StringArrayNullTerminatedOwnedHandle";
        public static string GetInternalUnownedHandleName() => "GLib.Internal.Utf8StringArrayNullTerminatedUnownedHandle";
        public static string GetInternalContainerHandleName() => "GLib.Internal.Utf8StringArrayNullTerminatedContainerHandle";
        public static string GetInternalHandleName() => "GLib.Internal.Utf8StringArrayNullTerminatedHandle";
    }
}
