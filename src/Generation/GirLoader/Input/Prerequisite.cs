using System.Xml.Serialization;

namespace GirLoader.Input;

public class Prerequisite
{
    [XmlAttribute("name")]
    public string? Name { get; set; }
}
