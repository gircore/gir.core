namespace GirLoader;

public class IncludeResolver(IRepositoryResolver repositoryResolver)
{
    public Input.Repository? ResolveInclude(Output.Include include)
    {
        if (include.Name == "win32") //Win32 API is not a gir file
            return Win32Repository.Get();

        var fileName = $"{include.Name}-{include.Version}.gir";

        return repositoryResolver.ResolveRepository(fileName);
    }
}
