using System;
using System.Configuration;
using GiveCRM.Admin.Web.Interfaces;

namespace GiveCRM.Admin.Web.Controllers
{
    public class WebConfigConfiguration : IConfiguration
    {
        public string BaseDomain
        {
            get { return ConfigurationManager.AppSettings["BaseDomain"]; }
            set { throw new NotImplementedException(); }
        }

        public string ExcelTemplatePath
        {
            get { return ConfigurationManager.AppSettings["ExcelTemplatePath"]; }
            set { throw new NotImplementedException(); }
        }
    }
}