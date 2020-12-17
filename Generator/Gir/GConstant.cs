using System.Xml.Serialization;

namespace Gir
{
    public class GConstant
    {
        #region Properties

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("value")]
        public string? Value { get; set; }

        [XmlElement("type")]
        public GType? Type { get; set; }
        
        [XmlElement("doc")]
        public GDoc? Doc { get; set; }

        #endregion
    }
}
