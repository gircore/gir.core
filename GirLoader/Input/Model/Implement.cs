using System.Xml.Serialization;

namespace Gir.Input.Model
{
    public class Implement
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
