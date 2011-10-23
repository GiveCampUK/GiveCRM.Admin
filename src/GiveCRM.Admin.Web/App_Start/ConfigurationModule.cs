using GiveCRM.Admin.Web.Controllers;
using GiveCRM.Admin.Web.Interfaces;
using Ninject.Modules;

namespace GiveCRM.Admin.Web.App_Start
{
    public class ConfigurationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>()
                .To<HardCodedConfiguration>()
                .InRequestScope();
        }
    }
}