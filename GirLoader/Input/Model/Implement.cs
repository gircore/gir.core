using System.Xml.Serialization;

namespace GirLoader.Input.Model
{
    public class Implement
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
