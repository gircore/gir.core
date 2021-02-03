using System.Xml.Serialization;

namespace Repository.Xml
{
    public class RecordInfo : ClassInfo
    {
        [XmlAttribute("is-gtype-struct-for", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public string? GLibIsGTypeStructFor;

        [XmlAttribute("disguised")]
        public bool Disguised;

        [XmlAttribute("introspectable")]
        public bool Introspectable = true;
    }
}
