using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Generator3.Converter;
using Generator3.Publication;
using GirLoader;
using GirLoader.PlatformSupport;
using Repository = GirLoader.Output.Repository;

namespace GirTool;

public partial class GenerateCommand : Command
{
    private void Execute(string[] input, string output, string? searchPathLinux, string? searchPathMacos, string? searchPathWindows, bool disableAsync, LogLevel logLevel, InvocationContext invocationContext)
    {
        try
        {
            Configuration.SetupLogLevel(logLevel);
            FilePublisher.TargetFolder = output;

            var platformHandlers = GetPlatformHandler(searchPathLinux, searchPathMacos, searchPathWindows, disableAsync, input, invocationContext);

            if (disableAsync)
            {
                foreach (var platformHandler in platformHandlers)
                    PlatformGenerator.Generate(platformHandler);
            }
            else
            {
                Parallel.ForEach(platformHandlers, PlatformGenerator.Generate);
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

    private static IEnumerable<PlatformHandler> GetPlatformHandler(string? searchPathLinux, string? searchPathMacos, string? searchPathWindows, bool disableAsync, string[] input, InvocationContext invocationContext)
    {
        try
        {
            (var linuxRepositories, var macosRepositories, var windowsRepositories) = LoadRepositories(searchPathLinux, searchPathMacos, searchPathWindows, disableAsync, input);

            var linuxNamespaceNames = linuxRepositories.Select(x => x.Namespace.GetCanonicalName());
            var macosNamespaceNames = macosRepositories.Select(x => x.Namespace.GetCanonicalName());
            var windowsNamespaceNames = windowsRepositories.Select(x => x.Namespace.GetCanonicalName());
            var namespacesNames = linuxNamespaceNames
                .Union(macosNamespaceNames)
                .Union(windowsNamespaceNames)
                .Distinct();

            var platformHandlers = new List<PlatformHandler>();

            foreach (var namespaceName in namespacesNames)
            {
                var linuxNamespace = linuxRepositories.FirstOrDefault(x => x.Namespace.GetCanonicalName() == namespaceName)?.Namespace;
                var macosNamespace = macosRepositories.FirstOrDefault(x => x.Namespace.GetCanonicalName() == namespaceName)?.Namespace;
                var windowsNamespace = windowsRepositories.FirstOrDefault(x => x.Namespace.GetCanonicalName() == namespaceName)?.Namespace;

                if (linuxNamespace is null)
                    Log.Information($"There is no {namespaceName} repository for linux");

                if (macosNamespace is null)
                    Log.Information($"There is no {namespaceName} repository for macos");

                if (windowsNamespace is null)
                    Log.Information($"There is no {namespaceName} repository for windows");

                platformHandlers.Add(new PlatformHandler(linuxNamespace, macosNamespace, windowsNamespace));
            }

            return platformHandlers;
        }
        catch (FileNotFoundException fileNotFoundException)
        {
            Log.Exception(fileNotFoundException);
            Log.Error("Please make sure that the given input files are readable.");

            invocationContext.ExitCode = 1;
        }

        return Enumerable.Empty<PlatformHandler>();
    }

    private static (List<Repository>, List<Repository>, List<Repository>) LoadRepositories(string? searchPathLinux, string? searchPathMacos, string? searchPathWindows, bool disableAsync, string[] input)
    {
        if (searchPathLinux is null && searchPathMacos is null && searchPathWindows is null)
            throw new Exception("Please define at least one search parth for a specific platform");

        List<Repository>? linuxRepositories = null;
        List<Repository>? macosRepositories = null;
        List<Repository>? windowsRepositories = null;

        void SetLinuxRepositories() => linuxRepositories = DeserializeInput(searchPathLinux, input);
        void SetMacosRepositories() => macosRepositories = DeserializeInput(searchPathMacos, input);
        void SetWindowsRepositories() => windowsRepositories = DeserializeInput(searchPathWindows, input);

        if (disableAsync)
        {
            SetLinuxRepositories();
            SetMacosRepositories();
            SetWindowsRepositories();
        }
        else
        {
            Parallel.Invoke(
                SetLinuxRepositories,
                SetMacosRepositories,
                SetWindowsRepositories
            );
        }

        return (linuxRepositories!, macosRepositories!, windowsRepositories!);
    }

    private static List<Repository> DeserializeInput(string? searchPath, string[] input)
    {
        if (searchPath is null)
            return new List<Repository>();

        var inputRepositories = input
            .Select(x => Path.Join(searchPath, x))
            .Select(x => new FileInfo(x).OpenRead().DeserializeGirInputModel())
            .ToList();

        var includeResolver = new IncludeResolver(searchPath);
        var loader = new GirLoader.Loader(includeResolver.ResolveInclude);
        return loader.Load(inputRepositories).ToList();
    }
}
