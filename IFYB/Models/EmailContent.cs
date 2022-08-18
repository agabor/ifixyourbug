public class EmailContent
{
    public string Title { get; set; } = string.Empty;
    public int? OrderNumber { get; set; }
    public string? OrderLink { get; set; }
    public List<EmailContentItem> Content { get; set; } = new List<EmailContentItem>();
    public decimal? EurPrice { get; set; }
    public decimal? UsdPrice { get; set; }
}

public class EmailContentItem
{
    public string Type { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? Link { get; set; }
}