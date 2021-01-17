using System.Collections.Generic;
using System.Xml.Serialization;

namespace Generator.Introspection
{
    public class GParameters
    {
        #region Properties

        [XmlElement("instance-parameter")]
        public GParameter? InstanceParameter { get; set; }

        [XmlElement("parameter")]
        public List<GParameter> Parameters { get; set; } = default!;

        public IEnumerable<GParameter> AllParameters
        {
            get
            {
                if (InstanceParameter is { })
                    yield return InstanceParameter;

                foreach (GParameter? parameter in Parameters)
                    yield return parameter;
            }
        }

        public int Count => (InstanceParameter is { } ? 1 : 0) + (Parameters?.Count ?? 0);

        #endregion
    }
}
