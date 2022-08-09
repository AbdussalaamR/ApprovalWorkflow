using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.DTOs;

namespace MEMOJET.Interfaces.Service
{
    public interface IApprovalService
    {
        public Task<ApprovalResponseModel> CreateApproval(CreateApprovalRequestModel model);
        public Task<ApprovalResponseModel> UpdateApproval(CreateApprovalRequestModel model, int id);
        public Task<ApprovalResponseModel> GetApproval(int id);
        public Task<ApprovalsResponseModel> GetAllApprovals();
        public Task<IList<int>> GetApprovalIdsOfUser(int userId);
        public Task<string> DeleteApproval(int id);
        public Task<ApprovalsResponseModel> GetApprovalsInRespoCentre(int centreId);
    }
}