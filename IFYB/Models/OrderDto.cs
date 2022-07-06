using IFYB.Entities;
namespace IFYB.Models;

public class OrderDto
{
    public int Id { get; set; }
    public string Number { get; set; }
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public string? ApplicationUrl { get; set; }
    public string? SpecificPlatform { get; set; }
    public string? SpecificPlatformVersion { get; set; }
    public string? ThirdPartyTool { get; set; }
    public string BugDescription { get; set; }
    public OrderState State { get; set; }
    public List<MessageDto>? Messages { get; set; }
    public int GitAccessId { get; set; }
    public int ClientId { get; set; }

    public OrderDto(int id, string number, Framework framework, string version, string? applicationUrl, string? specificPlatform, string? specificPlatformVersion, string? thirdPartyTool, string bugDescription, OrderState state, List<MessageDto>? messages, int gitAccessId, int clientId)
    {
        Id = id;
        Number = number;
        Framework = framework;
        Version = version;
        ApplicationUrl = applicationUrl;
        SpecificPlatform = specificPlatform;
        SpecificPlatformVersion = specificPlatformVersion;
        ThirdPartyTool = thirdPartyTool;
        BugDescription = bugDescription;
        State = state;
        Messages = messages;
        GitAccessId = gitAccessId;
        ClientId = clientId;
    }
}

public enum Framework { DotNet, Vue }