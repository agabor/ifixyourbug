namespace IFYB.Models;
public class StackoverflowRequestDto
{
    public int Number { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateTime { get; set; }
    public string Url { get; set; }
    public string Text { get; set; }
    public bool Solved { get; set; } = false;

    public StackoverflowRequestDto(int number, string name, string email, DateTime dateTime, string url, string text, bool solved)
    {
        Number = number;
        Name = name;
        Email = email;
        DateTime = dateTime;
        Url = url;
        Text = text;
        Solved = solved;
    }
}