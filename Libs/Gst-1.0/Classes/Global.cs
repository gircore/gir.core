namespace Gst
{
    public static partial class Global
    {
        public static uint ResourceErrorQuark
            => Native.resource_error_quark();

        public static uint StreamErrorQuark
            => Native.stream_error_quark();

        public static uint LibraryErrorQuark
            => Native.library_error_quark();

        public static uint CoreErrorQuark
            => Native.core_error_quark();
    }
}
