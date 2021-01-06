using System;
using System.IO;
using System.Reflection;

namespace Gir.Integration.CSharp
{
    internal static class AssemblyExtension
    {
        public static string ReadResourceAsString(this Assembly assembly, string resource)
        {
            Stream? stream = assembly.GetManifestResourceStream(resource);

            if (stream == null)
                throw new Exception("Cannot get resource file '" + resource + "'");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
