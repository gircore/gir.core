using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Function : GirModel.Function
    {
        GirModel.Namespace GirModel.Function.Namespace => _repository.Namespace;
        string GirModel.Function.Name => Name;
        GirModel.ReturnType GirModel.Function.ReturnType => ReturnValue;
        string GirModel.Function.CIdentifier => Identifier;
        IEnumerable<GirModel.Parameter> GirModel.Function.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
    }
}
