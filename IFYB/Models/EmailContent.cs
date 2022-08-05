public class EmailContent
{
    public string Title { get; set; } = string.Empty;
    public string? OrderNumber { get; set; }
    public string? OrderLink { get; set; }
    public List<EmailContentItem> Content { get; set; } = new List<EmailContentItem>();
}

public class EmailContentItem
{
    public string Type { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? Link { get; set; }
}