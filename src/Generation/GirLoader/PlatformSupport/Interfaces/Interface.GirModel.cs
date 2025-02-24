using System.Collections.Generic;

namespace GirLoader.PlatformSupport;

public partial class Interface : GirModel.Interface
{
    GirModel.Namespace GirModel.ComplexType.Namespace => _interface.Namespace;
    string GirModel.ComplexType.Name => _interface.Name;
    GirModel.Function GirModel.Interface.TypeFunction => _interface.TypeFunction;
    IEnumerable<GirModel.Constructor>? GirModel.Interface.Constructors => null; //TODO
    IEnumerable<GirModel.Function> GirModel.Interface.Functions => _interface.Functions;
    IEnumerable<GirModel.Method> GirModel.Interface.Methods => _interface.Methods;
    IEnumerable<GirModel.Property> GirModel.Interface.Properties => _interface.Properties;
    bool GirModel.Interface.Introspectable => _interface.Introspectable;
}
