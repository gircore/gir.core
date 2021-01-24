using System;
using System.IO;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace Generator
{
    public class Tool
    {
        static int Main(string[] args)
        {
            string girFile = "../gir-files/Gst-1.0.gir";

            string[] projects =
            {
                girFile
            };
            
            return new Generator(projects).WriteAsync();
        }
    }
}
