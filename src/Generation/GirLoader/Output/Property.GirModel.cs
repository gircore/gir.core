namespace GirLoader.Output;

public partial class Property : GirModel.Property
{
    string GirModel.Property.Name => Name;
    GirModel.AnyType GirModel.Property.AnyType => AnyTypeReference.Match(
        typeReference => GirModel.AnyType.From(typeReference.GetResolvedType()),
        arrayTypeReference => GirModel.AnyType.From(arrayTypeReference)
    );
    bool GirModel.Property.Readable => Readable;
    bool GirModel.Property.Writeable => Writeable;
    bool GirModel.Property.ConstructOnly => ConstructOnly;
    GirModel.Transfer GirModel.Property.Transfer => Transfer.ToGirModel();
    bool GirModel.Property.Introspectable => Introspectable;
    GirModel.Method? GirModel.Property.Getter => Getter?.GetMethod();
    GirModel.Method? GirModel.Property.Setter => Setter?.Method;
}
