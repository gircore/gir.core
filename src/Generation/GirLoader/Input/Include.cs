using System.Xml.Serialization;

namespace GirLoader.Input;

public class Include
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("version")]
    public string? Version { get; set; }
}
