using OneOf;

namespace GirLoader.Output;

public partial class Field : GirModel.Field
{
    string GirModel.Field.Name => Name;
    bool GirModel.Field.IsReadable => Readable;
    bool GirModel.Field.IsWritable => Writable;
    bool GirModel.Field.IsPrivate => Private;
    bool GirModel.Field.IsPointer => TypeReference.CTypeReference?.IsPointer ?? false;
    bool GirModel.Field.Introspectable => Introspectable;

    OneOf<GirModel.AnyType, GirModel.Callback> GirModel.Field.AnyTypeOrCallback
    {
        get
        {
            if (Callback is not null)
                return Callback;

            return TypeReference.GetResolvedAnyType();
        }
    }

    System.Collections.Generic.IReadOnlyList<GirModel.AnyType> GirModel.ElementTypeContainer.ElementTypes => TypeReference.GetResolvedElementTypes();
}
