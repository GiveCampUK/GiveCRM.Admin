using GiveCRM.Admin.Web.Interfaces;
using Postal;

namespace GiveCRM.Admin.Web.Services
{
    public class SignUpNonQueueingService : ISignUpQueueingService
    {
        public void QueueEmail()
        {
            dynamic email = new Email("SignUp");
            email.To = "robin@minto.co.uk";
            email.FunnyLink = "";
            email.Send();
        }

        public void QueueProvisioning()
        {

        }
    }
}