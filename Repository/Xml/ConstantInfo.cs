using System.Xml.Serialization;

#nullable enable

namespace Repository.Xml
{
    public class ConstantInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("value")]
        public string? Value { get; set; }

        [XmlElement("type")]
        public TypeInfo? Type { get; set; }
        
        [XmlElement("doc")]
        public DocInfo? Doc { get; set; }
    }
}
