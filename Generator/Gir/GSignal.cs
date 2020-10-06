using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    public class GSignal
    {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("return-value")]
        public GReturnValue? ReturnValue { get; set; }

        [XmlElement("doc")]
        public GDoc? Doc { get; set; }

        [XmlElement("parameters")]
        public GParameters? Parameters { get; set; }
        
        // TODO: Instance Parameters?
    }
}