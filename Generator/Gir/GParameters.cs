using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    public class GParameters
    {
        [XmlElement("instance-parameter")]
		public GParameter? InstanceParameter { get; set; }

		[XmlElement("parameter")]
		public List<GParameter> Parameters { get; set; } = default!;

		public int Count => InstanceParameter is {} ? 1 : 0 + Parameters?.Count ?? 0;
    }
}
