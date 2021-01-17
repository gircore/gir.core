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
            string girFile = "Gsk-4.0.gir";

            Project[] projects = {(new Project("Gst", girFile))};
            
            return new Generator(projects)
                .WriteAsync().Result;
        }
    }
}
