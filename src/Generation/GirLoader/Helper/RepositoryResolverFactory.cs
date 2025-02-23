using System.Reflection;

namespace GirLoader;

public class RepositoryResolverFactory
{
    private readonly string _platform;
    private readonly string? _platformSearchPath;
    private readonly Assembly _assembly;

    /// <summary>
    /// Construct a new RepositoryResolverFactory
    /// </summary>
    /// <param name="platform">The platform to resolve repositories for</param>
    /// <param name="platformSearchPath">Directory containing repository files for this platform.
    /// If null, no repositories will be resolved for this platform.</param>
    /// <param name="assembly">Assembly containing embedded repository files.
    /// These are used as a fallback if no repository file is found in the search path.</param>
    public RepositoryResolverFactory(string platform, string? platformSearchPath, Assembly assembly)
    {
        _platform = platform;
        _platformSearchPath = platformSearchPath;
        _assembly = assembly;
    }

    public IRepositoryResolver Create()
    {
        if (_platformSearchPath == null)
        {
            return new NullRepositoryResolver();
        }

        var directoryResolver = new DirectoryRepositoryResolver(_platformSearchPath);
        var embeddedResolver = new EmbeddedRepositoryResolver(_assembly, _platform);

        return new ChainedRepositoryResolver(
            new IRepositoryResolver[] { directoryResolver, embeddedResolver });
    }
}
