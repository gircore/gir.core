namespace GirLoader.Output
{
    public partial class SingleParameter : GirModel.Parameter
    {
        string GirModel.Parameter.Name => Name;
        int? GirModel.Parameter.Closure => ClosureIndex;
        int? GirModel.Parameter.Destroy => DestroyIndex;
        bool GirModel.Parameter.IsPointer => TypeReference.CTypeReference?.IsPointer ?? false;
        bool GirModel.Parameter.IsConst => TypeReference.CTypeReference?.IsConst ?? false;
        bool GirModel.Parameter.IsVolatile => TypeReference.CTypeReference?.IsVolatile ?? false;
        GirModel.AnyType GirModel.Parameter.AnyType => TypeReference switch
        {
            ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
            _ => GirModel.AnyType.From(TypeReference.GetResolvedType())
        };
        GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
        GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
        GirModel.Scope? GirModel.Parameter.Scope => CallbackScope.ToGirModel();
    }
}
