namespace GiveCRM.Admin.Web.Interfaces
{
    public interface IConfiguration
    {
        string BaseDomain { get; set; }
        string ExcelTemplatePath { get; set; }
        string CrmTestUrl { get; set; }
    }
}