using IFYB.Models;

namespace IFYB.Entities;
public class GitAccess
{
    public int Id { get; set; }
    public string Url { get; set; }
    public GitAccessMode AccessMode { get; set; }

    public GitAccess(string url, GitAccessMode accessMode)
    {
        Url = url;
        AccessMode = accessMode;
    }

    public static GitAccess FromDto(GitAccessDto dto)
    {
        return new GitAccess(dto.Url, dto.AccessMode);
    }

    public GitAccessDto ToDto()
    {
        return new GitAccessDto(Id, Url, AccessMode);
    }
}