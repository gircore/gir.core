using System;

namespace Gst
{
    public static partial class Global
    {
        public static uint ResourceErrorQuark
            => throw new NotImplementedException(); //TODO Native.resource_error_quark();

        public static uint StreamErrorQuark
            => throw new NotImplementedException(); //TODO Native.stream_error_quark();

        public static uint LibraryErrorQuark
            => throw new NotImplementedException(); //TODO Native.library_error_quark();

        public static uint CoreErrorQuark
            => throw new NotImplementedException(); //TODO Native.core_error_quark();
    }
}
