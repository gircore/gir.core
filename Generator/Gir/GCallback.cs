using System.Xml.Serialization;

namespace Gir
{
    public class GCallback
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("type",  Namespace="http://www.gtk.org/introspection/c/1.0")]
        public string? Type { get; set; }

        [XmlElement("return-value")]
        public GReturnValue? ReturnValue { get; set; }

        [XmlElement("doc", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public GDoc? Doc { get; set; }

        [XmlElement("parameters")]
        public GParameters? Parameters { get; set; }
    }
}
