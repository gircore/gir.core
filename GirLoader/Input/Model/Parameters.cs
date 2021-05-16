using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir.Input.Model
{
    public class Parameters
    {
        [XmlElement("instance-parameter")]
        public InstanceParameterInfo? InstanceParameter { get; set; }

        [XmlElement("parameter")]
        public List<Parameter> List { get; set; } = default!;
    }
}
