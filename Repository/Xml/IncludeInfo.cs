using System.Xml.Serialization;

namespace Repository.Xml
{
    public class IncludeInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("version")]
        public string? Version { get; set; }
    }
}
