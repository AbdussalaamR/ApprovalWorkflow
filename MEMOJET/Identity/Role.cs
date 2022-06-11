using System.Collections.Generic;
using MEMOJET.Contract;

namespace MEMOJET.Identity
{
    public class Role:AuditableEntity
    {
        public string Name{ get; set; }
        public  string Description{ get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}