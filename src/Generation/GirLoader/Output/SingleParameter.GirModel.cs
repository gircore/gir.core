namespace GirLoader.Output
{
    public partial class SingleParameter : GirModel.Parameter
    {
        string GirModel.Parameter.Name => Name;

        GirModel.AnyType GirModel.Parameter.AnyType => TypeReference switch
        {
            ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
            _ => GirModel.AnyType.From(TypeReference.GetResolvedType())
        };

        GirModel.Direction GirModel.Parameter.Direction => Direction.ToGirModel();
        GirModel.Transfer GirModel.Parameter.Transfer => Transfer.ToGirModel();
    }
}
