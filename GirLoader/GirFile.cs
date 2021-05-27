using System;
using System.IO;

namespace GirLoader
{
    public record GirFile
    {
        public string Path { get; }

        public GirFile(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException($"{path} was not found");

            if (!path.EndsWith(".gir"))
                throw new ArgumentException($"{path} is not ending with '.gir'");

            Path = new FileInfo(path).FullName;
        }
    }
}
