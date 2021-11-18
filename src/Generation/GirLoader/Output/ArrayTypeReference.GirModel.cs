namespace GirLoader.Output
{

    public partial class ArrayTypeReference : GirModel.ArrayType
    {
        private GirModel.AnyTypeReference? _typeReference;

        int? GirModel.ArrayType.Length => Length;
        bool GirModel.ArrayType.IsZeroTerminated => IsZeroTerminated;
        int? GirModel.ArrayType.FixedSize => FixedSize;
        GirModel.AnyTypeReference GirModel.ArrayType.AnyTypeReference => _typeReference ??= new AnyTypeReference(TypeReference);
    }
}
