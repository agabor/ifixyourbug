namespace IFYB.Models;

public class EmailDto
{
    public string Email { get; set; }
    public bool PrivacyPolicyAccepted { get; set; }
    public EmailDto(string email, bool privacyPolicyAccepted)
    {
        Email = email;
        PrivacyPolicyAccepted = privacyPolicyAccepted;
    }
}
