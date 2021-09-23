namespace GirModel
{
    public interface ArrayType : Type
    {
        public int? Length { get; init; }
        public bool IsZeroTerminated { get; init; }
        public int? FixedSize { get; init; }
        Type Type { get; }
    }
}
