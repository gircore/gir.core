using System;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace GirLoader
{
    internal static class Log
    {
        private static readonly Serilog.Core.Logger _logger;

        static Log()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();
        }

        public static void Exception(Exception exception)
            => _logger.Error("Exception occured: {$exception}", exception);

        public static void Error(string message)
            => _logger.Error(message);

        public static void Warning(string message)
            => _logger.Warning(message);

        public static void Information(string message)
            => _logger.Information(message);

        public static void Debug(string message)
            => _logger.Debug(message);
    }
}
