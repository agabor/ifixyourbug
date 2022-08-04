using System;

namespace IFYB.Entities;
public class ClientError
{
    public int Id { get; set; }
    public DateTime DateTime { get; init; }
    public string UserAgent { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
}