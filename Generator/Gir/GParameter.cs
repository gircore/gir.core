using System.Xml.Serialization;

namespace Gir
{
    public class GParameter : IType
    {
        #region Properties

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("transfer-ownership")]
        public string? TransferOwnership { get; set; }

        [XmlAttribute("direction")]
        public string? Direction { get; set; }
        
        [XmlAttribute ("caller-allocates")]
        public bool CallerAllocates;

        [XmlElement("doc")]
        public GDoc? Doc { get; set; }

        [XmlElement("type")]
        public GType? Type { get; set; }

        [XmlElement("array")]
        public GArray? Array { get; set; }

        [XmlElement("varargs")]
        public GVarArgs? VarArgs { get; set; }

        [XmlAttribute("nullable")]
        public bool Nullable;

        #endregion
    }
}
