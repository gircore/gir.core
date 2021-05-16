using System.Xml.Serialization;

namespace GirLoader.Input.Model
{
    public class Doc
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
