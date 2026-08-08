using OneOf;

namespace GirLoader.Output;

public partial class Field : GirModel.Field
{
    string GirModel.Field.Name => Name;
    bool GirModel.Field.IsReadable => Readable;
    bool GirModel.Field.IsWritable => Writable;
    bool GirModel.Field.IsPrivate => Private;
    bool GirModel.Field.IsPointer => AnyTypeReference.CTypeReference?.IsPointer ?? false;
    bool GirModel.Field.Introspectable => Introspectable;

    OneOf<GirModel.AnyType, GirModel.Callback> GirModel.Field.AnyTypeOrCallback
    {
        get
        {
            if (Callback is not null)
                return Callback;

            return AnyTypeReference.Match(
                typeReference => GirModel.AnyType.From(typeReference.GetResolvedType()),
                arrayTypeReference => GirModel.AnyType.From(arrayTypeReference)
            );
        }
    }
}
