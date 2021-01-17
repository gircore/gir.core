using System.Xml.Serialization;

namespace Introspection
{
    public class GDoc
    {
        #region Properties

        [XmlText]
        public string? Text { get; set; }

        #endregion
    }
}
