using System.Collections.Generic;
using System.Linq;
using Generator3.Converter;
using Generator3.Model.Public;

namespace Generator3.Generation.Class.Standard
{
    public class PublicConstructorsModel
    {
        private readonly GirModel.Class _class;

        public string Name => _class.Name;
        public string NamespaceName => _class.Namespace.GetPublicName();

        public IEnumerable<Constructor> Constructors { get; }

        public PublicConstructorsModel(GirModel.Class @class)
        {
            _class = @class;
            Constructors = _class.Constructors.Select(x => new Constructor(x, _class));
        }
    }
}
