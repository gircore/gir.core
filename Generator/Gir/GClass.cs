using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    public class GClass : GInterface
    {
        [XmlElement("constructor")]
        public List<GMethod> Constructors { get; set; } = default!;
    }
}
