using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Interface : GirModel.Interface
    {
        GirModel.Namespace GirModel.ComplexType.Namespace => Repository.Namespace;
        string GirModel.ComplexType.Name => OriginalName;
        GirModel.Function GirModel.Interface.TypeFunction => GetTypeFunction;
        IEnumerable<GirModel.Function> GirModel.Interface.Functions => Functions;
        IEnumerable<GirModel.Method> GirModel.Interface.Methods => Methods;
    }
}
