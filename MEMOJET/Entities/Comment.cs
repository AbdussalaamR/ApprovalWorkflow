using MEMOJET.Contract;

namespace MEMOJET.Entities
{
    public class Comment:AuditableEntity
    {
        public int UserFormId { get; set; }
        public UserForm UserForm { get; set; }
        public int ApprovalId { get; set; }
        public string ApprovalComment { get; set; }
    }
}