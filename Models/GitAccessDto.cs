namespace IFYB.Models;
public class GitAccessDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public GitAccessMode AccessMode { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public GitAccessDto(string url, GitAccessMode accessMode, string? username, string? password)
    {
        Url = url;
        AccessMode = accessMode;
        Username = username;
        Password = password;
    }
}
public enum GitAccessMode { Public, Invite, Credentials }