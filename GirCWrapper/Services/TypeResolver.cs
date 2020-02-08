namespace Gir
{
    public interface TypeResolver
    {
        string GetType(IType type, bool isParameter);
    }
}