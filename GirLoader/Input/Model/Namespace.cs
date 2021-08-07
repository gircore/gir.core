using System.Collections.Generic;
using System.Xml.Serialization;

namespace GirLoader.Input.Model
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
        public List<Class> Classes { get; set; } = new();

        [XmlElement("interface")]
        public List<Interface> Interfaces { get; set; } = new();

        [XmlElement("bitfield")]
        public List<Bitfield> Bitfields { get; set; } = new();

        [XmlElement("enumeration")]
        public List<Enum> Enumerations { get; set; } = new();

        [XmlElement("alias")]
        public List<Alias> Aliases { get; set; } = new();

        [XmlElement("callback")]
        public List<Callback> Callbacks { get; set; } = new();

        [XmlElement("record")]
        public List<Record> Records { get; set; } = new();

        [XmlElement("function")]
        public List<Method> Functions { get; set; } = new();

        [XmlElement("union")]
        public List<Union> Unions { get; set; } = new();

        [XmlElement("constant")]
        public List<Constant> Constants { get; set; } = new();
    }
}
