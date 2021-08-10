using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GirLoader.Input
{
    [XmlRoot(ElementName = "repository")]
    public class Repository
    {
        [XmlAttribute("version")]
        public string? Version { get; set; }

        [XmlElement("namespace")]
        public Namespace? Namespace { get; set; }

        [XmlElement("include")]
        public List<Include> Includes { get; set; } = new();

        public override string ToString()
        {
            var namespaceName = Namespace?.Name ?? throw new Exception("A repository without a namespace name is not valid");
            return "Repository: " + namespaceName;
        }
    }
}
