namespace GiveCRM.Admin.Web.Interfaces
{
    public interface IConfiguration
    {
        string BaseDomain { get; }
        string ExcelTemplatePath { get;  }
        string CrmTestUrl { get;  }
    }
}