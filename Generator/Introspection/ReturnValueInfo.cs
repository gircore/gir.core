using System.Xml.Serialization;

namespace Generator.Introspection
{
    public class ReturnValueInfo : IType
    {
        [XmlAttribute("transfer-ownership")]
        public string? TransferOwnership { get; set; }

        [XmlAttribute("nullable")]
        public bool Nullable { get; set; }

        [XmlElement("doc")]
        public DocInfo? Doc { get; set; }

        [XmlElement("type")]
        public TypeInfo? Type { get; set; }

        [XmlElement("array")]
        public ArrayInfo? Array { get; set; }
    }
}
