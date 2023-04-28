namespace PoseidonApi.Logger;

public sealed class ColorConsoleLoggerConfiguration
{
    public int EventId { get; set; }

    public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; } = new()
    {
        [LogLevel.Information] = ConsoleColor.DarkGreen,
        [LogLevel.Error] = ConsoleColor.DarkRed,
        [LogLevel.Warning] = ConsoleColor.Blue
    };
}