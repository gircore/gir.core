using System.Collections.Generic;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Callback : GirModel.Callback
{
    GirModel.Namespace ComplexType.Namespace => _callback.Namespace;
    string ComplexType.Name => _callback.Name;
    ReturnType GirModel.Callback.ReturnType => _callback.ReturnType;
    IEnumerable<Parameter> GirModel.Callback.Parameters => _callback.Parameters;
    bool GirModel.Callback.Introspectable => _callback.Introspectable;
}
