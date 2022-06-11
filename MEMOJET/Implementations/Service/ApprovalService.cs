using System.Linq;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;
using MEMOJET.Interfaces.Service;

namespace MEMOJET.Implementations.Service
{
    public class ApprovalService:IApprovalService
    {
        private readonly IApprovalRepo _approvalRepo;
        private readonly IResponsibilityCentreRepo _centreRepo;

        public ApprovalService(IApprovalRepo approvalRepo, IResponsibilityCentreRepo centreRepo)
        {
            _approvalRepo = approvalRepo;
            _centreRepo = centreRepo;
        }

        public async Task<ApprovalResponseModel> CreateApproval(CreateApprovalRequestModel model)
        {
            var approval = new Approval
            {
                Id = model.Id,
                UserId = model.UserId,
                Sequence = model.Sequence,
                ApprovalRole = model.ApprovalRole,
                ResponsibilityCentreId = model.ResponsibilityCentreId
            };
            var approvalCentre = new ApprovalRespoCentre
            {
                ResponsibilityCentreId = model.ResponsibilityCentreId,
                ResponsibilityCentre = await _centreRepo.GetCentre(model.ResponsibilityCentreId),
                Approval = approval,
                ApprovalId = approval.Id,
            };
            approval.ApprovalResponsibilityCentres.Add(approvalCentre);
            var newApproval = await _approvalRepo.CreateApproval(approval);
            if (newApproval == null)
            {
                return new ApprovalResponseModel
                {
                    Message = "Approval NOT created",
                    Status = false
                };
            }

            return new ApprovalResponseModel
            {
                Data = new ApprovalDto
                {
                    Id = newApproval.Id,
                    UserId = newApproval.UserId,
                    Sequence = newApproval.Sequence,
                    ResponsibilityCentreId = newApproval.ResponsibilityCentreId,
                    ApprovalRole = newApproval.ApprovalRole
                },
                Message = "Created Successfully",
                Status = true
            };
        }

        public async Task<ApprovalResponseModel> UpdateApproval(CreateApprovalRequestModel model, int id)
        {
            var approval = await _approvalRepo.GetApproval(id);
            if (approval== null)
            {
                return new ApprovalResponseModel
                {
                    Message = $"Approval with Id {id} not found",
                    Status = false
                };
            }

            approval.Sequence = model.Sequence;
            approval.UserId = model.UserId;
            approval.ApprovalRole = model.ApprovalRole;
            approval.ResponsibilityCentreId = model.ResponsibilityCentreId;
            var updatedApproval = await _approvalRepo.UpdateApproval(approval);
            if (updatedApproval == null)
            {
                return new ApprovalResponseModel
                {
                    Message = "Unable to update",
                    Status = false
                };
            }

            return new ApprovalResponseModel
            {
                Data = new ApprovalDto
                {
                    Id = updatedApproval.Id,
                    UserId = updatedApproval.UserId,
                    Sequence = updatedApproval.Sequence,
                    ResponsibilityCentreId = updatedApproval.ResponsibilityCentreId,
                    ApprovalRole = updatedApproval.ApprovalRole
                },
                Message = "Successful",
                Status = true
            };
        }

        public async Task<ApprovalResponseModel> GetApproval(int id)
        {
            var approval = await _approvalRepo.GetApproval(id);
            if (approval == null)
            {
                return new ApprovalResponseModel
                {
                    Message = $"Approval not found",
                    Status = false,
                };
            }

            return new ApprovalResponseModel
            {
                Message = "Successfully retrieved",
                Status = true,
                Data = new ApprovalDto
                {
                    Id = approval.Id,
                    UserId = approval.UserId,
                    Sequence = approval.Sequence,
                    ResponsibilityCentreId = approval.ResponsibilityCentreId,
                    ApprovalRole = approval.ApprovalRole
                }
            };
        }

        public async Task<ApprovalsResponseModel> GetAllApprovals()
        {
            var approvals = await _approvalRepo.GetApprovals();
            var approvalsDto = approvals.Select(x => new ApprovalDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Sequence = x.Sequence,
                ResponsibilityCentreId = x.ResponsibilityCentreId,
                ApprovalRole = x.ApprovalRole
            }).ToList();
            return new ApprovalsResponseModel
            {
                Message = "Retrieved successfully",
                Status = true,
                Data = approvalsDto
            };
        }

        public async Task<string> DeleteApproval(int id)
        {
            var approval = await _approvalRepo.GetApproval(id);
            approval.IsDeleted = true;
            await _approvalRepo.UpdateApproval(approval);
            return $"Deleted successfully";
        }

        public async Task<ApprovalsResponseModel> GetApprovalsInRespoCentre(int centreId)
        {
            var approvals = await _approvalRepo.GetApprovalsInCentre(centreId);
            var approvalsDto = approvals.Select(x => new ApprovalDto
            {
                Id = x.Id,
                UserId = x.UserId,
                Sequence = x.Sequence,
                ResponsibilityCentreId = x.ResponsibilityCentreId,
                ApprovalRole = x.ApprovalRole
            }).ToList();
            return new ApprovalsResponseModel
            {
                Message = "Retrieved successfully",
                Status = true,
                Data = approvalsDto
            };
            {
                
            }
        }
    }
}