using System.IO;

namespace GirLoader;

public class IncludeResolver
{
    private readonly string _inputDirectory;

    public IncludeResolver(string inputDirectory)
    {
        _inputDirectory = inputDirectory;
    }

    public Input.Repository? ResolveInclude(Output.Include include)
    {
        var fileName = $"{include.Name}-{include.Version}.gir";

        var path = Path.Combine(_inputDirectory, fileName);
        return File.Exists(path)
            ? new FileInfo(path).OpenRead().DeserializeGirInputModel()
            : null;
    }
}
