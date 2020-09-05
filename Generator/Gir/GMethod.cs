using System.Linq;
using System.Xml.Serialization;

namespace Gir
{
    public class GMethod
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("identifier", Namespace="http://www.gtk.org/introspection/c/1.0")]
        public string? Identifier { get; set; }

        [XmlAttribute("throws")]
        public bool Throws { get; set; }

        [XmlAttribute("deprecated")]
        public bool Deprecated { get; set; }

        [XmlAttribute("deprecated-version")]
        public string? DeprecatedVersion { get; set; }

        [XmlElement("return-value")]
        public GReturnValue? ReturnValue { get; set; }

        [XmlElement("doc")]
        public GDoc? Doc { get; set; }

        [XmlElement("doc-deprecated")]
        public GDoc? DocDeprecated { get; set; }

        [XmlElement("parameters")]
        public GParameters? Parameters { get; set; }
        
        [XmlAttribute("moved-to")]
        public string? MovedTo { get; set; }

        public bool HasVariadicParameter()
        {
            static bool IsVariadic(GParameter p) => p.VarArgs is {};

            return Parameters?.Parameters.Any(IsVariadic) ?? false;
        }
    }
}
