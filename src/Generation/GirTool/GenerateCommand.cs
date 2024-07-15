using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            var (allNamespaces, generatedNamespaces) = GetNamespaces(
                searchPathLinux, searchPathMacos, searchPathWindows, disableAsync, input, invocationContext);

            if (disableAsync)
            {
                foreach (var @namespace in allNamespaces)
                    PlatformGenerator.Fixup(@namespace);

                foreach (var @namespace in generatedNamespaces)
                    PlatformGenerator.Generate(@namespace, output);
            }
            else
            {
                Parallel.ForEach(allNamespaces, PlatformGenerator.Fixup);
                Parallel.ForEach(generatedNamespaces, x => PlatformGenerator.Generate(x, output));
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

    private static (IEnumerable<Namespace>, IEnumerable<Namespace>) GetNamespaces(string? searchPathLinux, string? searchPathMacos, string? searchPathWindows, bool disableAsync, string[] input, InvocationContext invocationContext)
    {
        try
        {
            (var linuxRepositories, var macosRepositories, var windowsRepositories) = LoadRepositories(searchPathLinux, searchPathMacos, searchPathWindows, disableAsync, input);

            var linuxNamespaceNames = linuxRepositories.Repositories.Select(x => GetNamespaceName(x.Namespace));
            var macosNamespaceNames = macosRepositories.Repositories.Select(x => GetNamespaceName(x.Namespace));
            var windowsNamespaceNames = windowsRepositories.Repositories.Select(x => GetNamespaceName(x.Namespace));
            var namespacesNames = linuxNamespaceNames
                .Union(macosNamespaceNames)
                .Union(windowsNamespaceNames)
                .Distinct();

            // We only generate code for namespaces that came from the set of inputs
            var generatedNamespaceNames = linuxRepositories.InputNamespaceNames
                .Concat(macosRepositories.InputNamespaceNames)
                .Concat(windowsRepositories.InputNamespaceNames)
                .ToHashSet();

            var allNamespaces = new List<Namespace>();
            var generatedNamespaces = new List<Namespace>();

            foreach (var namespaceName in namespacesNames)
            {
                var linuxNamespace = linuxRepositories.Repositories.FirstOrDefault(x => GetNamespaceName(x.Namespace) == namespaceName)?.Namespace;
                var macosNamespace = macosRepositories.Repositories.FirstOrDefault(x => GetNamespaceName(x.Namespace) == namespaceName)?.Namespace;
                var windowsNamespace = windowsRepositories.Repositories.FirstOrDefault(x => GetNamespaceName(x.Namespace) == namespaceName)?.Namespace;

                if (linuxNamespace is null)
                    Log.Information($"There is no {namespaceName} repository for linux");

                if (macosNamespace is null)
                    Log.Information($"There is no {namespaceName} repository for macos");

                if (windowsNamespace is null)
                    Log.Information($"There is no {namespaceName} repository for windows");

                var @namespace = new Namespace(new PlatformHandler(linuxNamespace, macosNamespace, windowsNamespace));
                allNamespaces.Add(@namespace);

                if (generatedNamespaceNames.Contains(namespaceName))
                {
                    generatedNamespaces.Add(@namespace);
                }
            }

            return (allNamespaces, generatedNamespaces);
        }
        catch (FileNotFoundException fileNotFoundException)
        {
            Log.Exception(fileNotFoundException);
            Log.Error("Please make sure that the given input files are readable.");

            invocationContext.ExitCode = 1;
        }

        return (Enumerable.Empty<Namespace>(), Enumerable.Empty<Namespace>());
    }

    private static (DeserializedInput, DeserializedInput, DeserializedInput) LoadRepositories(string? searchPathLinux, string? searchPathMacos, string? searchPathWindows, bool disableAsync, string[] input)
    {
        if (searchPathLinux is null && searchPathMacos is null && searchPathWindows is null)
            throw new Exception("Please define at least one search parth for a specific platform");

        DeserializedInput? linuxRepositories = null;
        DeserializedInput? macosRepositories = null;
        DeserializedInput? windowsRepositories = null;

        void SetLinuxRepositories() => linuxRepositories = DeserializeInput("linux", searchPathLinux, input);
        void SetMacosRepositories() => macosRepositories = DeserializeInput("macos", searchPathMacos, input);
        void SetWindowsRepositories() => windowsRepositories = DeserializeInput("windows", searchPathWindows, input);

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

    private static DeserializedInput DeserializeInput(string platformName, string? searchPath, string[] input)
    {
        var repositoryResolver = new RepositoryResolverFactory(
            platformName, searchPath, typeof(GenerateCommand).Assembly).Create();

        var inputRepositories = input
            .Select(fileName => repositoryResolver.ResolveRepository(fileName))
            .OfType<GirLoader.Input.Repository>()
            .ToList();

        // Get the namespaces corresponding to the input gir files.
        // There may be more namespaces included in the output if they are resolved from includes.
        var inputNamespaces = inputRepositories
            .Select(repository => repository.Namespace == null ? "" : GetNamespaceName(repository.Namespace))
            .ToList();

        var includeResolver = new IncludeResolver(repositoryResolver);
        var loader = new GirLoader.Loader(includeResolver.ResolveInclude);
        var outputRepositories = loader.Load(inputRepositories).ToList();

        return new DeserializedInput(outputRepositories, inputNamespaces);
    }

    private static string GetNamespaceName(GirModel.Namespace ns)
    {
        return $"{ns.Name}-{ns.Version}";
    }

    private static string GetNamespaceName(GirLoader.Input.Namespace ns)
    {
        return $"{ns.Name}-{ns.Version}";
    }

    private class DeserializedInput
    {
        public DeserializedInput(List<Repository> repositories, List<string> inputNamespaceNames)
        {
            Repositories = repositories;
            InputNamespaceNames = inputNamespaceNames;
        }

        public static DeserializedInput Empty() =>
            new DeserializedInput(new List<Repository>(), new List<string>());

        /// <summary>
        /// All resolved output repositories
        /// </summary>
        public List<Repository> Repositories { get; }

        /// <summary>
        /// Namespace names corresponding to the input gir files
        /// </summary>
        public List<string> InputNamespaceNames { get; }
    }
}
