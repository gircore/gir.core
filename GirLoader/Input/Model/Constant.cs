using System.Xml.Serialization;

namespace Gir.Input.Model
{
    public class Constant : AnyType
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("value")]
        public string? Value { get; set; }

        [XmlElement("type")]
        public Type? Type { get; set; }

        [XmlElement("array")]
        public Array? Array { get; set; }

        [XmlElement("doc")]
        public Doc? Doc { get; set; }
    }
}
