using System.Xml.Serialization;

namespace Gir.Input.Model
{
    public class Doc
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
