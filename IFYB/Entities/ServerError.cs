using System;

namespace IFYB.Entities;
public class ServerError
{
    public int Id { get; set; }
    public DateTime DateTime { get; init; }
    public string Message { get; init; } = string.Empty;
    public string? StackTrace { get; init; }
    public string? File { get; init; }
    public int Line { get; init; }
    public string? Source { get; init; }
    public int HResult { get; init; }
    public string Data { get; set; } = string.Empty;
}