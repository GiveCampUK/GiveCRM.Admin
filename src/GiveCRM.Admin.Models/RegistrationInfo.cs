using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCRM.Admin.Models
{
    public class RegistrationInfo
    {
        public string UserIdentifier { get; set; }
        public string Password { get; set; }
        public string CharityName { get; set; }
        public string SubDomain { get; set; }
        public bool TermsAccepted { get; set; }
    }
}
