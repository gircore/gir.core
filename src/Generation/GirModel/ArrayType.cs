namespace GirModel
{
    public interface ArrayType : Type
    {
        public int? Length { get; }
        public bool IsZeroTerminated { get; }
        public int? FixedSize { get; }
        Type Type { get; }
    }
}
