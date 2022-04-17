namespace GirTool;

internal class Configuration
{
    public static void SetupLogLevel(LogLevel logLevel)
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
}
