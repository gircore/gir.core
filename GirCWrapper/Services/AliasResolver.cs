namespace Gir
{
    public interface AliasResolver
    {
        bool TryGet(string alias, out string type);
    }
}
