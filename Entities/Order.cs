using System.Text.Json.Serialization;

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
    
    [JsonIgnore]
    public Client? Client { get; set; }
    public List<Message>? Messages { get; set; }
    public int GitAccessId { get; set; }
    public GitAccess? GitAccess { get; set; }

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