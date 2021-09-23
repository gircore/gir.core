using System;
using System.Collections.Generic;

namespace Generator3.Generation.Constants
{
    public class Data
    {
        private readonly HashSet<Constant> _constants = new();
        
        public string NamespaceName { get; }
        public IEnumerable<Constant> Constants => _constants;
        
        public Data(string namespaceName)
        {
            NamespaceName = namespaceName;
        }

        public void Add(Constant constant)
        {
            if (!_constants.Add(constant))
                throw new Exception($"Constant {constant.Name}: Can not be added. It is already present.");
        }
    }
}
