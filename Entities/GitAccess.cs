namespace IFYB.Entities;

public class GitAccess
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsPublic { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public GitAccessState State { get; set; }

    public GitAccess(string url, bool isPublic)
    {
        Url = url;
        IsPublic = isPublic;
    }
}

public enum GitAccessState { Submitted, Verified, Error }