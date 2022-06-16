using IFYB.Models;

namespace IFYB.Entities;

public class Order
{
    public int Id { get; set; }
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public string ApplicationUrl { get; set; }
    public string SpecificPlatform { get; set; }
    public string SpecificPlatformVersion { get; set; }
    public string ThirdPartyTool { get; set; }
    public string ProjectDescription { get; set; }
    public string BugDescription { get; set; }
    public OrderState State { get; set; }
    public Client? Client { get; set; }
    public List<Message>? Messages { get; set; }
    public int GitAccessId { get; set; }
    public GitAccess GitAccess { get; set; } = null!;


    public Order(int id, Framework framework, string version, string applicationUrl, string specificPlatform, string specificPlatformVersion, string thirdPartyTool, string projectDescription, string bugDescription, int gitAccessId)
    {
        Id = id;
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

    public static Order FromDto(OrderDto dto)
    {
        return new Order(dto.Id, dto.Framework, dto.Version, dto.ApplicationUrl, dto.SpecificPlatform, dto.SpecificPlatformVersion, dto.ThirdPartyTool, dto.ProjectDescription, dto.BugDescription, dto.GitAccessId);
    }

    public OrderDto ToDto()
    {
        return new OrderDto(Id, Framework, Version, ApplicationUrl, SpecificPlatform, SpecificPlatformVersion, ThirdPartyTool, ProjectDescription, BugDescription, Messages?.Select(m => m.ToDto()).ToList(), GitAccessId);
    }
}

public enum OrderState { Submitted, Accepted, Rejected, Payed, Completed, Refundable }