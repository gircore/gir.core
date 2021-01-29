using System.Xml.Serialization;

namespace Gir
{
    public class GField : IType
    {
        #region Properties

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("readable")]
        public bool Readable { get; set; }

        [XmlAttribute("writeable")]
        public bool Writeable { get; set; }

        [XmlAttribute("private")]
        public bool Private { get; set; }

        [XmlAttribute("bits")]
        public int Bits { get; set; }

        [XmlElement("callback")]
        public GCallback? Callback { get; set; }

        [XmlElement("type")]
        public GType? Type { get; set; }

        [XmlElement("array")]
        public GArray? Array { get; set; }

        [XmlElement("doc")]
        public GDoc? Doc { get; set; }

        #endregion
    }
}
