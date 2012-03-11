using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveCRM.Admin.Models
{
    public class Charity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string RegisteredCharityNumber { get; set; }
        public string SubDomain { get; set; }

        // Make the tests pass by including these two properties. 
        // TODO: We may not want to include them in the database in this form ultimately.
        public byte[] EncryptedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}
