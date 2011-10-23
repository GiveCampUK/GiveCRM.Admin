using System;

namespace GiveCRM.Admin.Models
{
    public class CharityMembership
    {
        public int Id { get; set; }
        public int CharityId { get; set; }
        public Guid UserId { get; set; }
    }
}
