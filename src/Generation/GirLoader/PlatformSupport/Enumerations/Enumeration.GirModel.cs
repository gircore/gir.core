using System.Collections.Generic;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Enumeration : GirModel.Enumeration
{
    GirModel.Namespace ComplexType.Namespace => _enumeration.Namespace;
    string ComplexType.Name => _enumeration.Name;
    Method? GirModel.Enumeration.TypeFunction => _enumeration.TypeFunction;
    IEnumerable<Member> GirModel.Enumeration.Members => _enumeration.Members;
    bool GirModel.Enumeration.Introspectable => _enumeration.Introspectable;
}
