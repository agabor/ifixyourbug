using System.Text;

public class EmailContent
{
    public string Title { get; set; } = string.Empty;
    public int? OrderNumber { get; set; }
    public string? OrderLink { get; set; }
    public List<EmailContentItem> Content { get; set; } = new List<EmailContentItem>();
    public string? Currency { get; set; }
    public decimal? Price { get; set; }
    public bool? ToAdmin { get; set; }

    public EmailContent ToPlainText()
    {
        return new EmailContent
        {
            Title = Title,
            OrderNumber = OrderNumber,
            OrderLink = OrderLink,
            Content = Content.Select(i => i.ToPlainText()).ToList(),
            Currency = Currency,
            Price = Price,
            ToAdmin = ToAdmin
        };
    }

}

public class EmailContentItem
{
    public string Type { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string? Link { get; set; }


    public EmailContentItem ToPlainText() {
        return new EmailContentItem
        {
            Type = Type,
            Text = RemoveTags(Text),
            Link = Link
        };
    }


    private string RemoveTags(string text)
    {
        text = text.Replace("<br>", "\n");
        bool inTag = false;
        var builder = new StringBuilder();
        foreach (char c in text) {
            if (c == '<') {
                inTag = true;
            } else if (c == '>') {
                inTag = false;
            } else if (!inTag) {
                builder.Append(c);
            }
        }
        return builder.ToString();
    }
}