using System.Xml.Serialization;

namespace Repository.Xml
{
    public class Implement
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
