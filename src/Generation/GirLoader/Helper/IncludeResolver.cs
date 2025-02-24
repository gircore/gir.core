namespace GirLoader;

public class IncludeResolver
{
    private readonly IRepositoryResolver _repositoryResolver;

    public IncludeResolver(IRepositoryResolver repositoryResolver)
    {
        _repositoryResolver = repositoryResolver;
    }

    public Input.Repository? ResolveInclude(Output.Include include)
    {
        var fileName = $"{include.Name}-{include.Version}.gir";

        return _repositoryResolver.ResolveRepository(fileName);
    }
}
