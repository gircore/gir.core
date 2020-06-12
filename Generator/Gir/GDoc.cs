using System.Xml.Serialization;

namespace Gir
{
    public class GDoc
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
