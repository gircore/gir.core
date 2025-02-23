using System;
using System.IO;

namespace GirLoader;

/// <summary>
/// Resolves repository files from a local directory
/// </summary>
public class DirectoryRepositoryResolver : IRepositoryResolver
{
    private readonly string _inputDirectory;

    public DirectoryRepositoryResolver(string inputDirectory)
    {
        _inputDirectory = inputDirectory;
    }

    public Input.Repository? ResolveRepository(string fileName)
    {
        var path = Path.Combine(_inputDirectory, fileName);
        if (File.Exists(path))
        {
            using var fileStream = new FileInfo(path).OpenRead();
            return fileStream.DeserializeGirInputModel();
        }
        else
        {
            return null;
        }
    }
}
