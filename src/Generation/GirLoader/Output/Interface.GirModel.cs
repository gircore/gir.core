using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Interface : GirModel.Interface
    {
        GirModel.Namespace GirModel.ComplexType.Namespace => Repository.Namespace;
        string GirModel.ComplexType.Name => Name;
        GirModel.Method GirModel.Interface.TypeFunction => GetTypeFunction;
        IEnumerable<GirModel.Method> GirModel.Interface.Functions => Functions;
        IEnumerable<GirModel.Method> GirModel.Interface.Methods => Methods;
    }
}
