using System.Collections.Generic;

namespace GirLoader.Output;

public partial class Union : GirModel.Union
{
    GirModel.Function? GirModel.Union.TypeFunction => GetTypeFunction;
    GirModel.Method? GirModel.Union.CopyFunction => CopyFunction;
    GirModel.Method? GirModel.Union.FreeFunction => FreeFunction;
    IEnumerable<GirModel.Function> GirModel.Union.Functions => Functions;
    IEnumerable<GirModel.Method> GirModel.Union.Methods => Methods;
    IEnumerable<GirModel.Constructor> GirModel.Union.Constructors => Constructors;
    IEnumerable<GirModel.Field> GirModel.Union.Fields => Fields;
    bool GirModel.Union.Introspectable => Introspectable;
}
