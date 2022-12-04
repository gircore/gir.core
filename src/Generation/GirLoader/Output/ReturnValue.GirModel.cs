namespace GirLoader.Output;

public partial class ReturnValue : GirModel.ReturnType
{
    GirModel.AnyType GirModel.ReturnType.AnyType => TypeReference switch
    {
        ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
        _ => GirModel.AnyType.From(TypeReference.GetResolvedType())
    };

    GirModel.Transfer GirModel.ReturnType.Transfer => Transfer.ToGirModel();

    bool GirModel.ReturnType.IsPointer => TypeReference.CTypeReference?.IsPointer ?? false;
}
