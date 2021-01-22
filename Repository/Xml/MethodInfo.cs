using System.Linq;
using System.Xml.Serialization;

#nullable enable

namespace Repository.Xml
{
    public class MethodInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("identifier", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? Identifier { get; set; }

        [XmlAttribute("throws")]
        public bool Throws { get; set; }

        [XmlAttribute("deprecated")]
        public bool Deprecated { get; set; }

        [XmlAttribute("deprecated-version")]
        public string? DeprecatedVersion { get; set; }

        [XmlElement("return-value")]
        public ReturnValueInfo? ReturnValue { get; set; }

        [XmlElement("doc")]
        public DocInfo? Doc { get; set; }

        [XmlElement("doc-deprecated")]
        public DocInfo? DocDeprecated { get; set; }

        [XmlElement("parameters")]
        public ParametersInfo? Parameters { get; set; }

        [XmlAttribute("moved-to")]
        public string? MovedTo { get; set; }
    }
}
