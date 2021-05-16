using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir.Input.Model
{
    public class Namespace
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("version")]
        public string? Version { get; set; }

        [XmlAttribute("identifier-prefixes", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? IdentifierPrefixes { get; set; }

        [XmlAttribute("symbol-prefixes", Namespace = "http://www.gtk.org/introspection/c/1.0")]
        public string? SymbolPrefixes { get; set; }

        [XmlAttribute("shared-library")]
        public string? SharedLibrary { get; set; }

        [XmlElement("class")]
        public List<Class> Classes { get; set; } = default!;

        [XmlElement("interface")]
        public List<Interface> Interfaces { get; set; } = default!;

        [XmlElement("bitfield")]
        public List<Enum> Bitfields { get; set; } = default!;

        [XmlElement("enumeration")]
        public List<Enum> Enumerations { get; set; } = default!;

        [XmlElement("alias")]
        public List<Alias> Aliases { get; set; } = default!;

        [XmlElement("callback")]
        public List<Callback> Callbacks { get; set; } = default!;

        [XmlElement("record")]
        public List<Record> Records { get; set; } = default!;

        [XmlElement("function")]
        public List<Method> Functions { get; set; } = default!;

        [XmlElement("union")]
        public List<Union> Unions { get; set; } = default!;

        [XmlElement("constant")]
        public List<Constant> Constants { get; set; } = default!;
    }
}
