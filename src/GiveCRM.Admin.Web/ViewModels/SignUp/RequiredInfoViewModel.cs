using System.ComponentModel.DataAnnotations;

namespace GiveCRM.Admin.Web.ViewModels.SignUp
{
    public class RequiredInfoViewModel
    {
        [Required(ErrorMessage = "Please enter your email address")]
        [Display(Name = "Email Address")]
        public string UserIdentifier { get; set; }
        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter the name of your charity")]
        [Display(Name = "Charity Name")]
        public string CharityName { get; set; }
        [Required(ErrorMessage = "Please accept the terms and conditions")]
        [Display(Name = "Terms and Conditions")]
        public bool TermsAccepted { get; set; }
    }
}