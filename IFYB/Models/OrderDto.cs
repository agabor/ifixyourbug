namespace IFYB.Models;

public class OrderDto
{
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public string ApplicationUrl { get; set; }
    public string SpecificPlatform { get; set; }
    public string SpecificPlatformVersion { get; set; }
    public string ThirdPartyTool { get; set; }
    public string ProjectDescription { get; set; }
    public string BugDescription { get; set; }
    public int GitAccessId { get; set; }

    public OrderDto(Framework framework, string version, string applicationUrl, string specificPlatform, string specificPlatformVersion, string thirdPartyTool, string projectDescription, string bugDescription, int gitAccessId)
    {
        Framework = framework;
        Version = version;
        ApplicationUrl = applicationUrl;
        SpecificPlatform = specificPlatform;
        SpecificPlatformVersion = specificPlatformVersion;
        ThirdPartyTool = thirdPartyTool;
        ProjectDescription = projectDescription;
        BugDescription = bugDescription;
        GitAccessId = gitAccessId;
    }
}

public enum Framework { DotNet, Vue }