namespace GirLoader.Output;

public partial class ReturnValue : GirModel.ReturnType
{
    GirModel.AnyType GirModel.ReturnType.AnyType => AnyTypeReference.Match(
        typeReference => GirModel.AnyType.From(typeReference.GetResolvedType()),
        arrayTypeReference => GirModel.AnyType.From(arrayTypeReference)
    );

    GirModel.Transfer GirModel.ReturnType.Transfer => Transfer.ToGirModel();

    bool GirModel.ReturnType.IsPointer => AnyTypeReference.CTypeReference?.IsPointer ?? false;
}
