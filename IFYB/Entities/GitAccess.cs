using IFYB.Models;

namespace IFYB.Entities;
public class GitAccess
{
    public int Id { get; set; }
    public string Url { get; set; }
    public GitAccessMode AccessMode { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public GitAccess(string url, GitAccessMode accessMode, string? username, string? password)
    {
        Url = url;
        AccessMode = accessMode;
        Username = username;
        Password = password;
    }

    public static GitAccess FromDto(GitAccessDto dto)
    {
        return new GitAccess(dto.Url, dto.AccessMode, dto.Username, dto.Password);
    }

    public GitAccessDto ToDto()
    {
        return new GitAccessDto(Id, Url, AccessMode, Username, Password);
    }
}