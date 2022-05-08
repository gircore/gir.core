namespace GirTool;

internal class Configuration
{
    public static void SetupLogLevel(LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Debug:
                GirLoader.Loader.EnableDebugOutput();
                Generator.Configuration.EnableDebugOutput();
                break;
            case LogLevel.Verbose:
                GirLoader.Loader.EnableVerboseOutput();
                Generator.Configuration.EnableVerboseOutput();
                break;
        }
    }
}
