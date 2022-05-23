namespace IFYB.Models;

public class OrderDto
{
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public bool ThirdPartyTool { get; set; }
    public string ProjectDescription { get; set; }
    public string BugDescription { get; set; }
    public int GitAccessId { get; set; }

    public OrderDto(Framework framework, string version, bool thirdPartyTool, string projectDescription, string bugDescription, int gitAccessId)
    {
        Framework = framework;
        Version = version;
        ThirdPartyTool = thirdPartyTool;
        ProjectDescription = projectDescription;
        BugDescription = bugDescription;
        GitAccessId = gitAccessId;
    }
}

public enum Framework { DotNet, Vue }