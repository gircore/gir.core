using System.Collections.Generic;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Interface : GirModel.Interface
{
    GirModel.Namespace ComplexType.Namespace => _interface.Namespace;
    string ComplexType.Name => _interface.Name;
    GirModel.Function GirModel.Interface.TypeFunction => _interface.TypeFunction;
    IEnumerable<GirModel.Function> GirModel.Interface.Functions => _interface.Functions;
    IEnumerable<Method> GirModel.Interface.Methods => _interface.Methods;
    bool GirModel.Interface.Introspectable => _interface.Introspectable;
}
