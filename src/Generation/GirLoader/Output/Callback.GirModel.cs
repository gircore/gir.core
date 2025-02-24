using System.Collections.Generic;
using System.Linq;
using GirModel;

namespace GirLoader.Output;

public partial class Callback : GirModel.Callback
{
    ReturnType GirModel.Callable.ReturnType => ReturnValue;
    bool GirModel.Callable.Throws => Throws;
    IEnumerable<GirModel.Parameter> GirModel.Callable.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
    GirModel.InstanceParameter? GirModel.Callable.InstanceParameter => null;
    GirModel.Callable? GirModel.Callable.Shadows => null;
    GirModel.Callable? GirModel.Callable.ShadowedBy => null;
    bool GirModel.Callback.Introspectable => Introspectable;
}
