using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Method : GirModel.Method
    {
        //TODO: This is only relevant for functions, not for method. Currently this class is used for methods + functions at the same time.
        GirModel.Namespace GirModel.Method.Namespace => _repository.Namespace;
        string GirModel.Method.Name => Name;
        GirModel.ReturnType GirModel.Method.ReturnType => ReturnValue;
        string GirModel.Method.CIdentifier => OriginalName;
        IEnumerable<GirModel.Parameter> GirModel.Method.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
    }
}
