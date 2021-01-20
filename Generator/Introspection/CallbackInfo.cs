using System.Xml.Serialization;

namespace Generator.Introspection
{
    public class CallbackInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? Type { get; set; }

        [XmlElement("return-value")]
        public ReturnValueInfo? ReturnValue { get; set; }

        [XmlElement("doc", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public DocInfo? Doc { get; set; }

        [XmlElement("parameters")]
        public ParametersInfo? Parameters { get; set; }
    }
}
