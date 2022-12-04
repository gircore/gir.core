using System.Xml.Serialization;

namespace GirLoader.Input;

public class Implement
{
    [XmlAttribute("name")]
    public string? Name { get; set; }
}
