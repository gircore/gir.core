using System.Xml.Serialization;

namespace Generator.Introspection
{
    public class DocInfo
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
