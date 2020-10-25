using SimpleExec;

namespace Build
{
    public static class DotNet
    {
        #region Methods

        public static void Build(string project, string configuration)
        {
            Command.Run(Commands.Dotnet, $"{Commands.Build} --nologo -c {configuration} {project}");
        }
        public static void Run(string project, string configuration)
        {
            Command.Run(Commands.Dotnet, $"{Commands.Run} -c {configuration} {project}", project);
        }
        public static void Clean(string project, string configuration)
        {
            Command.Run(Commands.Dotnet, $"{Commands.Clean} --nologo -c {configuration}", project);
        }

        #endregion
    }
}