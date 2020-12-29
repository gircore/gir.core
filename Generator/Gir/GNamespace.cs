using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Gir
{
    public class GNamespace
    {
        #region Properties

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
        public List<GClass> Classes { get; set; } = default!;

        [XmlElement("interface")]
        public List<GInterface> Interfaces { get; set; } = default!;

        [XmlElement("bitfield")]
        public List<GEnumeration> Bitfields { get; set; } = default!;

        [XmlElement("enumeration")]
        public List<GEnumeration> Enumerations { get; set; } = default!;

        [XmlElement("alias")]
        public List<GAlias> Aliases { get; set; } = default!;

        [XmlElement("callback")]
        public List<GCallback> Callbacks { get; set; } = default!;

        [XmlElement("record")]
        public List<GRecord> Records { get; set; } = default!;

        [XmlElement("function")]
        public List<GMethod> Functions { get; set; } = default!;

        [XmlElement("union")]
        public List<GRecord> Unions { get; set; } = default!;

        [XmlElement("constant")]
        public List<GConstant> Constants { get; set; } = default!;

        #endregion
    }
}
