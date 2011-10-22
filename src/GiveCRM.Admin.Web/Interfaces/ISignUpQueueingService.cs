using GiveCRM.Admin.Web.ViewModels.SignUp;

namespace GiveCRM.Admin.Web.Interfaces
{
    public interface ISignUpQueueingService
    {
        void QueueEmail(EmailViewModel emailViewModel);
        void QueueProvisioning();
    }
}