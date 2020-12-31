using System.Diagnostics.CodeAnalysis;
using SimpleExec;

namespace Build
{
    public enum Configuration
    {
        Release,
        Debug
    }
    
    public static class DotNet
    {
        #region Methods

        public static void Build(string project, Configuration configuration, string? version = null)
        {
            if (!TryGetVersionParameter(version, out var versionParameter))
                versionParameter = "";
            
            Command.Run(Commands.Dotnet, $"{Commands.Build} --nologo -c {configuration} {versionParameter} {project}");
        }

        public static void Run(string project, Configuration configuration)
        {
            Command.Run(Commands.Dotnet, $"{Commands.Run} -c {configuration} {project}", project);
        }

        public static void Test(string project, Configuration configuration)
        {
            Command.Run(Commands.Dotnet, $"{Commands.Test} -c {configuration}", project);
        }

        public static void Clean(string project, Configuration configuration)
        {
            Command.Run(Commands.Dotnet, $"{Commands.Clean} --nologo -c {configuration}", project);
        }

        public static void Pack(string project, Configuration configuration, string? version = null, string? outputDir = null)
        {
            if (!TryGetVersionParameter(version, out var versionParameter))
                versionParameter = "";

            if (!TryGetOutputDirParameter(outputDir, out var outputDirParameter))
                outputDirParameter = "";
            
            Command.Run(Commands.Dotnet, $"{Commands.Pack} --nologo -c {configuration} {versionParameter} {outputDirParameter} {project}");
        }

        private static bool TryGetOutputDirParameter(string? outputDir, [NotNullWhen(true)] out string? dir)
        {
            if (outputDir is null)
            {
                dir = null;
                return false;
            }

            dir = $"-o {outputDir}";
            return true;
        }
        
        private static bool TryGetVersionParameter(string? version, [NotNullWhen(true)] out string? parameter)
        {
            if (version is null)
            {
                parameter = null;
                return false;
            }

            parameter = $"/p:Version={version}";
            return true;
        }

        #endregion
        
        #region Commands
        
        private static class Commands
        {
            #region Constants

            public const string Dotnet = "dotnet";
            public const string Build = "build";
            public const string Run = "run";
            public const string Pack = "pack";
            public const string Nuget = "nuget";
            public const string Push = "push";
            public const string Restore = "restore";
            public const string Clean = "clean";
            public const string Test = "test";

            #endregion
        }
        
        #endregion
    }
}
