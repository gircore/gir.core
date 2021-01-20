using System.Xml.Serialization;

namespace Generator.Introspection
{
    public class ImplementInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
