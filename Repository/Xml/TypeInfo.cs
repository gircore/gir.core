using System.Xml.Serialization;

#nullable enable

namespace Repository.Xml
{
    public class TypeInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? CType { get; set; }
    }
}
