using System.Xml.Serialization;

namespace Gir.Xml
{
    public class Implement
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
