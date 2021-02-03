using System.Xml.Serialization;

namespace Repository.Xml
{
    public class FieldInfo
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("readable")]
        public bool Readable { get; set; }

        [XmlAttribute("writeable")]
        public bool Writeable { get; set; }

        [XmlAttribute("private")]
        public bool Private { get; set; }

        [XmlAttribute("bits")]
        public int Bits { get; set; }

        [XmlElement("callback")]
        public CallbackInfo? Callback { get; set; }

        [XmlElement("type")]
        public TypeInfo? Type { get; set; }

        [XmlElement("array")]
        public ArrayInfo? Array { get; set; }

        [XmlElement("doc")]
        public DocInfo? Doc { get; set; }
    }
}
