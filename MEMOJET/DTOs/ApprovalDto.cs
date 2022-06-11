using System.Collections.Generic;
using MEMOJET.Entities;

namespace MEMOJET.DTOs
{
    public class ApprovalDto
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        
        public int UserId { get; set; }
        
        public string ApprovalRole { get; set; } //referring to the role of the user approving
        
        public int ResponsibilityCentreId { get; set; }
        public ICollection<ApprovalRespoCentre> ApprovalResponsibilityCentres { get; set; } =
            new List<ApprovalRespoCentre>();
    }

    public class CreateApprovalRequestModel
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        
        public int UserId { get; set; }
        public string ApprovalRole { get; set; } //referring to the role of the user approving
        public int ResponsibilityCentreId { get; set; }
    }

    public class ApprovalResponseModel:BaseResponse
    {
        public ApprovalDto Data { get; set; }
    }
    
    public class ApprovalsResponseModel:BaseResponse
    {
        public IList<ApprovalDto> Data { get; set; }
    }
}