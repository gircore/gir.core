using System.Collections.Generic;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Class : GirModel.Class
{
    GirModel.Namespace ComplexType.Namespace => _class.Namespace;
    string ComplexType.Name => _class.Name;
    GirModel.Class? GirModel.Class.Parent => _class.Parent;
    IEnumerable<GirModel.Interface> GirModel.Class.Implements => _class.Implements;
    GirModel.Function GirModel.Class.TypeFunction => _class.TypeFunction;
    bool GirModel.Class.Fundamental => _class.Fundamental;
    IEnumerable<Field> GirModel.Class.Fields => _class.Fields;
    IEnumerable<GirModel.Function> GirModel.Class.Functions => _class.Functions;
    IEnumerable<Method> GirModel.Class.Methods => _class.Methods;
    IEnumerable<Constructor> GirModel.Class.Constructors => _class.Constructors;
    IEnumerable<Property> GirModel.Class.Properties => _class.Properties;
    IEnumerable<Signal> GirModel.Class.Signals => _class.Signals;
    IEnumerable<GirModel.Callback> GirModel.Class.Callbacks => _class.Callbacks;
    bool GirModel.Class.Introspectable => _class.Introspectable;
    bool GirModel.Class.Abstract => _class.Abstract;
    bool GirModel.Class.Final => _class.Final;
}
