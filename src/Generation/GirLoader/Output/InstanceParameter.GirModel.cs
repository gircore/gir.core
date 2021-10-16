namespace GirLoader.Output
{
    public partial class InstanceParameter : GirModel.Parameter
    {
        string GirModel.Parameter.Name => Name;
        
        //Instance parameter never contain an array
        GirModel.AnyType GirModel.Parameter.AnyType => GirModel.AnyType.From(TypeReference.GetResolvedType());
        GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
        GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
    }
}
