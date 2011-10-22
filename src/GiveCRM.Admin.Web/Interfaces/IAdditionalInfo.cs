namespace GiveCRM.Admin.Web.Interfaces
{
    public interface IAdditionalInfo
    {
        string UserName { get; set; }
        string RegisteredCharityNumber { get; set; }
        string SubDomain { get; set; }
    }
}