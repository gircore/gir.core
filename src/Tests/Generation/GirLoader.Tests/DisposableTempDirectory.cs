using System;
using System.IO;

namespace GirLoader.Test;

public class DisposableTempDirectory : IDisposable
{
    public DisposableTempDirectory()
    {
        Path = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(), System.IO.Path.GetRandomFileName());
        Directory.CreateDirectory(Path);
    }

    public string Path { get; }

    public void Dispose()
    {
        Directory.Delete(Path, recursive: true);
    }
}
