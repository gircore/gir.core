namespace GirLoader.Output
{
    public partial class ReturnValue : GirModel.ReturnValue
    {
        GirModel.Type GirModel.ReturnValue.Type => TypeReference.GetResolvedType();
        GirModel.Transfer GirModel.ReturnValue.Transfer => Transfer.ToGirModel();
    }
}
