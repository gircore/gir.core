using System.Collections.Generic;
using GirModel;

namespace GirLoader.PlatformSupport;

public partial class Bitfield : GirModel.Bitfield
{
    GirModel.Namespace ComplexType.Namespace => _bitfield.Namespace;
    string ComplexType.Name => _bitfield.Name;
    Method? GirModel.Bitfield.TypeFunction => _bitfield.TypeFunction;
    IEnumerable<Member> GirModel.Bitfield.Members => _bitfield.Members;
    bool GirModel.Bitfield.Introspectable => _bitfield.Introspectable;
}
