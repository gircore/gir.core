using System.IO;
using System.Xml.Serialization;

namespace Gir
{
    public class GirReader
    {
        public GRepository ReadRepository(string girFile)
        {
            var serializer = new XmlSerializer(typeof(GRepository), "http://www.gtk.org/introspection/core/1.0");

            using var fs = new FileStream(girFile, FileMode.Open);
            return (GRepository) serializer.Deserialize(fs);
        }
    }
}