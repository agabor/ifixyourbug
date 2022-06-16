using IFYB.Entities;
namespace IFYB.Models;

public class OrderDto
{
    public int Id { get; set; }
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public string ApplicationUrl { get; set; }
    public string SpecificPlatform { get; set; }
    public string SpecificPlatformVersion { get; set; }
    public string ThirdPartyTool { get; set; }
    public string BugDescription { get; set; }
    public List<MessageDto>? Messages { get; set; }
    public int GitAccessId { get; set; }

    public OrderDto(int id, Framework framework, string version, string applicationUrl, string specificPlatform, string specificPlatformVersion, string thirdPartyTool, string bugDescription, List<MessageDto>? messages, int gitAccessId)
    {
        Id = id;
        Framework = framework;
        Version = version;
        ApplicationUrl = applicationUrl;
        SpecificPlatform = specificPlatform;
        SpecificPlatformVersion = specificPlatformVersion;
        ThirdPartyTool = thirdPartyTool;
        BugDescription = bugDescription;
        Messages = messages;
        GitAccessId = gitAccessId;
    }
}

public enum Framework { DotNet, Vue }