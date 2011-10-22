using GiveCRM.Admin.Web.Interfaces;
using GiveCRM.Admin.Web.ViewModels.SignUp;
using Postal;

namespace GiveCRM.Admin.Web.Services
{
    public class SignUpNonQueueingService : ISignUpQueueingService
    {
        public void QueueEmail(EmailViewModel emailViewModel)
        {
            dynamic email = new Email("SignUp");
            email.To = emailViewModel.To;
            email.ActivationToken = emailViewModel.ActivationToken;
            email.Send();
        }

        public void QueueProvisioning()
        {

        }
    }
}