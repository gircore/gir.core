namespace GirLoader.Output;

public partial class ReturnValue : GirModel.ReturnType
{
    GirModel.AnyTypeReference GirModel.ReturnType.AnyTypeReference => AnyTypeReference.Match(
        GirModel.AnyTypeReference.From,
        GirModel.AnyTypeReference.From
    );

    GirModel.Transfer GirModel.ReturnType.Transfer => Transfer.ToGirModel();

    bool GirModel.ReturnType.IsPointer => AnyTypeReference.CTypeReference?.IsPointer ?? false;
}
