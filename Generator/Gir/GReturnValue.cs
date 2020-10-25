using System.Xml.Serialization;

namespace Gir
{
    public class GReturnValue : IType
    {
        [XmlAttribute("transfer-ownership")]
        public string? TransferOwnership { get; set; }

        [XmlAttribute("nullable")]
        public bool Nullable { get; set; }

        [XmlElement("doc")]
        public GDoc? Doc { get; set; }

        [XmlElement("type")]
        public GType? Type { get; set; }

        [XmlElement("array")]
        public GArray? Array { get; set; }
    }
}
