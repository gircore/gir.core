using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    public class GClass : GInterface
    {
        [XmlElement("constructor")] public List<GMethod> Constructors { get; set; } = default!;

        [XmlAttribute("parent")] public string? Parent { get; set; }

        public override IEnumerable<GMethod> AllMethods
        {
            get
            {
                foreach (var method in base.AllMethods)
                    yield return method;

                foreach (var method in Constructors)
                    if(!method.HasVariadicParameter())
                        yield return method;
            }
        }
    }
}