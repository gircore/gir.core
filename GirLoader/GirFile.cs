using System;

namespace GirLoader
{
    public record GirFile
    {
        public string Path { get;}
        
        public GirFile(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new ArgumentException($"{path} was not found");

            if (!path.EndsWith(".gir"))
                throw new ArgumentException($"{path} is not ending with '.gir'");
            
            Path = path;
        }
    }
}
