using IFYB.Models;

namespace IFYB.Entities;

public class Order
{
    public int Id { get; set; }
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public string ThirdPartyTool { get; set; }
    public string ProjectDescription { get; set; }
    public string BugDescription { get; set; }
    public OrderState State { get; set; }
    public Client? Client { get; set; }
    public List<Message>? Messages { get; set; }
    public int GitAccessId { get; set; }
    public GitAccess GitAccess { get; set; } = null!;


    public Order(Framework framework, string version, string thirdPartyTool, string projectDescription, string bugDescription, int gitAccessId)
    {
        Framework = framework;
        Version = version;
        ThirdPartyTool = thirdPartyTool;
        ProjectDescription = projectDescription;
        BugDescription = bugDescription;
        GitAccessId = gitAccessId;
    }

    public static Order FromDto(OrderDto dto)
    {
        return new Order(dto.Framework, dto.Version, dto.ThirdPartyTool, dto.ProjectDescription, dto.BugDescription, dto.GitAccessId);
    }

    public OrderDto ToDto()
    {
        return new OrderDto(Framework, Version, ThirdPartyTool, ProjectDescription, BugDescription, GitAccessId);
    }
}

public enum OrderState { Submitted, Accepted, Payed, Completed, Refundable }