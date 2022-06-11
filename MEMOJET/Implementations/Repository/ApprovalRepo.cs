using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MEMOJET.Implementations.Repository
{
    public class ApprovalRepo:IApprovalRepo
    {
        private readonly ApplicationContext _context;

        public ApprovalRepo(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Approval> CreateApproval(Approval approval)
        {
            await _context.Approvals.AddAsync(approval);
            await _context.SaveChangesAsync();
            return approval;
        }

        public async Task<Approval> UpdateApproval(Approval approval)
        {
            _context.Approvals.Update(approval);
            await _context.SaveChangesAsync();
            return approval;
        }

        public async Task<bool> DeleteApproval(Approval approval)
        {
            _context.Approvals.Remove(approval);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Approval> GetApproval(int id)
        {
           var approval =  await _context.Approvals.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
           return approval;
        }

        public async Task<IList<Approval>> GetApprovals()
        {
            var approvals = await _context.Approvals.ToListAsync();
            return approvals;
        }

        public async Task<IList<Approval>> GetApprovalsInCentre(int id)
        {
            var approvals = await _context.Approvals.Where(x => x.ResponsibilityCentreId == id).ToListAsync();
            return approvals;
        }
        
        public async Task<IList<ApprovalRespoCentre>> GetApprovalWithCentres(int id)
        {
            var approvas = await _context.ApprovalRespoCentres
                .Include(x => x.Approval)
                .Include(y => y.ResponsibilityCentre)
                .Where(x => x.ResponsibilityCentreId == id).ToListAsync();
            return approvas;
        }
    }
}