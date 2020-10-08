using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    public class GSignal
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("return-value", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public GReturnValue? ReturnValue { get; set; }

        [XmlElement("doc", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public GDoc? Doc { get; set; }

        [XmlElement("parameters", Namespace = "http://www.gtk.org/introspection/core/1.0")]
        public GParameters Parameters { get; set; } = default!;

        [XmlAttribute("deprecated")]
        public bool Deprecated { get; set; }

        public bool NeedsSignalArgs => Parameters?.Parameters.Count > 0;
        
        // TODO: Instance Parameters?

        // TODO: Implement more Signal attributes like When, Hooks, Recurse, etc
    }
}