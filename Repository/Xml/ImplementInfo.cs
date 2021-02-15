using System.Xml.Serialization;

namespace Repository.Xml
{
    public class ImplementInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
