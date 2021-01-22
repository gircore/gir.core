using System.Xml.Serialization;

#nullable enable

namespace Repository.Xml
{
    public class DocInfo
    {
        [XmlText]
        public string? Text { get; set; }
    }
}
