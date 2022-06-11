using System;

namespace MEMOJET.Contract
{
    public abstract class AuditableEntity:BaseEntity, IAuditableEntity, ISoftDelete
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
    }
}