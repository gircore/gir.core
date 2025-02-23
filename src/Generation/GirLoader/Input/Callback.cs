using System.Xml.Serialization;

namespace GirLoader.Input;

public class Callback
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
    public string? Type { get; set; }

    [XmlElement("return-value")]
    public ReturnValue? ReturnValue { get; set; }

    [XmlAttribute("throws")]
    public bool Throws { get; set; }

    [XmlElement("doc", Namespace = "http://www.gtk.org/introspection/core/1.0")]
    public Doc? Doc { get; set; }

    [XmlElement("parameters")]
    public Parameters? Parameters { get; set; }

    [XmlAttribute("introspectable")]
    public bool Introspectable = true;
}
