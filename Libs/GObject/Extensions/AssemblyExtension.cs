using System;
using System.IO;
using System.Reflection;
using GLib;

namespace GObject
{
    public static class AssemblyExtension
    {
        public static Bytes ReadResource(this Assembly assembly, string template)
        {
            Stream? stream = assembly.GetManifestResourceStream(template);

            if (stream == null)
                throw new Exception("Cannot get resource file '" + template + "'");

            var size = (int) stream.Length;
            var buffer = new byte[size];
            stream.Read(buffer, 0, size);
            stream.Close();

            return Bytes.From(buffer);
        }
    }
}
