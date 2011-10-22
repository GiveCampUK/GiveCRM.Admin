using System.Web;

namespace GiveCRM.Admin.Web.ViewModels
{
    public class ExcelImportViewModel
    {
        public readonly string ExcelTemplatePath = "~/Content/files/GiveCRM_Template.xls";
        public HttpPostedFileBase File;
    }
}