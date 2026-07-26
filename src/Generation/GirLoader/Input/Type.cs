using System.Collections.Generic;
using System.Xml.Serialization;

namespace GirLoader.Input;

public class Type
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
    public string? CType { get; set; }

    //Container types like GLib.List carry their element type as nested type
    //elements. GLib.HashTable carries two: the key and the value type.
    [XmlElement("type")]
    public List<Type> Types { get; set; } = new();
}
