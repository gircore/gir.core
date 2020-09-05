using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    public class GRecord : GClass
    {
        [XmlElement("function")]
        public List<GMethod> Functions { get; set; } = default!;

        [XmlElement("field")] 
        public List<GField> Fields { get; set; } = default!;

        public override IEnumerable<GMethod> AllMethods
        {
            get
            {
                foreach (var method in base.AllMethods)
                    yield return method;

                foreach (var function in Functions)
                {
                    //Do not make functions available which were moved to another part of the ABI
                    if(!string.IsNullOrEmpty(function.MovedTo))
                        if(!function.HasVariadicParameter())
                            yield return function;
                }
            }
        }
    }
}
