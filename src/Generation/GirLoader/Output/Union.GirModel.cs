using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Union : GirModel.Union
    {
        GirModel.Namespace GirModel.ComplexType.Namespace => Repository.Namespace;
        string GirModel.ComplexType.Name => Name;
        GirModel.Function? GirModel.Union.TypeFunction => GetTypeFunction;
        IEnumerable<GirModel.Function> GirModel.Union.Functions => Functions;
        IEnumerable<GirModel.Method> GirModel.Union.Methods => Methods;
        IEnumerable<GirModel.Constructor> GirModel.Union.Constructors => Constructors;
        IEnumerable<GirModel.Field> GirModel.Union.Fields => Fields;
    }
}
