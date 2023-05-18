using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class Function : GirModel.Function
{
    GirModel.Namespace GirModel.Function.Namespace => _repository.Namespace;
    GirModel.ComplexType? GirModel.Function.Parent => _parent;
    string GirModel.Callable.Name => Name;
    GirModel.ReturnType GirModel.Function.ReturnType => ReturnValue;
    string GirModel.Function.CIdentifier => Identifier;
    IEnumerable<GirModel.Parameter> GirModel.Callable.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
    GirModel.InstanceParameter? GirModel.Callable.InstanceParameter => null;
    bool GirModel.Function.Introspectable => Introspectable;
    string? GirModel.Function.Version => Version;
    GirModel.Callable? GirModel.Callable.Shadows => ShadowsReference?.GetResolvedCallable();
    GirModel.Callable? GirModel.Callable.ShadowedBy => ShadowedByReference?.GetResolvedCallable();
}
