using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

public sealed class Compiler(string csprojPath) : IDisposable
{
    private MSBuildWorkspace? _workspace;

    public async Task<Project> GetProjectAsync()
    {
        var path = Path.GetFullPath(csprojPath);

        if (!File.Exists(path))
            throw new FileNotFoundException("Project file not found.", path);

        _workspace = MSBuildWorkspace.Create();

        return await _workspace.OpenProjectAsync(path);
    }

    public void Dispose()
    {
        _workspace?.Dispose();
    }
}
