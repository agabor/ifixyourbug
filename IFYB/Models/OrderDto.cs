using IFYB.Entities;
namespace IFYB.Models;

public class OrderDto
{
    public int Number { get; set; }
    public Framework Framework { get; set; }
    public string Version { get; set; }
    public string? ApplicationUrl { get; set; }
    public string? SpecificPlatform { get; set; }
    public string? SpecificPlatformVersion { get; set; }
    public string? ThirdPartyTool { get; set; }
    public string BugDescription { get; set; }
    public OrderState State { get; set; }
    public List<MessageDto>? Messages { get; set; }
    public List<ImageDto>? Images { get; set; }
    public int GitAccessId { get; set; }
    public string? PaymentToken { get; set; }
    public int ClientId { get; set; }
    public decimal EurPrice { get; set; }
    public decimal UsdPrice { get; set; }

    public OrderDto(int number, Framework framework, string version, string? applicationUrl, string? specificPlatform, string? specificPlatformVersion, string? thirdPartyTool, string bugDescription, OrderState state, List<MessageDto>? messages, List<ImageDto>? images, int gitAccessId, string? paymentToken, int clientId, decimal eurPrice, decimal usdPrice)
    {
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
        Images = images;
        GitAccessId = gitAccessId;
        PaymentToken = paymentToken;
        ClientId = clientId;
        EurPrice = eurPrice;
        UsdPrice = usdPrice;
    }
}

public enum Framework { DotNet, Vue }