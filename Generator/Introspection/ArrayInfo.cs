using System.Xml.Serialization;

namespace Generator.Introspection
{
    public class ArrayInfo
    {
        [XmlAttribute("length")]
        public string? Length { get; set; }

        [XmlAttribute("zero-terminated")]
        public bool ZeroTerminated { get; set; }

        [XmlAttribute("fixed-size")]
        public string? FixedSize { get; set; }

        [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? CType { get; set; }

        [XmlElement("type")]
        public TypeInfo? Type { get; set; }
    }
}
