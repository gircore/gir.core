using System.Xml.Serialization;

namespace GirLoader.Input;

public class Doc
{
    [XmlText]
    public string? Text { get; set; }
}
