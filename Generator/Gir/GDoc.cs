using System.Xml.Serialization;

namespace Gir
{
    public class GDoc
    {
        #region Properties

        [XmlText]
        public string? Text { get; set; }

        #endregion
    }
}
