namespace IFYB;

public class GitOptions
{
    public const string Git = "Git";
    public List<GitService> Services { get; set; } = new List<GitService>();
    public string SshKey { get; set; } = String.Empty;
}

public class GitService
{
    public string Name { get; set; } = String.Empty;
    public string Domain { get; set; } = String.Empty;
    public string User { get; set; } = String.Empty;
}