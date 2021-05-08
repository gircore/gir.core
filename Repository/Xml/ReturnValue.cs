using System.Xml.Serialization;

namespace Repository.Xml
{
    public class ReturnValue : Typed
    {
        [XmlAttribute("transfer-ownership")]
        public string? TransferOwnership { get; set; }

        [XmlAttribute("nullable")]
        public bool Nullable { get; set; }

        [XmlElement("doc")]
        public Doc? Doc { get; set; }

        [XmlElement("type")]
        public Type? Type { get; set; }

        [XmlElement("array")]
        public Array? Array { get; set; }
    }
}
