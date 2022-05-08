using IFYB.Models;

namespace IFYB.Entities;
public class GitAccess
{
    public int Id { get; set; }
    public string Url { get; set; }
    public GitAccessMode AccessMode { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public GitAccessState State { get; set; }

    public GitAccess(string url)
    {
        Url = url;
    }
}