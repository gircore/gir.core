using System.Collections.Generic;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Union : GirModel.Union
{
    GirModel.Namespace ComplexType.Namespace => _union.Namespace;
    string ComplexType.Name => _union.Name;
    GirModel.Function? GirModel.Union.TypeFunction => _union.TypeFunction;
    GirModel.Method? GirModel.Union.CopyFunction => _union.CopyFunction;
    GirModel.Method? GirModel.Union.FreeFunction => _union.FreeFunction;
    IEnumerable<GirModel.Function> GirModel.Union.Functions => _union.Functions;
    IEnumerable<Method> GirModel.Union.Methods => _union.Methods;
    IEnumerable<Constructor> GirModel.Union.Constructors => _union.Constructors;
    IEnumerable<Field> GirModel.Union.Fields => _union.Fields;
    bool GirModel.Union.Introspectable => _union.Introspectable;
}
