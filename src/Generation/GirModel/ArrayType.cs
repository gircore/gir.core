namespace GirModel
{
    public interface ArrayType
    {
        public int? Length { get; }
        public bool IsZeroTerminated { get; }
        public int? FixedSize { get; }
        Type Type { get; }
    }
}
