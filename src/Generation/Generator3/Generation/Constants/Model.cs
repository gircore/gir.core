using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Public;

namespace Generator3.Generation.Constants
{
    public class Model
    {
        private readonly IEnumerable<GirModel.Constant> _constants;

        public string NamespaceName => _constants.First().Namespace.Name;
        public IEnumerable<Constant> Constants { get; }

        public Model(IEnumerable<GirModel.Constant> constants)
        {
            _constants = constants;

            Constants = constants
                .Select(constant => new Constant(constant))
                .ToList();
        }

    }
}
