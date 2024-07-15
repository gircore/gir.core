namespace GirLoader;

/// <summary>
/// Resolves input repository definitions from GIR file names
/// </summary>
public interface IRepositoryResolver
{
    Input.Repository? ResolveRepository(string fileName);
}
