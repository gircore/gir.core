using System.Xml.Serialization;

namespace Gir
{
    public class GMember
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("value")]
        public string? Value { get; set; }

        [XmlAttribute("identifier", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? Identifier { get; set; }

        [XmlElement("doc")]
        public GDoc? Doc { get; set; }
    }
}
