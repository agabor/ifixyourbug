namespace IFYB.Models;

public class OrderDto
{
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public string ThirdPartyTool { get; set; }
    public string ProjectDescription { get; set; }
    public string BugDescription { get; set; }

    public OrderDto(Framework framework, string version, string thirdPartyTool, string projectDescription, string bugDescription)
    {
        Framework = framework;
        Version = version;
        ThirdPartyTool = thirdPartyTool;
        ProjectDescription = projectDescription;
        BugDescription = bugDescription;
    }
}

public enum Framework { DotNet, Vue }