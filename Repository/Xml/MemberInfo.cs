using System.Xml.Serialization;

namespace Repository.Xml
{
    internal class MemberInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("value")]
        public string? Value { get; set; }

        [XmlAttribute("identifier", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? Identifier { get; set; }

        [XmlElement("doc")]
        public DocInfo? Doc { get; set; }
    }
}
