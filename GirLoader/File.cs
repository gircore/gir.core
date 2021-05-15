using System;

namespace Gir
{
    public record File
    {
        public string Path { get;}
        
        public File(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new ArgumentException($"{path} was not found");

            if (!path.EndsWith(".gir"))
                throw new ArgumentException($"{path} is not ending with '.gir'");
            
            Path = path;
        }
    }
}
