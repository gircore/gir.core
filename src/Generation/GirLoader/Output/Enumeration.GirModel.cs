using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Enumeration : GirModel.Enumeration
    {
        public string NamespaceName => Repository.Namespace.Name.Value; 
        public string Name => OriginalName.Value;

        IEnumerable<GirModel.Member> GirModel.Enumeration.Members => Members;
    }
}
