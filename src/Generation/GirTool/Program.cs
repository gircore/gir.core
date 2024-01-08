using System.CommandLine;
using GirTool;

var rootCommand = new RootCommand("GirTool generates C# bindings from GIR files.")
{
    new GenerateCommand(),
    new CleanCommand()
};

return rootCommand.Invoke(args);
