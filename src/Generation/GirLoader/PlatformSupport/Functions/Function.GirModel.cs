using System.Collections.Generic;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Function : GirModel.Function
{
    GirModel.Namespace GirModel.Function.Namespace => _function.Namespace;
    string GirModel.Function.Name => _function.Name;
    ReturnType GirModel.Function.ReturnType => _function.ReturnType;
    string GirModel.Function.CIdentifier => _function.CIdentifier;
    IEnumerable<Parameter> GirModel.Function.Parameters => _function.Parameters;
    bool GirModel.Function.Introspectable => _function.Introspectable;
}
