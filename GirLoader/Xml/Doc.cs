using System.Xml.Serialization;

namespace Gir.Xml
{
    public class Doc
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
