using System.Collections.Generic;

namespace GirLoader.Output;

public partial class ReturnValue : GirModel.ReturnType
{
    GirModel.AnyType GirModel.ReturnType.AnyType => TypeReference.GetResolvedAnyType();

    IReadOnlyList<GirModel.AnyType> GirModel.ElementTypeContainer.ElementTypes => TypeReference.GetResolvedElementTypes();

    GirModel.Transfer GirModel.ReturnType.Transfer => Transfer.ToGirModel();

    bool GirModel.ReturnType.IsPointer => TypeReference.CTypeReference?.IsPointer ?? false;
}
