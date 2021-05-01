using System.Collections.Generic;
using System.Xml.Serialization;

namespace Repository.Xml
{
    public class UnionInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("doc")]
        public DocInfo? Doc { get; set; }

        [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? CType { get; set; }

        [XmlAttribute("type-name", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public string? TypeName { get; set; }

        [XmlElement("method")]
        public List<MethodInfo> Methods { get; set; } = default!;

        [XmlElement("constructor")]
        public List<MethodInfo> Constructors { get; set; } = default!;

        [XmlElement("function")]
        public List<MethodInfo> Functions { get; set; } = default!;

        [XmlAttribute("get-type", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public string? GetTypeFunction { get; set; }

        [XmlElement("field")]
        public List<FieldInfo> Fields { get; set; } = default!;

        [XmlAttribute("disguised")]
        public bool Disguised;
    }
}
