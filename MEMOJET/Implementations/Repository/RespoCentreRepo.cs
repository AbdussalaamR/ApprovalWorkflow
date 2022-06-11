using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MEMOJET.Implementations.Repository
{
    public class RespoCentreRepo:IResponsibilityCentreRepo
    {
        private readonly ApplicationContext _context;

        public RespoCentreRepo(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<ResponsibilityCentre> CreateRespoCentre(ResponsibilityCentre respoCentre)
        {
            await _context.ResponsibilityCentres.AddAsync(respoCentre);
            await _context.SaveChangesAsync();
            return respoCentre;
        }

        public async Task<ResponsibilityCentre> UpdateRespoCentre(ResponsibilityCentre respoCentre)
        {
            _context.ResponsibilityCentres.Update(respoCentre);
           await _context.SaveChangesAsync();
            return respoCentre;
        }

        public async Task<bool> DeleteCentre(ResponsibilityCentre centre)
        {
            _context.ResponsibilityCentres.Remove(centre);
           await _context.SaveChangesAsync();
           return true;

        }

        public async Task<ResponsibilityCentre> GetCentre(int id)
        {
            var centre = await _context.ResponsibilityCentres
                .Include(x => x.ApprovalResponsibilityCentres)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            return centre;
        }

        public async Task<IList<ResponsibilityCentre>> GetCentres()
        {
            var centres = await _context.ResponsibilityCentres.ToListAsync();
            return centres;
        }

        public async Task<ResponsibilityCentre> GetCentreByName(string username)
        {
            var centre = await _context.ResponsibilityCentres
                .Include(x => x.ApprovalResponsibilityCentres)
                .FirstOrDefaultAsync(x => x.Name == username);
            return centre;
        }

        public async Task<bool> CentreExist(string name)
        {
            return await _context.ResponsibilityCentres.AnyAsync(x => x.Name == name);
        }
        
        
    }
}