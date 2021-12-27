using System.Xml.Serialization;

namespace GirLoader.Input
{
    public class Signal
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("return-value", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public ReturnValue? ReturnValue { get; set; }

        [XmlElement("doc", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public Doc? Doc { get; set; }

        [XmlElement("parameters", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public Parameters? Parameters { get; set; }

        [XmlAttribute("deprecated")]
        public bool Deprecated { get; set; }
        
        [XmlAttribute("introspectable")]
        public bool Introspectable = true;
    }
}
