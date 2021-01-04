using System.Xml.Serialization;

namespace Gir
{
    public class GVirtualMethod : GMethod
    {
        #region Properties

        [XmlAttribute("invoker")]
        public string? Invoker { get; set; }

        public bool HasInvoker => Invoker is not null;

        #endregion
    }
}
