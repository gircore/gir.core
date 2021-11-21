using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Internal;

namespace Generator3.Generation.Class.Fundamental
{
    public class PublicMethodsModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.Name;

        public PublicMethodsModel(GirModel.Class @class)
        {
            _class = @class;
        }
    }
}
