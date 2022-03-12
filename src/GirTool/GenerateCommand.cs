using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Invocation;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Generator3;
using GirLoader;
using GirLoader.Input;

namespace GirTool;

public class GenerateCommand : Command
{
    public GenerateCommand() : base(
        name: "generate",
        description: "Generate C# bindings from gir files"
    )
    {
        var inputArgument = new Argument<string[]>(
            name: "input",
            description: "The gir files which should be processed"
        );

        var outputOption = new Option<string>(
            aliases: new[] { "-o", "--output" },
            description: "The directory to write the generated C# files to",
            getDefaultValue: () => "./Libs"
        );

        var searchPathOption = new Option<string>(
            aliases: new[] { "-s", "--search-path" },
            description: "The directory which is searched for dependent gir files",
            getDefaultValue: () => "./Gir"
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
        AddOption(searchPathOption);
        AddOption(disableAsyncOption);
        AddOption(logLevelOption);

        this.SetHandler<string[], string, string, bool, LogLevel, InvocationContext>(
            handle: Execute,
            symbols: new IValueDescriptor[] { inputArgument, outputOption, searchPathOption, disableAsyncOption, logLevelOption }
        );
    }

    private void Execute(string[] input, string output, string searchPath, bool disableAsync, LogLevel logLevel, InvocationContext invocationContext)
    {
        try
        {
            SetupLogLevel(logLevel);

            if (!TryDeserializeInput(input, invocationContext, out List<Repository>? inputRepositories))
                return;

            var includeResolver = new IncludeResolver(searchPath);
            var loader = new GirLoader.Loader(includeResolver.ResolveInclude);
            var repositories = loader.Load(inputRepositories);

            if (disableAsync)
            {
                foreach (var repository in repositories)
                    repository.Namespace.Generate(output);
            }
            else
            {
                Parallel.ForEach(repositories, repository => repository.Namespace.Generate(output));
            }

            Log.Information("Done");
        }
        catch (Exception ex)
        {
            Log.Exception(ex);
            Log.Error("An error occurred while writing files. Please save a copy of your log output and open an issue at: https://github.com/gircore/gir.core/issues/new");
            invocationContext.ExitCode = 1;
        }
    }

    private void SetupLogLevel(LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Debug:
                GirLoader.Loader.EnableDebugOutput();
                Generator3.Configuration.EnableDebugOutput();
                break;
            case LogLevel.Verbose:
                GirLoader.Loader.EnableVerboseOutput();
                Generator3.Configuration.EnableVerboseOutput();
                break;
        }
    }

    private bool TryDeserializeInput(string[] input, InvocationContext invocationContext, [MaybeNullWhen(false)] out List<Repository> inputRepositories)
    {
        try
        {
            inputRepositories = input
                .Select(x => new FileInfo(x).OpenRead().DeserializeGirInputModel())
                .ToList();

            return true;
        }
        catch (FileNotFoundException fileNotFoundException)
        {
            Log.Exception(fileNotFoundException);
            Log.Error("Please make sure that the given input files are readable.");

            invocationContext.ExitCode = 1;
            inputRepositories = null;
            return false;
        }
    }
}
