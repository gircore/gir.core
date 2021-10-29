namespace GirLoader.Output
{
    public partial class InstanceParameter : GirModel.Parameter
    {
        string GirModel.Parameter.Name => Name;

        bool GirModel.Parameter.IsPointer => TypeReference.CTypeReference?.IsPointer ?? false;
        bool GirModel.Parameter.IsConst => TypeReference.CTypeReference?.IsConst ?? false;
        bool GirModel.Parameter.IsVolatile => TypeReference.CTypeReference?.IsVolatile ?? false;
        
        //Instance parameter never contain an array
        GirModel.AnyType GirModel.Parameter.AnyType => GirModel.AnyType.From(TypeReference.GetResolvedType());
        GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
        GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
    }
}
