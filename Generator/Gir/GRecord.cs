using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gir
{
    public class GRecord : GClass
    {
        [XmlElement("function")]
        public List<GMethod> Functions { get; set; } = default!;
    }
}
