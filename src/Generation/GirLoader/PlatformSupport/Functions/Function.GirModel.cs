using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public partial class Function : GirModel.Function
{
    GirModel.Namespace GirModel.Function.Namespace => _function.Namespace;
    GirModel.ComplexType? GirModel.Function.Parent => _function.Parent;
    string GirModel.Function.Name => _function.Name;
    GirModel.ReturnType GirModel.Function.ReturnType => _function.ReturnType;
    string GirModel.Function.CIdentifier => _function.CIdentifier;
    IEnumerable<GirModel.Parameter> GirModel.Function.Parameters => _function.Parameters;
    bool GirModel.Function.Introspectable => _function.Introspectable;
    string? GirModel.Function.Version => _function.Version;
}
