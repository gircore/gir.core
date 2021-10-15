namespace GirLoader.Output
{

    public partial class ArrayTypeReference : GirModel.ArrayType
    {
        int? GirModel.ArrayType.Length => Length;
        bool GirModel.ArrayType.IsZeroTerminated => IsZeroTerminated;
        int? GirModel.ArrayType.FixedSize => FixedSize;
        GirModel.Type GirModel.ArrayType.Type => GetResolvedType();
    }
}
