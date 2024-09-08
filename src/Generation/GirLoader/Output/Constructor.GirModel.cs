using System;
using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class Constructor : GirModel.Constructor
{
    GirModel.ComplexType GirModel.Constructor.Parent => _parent ?? throw new Exception($"{Identifier}: Unknown parent");
    string GirModel.Callable.Name => Name;
    GirModel.ReturnType GirModel.Callable.ReturnType => ReturnValue;
    string GirModel.Constructor.CIdentifier => Identifier;
    string? GirModel.Constructor.Version => Version;
    IEnumerable<GirModel.Parameter> GirModel.Callable.Parameters => ParameterList.GetParameters().Cast<GirModel.Parameter>();
    GirModel.InstanceParameter? GirModel.Callable.InstanceParameter => null;
    GirModel.Callable? GirModel.Callable.Shadows => ShadowsReference?.GetResolvedCallable();
    GirModel.Callable? GirModel.Callable.ShadowedBy => ShadowedByReference?.GetResolvedCallable();
}
