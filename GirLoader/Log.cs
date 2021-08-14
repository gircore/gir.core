using System;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

/*
 * This class is shared among different projects to provide an
 * identical logging interface to all projects.
 *
 * As this class has no namespace it can be used anywhere it is
 * linked. The code is kept clean as it does not require an
 * object instance. 
 */

internal static class Log
{
    private static readonly Logger Logger;
    private static readonly LoggingLevelSwitch Switch;

    static Log()
    {
        Switch = new LoggingLevelSwitch();

        Logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(Switch)
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .CreateLogger();
    }

    public static void EnableDebugOutput()
        => Switch.MinimumLevel = LogEventLevel.Debug;

    public static void EnableVerboseOutput()
        => Switch.MinimumLevel = LogEventLevel.Verbose;

    public static void Exception(Exception exception)
        => Logger.Error("Exception occured: {$exception}", exception);

    public static void Error(string message)
        => Logger.Error(message);

    public static void Warning(string message)
        => Logger.Warning(message);

    public static void Information(string message)
        => Logger.Information(message);

    public static void Debug(string message)
        => Logger.Debug(message);

    public static void Verbose(string message)
        => Logger.Verbose(message);
}
