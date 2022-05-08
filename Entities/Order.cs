namespace IFYB;

class Order
{
    public int Id { get; set; }
    public Framework Framework { get; set; }
    public Version Version { get; set; }
    public string? ThirdPartyTool { get; set; }
    public string ProjectDescription { get; set; }
    public string BugDescription { get; set; }
    public Client Client { get; set; } = null!;
    public List<Message> Messages { get; set; } = null!;
    public GitAccess GitAccess { get; set; } = null!;

    public Order(Framework framework, Version version, string projectDescription, string bugDescription)
    {
        Framework = framework;
        Version = version;
        ProjectDescription = projectDescription;
        BugDescription = bugDescription;
    }
}

enum Framework { DotNet, Vue }