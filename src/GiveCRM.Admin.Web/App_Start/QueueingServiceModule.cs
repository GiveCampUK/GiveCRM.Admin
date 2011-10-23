using GiveCRM.Admin.Web.Interfaces;
using GiveCRM.Admin.Web.Services;
using Ninject.Modules;

namespace GiveCRM.Admin.Web.App_Start
{
    public class QueueingServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISignUpQueueingService>()
                .To<SignUpNonQueueingService>()
                .InRequestScope();
        }
    }
}