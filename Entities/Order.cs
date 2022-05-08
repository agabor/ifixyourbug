namespace IFYB;

public class Order
{
    public int Id { get; set; }
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public string? ThirdPartyTool { get; set; }
    public string ProjectDescription { get; set; }
    public string BugDescription { get; set; }
    public OrderState State { get; set; }
    public Client Client { get; set; } = null!;
    public List<Message> Messages { get; set; } = null!;
    public GitAccess GitAccess { get; set; } = null!;

    public Order(Framework framework, string version, string projectDescription, string bugDescription)
    {
        Framework = framework;
        Version = version;
        ProjectDescription = projectDescription;
        BugDescription = bugDescription;
    }
}

public enum Framework { DotNet, Vue }

public enum OrderState { Submitted, Accepted, Payed, Completed, Refundable }