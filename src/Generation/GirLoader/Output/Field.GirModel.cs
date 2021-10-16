using OneOf;

namespace GirLoader.Output
{
    public partial class Field : GirModel.Field
    {
        string GirModel.Field.Name => Name;
        bool GirModel.Field.Readable => Readable;
        bool GirModel.Field.Private => Private;

        OneOf<GirModel.AnyType, GirModel.Callback> GirModel.Field.AnyTypeOrCallback
        {
            get
            {
                if (Callback is not null)
                    return Callback;

                return TypeReference switch
                {
                    ArrayTypeReference arrayTypeReference => GirModel.AnyType.From(arrayTypeReference),
                    _ => GirModel.AnyType.From(TypeReference.GetResolvedType())
                };
            }
        }
    }

}
