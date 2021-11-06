using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Record : GirModel.Record
    {
        GirModel.Namespace GirModel.ComplexType.Namespace => Repository.Namespace;
        string GirModel.ComplexType.Name => Name;
        GirModel.Method? GirModel.Record.TypeFunction => GetTypeFunction;
        IEnumerable<GirModel.Method> GirModel.Record.Functions => Functions;
        IEnumerable<GirModel.Method> GirModel.Record.Methods => Methods;
        IEnumerable<GirModel.Method> GirModel.Record.Constructors => Constructors;
        IEnumerable<GirModel.Field> GirModel.Record.Fields => Fields;
    }
}
