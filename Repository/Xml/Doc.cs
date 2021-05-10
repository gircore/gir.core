using System.Xml.Serialization;

namespace Repository.Xml
{
    public class Doc
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
