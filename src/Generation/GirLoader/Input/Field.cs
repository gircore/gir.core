using System.Xml.Serialization;

namespace GirLoader.Input;

public class Field : AnyType
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("readable")]
    public bool Readable { get; set; } = true;

    [XmlAttribute("writable")]
    public bool Writable { get; set; }

    [XmlAttribute("private")]
    public bool Private { get; set; }

    [XmlAttribute("bits")]
    public int Bits { get; set; }

    [XmlElement("callback")]
    public Callback? Callback { get; set; }

    [XmlElement("type")]
    public Type? Type { get; set; }

    [XmlElement("array")]
    public ArrayType? Array { get; set; }

    [XmlElement("doc")]
    public Doc? Doc { get; set; }

    [XmlAttribute("introspectable")]
    public bool Introspectable { get; set; } = true;
}
