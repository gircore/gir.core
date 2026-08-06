using System.Collections.Generic;
using System.Linq;

namespace GirLoader.Output;

public partial class TypeReference : GirModel.TypeReference
{
    GirModel.Type GirModel.TypeReference.Type => GetResolvedType();

    IReadOnlyList<GirModel.AnyTypeReference> GirModel.TypeReference.ElementTypes => ElementTypeReferences
        .Select(x => x.Match(GirModel.AnyTypeReference.From, GirModel.AnyTypeReference.From))
        .ToList();
}
