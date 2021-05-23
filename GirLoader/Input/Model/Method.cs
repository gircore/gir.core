using System.Xml.Serialization;

namespace GirLoader.Input.Model
{
    public class Method
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
        public ReturnValue? ReturnValue { get; set; }

        [XmlElement("doc")]
        public Doc? Doc { get; set; }

        [XmlElement("doc-deprecated")]
        public Doc? DocDeprecated { get; set; }

        [XmlElement("parameters")]
        public Parameters? Parameters { get; set; }

        [XmlAttribute("moved-to")]
        public string? MovedTo { get; set; }

        public override string? ToString()
            => Name ?? Identifier ?? base.ToString();
    }
}
