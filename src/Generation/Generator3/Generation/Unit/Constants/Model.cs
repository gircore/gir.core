using System;
using System.Collections.Generic;

namespace Generator3.Generation.Unit.Constants
{
    public class Model
    {
        private readonly HashSet<Generation.Model.Constant> _constants = new();

        public string NamespaceName { get; }
        public IEnumerable<Generation.Model.Constant> Constants => _constants;

        public Model(string namespaceName)
        {
            NamespaceName = namespaceName;
        }

        public void Add(Generation.Model.Constant constant)
        {
            if (!_constants.Add(constant))
                throw new Exception($"Constant {constant.Name}: Can not be added. It is already present.");
        }
    }
}
