using System.Collections.Generic;
using System.Linq;
using Generator3.Model.Internal;

namespace Generator3.Generation.Functions
{
    public class InternalModel
    {
        public IEnumerable<GirModel.Function> Functions { get; }

        public string NamespaceName => Functions.First().Namespace.GetInternalName();
        public IEnumerable<Function> InternalFunctions { get; }

        public InternalModel(IEnumerable<GirModel.Function> functions)
        {
            Functions = functions;

            InternalFunctions = functions
                .Select(function => new Function(function))
                .ToList();
        }
    }
}
