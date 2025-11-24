using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class Interface : GirModel.Interface
{
    GirModel.Function GirModel.Interface.TypeFunction => GetTypeFunction;
    IEnumerable<GirModel.ComplexType> GirModel.Interface.Prerequisites => Prerequisites.Select(x => x.GetResolvedType()).Cast<GirModel.ComplexType>();
    IEnumerable<GirModel.Constructor>? GirModel.Interface.Constructors => null; //TODO: not implemented yet
    IEnumerable<GirModel.Function> GirModel.Interface.Functions => Functions;
    IEnumerable<GirModel.Method> GirModel.Interface.Methods => Methods;
    IEnumerable<GirModel.Property> GirModel.Interface.Properties => Properties;
    IEnumerable<GirModel.Signal> GirModel.Interface.Signals => Signals;
    bool GirModel.Interface.Introspectable => Introspectable;
}
