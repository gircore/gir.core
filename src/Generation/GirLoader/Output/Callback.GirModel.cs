using System.Collections.Generic;
using System.Linq;
using GirModel;

namespace GirLoader.Output
{
    public partial class Callback : GirModel.Callback
    {
        string GirModel.ComplexType.NamespaceName => Repository.Namespace.Name;
        string GirModel.ComplexType.Name => Name;
        ReturnType GirModel.Callback.ReturnType => ReturnValue;
        IEnumerable<GirModel.Parameter> GirModel.Callback.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
    }
}
