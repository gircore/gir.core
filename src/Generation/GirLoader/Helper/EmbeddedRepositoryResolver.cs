using System;
using System.IO;
using System.Reflection;

namespace GirLoader;

public class EmbeddedRepositoryResolver : IRepositoryResolver
{
    private readonly Assembly _assembly;
    private readonly string _platformName;
    private readonly string _assemblyName;

    public EmbeddedRepositoryResolver(Assembly assembly, string platformName)
    {
        _assembly = assembly;
        _platformName = platformName;
        _assemblyName = _assembly.GetName().Name ?? throw new Exception("Could not get assembly name");
    }

    public Input.Repository? ResolveRepository(string fileName)
    {
        var resourceName = $"{_assemblyName}.{_platformName}.{fileName}";
        try
        {
            using var stream = _assembly.GetManifestResourceStream(resourceName);
            return stream?.DeserializeGirInputModel();
        }
        catch (FileNotFoundException)
        {
            return null;
        }
    }
}
