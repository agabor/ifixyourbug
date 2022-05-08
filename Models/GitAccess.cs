namespace IFYB.Models;
public class GitAccessDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public GitAccessMode AccessMode { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public GitAccessState State { get; set; }

    public GitAccessDto(string url)
    {
        Url = url;
    }
}

public enum GitAccessState { Submitted, Verified, Error }
public enum GitAccessMode { Public, Invite, Credentials }