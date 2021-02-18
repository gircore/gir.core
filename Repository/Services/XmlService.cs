using System.IO;
using System.Xml.Serialization;
I think we can improve the public
namespace Repository.Services
{
    internal class XmlService
    {
        public T? Deserialize<T>(FileInfo girFile) where T : class
        {
            var serializer = new XmlSerializer(
                type: typeof(T),
                defaultNamespace: "http://www.gtk.org/introspection/core/1.0");

            using FileStream fs = girFile.OpenRead();

            return (T?) serializer.Deserialize(fs);
        }
    }
}
