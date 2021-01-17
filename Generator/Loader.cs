using System;
using System.IO;
using System.Xml.Serialization;
using Introspection;

namespace Generator
{
    internal class Loader
    {
        internal static GRepository SerializeGirFile(string girFile)
        {
            var serializer = new XmlSerializer(typeof(GRepository), "http://www.gtk.org/introspection/core/1.0");

            using var fs = new FileStream(girFile, FileMode.Open);
            object? repository = serializer.Deserialize(fs);
            
            return repository as GRepository ?? throw new Exception($"Could not deserialize {girFile}");
        }
    }
}
