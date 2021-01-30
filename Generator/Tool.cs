using System;
using System.IO;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using Repository;

namespace Generator
{
    public class Tool
    {
        // Example Usage: dotnet run -- ../gir-files/Gst-1.0.gir
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: dotnet run -- file1 file2 file3 ...");
                return 0;
            }

            foreach (var arg in args)
            {
                if (!File.Exists(arg))
                {
                    Log.Error($"Could not find file at '{arg}'. Make sure it is a valid path");
                    return -1;
                }   
            }

            return new Generator(args).WriteAsync();
        }
    }
}
