namespace GirModel
{
    public interface AnyTypeReference
    {
        bool IsPointer { get; }
        bool IsConst { get; }
        bool IsVolatile { get; }
        
        AnyType AnyType { get; }
    }
}
