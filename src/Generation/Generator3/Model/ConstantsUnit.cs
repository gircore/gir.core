using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator3.Model
{
    public class ConstantsUnit
    {
        private readonly IEnumerable<GirModel.Constant> _constants;
        private readonly HashSet<Model.Constant> _modelConstants = new();
        
        public string NamespaceName => _constants.First().NamespaceName;
        public IEnumerable<Model.Constant> Constants => _modelConstants;

        public ConstantsUnit(IEnumerable<GirModel.Constant> constants)
        {
            _constants = constants;
            
            foreach (var constant in constants)
                Add(new Constant(constant));
        }

        private void Add(Constant constant)
        {
            if (!_modelConstants.Add(constant))
                throw new Exception($"Constant {constant.Name}: Can not be added. It is already present.");
        }
    }
}
