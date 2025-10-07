using System;
using System.Globalization;

namespace Logger;

public static class BaseLoggerMixins
{
    public static void Error(this BaseLogger logger, string message, params object[] args)
       => LogWithLevel(logger, LogLevel.Error, message, args);

    public static void Warning(this BaseLogger logger, string message, params object[] args)
        => LogWithLevel(logger, LogLevel.Warning, message, args);

    public static void Information(this BaseLogger logger, string message, params object[] args)
        => LogWithLevel(logger, LogLevel.Information, message, args);

    public static void Debug(this BaseLogger logger, string message, params object[] args)
        => LogWithLevel(logger, LogLevel.Debug, message, args);

    private static void LogWithLevel(BaseLogger logger, LogLevel level, string message, object[] args)
    {
       ArgumentNullException.ThrowIfNull(logger);

        var formatted = (args is { Length: > 0 })
            ? string.Format(CultureInfo.InvariantCulture,message, args)
            : message;

        logger.Log(level, formatted);
    }
}