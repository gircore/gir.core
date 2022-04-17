using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Invocation;

namespace GirTool;

public partial class GenerateCommand : Command
{
    public GenerateCommand() : base(
        name: "generate",
        description: "Generate C# bindings from gir files"
    )
    {
        var inputArgument = new Argument<string[]>(
            name: "input",
            description: "The names of gir files which should be processed"
        );

        var outputOption = new Option<string>(
            aliases: new[] { "-o", "--output" },
            description: "The directory to write the generated C# files to",
            getDefaultValue: () => "./Libs"
        );

        var searchPathOptionLinux = new Option<string>(
            aliases: new[] { "-sl", "--search-path-linux" },
            description: "The directory which is searched for dependent linux gir files"
        );

        var searchPathOptionMacos = new Option<string>(
            aliases: new[] { "-sm", "--search-path-macos" },
            description: "The directory which is searched for dependent macos gir files"
        );

        var searchPathOptionWindows = new Option<string>(
            aliases: new[] { "-sw", "--search-path-windows" },
            description: "The directory which is searched for dependent windows gir files"
        );

        var disableAsyncOption = new Option<bool>(
            aliases: new[] { "-d", "--disable-async" },
            getDefaultValue: () => false,
            description: "Generate files synchronously - useful for debugging"
        );

        var logLevelOption = new Option<LogLevel>(
            aliases: new[] { "-l", "--log-level" },
            getDefaultValue: () => LogLevel.Standard,
            description: "Set the log level"
        );

        AddArgument(inputArgument);
        AddOption(outputOption);
        AddOption(searchPathOptionLinux);
        AddOption(searchPathOptionMacos);
        AddOption(searchPathOptionWindows);
        AddOption(disableAsyncOption);
        AddOption(logLevelOption);

        this.SetHandler<string[], string, string?, string?, string?, bool, LogLevel, InvocationContext>(
            handle: Execute,
            symbols: new IValueDescriptor[] { inputArgument, outputOption, searchPathOptionLinux, searchPathOptionMacos, searchPathOptionWindows, disableAsyncOption, logLevelOption }
        );
    }
}
