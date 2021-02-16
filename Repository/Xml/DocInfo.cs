using System.Xml.Serialization;

namespace Repository.Xml
{
    public class DocInfo
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
