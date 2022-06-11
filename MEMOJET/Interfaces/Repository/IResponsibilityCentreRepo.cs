using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Entities;

namespace MEMOJET.Interfaces.Repository
{
    public interface IResponsibilityCentreRepo
    {
        Task<ResponsibilityCentre> CreateRespoCentre (ResponsibilityCentre respoCentre);
        Task<ResponsibilityCentre> UpdateRespoCentre(ResponsibilityCentre respoCentre);
        Task<bool> DeleteCentre(ResponsibilityCentre centre);
        Task<ResponsibilityCentre> GetCentre(int id);
        Task<IList<ResponsibilityCentre>> GetCentres();
        Task<ResponsibilityCentre> GetCentreByName (string username);
        Task<bool> CentreExist(string email);
        
    }
}