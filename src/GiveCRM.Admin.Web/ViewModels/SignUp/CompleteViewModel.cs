using System.ComponentModel.DataAnnotations;
using GiveCRM.Admin.Web.Interfaces;

namespace GiveCRM.Admin.Web.ViewModels.SignUp
{
    public class CompleteViewModel : IAdditionalInfo, IConfiguration
    {
        //IAdditionalInfo
        public string UserName { get; set; }
        public string RegisteredCharityNumber { get; set; }
        [Required(ErrorMessage = "Please enter a subdomain")]
        public string SubDomain { get; set; }

        //IConfiguration
        public string BaseDomain { get; set; }
        public string ExcelTemplatePath { get; set; }

        public CompleteViewModel WithConfig(IConfiguration configuration)
        {
            BaseDomain = configuration.BaseDomain;
            ExcelTemplatePath = configuration.ExcelTemplatePath;
            return this;
        }
    }
}