namespace IFYB.Models;
public class GitAccessDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public GitAccessMode AccessMode { get; set; }

    public GitAccessDto(int id, string url, GitAccessMode accessMode)
    {
        Id = id;
        Url = url;
        AccessMode = accessMode;
    }
}
public enum GitAccessMode { Public, Invite, CreateUser }