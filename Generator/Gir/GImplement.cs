using System.Xml.Serialization;

namespace Gir
{
    public class GImplement
    {
        #region Properties

        [XmlAttribute("name")]
        public string? Name { get; set; }

        #endregion

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ? Name : base.ToString();
        }
    }
}
