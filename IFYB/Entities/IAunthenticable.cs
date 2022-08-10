namespace IFYB.Entities;

public interface IAunthenticable
{
    string Email { get; set; }
    string? Password { get; set; }
    int FailedLoginAtemptCount { get; set; }
}
