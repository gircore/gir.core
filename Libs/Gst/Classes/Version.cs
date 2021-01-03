using System;
using System.Runtime.InteropServices;
using GLib;
using String = GLib.String;

namespace Gst
{
    public static class Version
    {
        public static int Major => Constants.VERSION_MAJOR;
        public static int Minor => Constants.VERSION_MINOR;
        public static int Micro => Constants.VERSION_MICRO;
        public static int Nano => Constants.VERSION_NANO;

        public static string VersionString
            => StringHelper.ToAnsiStringAndFree(Global.Native.version_string());
    }
}
