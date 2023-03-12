using System.Collections.Generic;

namespace GirLoader.Output;

public partial class Interface : GirModel.Interface
{
    GirModel.Function GirModel.Interface.TypeFunction => GetTypeFunction;
    IEnumerable<GirModel.Constructor>? GirModel.Interface.Constructors => null; //TODO: not implemented yet
    IEnumerable<GirModel.Function> GirModel.Interface.Functions => Functions;
    IEnumerable<GirModel.Method> GirModel.Interface.Methods => Methods;
    IEnumerable<GirModel.Property> GirModel.Interface.Properties => Properties;
    bool GirModel.Interface.Introspectable => Introspectable;
}
