using System;
using System.Collections.Generic;

namespace GirLoader.Output;

public partial class Enumeration : GirModel.Enumeration
{
    GirModel.Method GirModel.Enumeration.TypeFunction => throw new NotImplementedException();
    IEnumerable<GirModel.Member> GirModel.Enumeration.Members => Members;
    bool GirModel.Enumeration.Introspectable => Introspectable;
}
