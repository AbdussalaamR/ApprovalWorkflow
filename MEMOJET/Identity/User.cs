using System.Collections;
using System.Collections.Generic;
using MEMOJET.Contract;
using MEMOJET.Entities;

namespace MEMOJET.Identity
{
    public class User:AuditableEntity
    {
        public  string UserName{ get; set; }
        public  string Password{ get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
        public ICollection<UserForm> UserForms { get; set; } = new List<UserForm>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}