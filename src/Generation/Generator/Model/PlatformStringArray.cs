namespace Generator.Model;

internal static class PlatformStringArray
{
    internal static class NullTerminated
    {
        public static string GetInternalOwnedHandleName() => "GLib.Internal.PlatformStringArrayNullTerminatedOwnedHandle";
        public static string GetInternalUnownedHandleName() => "GLib.Internal.PlatformStringArrayNullTerminatedUnownedHandle";
        public static string GetInternalContainerHandleName() => "GLib.Internal.PlatformStringArrayNullTerminatedContainerHandle";
        public static string GetInternalHandleName() => "GLib.Internal.PlatformStringArrayNullTerminatedHandle";
    }
}
