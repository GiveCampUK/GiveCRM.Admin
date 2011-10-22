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

        public string ExcelTemplatePath
        {
            get { return "/Content/Files/GiveCRM_Template.xls"; }
            set { throw new NotImplementedException(); }
        }
    }
}