using MEMOJET.Contract;

namespace MEMOJET.Entities
{
    public class ApprovalRespoCentre:AuditableEntity
    {
        public int ApprovalId { get; set; }
        public Approval Approval { get; set; }
        public int ResponsibilityCentreId { get; set; }
        public ResponsibilityCentre ResponsibilityCentre { get; set; }
    }
}