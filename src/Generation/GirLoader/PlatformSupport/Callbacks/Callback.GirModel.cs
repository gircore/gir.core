using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public partial class Callback : GirModel.Callback
{
    GirModel.Namespace GirModel.ComplexType.Namespace => _callback.Namespace;
    string GirModel.Callback.Name => _callback.Name;
    string GirModel.ComplexType.Name => _callback.Name;
    string GirModel.Callable.Name => _callback.Name;
    bool GirModel.Callable.Throws => _callback.Throws;
    GirModel.ReturnType GirModel.Callback.ReturnType => _callback.ReturnType;
    IEnumerable<GirModel.Parameter> GirModel.Callable.Parameters => _callback.Parameters;
    bool GirModel.Callback.Introspectable => _callback.Introspectable;
    GirModel.InstanceParameter? GirModel.Callable.InstanceParameter => null;
    GirModel.Callable? GirModel.Callable.Shadows => null;
    GirModel.Callable? GirModel.Callable.ShadowedBy => null;
}
