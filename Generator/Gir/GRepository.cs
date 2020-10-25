using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    [XmlRoot(ElementName = "repository")]
    public class GRepository
    {
        [XmlAttribute("version")]
        public string? Version { get; set; }

        [XmlElement("namespace")]
        public GNamespace? Namespace { get; set; }

        [XmlElement("include")]
        public List<GInclude> Includes { get; set; } = default!;
    }
}
