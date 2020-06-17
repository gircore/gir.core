using System.Xml.Serialization;

namespace Gir
{
    public class GConstant
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("value")]
        public string? Value { get; set; }

        [XmlElement("type")]
        public GType? Type { get; set; }
    }
}
