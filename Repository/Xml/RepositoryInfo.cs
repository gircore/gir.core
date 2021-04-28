using System.Collections.Generic;
using System.Xml.Serialization;

namespace Repository.Xml
{
    [XmlRoot(ElementName = "repository")]
    public class RepositoryInfo
    {
        [XmlAttribute("version")]
        public string? Version { get; set; }

        [XmlElement("namespace")]
        public NamespaceInfo? Namespace { get; set; }

        [XmlElement("include")]
        public List<IncludeInfo> Includes { get; set; } = default!;
    }
}
