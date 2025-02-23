namespace GirLoader;

public class NullRepositoryResolver : IRepositoryResolver
{
    public Input.Repository? ResolveRepository(string fileName)
    {
        return null;
    }
}
