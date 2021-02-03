using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Repository.Xml
{
    public class ClassInfo : InterfaceInfo
    {
        [XmlElement("function")]
        public List<MethodInfo> Functions { get; set; } = default!;

        [XmlElement("constructor")]
        public List<MethodInfo> Constructors { get; set; } = default!;

        [XmlElement("signal", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public List<SignalInfo> Signals { get; set; } = default!;
        
        [XmlElement ("field")]
        public List<FieldInfo> Fields { get; set; } = default!;

        [XmlAttribute("parent")]
        public string? Parent { get; set; }

        [XmlAttribute("abstract")]
        public bool Abstract { get; set; }

        [XmlAttribute("fundamental", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public bool Fundamental { get; set; }

        [XmlElement("implements")]
        public List<ImplementInfo> Implements { get; set; } = default!;
    }
}
