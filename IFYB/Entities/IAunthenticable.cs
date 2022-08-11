namespace IFYB.Entities;

public interface IAunthenticable
{
    int Id { get; set; }
    string Email { get; set; }
    string? Password { get; set; }
    int FailedLoginAtemptCount { get; set; }
    string Role { get; }
}
