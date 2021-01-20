using System.Xml.Serialization;

namespace Generator.Introspection
{
    public class TypeInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? CType { get; set; }
    }
}
