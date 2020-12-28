using System;
using System.IO;
using System.Reflection;
using GLib;

namespace GObject
{
    public static class AssemblyExtension
    {
        public static Bytes ReadResource(this Assembly assembly, string resource)
        {
            Stream? stream = assembly.GetManifestResourceStream(resource);

            if (stream == null)
                throw new Exception("Cannot get resource file '" + resource + "'");

            var size = (int) stream.Length;
            var buffer = new byte[size];
            stream.Read(buffer, 0, size);
            stream.Close();

            return Bytes.From(buffer);
        }
    }
}
