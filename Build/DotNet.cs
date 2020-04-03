
using SimpleExec;
using static Commands;

static class DotNet
{
    public static void Build(string project, string configuration) 
    {
        Command.Run(dotnet, $"{Commands.build} --nologo -c {configuration} -o ..\\Output\\{configuration}\\Binaries {project}");
    } 
    public static void Run(string project, string configuration) 
    {
        Command.Run(dotnet, $"{run} -c {configuration} {project}", project);
    }
    public static void Clean(string project, string configuration) 
    {
        Command.Run(dotnet, $"{clean} --nologo -c {configuration}", project);
    }
    public static void Pack(string project, string configuration)
    {
        Command.Run(dotnet, $"{pack} --nologo -c {configuration} -o ..\\..\\..\\Output\\{configuration}\\Nuget", project);
    }
}