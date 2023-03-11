using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class Constructor : GirModel.Constructor
{
    string GirModel.Callable.Name => Name;
    GirModel.ReturnType GirModel.Constructor.ReturnType => ReturnValue;
    string GirModel.Callable.CIdentifier => Identifier;
    string? GirModel.Constructor.Version => Version;
    IEnumerable<GirModel.Parameter> GirModel.Callable.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
    GirModel.InstanceParameter? GirModel.Callable.InstanceParameter => null;
    GirModel.Callable? GirModel.Callable.Shadows => ShadowsReference?.GetResolvedCallable();
    GirModel.Callable? GirModel.Callable.ShadowedBy => ShadowedByReference?.GetResolvedCallable();
}
