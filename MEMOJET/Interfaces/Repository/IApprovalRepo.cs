using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.Entities;

namespace MEMOJET.Interfaces.Repository
{
    public interface IApprovalRepo
    {
        Task<Approval> CreateApproval (Approval approval);
        Task<Approval> UpdateApproval (Approval approval);
        Task<bool> DeleteApproval (Approval approval);
        Task<Approval> GetApproval (int id);
        Task<IList<Approval>> GetAllApprovals();
        Task<IList<Approval>> GetApprovalsByUserId(int userId);
        Task<IList<Approval>> GetApprovalsInCentre(int id);
        public Task<IList<ApprovalRespoCentre>> GetApprovalWithCentres(int id);
        Task<bool> ApproverExist(int userId, int centId);
    }
}