using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Class : GirModel.Class
    {
        GirModel.Class? GirModel.Class.Parent => (GirModel.Class?) Parent?.Type;
        GirModel.Namespace GirModel.ComplexType.Namespace => Repository.Namespace; 
        string GirModel.ComplexType.Name => OriginalName;
        GirModel.Function GirModel.Class.TypeFunction => GetTypeFunction;
        bool GirModel.Class.IsFundamental => IsFundamental;
        IEnumerable<GirModel.Field> GirModel.Class.Fields => Fields;
        IEnumerable<GirModel.Function> GirModel.Class.Functions => Functions;
        IEnumerable<GirModel.Method> GirModel.Class.Methods => Methods;
        IEnumerable<GirModel.Constructor> GirModel.Class.Constructors => Constructors;
        IEnumerable<GirModel.Property> GirModel.Class.Properties => Properties;
    }
}
