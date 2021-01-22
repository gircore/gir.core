using System.Xml.Serialization;

#nullable enable

namespace Repository.Xml
{
    public class ImplementInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
