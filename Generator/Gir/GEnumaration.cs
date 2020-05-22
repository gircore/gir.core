using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    public class GEnumeration
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("type", Namespace="http://www.gtk.org/introspection/c/1.0")]
        public string? Type { get; set; }

        [XmlAttribute("type-name", Namespace="http://www.gtk.org/introspection/glib/1.0")]
        public string? TypeName { get; set; }

        [XmlElement("doc")]
        public GDoc? Doc { get; set; }

        [XmlElement("member")]
        public List<GMember> Members { get; set; } = default!;
    }
}
