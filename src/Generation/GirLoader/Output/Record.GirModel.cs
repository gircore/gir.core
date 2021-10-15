using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Record : GirModel.Record
    {
        string GirModel.ComplexType.NamespaceName => Repository.Namespace.Name;
        string GirModel.ComplexType.Name => Name;
        IEnumerable<GirModel.Method> GirModel.Record.Functions => _functions;
    }
}
