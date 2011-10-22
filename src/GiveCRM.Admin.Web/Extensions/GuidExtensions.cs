using System;

namespace GiveCRM.Admin.Web.Extensions
{
    public static class GuidExtensions
    {
        public static string AsQueryString(this Guid value)
        {
            return value.ToString().Replace("-", "");
        }
    }
}