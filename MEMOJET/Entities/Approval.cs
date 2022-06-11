using System.Collections.Generic;
using MEMOJET.Contract;

namespace MEMOJET.Entities
{
    public class Approval:AuditableEntity
    {
        public int Sequence { get; set; }
        
        public int UserId { get; set; }
        
        public string ApprovalRole { get; set; } //referring to the role of the user approving
        
        public int ResponsibilityCentreId { get; set; }
        public ICollection<ApprovalRespoCentre> ApprovalResponsibilityCentres { get; set; } =
            new List<ApprovalRespoCentre>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}