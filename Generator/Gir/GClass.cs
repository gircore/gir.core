using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gir
{
    public class GClass : GInterface
    {
        [XmlElement("constructor")] 
        public List<GMethod> Constructors { get; set; } = default!;

        [XmlElement ("signal", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
		public List<GSignal> Signals { get; set; } = default!;

        [XmlAttribute("parent")] 
        public string? Parent { get; set; }
        
        [XmlAttribute("abstract")]
        public bool Abstract { get; set; }
        
        [XmlAttribute("fundamental", Namespace="http://www.gtk.org/introspection/glib/1.0")]
        public bool Fundamental { get; set; }

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