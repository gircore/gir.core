using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class ConstantsUnit
    {
        private readonly IEnumerable<GirModel.Constant> _constants;

        public string NamespaceName => _constants.First().NamespaceName;
        public IEnumerable<Constant> Constants { get; }

        public ConstantsUnit(IEnumerable<GirModel.Constant> constants)
        {
            _constants = constants;

            Constants = constants
                .Select(constant => new Constant(constant))
                .ToList();
        }
    
    }
}
