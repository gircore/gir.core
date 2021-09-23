namespace GirLoader.Output
{
    public partial class SingleParameter : GirModel.Parameter
    {
        string GirModel.Parameter.Name => Name;
        GirModel.Type GirModel.Parameter.Type => TypeReference.Type;
        GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
        GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
    }
}
