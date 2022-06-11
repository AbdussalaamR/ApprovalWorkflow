using System.Threading.Tasks;
using MEMOJET.DTOs;

namespace MEMOJET.Interfaces.Service
{
    public interface IRespoCentreService
    {
        Task<RespoCentreResponse> CreateCentre(CreateRespoCentreDto model);
        Task<RespoCentreResponse> UpdateCentre(int id, CreateRespoCentreDto model);
        Task<RespoCentreResponse> DeleteApprovalFromCentre(int approvalId, int respoCentreId);
        Task<RespoCentreResponse> GetCentre(int id);
        Task<RespoCentreResponse> GetCentreByName(string name);
        Task<RespoCentresResponse> GetCentres();
        Task<bool> DeleteCentre(int id);
    }
}