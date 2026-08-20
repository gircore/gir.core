using System.Collections.Generic;
using System.Xml.Serialization;

namespace GirLoader.Input;

public class Type
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
    public string? CType { get; set; }

    /// <summary>
    /// The element types of the type. Container types can contain one or more element types
    /// depending on the container type (e.g., GLib.List or GLib.HashTable).
    /// </summary>
    [XmlElement("type", typeof(Type))]
    [XmlElement("array", typeof(ArrayType))]
    public List<object> ElementTypes { get; set; } = new();
}
