using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gir
{
    public class GRecord
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("doc")]
        public GDoc? Doc { get; set; }

        [XmlAttribute("type",  Namespace="http://www.gtk.org/introspection/c/1.0")]
        public string? Type { get; set; }

        [XmlElement("method")]
        public List<GMethod> Methods { get; set; } = default!;

        [XmlElement("function")]
        public List<GMethod> Functions { get; set; } = default!;
        
        [XmlElement("constructor")]
        public List<GMethod> Constructors { get; set; } = default!;
    }
}
