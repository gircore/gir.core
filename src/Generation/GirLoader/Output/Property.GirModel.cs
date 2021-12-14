namespace GirLoader.Output
{
    public partial class Property : GirModel.Property
    {
        string GirModel.Property.Name => Name;
        GirModel.AnyType GirModel.Property.AnyType => TypeReference switch
        {
            ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
            _ => GirModel.AnyType.From(TypeReference.GetResolvedType())
        };
        GirModel.Transfer GirModel.Property.Transfer => Transfer.ToGirModel();
    }
}
