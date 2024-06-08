using System.Collections.Generic;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Record : GirModel.Record
{
    GirModel.Namespace ComplexType.Namespace => _record.Namespace;
    string ComplexType.Name => _record.Name;
    GirModel.Function? GirModel.Record.TypeFunction => _record.TypeFunction;
    GirModel.Method? GirModel.Record.CopyFunction => _record.CopyFunction;
    GirModel.Method? GirModel.Record.FreeFunction => _record.FreeFunction;
    IEnumerable<GirModel.Function> GirModel.Record.Functions => _record.Functions;
    IEnumerable<Method> GirModel.Record.Methods => _record.Methods;
    IEnumerable<Constructor> GirModel.Record.Constructors => _record.Constructors;
    IEnumerable<Field> GirModel.Record.Fields => _record.Fields;
    bool GirModel.Record.Introspectable => _record.Introspectable;
    bool GirModel.Record.Foreign => _record.Foreign;
    bool GirModel.Record.Opaque => _record.Opaque;
    bool GirModel.Record.Pointer => _record.Pointer;
}
