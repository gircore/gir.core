using System.Collections.Generic;
using System.Xml.Serialization;

namespace GirLoader.Input;

public class Type
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("type", Namespace = "http://www.gtk.org/introspection/c/1.0")]
    public string? CType { get; set; }

    //Container types like GLib.List carry their element type as nested type or
    //array elements ("AnyType*" in the GIR schema). GLib.HashTable carries two:
    //the key and the value type. The document order is preserved as it
    //distinguishes the key from the value type.
    [XmlElement("type", typeof(Type))]
    [XmlElement("array", typeof(ArrayType))]
    public List<object> AnyTypes { get; set; } = new();
}
