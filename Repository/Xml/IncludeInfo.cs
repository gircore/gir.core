using System.Xml.Serialization;

#nullable enable

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
