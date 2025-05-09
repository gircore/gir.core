namespace GirLoader.Output;

public partial class Property : GirModel.Property
{
    string GirModel.Property.Name => Name;
    GirModel.AnyType GirModel.Property.AnyType => TypeReference switch
    {
        ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
        _ => GirModel.AnyType.From(TypeReference.GetResolvedType())
    };
    bool GirModel.Property.Readable => Readable;
    bool GirModel.Property.Writeable => Writeable;
    bool GirModel.Property.ConstructOnly => ConstructOnly;
    GirModel.Transfer GirModel.Property.Transfer => Transfer.ToGirModel();
    bool GirModel.Property.Introspectable => Introspectable;
    GirModel.Method? GirModel.Property.Getter => Getter?.GetMethod();
    GirModel.Method? GirModel.Property.Setter => Setter?.Method;
}
