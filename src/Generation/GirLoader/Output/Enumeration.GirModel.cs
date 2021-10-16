using System;
using System.Collections.Generic;

namespace GirLoader.Output
{
    public partial class Enumeration : GirModel.Enumeration
    {
        string GirModel.ComplexType.NamespaceName => Repository.Namespace.Name; 
        string GirModel.ComplexType.Name => OriginalName;
        GirModel.Method GirModel.Enumeration.TypeFunction => throw new NotImplementedException();
        IEnumerable<GirModel.Member> GirModel.Enumeration.Members => Members;
    }
}
