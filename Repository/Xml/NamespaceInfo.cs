using System.Collections.Generic;
using System.Xml.Serialization;

namespace Repository.Xml
{
    public class NamespaceInfo
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
        public List<ClassInfo> Classes { get; set; } = default!;

        [XmlElement("interface")]
        public List<InterfaceInfo> Interfaces { get; set; } = default!;

        [XmlElement("bitfield")]
        public List<EnumInfo> Bitfields { get; set; } = default!;

        [XmlElement("enumeration")]
        public List<EnumInfo> Enumerations { get; set; } = default!;

        [XmlElement("alias")]
        public List<AliasInfo> Aliases { get; set; } = default!;

        [XmlElement("callback")]
        public List<CallbackInfo> Callbacks { get; set; } = default!;

        [XmlElement("record")]
        public List<RecordInfo> Records { get; set; } = default!;

        [XmlElement("function")]
        public List<MethodInfo> Functions { get; set; } = default!;

        [XmlElement("union")]
        public List<UnionInfo> Unions { get; set; } = default!;

        [XmlElement("constant")]
        public List<ConstantInfo> Constants { get; set; } = default!;
    }
}
