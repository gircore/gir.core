using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Gir
{
    public class GClass : GInterface
    {
        #region Properties

        [XmlElement("function")]
        public List<GMethod> Functions { get; set; } = default!;

        [XmlElement("constructor")]
        public List<GMethod> Constructors { get; set; } = default!;

        [XmlElement("signal", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public List<GSignal> Signals { get; set; } = default!;

        [XmlAttribute("parent")]
        public string? Parent { get; set; }

        [XmlAttribute("abstract")]
        public bool Abstract { get; set; }

        [XmlAttribute("fundamental", Namespace = "http://www.gtk.org/introspection/glib/1.0")]
        public bool Fundamental { get; set; }

        [XmlElement("implements")]
        public List<GImplement> Implements { get; set; } = default!;

        //TODO: Workaround as long as no ATK is supported
        public List<GImplement> ImplementsWithoutAtk 
            => Implements.Where(x => x.Name is {} && !x.Name.Contains("Atk")).ToList();
        
        public override IEnumerable<GMethod> AllMethods
        {
            get
            {
                foreach (GMethod? method in base.AllMethods)
                    yield return method;

                foreach (GMethod? method in Constructors)
                {
                    if (!method.HasVariadicParameter())
                        yield return method;
                }

                foreach (GMethod? function in Functions)
                {
                    // Do not make functions available which were moved to another part of the ABI
                    if (string.IsNullOrEmpty(function.MovedTo) && !function.HasVariadicParameter())
                        yield return function;
                }
            }
        }

        public bool SignalsHaveEventArgs => Signals.Any(x => !x.NeedsSignalArgs);

        #endregion
    }
}
