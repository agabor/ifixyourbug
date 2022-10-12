namespace IFYB.Models;
public class StackoverflowRequestDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Url { get; set; }
    public string Text { get; set; }

    public StackoverflowRequestDto(string name, string email, string url, string text)
    {
        Name = name;
        Email = email;
        Url = url;
        Text = text;
    }
}