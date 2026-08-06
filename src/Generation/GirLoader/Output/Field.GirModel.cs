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

    OneOf<GirModel.AnyTypeReference, GirModel.Callback> GirModel.Field.AnyTypeReferenceOrCallback
    {
        get
        {
            if (Callback is not null)
                return Callback;

            return AnyTypeReference.Match(GirModel.AnyTypeReference.From, GirModel.AnyTypeReference.From);
        }
    }
}
