using System.Collections;
using System.Collections.Generic;
using MEMOJET.Contract;

namespace MEMOJET.Entities
{
    public class Form:AuditableEntity 
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public int RespoCentreId { get; set; }
        public ICollection<UserForm> UserForms { get; set; } = new List<UserForm>();
    }
}