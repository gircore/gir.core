using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class TypeReference : GirModel.TypeReference
{
    GirModel.Type GirModel.TypeReference.Type => GetResolvedType();

    IReadOnlyList<GirModel.AnyType> GirModel.TypeReference.ElementTypes => ElementTypeReferences
        .Select(x => x.Match(GirModel.AnyType.From, GirModel.AnyType.From))
        .ToList();
}