using System.Xml.Serialization;

namespace Repository.Xml
{
    internal class ImplementInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
