using System.Xml.Serialization;

namespace Gir
{
    public class GField
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("readable")]
        public bool Readable { get; set; }
        
        [XmlAttribute("private")]
        public bool Private { get; set; }
        
        [XmlElement("type")]
        public GType? Type { get; set; }
    }
}
