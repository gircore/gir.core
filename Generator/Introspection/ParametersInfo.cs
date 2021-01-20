using System.Collections.Generic;
using System.Xml.Serialization;

namespace Generator.Introspection
{
    public class ParametersInfo
    {
        [XmlElement("instance-parameter")]
        public ParameterInfo? InstanceParameter { get; set; }

        [XmlElement("parameter")]
        public List<ParameterInfo> Parameters { get; set; } = default!;
    }
}
