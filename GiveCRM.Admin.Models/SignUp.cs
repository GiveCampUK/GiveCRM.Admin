namespace GiveCRM.Admin.Models
{
    public class SignUp
    {
        public string UserIdentifier { get; set; }
        public string Password { get; set; }
        public string CharityName { get; set; }
        public bool TermsAccepted { get; set; }
    }
}
