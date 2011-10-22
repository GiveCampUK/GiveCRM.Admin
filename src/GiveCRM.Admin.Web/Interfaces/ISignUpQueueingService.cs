namespace GiveCRM.Admin.Web.Interfaces
{
    public interface ISignUpQueueingService
    {
        void QueueEmail();
        void QueueProvisioning();
    }
}