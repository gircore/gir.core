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

    internal static class Sized
    {
        public static string GetInternalOwnedHandleName() => "GLib.Internal.PlatformStringArraySizedOwnedHandle";
        public static string GetInternalUnownedHandleName() => "GLib.Internal.PlatformStringArraySizedUnownedHandle";
        public static string GetInternalContainerHandleName() => "GLib.Internal.PlatformStringArraySizedContainerHandle";
        public static string GetInternalHandleName() => "GLib.Internal.PlatformStringArraySizedHandle";
    }
}
