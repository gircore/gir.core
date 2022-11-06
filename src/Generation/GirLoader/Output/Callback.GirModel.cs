using System.Collections.Generic;
using System.Linq;
using GirModel;

namespace GirLoader.Output
{
    public partial class Callback : GirModel.Callback
    {
        ReturnType GirModel.Callback.ReturnType => ReturnValue;
        IEnumerable<GirModel.Parameter> GirModel.Callback.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
        bool GirModel.Callback.Introspectable => Introspectable;
    }
}
