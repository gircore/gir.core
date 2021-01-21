using System.Xml.Serialization;

namespace Repository.Xml
{
    internal class DocInfo
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
