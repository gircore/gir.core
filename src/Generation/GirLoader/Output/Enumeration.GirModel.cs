using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Enumeration : GirModel.Enumeration
    {
        public string NamespaceName => Repository.Namespace.Name.Value; 
        public string Name => OriginalName.Value;
        GirModel.Method GirModel.Enumeration.TypeFunction => null; //TODO: Should be implemented
        IEnumerable<GirModel.Member> GirModel.Enumeration.Members => Members;
    }
}
