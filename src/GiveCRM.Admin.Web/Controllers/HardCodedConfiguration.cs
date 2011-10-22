using System;
using GiveCRM.Admin.Web.Interfaces;

namespace GiveCRM.Admin.Web.Controllers
{
    public class HardCodedConfiguration : IConfiguration
    {
        public string BaseDomain
        {
            get { return "givecamp.org"; }
            set { throw new NotImplementedException(); }
        }
    }
}