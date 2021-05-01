using System.Xml.Serialization;

namespace Repository.Xml
{
    public class SignalInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("return-value", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public ReturnValueInfo? ReturnValue { get; set; }

        [XmlElement("doc", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public DocInfo? Doc { get; set; }

        [XmlElement("parameters", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public ParametersInfo? Parameters { get; set; }

        [XmlAttribute("deprecated")]
        public bool Deprecated { get; set; }
    }
}
