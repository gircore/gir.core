namespace GirLoader.Output;

public partial class Property : GirModel.Property
{
    string GirModel.Property.Name => Name;
    GirModel.AnyTypeReference GirModel.Property.AnyTypeReference => AnyTypeReference.Match(GirModel.AnyTypeReference.From, GirModel.AnyTypeReference.From);
    bool GirModel.Property.Readable => Readable;
    bool GirModel.Property.Writeable => Writeable;
    bool GirModel.Property.ConstructOnly => ConstructOnly;
    GirModel.Transfer GirModel.Property.Transfer => Transfer.ToGirModel();
    bool GirModel.Property.Introspectable => Introspectable;
    GirModel.Method? GirModel.Property.Getter => Getter?.GetMethod();
    GirModel.Method? GirModel.Property.Setter => Setter?.Method;
}
