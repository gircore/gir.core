using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Public;

namespace Generator3.Generation.Class.Standard
{
    public class PublicPropertiesModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.Name;
        
        public IEnumerable<Property> Properties { get; }

        public PublicPropertiesModel(GirModel.Class @class)
        {
            _class = @class;
            Properties = _class.Properties.Select(x => new Property(x, Name));
        }
    }
}
