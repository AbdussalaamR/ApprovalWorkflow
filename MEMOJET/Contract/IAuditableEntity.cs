using System;

namespace MEMOJET.Contract
{
    public interface IAuditableEntity
    {
        public DateTime CreatedOn{ get; set; }
        // public  int LastModifiedBy{ get; set; }
        // public DateTime?LastModifiedOn{ get; set; }
    }
}