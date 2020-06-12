using System.Xml.Serialization;

namespace Gir
{
    public class GInclude
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("version")]
        public string? Version { get; set; }
    }
}
