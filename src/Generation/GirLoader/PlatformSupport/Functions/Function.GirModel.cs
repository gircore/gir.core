using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public partial class Function : GirModel.Function
{
    GirModel.Namespace GirModel.Function.Namespace => _function.Namespace;
    GirModel.ComplexType? GirModel.Function.Parent => _function.Parent;
    string GirModel.Callable.Name => _function.Name;
    GirModel.ReturnType GirModel.Function.ReturnType => _function.ReturnType;
    string GirModel.Function.CIdentifier => _function.CIdentifier;
    IEnumerable<GirModel.Parameter> GirModel.Callable.Parameters => _function.Parameters;
    GirModel.InstanceParameter? GirModel.Callable.InstanceParameter => _function.InstanceParameter;
    bool GirModel.Callable.Throws => _function.Throws;
    bool GirModel.Function.Introspectable => _function.Introspectable;
    string? GirModel.Function.Version => _function.Version;
    GirModel.Callable? GirModel.Callable.Shadows => _function.Shadows;
    GirModel.Callable? GirModel.Callable.ShadowedBy => _function.ShadowedBy;
}
