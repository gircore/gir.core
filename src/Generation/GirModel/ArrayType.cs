namespace GirModel
{
    public interface ArrayType
    {
        public int? Length { get; }
        public bool IsZeroTerminated { get; }
        public int? FixedSize { get; }
        bool IsPointer { get; }
        bool IsConst { get; }
        bool IsVolatile { get; }
        AnyType AnyType { get; }
    }
}
