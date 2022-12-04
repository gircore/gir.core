using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output
{
    public partial class Class : GirModel.Class
    {
        GirModel.Class? GirModel.Class.Parent => (GirModel.Class?) Parent?.Type;
        IEnumerable<GirModel.Interface> GirModel.Class.Implements => Implements.Select(x => x.GetResolvedType()).Cast<Interface>();
        GirModel.Function GirModel.Class.TypeFunction => GetTypeFunction;
        bool GirModel.Class.Fundamental => Fundamental;
        IEnumerable<GirModel.Field> GirModel.Class.Fields => Fields;
        IEnumerable<GirModel.Function> GirModel.Class.Functions => Functions;
        IEnumerable<GirModel.Method> GirModel.Class.Methods => Methods;
        IEnumerable<GirModel.Constructor> GirModel.Class.Constructors => Constructors;
        IEnumerable<GirModel.Property> GirModel.Class.Properties => Properties;
        IEnumerable<GirModel.Signal> GirModel.Class.Signals => Signals;
        bool GirModel.Class.Introspectable => Introspectable;
        bool GirModel.Class.Abstract => Abstract;
        bool GirModel.Class.Final => Final;
    }
}
