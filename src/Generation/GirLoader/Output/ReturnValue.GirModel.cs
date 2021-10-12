namespace GirLoader.Output
{
    public partial class ReturnValue : GirModel.ReturnType
    {
        GirModel.Type GirModel.ReturnType.Type => TypeReference.GetResolvedType();
        GirModel.Transfer GirModel.ReturnType.Transfer => Transfer.ToGirModel();
    }
}
